using System;
using System.Data;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace QLBTS_DAL
{
    public class DataProvider
    {
        // Lấy connection string từ App.config
        // Vô hiệu hóa dòng cũ bằng cách thêm 2 dấu gạch chéo
        // private static string connectionString = ConfigurationManager.ConnectionStrings["QLBTS"].ConnectionString;

        // ✅ THÊM DÒNG MỚI NÀY VÀO ĐỂ TEST
        private static string connectionString = "Server=localhost;Database=qlbts;User Id=root;Password=09112005;SslMode=none;"; 

        // Phương thức lấy kết nối mới
        public static MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }

        // Phương thức thực thi câu lệnh SELECT - Trả về DataTable
        public static DataTable ExecuteQuery(string query, MySqlParameter[] parameters = null)
        {
            DataTable dataTable = new DataTable();

            try
            {
                using (MySqlConnection conn = GetConnection())
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        if (parameters != null)
                        {
                            cmd.Parameters.AddRange(parameters);
                        }

                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                        {
                            adapter.Fill(dataTable);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi thực thi truy vấn: " + ex.Message);
            }

            return dataTable;
        }

        // Phương thức thực thi INSERT, UPDATE, DELETE - Trả về số dòng bị ảnh hưởng
        public static int ExecuteNonQuery(string query, MySqlParameter[] parameters = null)
        {
            int result = 0;

            try
            {
                using (MySqlConnection conn = GetConnection())
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        if (parameters != null)
                        {
                            cmd.Parameters.AddRange(parameters);
                        }

                        result = cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi thực thi lệnh: " + ex.Message);
            }

            return result;
        }

        // Phương thức lấy giá trị đơn (COUNT, MAX, SUM, AVG...)
        public static object ExecuteScalar(string query, MySqlParameter[] parameters = null)
        {
            object result = null;

            try
            {
                using (MySqlConnection conn = GetConnection())
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        if (parameters != null)
                        {
                            cmd.Parameters.AddRange(parameters);
                        }

                        result = cmd.ExecuteScalar();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi thực thi lệnh scalar: " + ex.Message);
            }

            return result;
        }

        // Phương thức test kết nối
        public static bool TestConnection()
        {
            try
            {
                using (MySqlConnection conn = GetConnection())
                {
                    conn.Open();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}