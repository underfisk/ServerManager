using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using MahApps.Metro.Controls;
using System.Windows.Interop;
using MahApps.Metro.Controls.Dialogs;
using System.Drawing.Printing;
using System.Drawing;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Text.RegularExpressions;

namespace ServerManager
{

    public partial class MainMenu : MetroWindow
    {
        getInfos get = new getInfos();
        public static string IP, ID, PW, Port;
        IniFile config = new IniFile();

        public MainMenu()
        {
            InitializeComponent();
            Statics();
            Status();
            loadConfigs();

            LiveMap.engine get = new LiveMap.engine(this) ; get.LoadMaps();
            this.editchar.MouseEnter += new MouseEventHandler(editchar_MouseEnter);
            this.editchar.MouseLeave += new MouseEventHandler(editchar_MouseLeave);

            this.newacc.MouseEnter += new MouseEventHandler(newacc_MouseEnter);
            this.newacc.MouseLeave += new MouseEventHandler(newacc_MouseLeave);

            this.deletechar.MouseEnter += new MouseEventHandler(deletechar_MouseEnter);
            this.deletechar.MouseLeave += new MouseEventHandler(deletechar_MouseLeave);

            this.deleteaccount.MouseEnter += new MouseEventHandler(deleteaccount_MouseEnter);
            this.deleteaccount.MouseLeave += new MouseEventHandler(deleteaccount_MouseLeave);

            this.banacc.MouseEnter += new MouseEventHandler(banacc_MouseEnter);
            this.banacc.MouseLeave += new MouseEventHandler(banacc_MouseLeave);

            this.editcoins.MouseEnter += new MouseEventHandler(editcoins_MouseEnter);
            this.editcoins.MouseLeave += new MouseEventHandler(editcoins_MouseLeave);

        }

        protected void loadConfigs()
        {
            //MSSQL Load
            sqlipchange.Text = config.Read("SQL IP", "MSSQL");
            sqlportchange.Text = config.Read("SQL Port", "MSSQL");
            sqlidchange.Text = config.Read("SQL Username", "MSSQL");
            sqlpwchange.Text = config.Read("SQL Password", "MSSQL");
            //Action Load
            action.Text = config.Read("Server Action", "ACTION");
            //Ports Load
            worldedit.Text = config.Read("World Port", "PORTS");
            arenaedit.Text = config.Read("Arena Port", "PORTS");
            loginedit.Text = config.Read("Login Port", "PORTS");
            webedit.Text = config.Read("Web Port", "PORTS");

            //block until edit
            sqlipchange.IsEnabled = false;
            sqlportchange.IsEnabled = false;
            sqlidchange.IsEnabled = false;
            sqlpwchange.IsEnabled = false;
            action.IsEnabled = false;
            worldedit.IsEnabled = false;
            arenaedit.IsEnabled = false;
            loginedit.IsEnabled = false;
            webedit.IsEnabled = false;

        }

        public void editcoins_MouseEnter(object sender, MouseEventArgs e)
        {
            var transimage = Imaging.CreateBitmapSourceFromHBitmap(Properties.Resources.dcoins_hover.GetHbitmap(),
                      IntPtr.Zero,
                      Int32Rect.Empty,
                      BitmapSizeOptions.FromEmptyOptions());
            editcoins.Background = new ImageBrush(transimage);
        }

        public void editcoins_MouseLeave(object sender, MouseEventArgs e)
        {
            var transimage = Imaging.CreateBitmapSourceFromHBitmap(Properties.Resources.dcoins_normal.GetHbitmap(),
                      IntPtr.Zero,
                      Int32Rect.Empty,
                      BitmapSizeOptions.FromEmptyOptions());
            editcoins.Background = new ImageBrush(transimage);
        }

