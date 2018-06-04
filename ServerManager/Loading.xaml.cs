using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System.Data.SqlClient;
using System.Windows;

namespace ServerManager
{
    public partial class Loading : MetroWindow
    {
        /// <summary>
        /// Instance object to read ini file
        /// </summary>
        IniFile configFile = new IniFile();

        /// <summary>
        /// Initializes the base data
        /// </summary>
        public Loading()
        {
            InitializeComponent();
            checkConfig();
            load.IsActive = false;
        }

        /// <summary>
        /// Verifies if exists the base sql configuration in the ini file
        /// </summary>
        protected void checkConfig()
        {
            if (configFile.Read("SQL IP", "MSSQL") == "")
                MessageBox.Show("We recommend you to save your info for further use", "Save for further use", MessageBoxButton.OK, MessageBoxImage.Information);
            else
            {
                sqlip.Text = configFile.Read("SQL IP", "MSSQL");
                sqlport.Text = configFile.Read("SQL Port", "MSSQL");
                sqlusr.Text = configFile.Read("SQL Username", "MSSQL");
                sqlpwd.Text = configFile.Read("SQL Password", "MSSQL");
            }
        }

        /// <summary>
        /// Handles the overwrite button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void button1_Click(object sender, RoutedEventArgs e)
        {
            configFile.Write("SQL IP", sqlip.Text, "MSSQL");
            configFile.Write("SQL Port", sqlport.Text, "MSSQL");
            configFile.Write("SQL Username", sqlusr.Text, "MSSQL");
            configFile.Write("SQL Password", sqlpwd.Text, "MSSQL");
            await this.ShowMessageAsync("Information updated", "Your sql information is updated!");
        }

        /// <summary>
        /// Handles the connection test button
        /// </summary>
        protected async void conTest()
        {
            if (configFile.Read("World Port", "PORTS") == "")
            {
                configFile.Write("World Port", "50005", "PORTS");
                configFile.Write("Arena Port", "60006", "PORTS");
                configFile.Write("Login Port", "10001", "PORTS");
                configFile.Write("Web Port", "80", "PORTS");
                await this.ShowMessageAsync("Adding default server ports", "We've added the default server ports");
            }

            if (configFile.Read("Server Action") == "")
            {
                configFile.Write("Server Action", "A9", "ACTION");
            }

            string cn = $"Data Source={sqlip.Text},{sqlport.Text}; Network Library=DBMSSOCN; Initial Catalog=account; User ID={sqlusr.Text}; Password={sqlpwd.Text};";

            using (SqlConnection cnn = new SqlConnection(cn))
            {
                try
                {
                    cnn.Open();
                    //update details
                    MainMenu.ID = sqlusr.Text;
                    MainMenu.PW = sqlpwd.Text;
                    MainMenu.IP = sqlip.Text;
                    MainMenu.Port = sqlport.Text;
                    //For show info after
                    MainMenu open = new MainMenu();
                    open.sqlusr.Content = sqlusr.Text;
                    open.svconnected.Content = sqlip.Text;
                    cnn.Close();

                    //Open now
                    this.Close();
                    open.Show();
                }
                catch
                {
                    load.IsActive = false;
                    connect.IsEnabled = true;
                    await this.ShowMessageAsync("Connection string is wrong or login failed", "Check your connection info and make sure SQL TCP/IP is available.", MessageDialogStyle.Affirmative);
                }
            }
        }

        /// <summary>
        /// Handles the connect click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void connect_Click(object sender, RoutedEventArgs e)
        {
            connect.IsEnabled = false;
            load.IsActive = true;
            conTest();

        }
    }
}
