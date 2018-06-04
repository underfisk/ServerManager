using System.Data;
using System.Windows;


namespace ServerManager.LiveMap
{
    public partial class Details : Window
    {
        /// <summary>
        /// Holds the map index 
        /// </summary>
        public static int mapcode;

        /// <summary>
        /// On constructor call we initialize
        /// </summary>
        public Details()
        {
            DataTable dt = new DataTable();

            InitializeComponent();
            data.IsReadOnly = true;          
            dt = LMSQL.PlayersPosition(mapcode);
            data.ItemsSource = dt.DefaultView;
        }
    }
}
