using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
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
    /// <summary>
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : MetroWindow
    {
        public Register()
        {
            InitializeComponent();
        }

        protected async Task<bool> checkFields()
        {
            //Verify is the 4 fields are blank or not
            if (String.IsNullOrWhiteSpace(usr.Text))
            {
                await this.ShowMessageAsync("Blank space detection", "You must enter a username.");
                return false;
            }
            else if (String.IsNullOrWhiteSpace(pw1.Text))
            {
                await this.ShowMessageAsync("Blank space detection", "You must enter a password.");
                return false;
            }
            else if (String.IsNullOrWhiteSpace(pw2.Text))
            {
                await this.ShowMessageAsync("Blank space detection", "You must confirm your password");
                return false;
            }
            else if (String.IsNullOrWhiteSpace(email.Text))
            {
                await this.ShowMessageAsync("Blank space detection", "You must enter a email");
                return false;
            }
            //Verify if email is correct and if password match
            else if (pw1.Text != pw2.Text)
            {
                await this.ShowMessageAsync("Password does not match", "Passwords are not equal, please make sure they do");
                return false;
            }
            else if (!IsValid(email.Text))
            {
                await this.ShowMessageAsync("Invalid Email", "Please enter a valid email to proceed");
                return false;
            }
            return true;
        }

        public bool IsValid(string emailaddress)
        {
            //Instead of regex i prefer to use MailClass
            try
            {
                MailAddress m = new MailAddress(emailaddress);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        private async void register_Click(object sender, RoutedEventArgs e)
        {
            dbfunc.editInfo h = new dbfunc.editInfo();
            bool y = await checkFields();
            if (y == true)
            {
                if (!h.register(usr.Text, pw1.Text))
                {
                    await this.ShowMessageAsync("Account exists", "Please type another account username.");
                }
                else
                {
                    await this.ShowMessageAsync("Account created", "Your account was been created. ID=" + usr.Text + " PW=" + pw1.Text);
                }  
            }
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
