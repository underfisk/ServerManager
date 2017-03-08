using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ServerManager.LiveMap
{
    class database
    {
        public string cn = "Data Source=" + MainMenu.IP + "," + MainMenu.Port + "; Network Library=DBMSSOCN; Initial Catalog=" + "account" + "; User ID=" + MainMenu.ID + "; Password=" + MainMenu.PW + ";";

        public DataTable IsOnline()
        {
            DataTable dt = new DataTable();
            var con = new SqlConnection(cn);
            string s = "SELECT user_no FROM account.dbo.USER_PROFILE WHERE login_flag='20'";
            var cmd = new SqlCommand(s,con);

            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            con.Close();

            return dt;
            
        }

        public DataTable PlayersPosition(int mapcode)
        {
            /*
             * So i get the mapindex and i get everyplayer in that map
             * After i'll return the character i found, (name,level,x and y)
             */

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
