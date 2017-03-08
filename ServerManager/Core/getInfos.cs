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

namespace ServerManager
{
    class getInfos
    {
        public string cn = "Data Source=" + MainMenu.IP + ","+ MainMenu.Port +"; Network Library=DBMSSOCN; Initial Catalog=" + "account" + "; User ID=" + MainMenu.ID + "; Password=" + MainMenu.PW + ";";

        /*
         *  Total acc return the count of accounts that he find in user_profile (USER_INFO, extra table from janvier is not used in this case!)
         */
        public int TotalAccounts()
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

        /*
         * Total df = How many df exist in game and return
         */
        public int Totaldfs()
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

        /*
         * Total guilds - Select count of guilds in guild_info table
         */
        public int TotalGuilds()
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

        /*
         * Total chars - SELECT Count of characters in the user_character table, its pretty simple
         */
        public int TotalChars()
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

        /*
         * PVP System - If first 1 have the top PVPPoints and if is more than 0 i pick him
         */
        public string PVPRanking()
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
                catch 
                {
                    //No logic showing MessageBox.Show("The database does not exist or its connection is wrong.", ex.ToString());
                }
            }
            return "None";
        }

        /*
         * PK Ranking System- This one is pretty simple same as PVP, you just need to verify if the top 1 player, the one who have more points have the pkcount more than 0 ofc
         */
        public string PKRanking()
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
                catch 
                {
                    
                    // There is no logic showing that there is no pk player on top MessageBox.Show(ex.ToString());
                }
            }
            return "None";
        }

        /*
         * Online players system - Select every acc WHERE login_flag(the login flag can be 1100 or 20 but most of A9's use 20, just need to check on Cast Server) = 20
         */
        public int OnlinePlayers()
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

        /*
         * Ban player system - Check if login_tag if equal to N [Y = not banned, N = banned]
         */
        public int BannedPlayers()
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

        public string tcp(int port)
        {
            IniFile configFile = new IniFile();
            //Check if is open or not on the server ip 
            using (TcpClient rec = new TcpClient())
            {
                try
                {
                    rec.Connect(configFile.Read("SQL IP", "MSSQL"), port);
                    if (rec.Connected == true) { return "Online"; }
                }
                catch
                {
                }
            }
            return "Offline";
        }
    }
}
