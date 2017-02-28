using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinToss.Business.ViewModels
{
    public class SingersViewModel
    {
        public int Id { get; set; }
        public string Singer { get; set; }
        public string Nationality { get; set; }
        public string City { get; set; }
        public int Year { get; set; }
    }
}