        public void banacc_MouseEnter(object sender, MouseEventArgs e)
        {
            var transimage = Imaging.CreateBitmapSourceFromHBitmap(Properties.Resources.ban_hover.GetHbitmap(),
                      IntPtr.Zero,
                      Int32Rect.Empty,
                      BitmapSizeOptions.FromEmptyOptions());
            banacc.Background = new ImageBrush(transimage);
        }

        public void banacc_MouseLeave(object sender, MouseEventArgs e)
        {
            var transimage = Imaging.CreateBitmapSourceFromHBitmap(Properties.Resources.ban.GetHbitmap(),
                      IntPtr.Zero,
                      Int32Rect.Empty,
                      BitmapSizeOptions.FromEmptyOptions());
            banacc.Background = new ImageBrush(transimage);
        }

        public void deleteaccount_MouseEnter(object sender, MouseEventArgs e)
        {
            var transimage = Imaging.CreateBitmapSourceFromHBitmap(Properties.Resources.delete_hover.GetHbitmap(),
                                  IntPtr.Zero,
                                  Int32Rect.Empty,
                                  BitmapSizeOptions.FromEmptyOptions());
            deleteaccount.Background = new ImageBrush(transimage);
        }

        public void deleteaccount_MouseLeave(object sender, MouseEventArgs e)
        {
            var transimage = Imaging.CreateBitmapSourceFromHBitmap(Properties.Resources.delete_normal.GetHbitmap(),
                                  IntPtr.Zero,
                                  Int32Rect.Empty,
                                  BitmapSizeOptions.FromEmptyOptions());
            deleteaccount.Background = new ImageBrush(transimage);
        }

        public void deletechar_MouseEnter(object sender, MouseEventArgs e)
        {
            var transimage = Imaging.CreateBitmapSourceFromHBitmap(Properties.Resources.delchar_hover.GetHbitmap(),
                                  IntPtr.Zero,
                                  Int32Rect.Empty,
                                  BitmapSizeOptions.FromEmptyOptions());
            deletechar.Background = new ImageBrush(transimage);
        }

        public void deletechar_MouseLeave(object sender, MouseEventArgs e)
        {
            var transimage = Imaging.CreateBitmapSourceFromHBitmap(Properties.Resources.delchar_normal.GetHbitmap(),
                                  IntPtr.Zero,
                                  Int32Rect.Empty,
                                  BitmapSizeOptions.FromEmptyOptions());
            deletechar.Background = new ImageBrush(transimage);
        }

        public void newacc_MouseEnter(object sender, MouseEventArgs e)
        {
            var transimage = Imaging.CreateBitmapSourceFromHBitmap(Properties.Resources.register_hover.GetHbitmap(),
                                  IntPtr.Zero,
                                  Int32Rect.Empty,
                                  BitmapSizeOptions.FromEmptyOptions());
            newacc.Background = new ImageBrush(transimage);
        }

        public void newacc_MouseLeave(object sender, MouseEventArgs e)
        {
            var transimage = Imaging.CreateBitmapSourceFromHBitmap(Properties.Resources.register_normal.GetHbitmap(),
                      IntPtr.Zero,
                      Int32Rect.Empty,
                      BitmapSizeOptions.FromEmptyOptions());
            newacc.Background = new ImageBrush(transimage);
        }

        public void editchar_MouseEnter(object sender, MouseEventArgs e)
        {
            var transimage = Imaging.CreateBitmapSourceFromHBitmap(Properties.Resources.editchar_hover.GetHbitmap(),
                                  IntPtr.Zero,
                                  Int32Rect.Empty,
                                  BitmapSizeOptions.FromEmptyOptions());
            editchar.Background = new ImageBrush (transimage);
        }

        public void editchar_MouseLeave(object sender,MouseEventArgs e)
        {
            var transimage = Imaging.CreateBitmapSourceFromHBitmap(Properties.Resources.editchar_normal.GetHbitmap(),
                      IntPtr.Zero,
                      Int32Rect.Empty,
                      BitmapSizeOptions.FromEmptyOptions());
            editchar.Background = new ImageBrush(transimage);
        }

