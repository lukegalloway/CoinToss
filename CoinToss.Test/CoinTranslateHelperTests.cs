using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CoinToss.Business;

namespace CoinToss.Test
{
    [TestClass]
    public class CoinTranslateHelperTests
    {
        Examples ex = new Examples();
        [TestMethod]
        public void TranslateCoinExtractToSQL_Test1()
        {
            var coin = new CoinTranslate(ex.Coin["COIN1"]);
        }
    }
}
