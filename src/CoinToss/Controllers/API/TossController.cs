using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CoinToss.Business;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace CoinToss.Controllers.API
{
    [Route("api/[controller]")]
    public class TossController : Controller
    {
        private static string COIN_TEST_QUERY_1 = "extract Singer, Title from singers using (S/\"Celine Dion\") where Year=1968 AND '1' = '1'";
        //SELECT Singer, Title FROM singers WHERE Singer LIKE '%celine Dion%' AND Year=1968

        // GET: api/values
        [HttpGet]
        public string Get()
        {
            var coin = new CoinTranslate(COIN_TEST_QUERY_1);
            return coin.Translate();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
