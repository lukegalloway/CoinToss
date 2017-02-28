using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinToss.Business
{
    public static class StringExtension
    {
        public static string Coalesce(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return null;
            else
            {
                return value;
            }
        }
    }
}
