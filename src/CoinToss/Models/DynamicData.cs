using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoinToss.Models
{
    public class DynamicData
    {
        public class Column
        {
            public Column(string t)
            {
                title = t;
                visible = true;
            }
            public string title { get; set; }
            public bool visible { get; set; }
        }
        public DynamicData()
        {
            COLUMNS = new List<Column>();
            DATA = new List<List<string>>();
        }
        public List<Column> COLUMNS { get; set; }
        public List<List<string>> DATA { get; set; }
    }
}
