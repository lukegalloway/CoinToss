﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinToss.Business.Helpers
{
    public class DbHelper
    {
        public static string Conn()
        {
            return GetDbConn();
        }
        public static string Name()
        {
            return GetDbName();
        }
        private static string GetDbName()
        {
            //return "testdb";
            return "mysql_111979_turtle";
        }
        private static string GetDbConn()
        {
            //return "Server=localhost;Port=3306;Database=TestDb;Uid=luke;Pwd=salasana;";
            return "Server=my02.winhost.com;Port=3306;Database=mysql_111979_turtle;Uid=turtleuser;Pwd=TyhmaTurtle1@3;";
        }
    }
}
