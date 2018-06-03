using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using ServerManager.Core;
using System;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Windows;

namespace ServerManager.DatabaseControl
{
    public partial class Register : MetroWindow
    {
        /// <summary>
        /// Initializes the GUI components
        /// </summary>
        public Register() => InitializeComponent();

        /// <summary>
        /// Does an async task checking the fields in another thread
        /// </summary>
        /// <returns>boolean</returns>
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

        /// <summary>
        /// Tries to conver a given string to mailaddress object and if there are no errors than its a valid email
        /// </summary>
        /// <param name="emailaddress"></param>
        /// <returns>boolean</returns>
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

        /// <summary>
        /// Handles the register click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void register_Click(object sender, RoutedEventArgs e)
        {
            bool y = await checkFields();
            if (y)
            {
                if (!DekaronCRUD.Register(usr.Text, pw1.Text))
                    await this.ShowMessageAsync("Account exists", "Please type another account username.");
                else
                    await this.ShowMessageAsync("Account created", $"Your account was been created. ID={usr.Text} PW={pw1.Text}");
            }
        }

        /// <summary>
        /// Handles the cancel click 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cancel_Click(object sender, RoutedEventArgs e) => Close();
    }
}
