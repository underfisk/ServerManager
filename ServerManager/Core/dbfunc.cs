using ServerManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace dbfunc
{
    class editInfo
    {
        getInfos get = new getInfos();

        /*
         * Delete character according to account name and not user_number
         */
        public void deletechar(string charname)
        {
            using (var con = new SqlConnection(get.cn))
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

        /*
         * Ban player by account name not by user_number, this simply the majority of work
         * Missing : Verify if the login_tag is equalt to N and if yes to return something to know its already banned
         */
        public void BanPlayer(string accname)
        {
            using (var con = new SqlConnection(get.cn))
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

        /*
         * Get player name id func is recursive to verify if the user exist or not
         */
        public string PlayerNameByID(string accname)
        {
            using (var con = new SqlConnection(get.cn))
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

        /*
         *  Get cash amount (free amount is quite useless) and return the coins that acc has in
         */
        public int GetCash(string id)
        {
            using (var con = new SqlConnection(get.cn))
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

        /*
         * Edit cash is just an update by user_no, because the admin just edit the amount X and freeamount Y
         */
        public void EditCash(string accid, int coins)
        {
            using (var con = new SqlConnection(get.cn))
            {
                try
                {
                    con.Open();
                    string editcash = "UPDATE cash.dbo.user_cash SET amount=@k WHERE user_no=@id";
                    using (var cmd = new SqlCommand(editcash, con))
                    {
                        cmd.Parameters.AddWithValue("@k",coins);
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

        /*
         * Delete account by accname too 
         */
        public void deleteacc(string accname)
        {
            using (var con = new SqlConnection(get.cn))
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

        /*
         * This is the function where i use a datatable to count rows, its quite important and recursive one
         */
        public bool accExist(string accname)
        {
            using (var con = new SqlConnection(get.cn))
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
                        if (dt.Rows.Count > 0)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            return false;
        }

        /*
         * Recursive to register, where i check the user number and compare to my random userno(if my random is equal to their user_no i random again
         */
        public long getUserNo()
        {
            Random userno = new Random();
            using (var con = new SqlConnection(get.cn))
            {
                try
                {
                    con.Open();
                    string sql = "SELECT user_no FROM account.dbo.USER_PROFILE";
                    using (var get = new SqlCommand(sql,con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter(get);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        long x = userno.Next(1,999999999);
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

        /*
         * Verify acc is a recursive function too
         */
        private bool verifyAcc(string user)
        {

            using (var con = new SqlConnection(get.cn))
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

        /*
         * Register account is just using the main db, not the tbl_user from janvier to set the info(Also email is here, but soon i'll add tbl_user too)
         */
        public bool register(string user,string pwd)
        {
            if (!accExist(user))
            {
                //ASCII NULL
                byte[] theBytes = Encoding.UTF8.GetBytes("0");
                using (var con = new SqlConnection(get.cn))
                {
                    try
                    {
                        con.Open();
                        string sql = "INSERT INTO account.dbo.USER_PROFILE Values (@userno,@user,@pwd,@resident,@usertype,@loginflag,@logintag,@ipttime,@login_time,@logout_time,@userip,@serverid,@authtag)";
                        using (var cmd = new SqlCommand(sql, con))
                        {
                            cmd.Parameters.AddWithValue("@userno", getUserNo());
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

       
        /*
         * charExist is just a function to check if my character name really exist and if yes i return it.
         * After when i load the Character form, i'll be loading by name of character found  
         */

        public bool charExist(string name)
        {
            using (var con = new SqlConnection(get.cn))
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
                        if (dt.Rows.Count > 0)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
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
