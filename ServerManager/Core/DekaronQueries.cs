using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace ServerManager.Core
{
    static class DekaronQueries
    {
        /// <summary>
        /// Holds the mssql connection string
        /// </summary>
        public static string cn = $"Data Source={MainMenu.IP},{MainMenu.Port}; Network Library=DBMSSOCN; Initial Catalog=account; User ID={MainMenu.ID}; Password={MainMenu.PW};";

     
        /// <summary>
        /// Retrieves the total of account in character.dbo account
        /// </summary>
        /// <returns>total of accounts in database</returns>
        public static int TotalAccounts()
        {
            using (var con = new SqlConnection(cn))
            {
                try
                {
                    con.Open();
                    string sql = "SELECT COUNT (*) FROM account.dbo.USER_PROFILE";
                    using (var cmd = new SqlCommand(sql, con))
                    {
                        var z = Convert.ToInt32(cmd.ExecuteScalar());
                        con.Close();
                        return z;
                    }
                }
                catch
                {
                    MessageBox.Show("Cant reach the desired server. Please check your connection state.", "Cant reach the SQL Server" , MessageBoxButton.OK , MessageBoxImage.Error);
                }
                return 0;
            }
        }

       
        /// <summary>
        /// Gets the total deadfronts in database (The events that are agended)
        /// </summary>
        /// <returns>number of events rows in database</returns>
        public static int Totaldfs()
        {
            using (var con = new SqlConnection(cn))
            {
                try
                {
                    con.Open();
                    string sql = "SELECT COUNT (*) FROM CHARACTER.dbo.CM_BCD_ITEM WHERE bcd_id='DEADFRONT'";
                    using (var cmd = new SqlCommand(sql, con))
                    {
                        var z = Convert.ToInt32(cmd.ExecuteScalar());
                        con.Close();
                        return z;
                    }
                }
                catch 
                {
                    MessageBox.Show("Cant reach the desired server. Please check your connection state.", "Cant reach the SQL Server", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                return 0;
            }
        }

       
        /// <summary>
        /// Retrieves the total guilds in the database table character.dbo
        /// </summary>
        /// <returns>total of guilds</returns>
        public static int TotalGuilds()
        {
            using (var con = new SqlConnection(cn))
            {
                try
                {
                    con.Open();
                    string sql = "SELECT Count (*) FROM CHARACTER.dbo.guild_info";
                    using (var cmd = new SqlCommand(sql, con))
                    {
                        var y = Convert.ToInt32(cmd.ExecuteScalar());
                        con.Close();
                        return y;
                    }
                }
                catch 
                {
                    MessageBox.Show("Cant reach the desired server. Please check your connection state.", "Cant reach the SQL Server", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            return 0;
        }

        
        /// <summary>
        /// Retrieves the total characters in character.dbo
        /// </summary>
        /// <returns>total of characters(couting with [DEV] chars)</returns>
        public static int TotalChars()
        {
            using (var con = new SqlConnection(cn))
            {
                try
                {
                    con.Open();
                    var sql = "SELECT Count (*) FROM CHARACTER.dbo.user_character";
                    using (var cmd = new SqlCommand(sql, con))
                    {
                        var j = Convert.ToInt32(cmd.ExecuteScalar());
                        return j;
                    }
                }
                catch 
                {
                    MessageBox.Show("Cant reach the desired server. Please check your connection state.", "Cant reach the SQL Server", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                return 0;
            }
        }


        /// <summary>
        /// Retrieves the top character if he has more than 0 dwPVPPoints
        /// </summary>
        /// <returns>top1 character name</returns>
        public static string PVPRanking()
        {
            using (var con = new SqlConnection(cn))
            {
                try
                {
                    con.Open();
                    var sql = "SELECT TOP 1 character_name FROM CHARACTER.dbo.user_character WHERE dwPVPPoint > '0'";
                    using (var cmd = new SqlCommand(sql, con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        con.Close();
                        return dt.Rows[0]["character_name"].ToString();
                    }
                }
                catch(Exception e) { }
            }
            return "None";
        }


        /// <summary>
        /// Retrieves the top character on PK if he has more than 0 dwPKPoints
        /// </summary>
        /// <returns>top1 character nam</returns>
        public static string PKRanking()
        {
            using (var con = new SqlConnection(cn))
            {
                try
                {
                    con.Open();
                    var sql = "SELECT TOP 1 character_name FROM CHARACTER.dbo.user_character WHERE wPKCount > '0'";
                    using (var cmd = new SqlCommand(sql, con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        con.Close();
                        return dt.Rows[0]["character_name"].ToString();
                    }
                }
                catch(Exception e) { }
            }
            return "None";
        }

       
        /// <summary>
        /// Retrieves the online characters which has logged in with login_flag 20
        /// </summary>
        /// <returns>count of players</returns>
        public static int OnlinePlayers()
        {
            using (var con = new SqlConnection(cn))
            {
                try
                {
                    con.Open();
                    var sql = "SELECT user_no FROM account.dbo.USER_PROFILE WHERE login_flag = '20'";
                    using (var cmd = new SqlCommand(sql, con))
                    {
                        var x = Convert.ToInt32(cmd.ExecuteScalar());
                        con.Close();
                        return x;
                    }
                }
                catch
                {
                    MessageBox.Show("Cant reach the desired server. Please check your connection state.", "Cant reach the SQL Server", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                return 0;
            }
        }

        /// <summary>
        /// Retrieves the players with login_tag equal to N
        /// Check if login_tag if equal to N [Y = not banned, N = banned]
        /// </summary>
        /// <returns>count of players</returns>
        public static int BannedPlayers()
        {
            using (var con = new SqlConnection(cn))
            {
                try
                {
                    con.Open();
                    var sql = "SELECT Count (*) FROM account.dbo.USER_PROFILE WHERE login_tag = 'N'";
                    using (var cmd = new SqlCommand(sql, con))
                    {
                        var x = Convert.ToInt32(cmd.ExecuteScalar());
                        con.Close();
                        return x;
                    }
                }
                catch 
                {
                    MessageBox.Show("Cant reach the desired server. Please check your connection state.", "Cant reach the SQL Server", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                return 0;
            }
        }

        /// <summary>
        /// Checks if is open or not on the server ip with the given port
        /// </summary>
        /// <param name="port"></param>
        /// <returns></returns>
        public static string TcpConnect(int port)
        {
            IniFile configFile = new IniFile();
            using (TcpClient rec = new TcpClient())
            {
                try
                {
                    rec.Connect(configFile.Read("SQL IP", "MSSQL"), port);
                    return rec.Connected ? "Online" : "Offline";
                }
                catch(Exception e) { }
            }
            return "Offline";
        }
    }
}
