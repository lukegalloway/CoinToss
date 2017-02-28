using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinToss.Business
{
    public class CoinTranslate
    {
        public string COIN_QUERY { get; set; }
        public string SQL_QUERY { get; set; }
        private string FIRST_SELECT { get; set; }
        private bool SQL_IS_GENERATED { get; set; }
        private bool HAS_COIN_KEYWORD_USING { get; set; }
        public CoinTranslate() { }
        public CoinTranslate(string coin)
        {
            COIN_QUERY = coin;
            SQL_IS_GENERATED = false;
        }
        public bool IsCoinTranslateDone()
        {
            return SQL_IS_GENERATED;
        }
        public string Translate()
        {
            string sql = string.Empty;
            string[] phrase = this.COIN_QUERY.Split(' ');
            this.FIRST_SELECT = GetFirstSelect(phrase);
            //coin: "extract Singer, Title from singer using (S/”Celine Dion”) where Year=1968"

            //sql: select c.Singer, c.Title from testdb.singers s 
            // left outer join testdb.concerts c on s.Singer = c.Singer 
            // where c.Singer LIKE "%Celine Dion%" AND Year = 1968;
            List<string> newPhrase = new List<string>();
            foreach(string word in phrase)
            {
                string x = TranslateCoinKeyword(word).Coalesce() ?? GetSqlKeyword(word).ToUpper().Coalesce() ?? word;
                newPhrase.Add(x.Replace("”)", "%'").Replace("” )", "%'").Replace("\")", "%'"));
            }

            sql = string.Join(" ", newPhrase);
            this.SQL_QUERY = sql;
            this.SQL_IS_GENERATED = true;
            return sql;
        }
        private string GetFirstSelect(string[] phrase)
        {
            foreach(var f in phrase)
            {
                if (f.Contains(","))
                {
                    return f.Replace(",", "");
                }
            }
            return string.Empty;
        }

        private string TranslateCoinKeyword(string word)
        {
            string w = word.ToLower();
            string end = string.Empty;
            if (w.Contains("s/"))
            {
                word = w.Replace("(s/", "").Replace("( s/", "").Replace("”", "").Replace("\"", "");
                return string.Format("{0} LIKE '%{1}", this.FIRST_SELECT, word);
            }
            switch (w)
            {
                case "extract": end = "select";
                    break;
                case "using":
                    this.HAS_COIN_KEYWORD_USING = true;
                    end = "where";
                    break;
                case "”":
                    end = "\"";
                    break;
                default: end = string.Empty; break;
            }
            return end.ToUpper();
        }
        private string GetSqlKeyword(string w)
        {
            w = w.ToLower();
            switch (w)
            {
                case "select": return "select";
                case "from": return "from";
                case "where":
                    if (this.HAS_COIN_KEYWORD_USING)
                    {
                        return "and";
                    }
                    return "where";
                default: return string.Empty;
            }
        }

        public string TranslateCoinExtractToSQL(string coin)
        {
            string sql = string.Empty;
            string [] words = coin.Split(' ');

            this.SQL_IS_GENERATED = true;
            return sql;
        }

        [Obsolete]
        public static string TranslateCoinExtractToSQL_Old(string coin)
        {
            string sql = string.Empty;

            //extract A1,...,Ak from v using r as (P1/B1,...,Pm/Bm) where θ
            //Translates TO
            //select A1,..., An from s where ψ

            //extract Singer, Title from singer using (S/”Celine Dion”) where Year=1968
            //Translates TO
            //select c.Singer, c.Title from testdb.singers s left outer join testdb.concerts c on s.Singer = c.Singer where c.Singer LIKE "%Celine Dion%" AND Year = 1968;
            coin.Replace("extract", "select");
            int indexOfUsing = coin.IndexOf("using");
            int indexOfWhere = coin.IndexOf("where");
            string usingClause = coin.Substring(indexOfUsing, indexOfWhere - 1);


            //Translation is one-way, COIN to SQL.  
            //SQL is a subset of COIN.  


            return sql;
        }
    }
}
