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
            "Server=localhost;Database=QLBTS;Uid=root;Pwd=0866582512;";

        public static MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }
    }
}