        void Statics()
        {
            totaldfs.Content = get.Totaldfs();
            totalguilds.Content = get.TotalGuilds();
            totalcharacters.Content = get.TotalChars();
            toppvp.Content = get.PVPRanking();
            toppk.Content = get.PKRanking();
            onlineplayers.Content = get.OnlinePlayers();
            bannedplayers.Content = get.BannedPlayers();
            totalaccounts.Content = get.TotalAccounts();
        }

        void Status()
        {
            dksv.Content = get.tcp(Convert.ToInt32(config.Read("World Port","PORTS").ToString()));
            arenasv.Content = get.tcp(Convert.ToInt32(config.Read("Arena Port","PORTS").ToString()));
            loginsv.Content = get.tcp(Convert.ToInt32(config.Read("Login Port","PORTS").ToString()));
            websv.Content = get.tcp(Convert.ToInt32(config.Read("Web Port","PORTS").ToString()));
        }

        private void refreshstatics_Click(object sender, RoutedEventArgs e)
        {
            Statics();
        }

     
        private async void editchar_Click(object sender, RoutedEventArgs e)
        {
            var x = await this.ShowInputAsync("Database character edit", "Please type the character name to find");
            if (!String.IsNullOrWhiteSpace(x))
            {
                dbfunc.editInfo d = new dbfunc.editInfo();
                DatabaseControl.Character open = new DatabaseControl.Character();

                if (!d.charExist(x))
                {
                    await this.ShowMessageAsync("Character name is wrong or does not exist", "Please make sure you are finding for a valid char name");
                }
                else
                {
                    open.searchCharacter(x);
                    open.ShowDialog();
                }

            }
            
        }


        private async void deleteaccount_Click(object sender, RoutedEventArgs e)
        {
            var accname = await this.ShowInputAsync("Database account delete", "Please type the account name to delete");
            if (!String.IsNullOrWhiteSpace(accname))
            {

                dbfunc.editInfo d = new dbfunc.editInfo();
                if (!d.accExist(accname))
                {
                    await this.ShowMessageAsync("Account name is wrong or does not exist", "Please make sure you are finding for a valid account");
                }
                else
                {
                    var confirm = await this.ShowMessageAsync("Click OK to delete " + accname, "Please click OK to make sure you want to delete this account",MessageDialogStyle.AffirmativeAndNegative);
                    if (confirm == MessageDialogResult.Affirmative)
                    {
                        d.deleteacc(accname);
                        await this.ShowMessageAsync("Account deleted", "Your account: [" + accname + "] was been sucessfully deleted!");
                    }
                    else
                    {
                        await this.ShowMessageAsync("Account not deleted", "Your account wasn't deleted!");
                    }
                }
            }
        }

        
        private void newacc_Click(object sender, RoutedEventArgs e)
        {
            DatabaseControl.Register x = new DatabaseControl.Register();
            x.ShowDialog();
        }

        private async void deletechar_Click(object sender, RoutedEventArgs e)
        {
            var charname = await this.ShowInputAsync("Database character delete", "Please type the character name to delete");
            if (!String.IsNullOrWhiteSpace(charname))
            {
                dbfunc.editInfo d = new dbfunc.editInfo();
                if (!d.charExist(charname))
                {
                    await this.ShowMessageAsync("Character name is wrong or does not exist", "Please make sure you are finding for a valid character");
                }
                else
                {
                    var confirm = await this.ShowMessageAsync("Click OK to delete " + charname , "Please click OK to make sure you want to delete this character", MessageDialogStyle.AffirmativeAndNegative);
                    if (confirm == MessageDialogResult.Affirmative)
                    {
                        d.deletechar(charname);
                        await this.ShowMessageAsync("Character deleted", "Your character: [" + charname + "] was been sucessfully deleted!");
                    }
                    else
                    {
                        await this.ShowMessageAsync("Character not deleted", "Your Character wasn't deleted!");
                    }
                }
            }
        }

