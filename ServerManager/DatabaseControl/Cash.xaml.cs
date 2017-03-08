using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ServerManager.DatabaseControl
{

    public partial class Cash : MetroWindow
    {
        getInfos get = new getInfos();
        public Cash()
        {
            InitializeComponent();
        }

        dbfunc.editInfo s = new dbfunc.editInfo();
        DataTable dt = new DataTable();

        /*
         * Load cash to the textbox for user can edit
         */
        public void LoadCash(string name)
        {
            using (var con = new SqlConnection(get.cn))
            {
                try
                {
                    string load = "SELECT * FROM cash.dbo.user_cash WHERE user_no=@k";
                    using (var cmd = new SqlCommand(load, con))
                    {
                        cmd.Parameters.AddWithValue("@k", GetUserNo(name));
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dt);
                        con.Close();
                        amount.Text = dt.Rows[0]["amount"].ToString();
                        freeamount.Text = dt.Rows[0]["free_amount"].ToString();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        // Get user number recursive
        public int GetUserNo(string k)
        {
            using (var con = new SqlConnection(get.cn))
            {
                con.Open();
                var cmd = new SqlCommand("Select * FROM account.dbo.USER_PROFILE WHERE user_id = @k", con);
                cmd.Parameters.AddWithValue("@k", k);
                var z = Convert.ToInt32(cmd.ExecuteScalar());
                return z;
            }

        }

        private bool validate()
        {
            if (amount.Text.Contains(".") || freeamount.Text.Contains(".")) { return false; }
            return true;
        }

        /*
         * Save button works like- If the validation is valid then i dont let the cash be updated, else i do.
         */
        private async void save_Btn_Click(object sender, RoutedEventArgs e)
        {
            if (!validate())
            {
                await this.ShowMessageAsync("Invalid value", "The value is wrong, please enter a positive number");
            }
            else
            using (var con = new SqlConnection(get.cn))
            {
                try
                {
                    string x = "UPDATE cash.dbo.user_cash SET id=@accid ,user_no=@k ,group_id=@groupid ,amount=@cash, free_amount=@cashfree WHERE user_no =@k";
                    using (var cmd = new SqlCommand(x, con))
                    {
                        con.Open();
                        cmd.Parameters.AddWithValue("@k", dt.Rows[0]["user_no"].ToString());
                        cmd.Parameters.AddWithValue("@accid", dt.Rows[0]["id"].ToString());
                        cmd.Parameters.AddWithValue("@groupid", dt.Rows[0]["group_id"].ToString());
                        cmd.Parameters.AddWithValue("@cash", amount.Text);
                        cmd.Parameters.AddWithValue("@cashfree", freeamount.Text);
                        cmd.ExecuteNonQuery();
                        con.Close();
                        await this.ShowMessageAsync("Account cash updated", "Your account cash have been updated.");
                        this.Close();

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }


            }
        }

        //i will try to parse allways for protect the input
        private bool IsNumber(string txt)
        {
            int o;
            return int.TryParse(txt, out o);
        }

        /*
         * In previews i try to force the protection against characters.
         * If i find a diff than number i will handle it
         */
        private void amount_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if(e.Text != "." && !IsNumber(e.Text))
            {
                e.Handled = true;
            }
            else if(e.Text == ".")
            {
                if (((TextBox)sender).Text.IndexOf(e.Text) > -1)
                {
                    amount.Text = "";
                    e.Handled = true;
                }
            }
        }

        private void freeamount_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (e.Text != "." && !IsNumber(e.Text))
            {
                e.Handled = true;
            }
            else if (e.Text == ".")
            {
                if (((TextBox)sender).Text.IndexOf(e.Text) > -1)
                {
                    freeamount.Text = "";
                    e.Handled = true;
                }
            }
        }
    }
}
