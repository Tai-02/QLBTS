using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace QLBTS_DAL
{
    public static class DatabaseHelper
    {
        private static string connectionString =
            "Server=127.0.0.1;Database=QLBTS;Uid=root;Pwd=12345gray;";

        public static MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }
    }
}