        private void updateonline_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void changemap_Click(object sender, RoutedEventArgs e)
        {
            LiveMap.engine lm = new LiveMap.engine(this);
            lm.UpdateMap(mapList.SelectedIndex);
        }

        private async void banacc_Click(object sender, RoutedEventArgs e)
        {
            var charname = await this.ShowInputAsync("Database account ban", "Please type the account name to ban");
            if (!String.IsNullOrWhiteSpace(charname))
            {

                dbfunc.editInfo d = new dbfunc.editInfo();
                if (!d.accExist(charname))
                {
                    await this.ShowMessageAsync("Account name is wrong or does not exist", "Please make sure you are finding for a valid acount");
                }
                else
                {
                    var confirm = await this.ShowMessageAsync("Click OK to ban" + charname  ,"Please click OK to make sure you want to ban this account", MessageDialogStyle.AffirmativeAndNegative);
                    if (confirm == MessageDialogResult.Affirmative)
                    {
                        d.BanPlayer(charname);
                        await this.ShowMessageAsync("Account banned", "Your account: [" + charname + "] was been sucessfully banned!");
                    }
                    else
                    {
                        await this.ShowMessageAsync("Account not banned", "Your account wasn't banned!");
                    }
                }
            }
        }

        private async void savechanges_Click(object sender, RoutedEventArgs e)
        {
            if ((string)savechanges.Content == "Edit")
            {
                //unlock
                sqlipchange.IsEnabled = true;
                sqlportchange.IsEnabled = true;
                sqlidchange.IsEnabled = true;
                sqlpwchange.IsEnabled = true;
                action.IsEnabled = true;
                worldedit.IsEnabled = true;
                arenaedit.IsEnabled = true;
                loginedit.IsEnabled = true;
                webedit.IsEnabled = true;
                savechanges.Content = "Save";
            }
            else
            {
                config.Write("SQL IP", sqlipchange.Text,"MSSQL");
                config.Write("SQL Port", sqlportchange.Text,"MSSQL");
                config.Write("SQL Username", sqlidchange.Text,"MSSQL");
                config.Write("SQL Password", sqlpwchange.Text,"MSSQL");
                config.Write("Server Action", action.Text,"ACTION");
                config.Write("World Port", worldedit.Text,"PORTS");
                config.Write("Arena Port", arenaedit.Text,"PORTS");
                config.Write("Login Port", loginedit.Text,"PORTS");
                config.Write("Web Port", webedit.Text,"PORTS");
                await this.ShowMessageAsync("Config file updated", "Your config file was been updated!");
                loadConfigs();
                savechanges.Content = "Edit";
            }
        }

        private void detailview_Click(object sender, RoutedEventArgs e)
        {
            LiveMap.Details.mapcode= mapList.SelectedIndex;
            LiveMap.Details x = new LiveMap.Details();
            x.ShowDialog();
        }

        private async void editcoins_Click(object sender, RoutedEventArgs e)
        {
            var accid = await this.ShowInputAsync("Database edit coins", "Please type the account name to edit coins");
            if (!String.IsNullOrWhiteSpace(accid))
            {
                dbfunc.editInfo d = new dbfunc.editInfo();
                if (!d.accExist(accid))
                {
                    await this.ShowMessageAsync("Account name is wrong or does not exist", "Please make sure you are finding for a valid acount");
                }
                else
                {
                    if (d.GetCash(d.PlayerNameByID(accid)) < 0)
                    {
                        await this.ShowMessageAsync("Account coins is less than 0", "Your account haven't visited the d-shop yet");
                    }
                    else
                    {
                        DatabaseControl.Cash x = new DatabaseControl.Cash();
                        x.LoadCash(accid);
                        x.ShowDialog();
                    }
                }
            }
        }

        private void refreshstatus_Click(object sender, RoutedEventArgs e)
        {
            Status();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

    }
}
