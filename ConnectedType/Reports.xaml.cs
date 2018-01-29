using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
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

namespace ConnectedType
{
    /// <summary>
    /// Interaction logic for Reports.xaml
    /// </summary>
    public partial class Reports : Window
    {
        private string _connectionString2 = @"Data Source = DESKTOP-PG10UGI\SQLEXPRESS; Initial Catalog = MCS; User Id = sa; Password = Mc123456";
        public Reports()
        {
            InitializeComponent();
        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(_connectionString2);
                int i = 0;
               
                string SqlText = "";
                if (TextBoxId.IsEnabled == true)
                {
                    SqlText = "select * from TrackMeter where intMeteredId=" + TextBoxId.Text;
                }
                else if (TextBoxStartDate.IsEnabled == true)
                {
                    string td = TextBoxStartDate.SelectedDate.ToString();
                    DateTime dt = DateTime.Parse(td);
                    

                    SqlText = "select * from TrackMeter where dMeterDate= '" + dt.ToString("u") +"'";

                }
                else SqlText = "select * from TrackMeter";
                SqlCommand cmd = new SqlCommand(SqlText, con);



                using (con)
                {
                    con.Open();
                    SqlDataReader sdr = cmd.ExecuteReader();
                    List<TrackMeter> trackMeters = new List<TrackMeter>();
                    while (sdr.Read())
                    {
                        TrackMeter tm = new TrackMeter();
                        tm.intMeteredId = Int32.Parse(sdr[0].ToString());
                        tm.intEquipmentID = Int32.Parse(sdr[1].ToString());
                        tm.dMeterDate = sdr.GetDateTime(2);
                        tm.intMeterReading = sdr[3].ToString();
                        tm.intHoursHoursOperation = sdr[4].ToString();
                        trackMeters.Add(tm);
                    }

                    ListViewConnected.ItemsSource = trackMeters;
                }

                //SqlCommand command = new SqlCommand(sqlExpression, con);
                //int number = command.ExecuteNonQuery();
                //MessageBox.Show("Обновлено объектов " + number);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public class TrackMeter
        {
            public int intMeteredId { get; set; }
            public int intEquipmentID { get; set; }
            public DateTime dMeterDate { get; set; }
            public string intMeterReading { get; set; }
            public string intHoursHoursOperation { get; set; }
        }

    }
}
