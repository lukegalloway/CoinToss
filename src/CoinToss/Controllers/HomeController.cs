using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Configuration;
using Dapper;
using CoinToss.Models;
using Newtonsoft.Json;
using static CoinToss.Models.DynamicData;
using CoinToss.Business;
using CoinToss.Business.Helpers;

namespace CoinToss.Controllers
{
    public class HomeController : Controller
    {
        Examples ex = new Examples();
        public async Task<IActionResult> CoinSelect()
        {
            var singers = await GetListFromSqlQuery("select singer from " + DbHelper.Name() + ".singers");
            return View(singers);
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> CoinSelect(string SingerSelection)
        {
            if (string.IsNullOrEmpty(SingerSelection))
                return View(null);
            var coinQuery = string.Format("extract Singer, Title from singers using (S/\"{0}\")", SingerSelection);
            //string InjectionAttack = "extract UserName, PasswordHash from users using (S/\"admin\") and (S/\"pass\") where ";
            //string sqlAttack = "select username, passwordHash, from users where username = 'admin' and (pass = 0) = 0";
            var coin = new CoinTranslate(coinQuery);
            var sqlQuery = coin.Translate();
            var CoIn_Interface_View = await Create_CoinInterfaceView(sqlQuery);
            ViewData["json"] = ConvertAnyModelToJson(CoIn_Interface_View);

            var singers = await GetListFromSqlQuery("select singer from " + DbHelper.Name() + ".singers");
            return View(singers);
        }
        public async Task<IActionResult> SqlSelect()
        {
            var singers = await GetListFromSqlQuery("select singer from " + DbHelper.Name() + ".singers");
            return View(singers);
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> SqlSelect(string SingerSelection)
        {
            if (string.IsNullOrEmpty(SingerSelection))
                return View(null);
            var json = await GetAnyGridFromSqlQuery(string.Format("select * from " + DbHelper.Name() + ".singers where singer LIKE '%{0}%'", SingerSelection));
            ViewData["json"] = json;

            var singers = await GetListFromSqlQuery("select singer from " + DbHelper.Name() + ".singers");
            return View(singers);
        }
        public async Task<IActionResult> CoinToss()
        {
            ViewData["json"] = string.Empty;
            ViewData["SqlExamples"] = ex.Sql;
            ViewData["CoinExamples"] = ex.Coin;
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> CoinToss(string query, string coinquery, string sqlquery)
        {
            if (string.IsNullOrEmpty(query))
                return View(null);
            var coin = new CoinTranslate(query);
            //string json = await GetAnyGridFromSqlQuery(query);
            string json = await GetAnyGridFromSqlQuery(coin.Translate());

            ViewData["json"] = json;
            ViewData["SqlExamples"] = ex.Sql;
            ViewData["CoinExamples"] = ex.Coin;
            ViewData["CoinQuery"] = coinquery;
            ViewData["SqlQuery"] = sqlquery;
            return View();
        }
        private static async Task<ICollection<string>> GetListFromSqlQuery(string query)
        {
            var dropDownList = new List<string>();
            //Send Query To Database
            using (var conn = new MySql.Data.MySqlClient.MySqlConnection(DbHelper.Conn()))
            {
                await conn.OpenAsync();
                dropDownList = (await conn.QueryAsync<string>(query)).ToList();
            }
            return dropDownList;
        }
        private static string ConvertAnyModelToJson(List<AnyModel> coinInterfaceView)
        {
            string json = string.Empty;
            var dd = new DynamicData();
            dd.COLUMNS.Add(new Column("Id"));
            dd.COLUMNS.Add(new Column("Title"));
            dd.COLUMNS.Add(new Column("City"));
            dd.COLUMNS.Add(new Column("Singer"));
            dd.COLUMNS.Add(new Column("Nationality"));
            dd.COLUMNS.Add(new Column("Year"));
            foreach (var v in coinInterfaceView)
            {
                var d = new List<string>();
                d.Add(v.Id.ToString());
                d.Add(v.Title);
                d.Add(v.City);
                d.Add(v.Singer);
                d.Add(v.Nationality);
                d.Add(v.Year.ToString());
                dd.DATA.Add(d);
            }
            return JsonConvert.SerializeObject(dd);
        }
        private static async Task<List<AnyModel>> Create_CoinInterfaceView(string query)
        {
            var rows = new List<AnyModel>();
            //Send Query To Database
            using (var conn = new MySql.Data.MySqlClient.MySqlConnection(DbHelper.Conn()))
            {
                await conn.OpenAsync();
                rows = (await conn.QueryAsync<AnyModel>(query)).ToList();
            }
            return rows;
        }

        private static async Task<string> GetAnyGridFromSqlQuery(string query)
        {
            string json = string.Empty;
            var dd = new DynamicData();
            var rows = new List<AnyModel>();
            //Send Query To Database
            using (var conn = new MySql.Data.MySqlClient.MySqlConnection(DbHelper.Conn()))
            {
                await conn.OpenAsync();
                rows = (await conn.QueryAsync<AnyModel>(query)).ToList();
                int count = 0;
                dd.COLUMNS.Add(new Column("Id"));
                dd.COLUMNS.Add(new Column("Title"));
                dd.COLUMNS.Add(new Column("City"));
                dd.COLUMNS.Add(new Column("Singer"));
                dd.COLUMNS.Add(new Column("Nationality"));
                dd.COLUMNS.Add(new Column("Year"));
                foreach (var v in rows)
                {
                    var d = new List<string>();
                    d.Add(v.Id.ToString());
                    d.Add(v.Title);
                    d.Add(v.City);
                    d.Add(v.Singer);
                    d.Add(v.Nationality);
                    d.Add(v.Year.ToString());
                    dd.DATA.Add(d);
                    count++;
                }
                json = JsonConvert.SerializeObject(dd);
            }
            return json;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
