using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using ServerManager.Core;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace ServerManager.DatabaseControl
{
    public partial class Cash : MetroWindow
    {
        /// <summary>
        /// Initializes GUI Componenents 
        /// </summary>
        public Cash() => InitializeComponent();

        /// <summary>
        /// Database used for cash
        /// </summary>
        private DataTable dt = new DataTable();

        /// <summary>
        /// Loads the d-shop caash to the textbox so the user can edit it
        /// </summary>
        /// <param name="name"></param>
        public void LoadCash(string name)
        {
            using (var con = new SqlConnection(DekaronQueries.cn))
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

        /// <summary>
        /// Gets the user_no from the database as parameter
        /// </summary>
        /// <param name="k"></param>
        /// <returns>user_no if cfound</returns>
        public int GetUserNo(string k)
        {
            using (var con = new SqlConnection(DekaronQueries.cn))
            {
                con.Open();
                var cmd = new SqlCommand("Select * FROM account.dbo.USER_PROFILE WHERE user_id = @k", con);
                cmd.Parameters.AddWithValue("@k", k);
                var z = Convert.ToInt32(cmd.ExecuteScalar());
                return z;
            }

        }

        /// <summary>
        /// Small validation to check if the amount textbox or freeamount contains .
        /// </summary>
        /// <returns>boolean</returns>
        private bool validate()
        {
            return amount.Text.Contains(".") || freeamount.Text.Contains(".") ? false : true;
        }

        /// <summary>
        /// Save button works like- If the validation is valid then i dont let the cash be updated, else i do.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void save_Btn_Click(object sender, RoutedEventArgs e)
        {
            if (!validate())
            {
                await this.ShowMessageAsync("Invalid value", "The value is wrong, please enter a positive number");
            }
            else
            {
                using (var con = new SqlConnection(DekaronQueries.cn))
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
                            Close();

                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
            }
        }

        /// <summary>
        /// This function verifies if the given string is a number 
        /// </summary>
        /// <param name="txt"></param>
        /// <returns>boolean </returns>
        private bool IsNumber(string txt)
        {
            int o;
            return int.TryParse(txt, out o);
        }


        /// <summary>
        /// If we find a different character in this function we handle it
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void amount_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if(e.Text != "." && !IsNumber(e.Text))
                e.Handled = true;
            else if(e.Text == ".")
            {
                if (((TextBox)sender).Text.IndexOf(e.Text) > -1)
                {
                    amount.Text = "";
                    e.Handled = true;
                }
            }
        }

        /// <summary>
        /// Does the same as amount
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void freeamount_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (e.Text != "." && !IsNumber(e.Text))
                e.Handled = true;
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
