using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ServerManager.LiveMap
{
    /// <summary>
    /// Main functionalities goes here
    /// </summary>
    class Engine
    {
        /// <summary>
        /// MainMenu Instance
        /// </summary>
        private readonly MainMenu form;

        /// <summary>
        /// DataTable local instance
        /// </summary>
        protected DataTable values;

        //Constructor to use the mainmenu objects
        public Engine(MainMenu form1) => form = form1;

        /// <summary>
        /// Binds the maps by the real index also into the mapList
        /// Load maps and set their position because braiken is 1 but ardeca is 150 -> high jump / and 150 can jump into 553 -> mitera
        /// </summary>
        public void LoadMaps()
        {
            form.mapList.Items.Insert(0, "Braiken Castle");
            form.mapList.Items.Insert(1, "North Ares");
            form.mapList.Items.Insert(2, "Norak Cave");
            form.mapList.Items.Insert(3, "Deneb");
            form.mapList.Items.Insert(4, "East Ares");
            form.mapList.Items.Insert(5, "Heihaff");
            form.mapList.Items.Insert(6, "Parca Temple");
            form.mapList.Items.Insert(7, "Loa Castle");
            form.mapList.Items.Insert(8, "North Morte");
            form.mapList.Items.Insert(9, "West Morte");
            form.mapList.Items.Insert(10, "Castor");
            form.mapList.Items.Insert(11, "Crevasse");
            form.mapList.Items.Insert(12, "Crespo");
            form.mapList.Items.Insert(13, "Draco Desert");
            form.mapList.Items.Insert(14, "Norak 2nd Floor");
            form.mapList.Items.Insert(15, "Castor 2nd Floor");
            form.mapList.Items.Insert(16, "Prison");
            form.mapList.Items.Insert(17, "Requies Beach");
            form.mapList.Items.Insert(18, "Avalon");
            form.mapList.Items.Insert(19, "Python");
            form.mapList.Items.Insert(20, "Tomb of Black Dragon");
            form.mapList.Items.Insert(21, "Doomed Maze");
            form.mapList.Items.Insert(22, "Undo Stadium");
            form.mapList.Items.Insert(23, "Siege Castle");
            form.mapList.Items.Insert(24, "Magic Field of Crevice");
            form.mapList.Items.Insert(25, "Crespo Treasure");
            form.mapList.Items.Insert(26, "Aquarius");

            //Define the default map [Braiken Castle]
            form.mapList.SelectedIndex = 0;
        }

        /// <summary>
        /// Updates the map with a given map index (dekaron map index)
        /// </summary>
        /// <param name="mapindex"></param>
        public void UpdateMap(int mapindex)
        {
            //values = playersposition because i return the whole datatable result
            ChangeImage(mapindex);
            values = LMSQL.PlayersPosition(mapindex);
            if (values.Rows.Count > 0)
            {
                form.detailview.IsEnabled = true;
                form.playersfound.Content = values.Rows.Count.ToString();
                DrawPlayers();
            }
            else
            {
                form.detailview.IsEnabled = false;
                form.playersfound.Content = 0;
                form.cv.Children.Clear();
                //no players and i need update the label
            }
        }

        /// <summary>
        /// Changes the map background image according to the map index
        /// </summary>
        /// <param name="mapcode"></param>
        public void ChangeImage(int mapcode)
        {
            try
            {
                Uri path = new Uri(@"pack://application:,,,/LiveMap/mapimg/" + mapcode + ".png");
                BitmapImage change = new BitmapImage(path);
                form.mapimg.Source = change;
            }
            catch
            {
                MessageBox.Show("The file map " + mapcode + "is not found or does not exist!");
            }
        }


        /// <summary>
        /// This function was made to draw the players characters icon depending on their location, x and y
        /// </summary>
        public void DrawPlayers()
        {
            DataTable temp = new DataTable();

            form.cv.Children.Clear();
            for (int i = 0; i < values.Rows.Count; i++)
            {
                Ellipse ellipse = new Ellipse();
                ellipse.Width = 10;
                ellipse.Height = 10;
                ellipse.StrokeThickness = 2;
                temp = LMSQL.OnlinePlayers();

                ellipse.Fill = System.Windows.Media.Brushes.Red;

                for (int j = 0; j < temp.Rows.Count; i++)
                {
                    // not working yet
                    if (values.Rows[i]["user_no"].ToString() == temp.Rows[j]["user_no"].ToString())
                        ellipse.Fill = System.Windows.Media.Brushes.Green;

                }

                form.cv.Children.Add(ellipse);
                Canvas.SetLeft(ellipse, Convert.ToDouble(values.Rows[i]["wPosX"].ToString()));
                Canvas.SetTop(ellipse, Convert.ToDouble(values.Rows[i]["wPosY"].ToString()));
            }


        }

        /// <summary>
        /// Returns an image according to the class icon
        /// Not done yet just a class mapping
        /// </summary>
        /// <param name="pcclass"></param>
        public void ClassIcon(int pcclass)
        {
            switch (pcclass)
            {
                case 0:
                    //AK
                    break;
                case 1:
                    //Hunter
                    break;
                case 2:
                    //Mage
                    break;
                case 3:
                    //Summoner
                    break;
                case 4:
                    //SEGNALE
                    break;
                case 5:
                    //BAGi
                    break;
                case 6:
                    //ALOKEN
                    break;
                case 7:
                    //DRAGON Knight
                    break;
                case 10:
                    //Concera
                    break;
                case 11:
                    //SEGU
                    break;
                case 12:
                    //HAlf bagi
                    break;

            }

        }
    }
}
