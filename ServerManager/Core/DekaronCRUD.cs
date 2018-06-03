using System;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Windows;

namespace ServerManager.Core
{
    static class DekaronCRUD
    {

        /// <summary>
        /// Deletes a character with the given name as paramter
        /// </summary>
        /// <param name="charname"></param>
        public static void DeleteCharacter(string charname)
        {
            using (var con = new SqlConnection(DekaronQueries.cn))
            {
                try
                {
                    con.Open();
                    string delete = "DELETE FROM CHARACTER.dbo.user_character WHERE character_name=@k";
                    using (var cmd = new SqlCommand(delete, con))
                    {
                        cmd.Parameters.AddWithValue("@k", charname);
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }


        /// <summary>
        /// Ban player by account name not by user_number, this simply the majority of work
        /// Missing : Verify if the login_tag is equalt to N and if yes to return something to know its already banned
        /// </summary>
        /// <param name="accname"></param>
        public static void BanPlayer(string accname)
        {
            using (var con = new SqlConnection(DekaronQueries.cn))
            {
                try
                {
                    con.Open();
                    string banAcc = "UPDATE account.dbo.USER_PROFILE SET login_tag='N' WHERE user_id=@k";
                    using (var cmd = new SqlCommand(banAcc, con))
                    {
                        cmd.Parameters.AddWithValue("@k", accname);
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        /// <summary>
        ///  Get the player name with the given accname
        /// </summary>
        /// <param name="accname"></param>
        /// <returns>user_no</returns>
        public static string PlayerNameByID(string accname)
        {
            using (var con = new SqlConnection(DekaronQueries.cn))
            {
                con.Open();
                string x = "Select user_no FROM account.dbo.USER_PROFILE WHERE user_id=@k";
                using (var cmd = new SqlCommand(x, con))
                {
                    //Parameteres for Anti-SQL Injection
                    cmd.Parameters.AddWithValue("@k", accname);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    con.Close();
                    return dt.Rows[0]["user_no"].ToString();
                }
            }
        }

        /// <summary>
        /// Gets the cash amount (free amount is quite useless) and return the coins that acc has in
        /// </summary>
        /// <param name="id"></param>
        /// <returns>D-shop coins</returns>
        public static int GetCash(string id)
        {
            using (var con = new SqlConnection(DekaronQueries.cn))
            {
                try
                {
                    con.Open();
                    string getCoins = "SELECT amount FROM cash.dbo.user_cash WHERE user_no=@k";
                    using (var cmd = new SqlCommand(getCoins, con))
                    {
                        cmd.Parameters.AddWithValue("@k", id);
                        var z = Convert.ToInt32(cmd.ExecuteScalar());
                        con.Close();
                        return z;
                    }
                }
                catch
                {
                    return 0;
                }
            }
        }

        /// <summary>
        /// Updates the user cash by user_no, because the admin just edit the amount X and freeamount Y
        /// </summary>
        /// <param name="accid">user_no</param>
        /// <param name="coins">D-shop coins</param>
        public static void EditCash(string accid, int coins)
        {
            using (var con = new SqlConnection(DekaronQueries.cn))
            {
                try
                {
                    con.Open();
                    string editcash = "UPDATE cash.dbo.user_cash SET amount=@k WHERE user_no=@id";
                    using (var cmd = new SqlCommand(editcash, con))
                    {
                        cmd.Parameters.AddWithValue("@k", coins);
                        cmd.Parameters.AddWithValue("@id", accid);
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        /// <summary>
        /// Deletes an account with the given name
        /// </summary>
        /// <param name="accname"></param>
        public static void DeleteAccount(string accname)
        {
            using (var con = new SqlConnection(DekaronQueries.cn))
            {
                try
                {
                    con.Open();
                    string deleteAcc = "DELETE FROM account.dbo.USER_PROFILE WHERE user_id=@k";
                    using (var cmd = new SqlCommand(deleteAcc, con))
                    {
                        cmd.Parameters.AddWithValue("@k", accname);
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        /// <summary>
        /// Tries to retrieve an account, and if exist returns true else false
        /// </summary>
        /// <param name="accname"></param>
        /// <returns>boolean whether the account exists</returns>
        public static bool AccountExists(string accname)
        {
            using (var con = new SqlConnection(DekaronQueries.cn))
            {
                try
                {
                    con.Open();
                    string c = "Select * FROM account.dbo.USER_PROFILE WHERE user_id=@k";
                    using (var cmd = new SqlCommand(c, con))
                    {
                        //Parameteres for Anti-SQL Injection
                        cmd.Parameters.AddWithValue("@k", accname);
                        con.Close();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        return dt.Rows.Count > 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            return false;
        }

        /// <summary>
        /// Gets the user_no of every account and compares to make a new random number (used for register)
        /// </summary>
        /// <returns>new random number</returns>
        public static long GetUserNo()
        {
            Random userno = new Random();
            using (var con = new SqlConnection(DekaronQueries.cn))
            {
                try
                {
                    con.Open();
                    string sql = "SELECT user_no FROM account.dbo.USER_PROFILE";
                    using (var get = new SqlCommand(sql, con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter(get);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        long x = userno.Next(1, 999999999);
                        if (dt.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                if (x == i)
                                    x = userno.Next(1, 999999999);
                            }
                            return x;
                        }
                        else
                        {
                            return x;
                        }

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            return 0;
        }

        /// <summary>
        /// Verifies if an account exists with a given user_id
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private static bool VerifyAccount(string user)
        {

            using (var con = new SqlConnection(DekaronQueries.cn))
            {
                try
                {
                    con.Open();
                    string c = "SELECT * FROM account.dbo.USER_PROFILE WHERE user_id=@user";
                    using (var cmd = new SqlCommand(c, con))
                    {
                        cmd.Parameters.AddWithValue("@user", user);
                        SqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                            return true;
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            return false;
        }

        /// <summary>
        /// Registers an account using the main db, not the tbl_user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="pwd"></param>
        /// <returns>boolean</returns>
        public static bool Register(string user, string pwd)
        {
            if (!DekaronCRUD.AccountExists(user))
            {
                //ASCII NULL
                byte[] theBytes = Encoding.UTF8.GetBytes("0");
                using (var con = new SqlConnection(DekaronQueries.cn))
                {
                    try
                    {
                        con.Open();
                        string sql = "INSERT INTO account.dbo.USER_PROFILE Values (@userno,@user,@pwd,@resident,@usertype,@loginflag,@logintag,@ipttime,@login_time,@logout_time,@userip,@serverid,@authtag)";
                        using (var cmd = new SqlCommand(sql, con))
                        {
                            cmd.Parameters.AddWithValue("@userno", GetUserNo());
                            cmd.Parameters.AddWithValue("@user", user);
                            cmd.Parameters.AddWithValue("@pwd", MD5Hash(pwd));
                            cmd.Parameters.AddWithValue("@resident", "801011000000");
                            cmd.Parameters.AddWithValue("@usertype", "1");
                            cmd.Parameters.AddWithValue("@loginflag", "0");
                            cmd.Parameters.AddWithValue("@logintag", "Y");
                            cmd.Parameters.AddWithValue("@ipttime", SqlDbType.DateTime).Value = DateTime.Today;
                            cmd.Parameters.AddWithValue("@login_time", SqlDbType.DateTime).Value = DateTime.Today;
                            cmd.Parameters.AddWithValue("@logout_time", SqlDbType.DateTime).Value = DateTime.Today;
                            cmd.Parameters.AddWithValue("@userip", theBytes[0]);
                            cmd.Parameters.AddWithValue("@serverid", "000");
                            cmd.Parameters.AddWithValue("@authtag", "0");

                            cmd.ExecuteNonQuery();
                            con.Close();
                            return true;

                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Converts an string to MD5 (Not secure nowdays but dekaron uses it for we have to stick with the emulator workflow)
        /// </summary>
        /// <param name="input"></param>
        /// <returns>md5 hash</returns>
        public static string MD5Hash(string input)
        {
            // #convert string to md5
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }



        /// <summary>
        /// Tries to retrieve a character with the given name in parameter and returns the state if exists or not
        /// </summary>
        /// <param name="name"></param>
        /// <returns>boolean</returns>
        public static bool CharacterExists(string name)
        {
            using (var con = new SqlConnection(DekaronQueries.cn))
            {
                try
                {
                    con.Open();
                    string c = "Select * FROM CHARACTER.dbo.user_character WHERE character_name=@k";
                    using (var cmd = new SqlCommand(c, con))
                    {
                        //Parameteres for Anti-SQL Injection
                        cmd.Parameters.AddWithValue("@k", name);
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        return dt.Rows.Count > 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            return false;
        }
    }
}
