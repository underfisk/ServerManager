using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace ServerManager.LiveMap
{
    static class LMSQL
    {
        /// <summary>
        /// Holds the mssql connection string
        /// </summary>
        public static string cn = $"Data Source={MainMenu.IP},{MainMenu.Port}; Network Library=DBMSSOCN; Initial Catalog=account; User ID={MainMenu.ID}; Password={MainMenu.PW};";

        /// <summary>
        /// Retrieves a table with the online players with the login flag = 20
        /// </summary>
        /// <returns>Datatable</returns>
        public static DataTable OnlinePlayers()
        {
            DataTable dt = new DataTable();
            var con = new SqlConnection(cn);
            string s = "SELECT user_no FROM account.dbo.USER_PROFILE WHERE login_flag='20'";
            var cmd = new SqlCommand(s, con);

            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            con.Close();

            return dt;

        }

        /// <summary>
        /// Retrieves the player positions to be drawn in a map.
        /// </summary>
        /// <param name="mapcode"></param>
        /// <returns>Datatable</returns>
        public static DataTable PlayersPosition(int mapcode)
        {
            DataTable dt = new DataTable();
            using (var con = new SqlConnection(cn))
            {
                try
                {
                    string get = "SELECT character_name,wLevel,byPCClass,wMapIndex,wPosX,wPosY FROM CHARACTER.dbo.user_character WHERE wMapIndex=@k";
                    using (var cmd = new SqlCommand(get, con))
                    {
                        cmd.Parameters.AddWithValue("@k", mapcode);
                        con.Open();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dt);
                        con.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            return dt;
        }
    }
}
