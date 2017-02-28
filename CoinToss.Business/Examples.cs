using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinToss.Business
{
    public class Examples
    {
        public Dictionary<string, string> Sql;
        public Dictionary<string, string> Coin;
        public Examples()
        {
            Sql = new Dictionary<string, string>();
            Coin = new Dictionary<string, string>();

            Coin.Add("COIN1", "extract Singer, Title from singer using (S/”Celine Dion”) where Year=1968");

            Sql.Add("2A", "select * from concerts");
            Sql.Add("2B", "select * from singers");
            /// Query singer, :S is coming from the User Input.  Figure 2 C and D have the same :S which is 'Celine Dion'
            Sql.Add("2C", "select Nationality, City from singers where Singer = :S and Singer <> 'Cat Stevens'");
            Sql.Add("2D", "select Nationality, City from singers where Singer = 'Celine Dion'");
            /// :S="Celine Dion"
            Sql.Add("2E", "select Title, Singer from concerts where City = :C");
            Sql.Add("2F", "select Title, Singer from concerts where City = 'Sydney'");
            /// Query performance
            Sql.Add("2G", "select Singer, Title from singers as S, concerts as C where C.City = :C and Nationality = :N and S.Singer = C.Singer");
            /// :N="Canadian",:C="Hollywood"
            Sql.Add("2H", "select Singer, Title from singers as S, concerts as C where C.City = 'Hollywood' and Nationality = 'Canadian' and S.Singer = C.Singer");
            /// Query Celine's concert
            Sql.Add("2I", "select Title from concerts where City = :C and Singer = 'Celine Dion'");
            /// :C="Hollywood" and Singer="Celine Dion"
            Sql.Add("2J", "select Title from concerts where City = 'Hollywood' and Singer = 'Celine Dion'");
            /// :N="British" and :C="Sydney"
            Sql.Add("2K", "select singer, Title from singers as S, concerts as C where C.City = 'Sydney' and Nationality = 'British'");
            /// Performance by all but not Cat Stevens
            Sql.Add("2L", "select singer, Title from singers as S, concerts as C where C.City = :C and Nationality = :N and S.Singer = C.Singer and S.Singer <> 'Cat Stevens'");
            /// Query with soft constraint S.Year = 1927
            Sql.Add("2M", "select Singer, Title from singers as S, concerts as C where C.City = :C and Nationality = :N and S.Singer = C.Singer and S.Year = 1927");
            /// :N="American", :C="Paris"
            Sql.Add("2N", "select Singer, Title from singers as S, concerts as C where C.City = 'Paris' and Nationality = 'American'");
            /// Concert performance by older singers
            Sql.Add("2O", "select S.Singer, C.Title from singers as S, concerts as C where C.City = :C and Nationality = :N and S.Singer = C.Singer and S.Year < 1968");
            /// :N="Canadian", :C="Hollywood"
            Sql.Add("2P", "select Singer, Title from singers as S, concerts as C where C.City = 'Hollywood' and Nationality = 'Canadian' and S.Singer = C.Singer and S.Year < 1968");
            /// Figure 2Q - Probe query with soft constraint S.Year = 1965
            Sql.Add("2Q", "select Singer, Title from singers as S, concerts as C where C.City = :C and Nationality = :N and S.Singer = C.Singer and S.Year < 1968 and S.Year = 1965");
        }        
    }
}
