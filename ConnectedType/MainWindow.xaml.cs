using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ConnectedType
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string _connectionString = @"Data Source = DESKTOP-PG10UGI\SQLEXPRESS; Initial Catalog = MCS; User Id = sa; Password = Mc123456";
        public MainWindow()
        {

            InitializeComponent();
        }

        private void ButtonDownload_Click(object sender, RoutedEventArgs e)
        {
        
            try
            {
                SqlConnection con = new SqlConnection(_connectionString);
                SqlCommand command = new SqlCommand("select * from newEquipment", con);
               

                using (con)
                {
                    con.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    List<Equipments> equipmentses = new List<Equipments>();
                    
                    while (reader.Read())
                    {
                       Equipments eq = new Equipments();
                        eq.EquipmentId = Int32.Parse(reader[0].ToString());
                        eq.GarageRoom = reader[1].ToString();
                        eq.SerialNo = reader[6].ToString();
                        eq.ManufYear = reader[4].ToString();
                        equipmentses.Add(eq);
                    }

                    ListViewConnected.ItemsSource = equipmentses;
                }

    


            }
            catch (Exception ex)
            {
               
            }
        }

        public class Equipments
        {
            public int EquipmentId { get; set; }
            public string GarageRoom { get; set; }
            public string SerialNo { get; set; }
            public string ManufYear { get; set; }
            public int ModelId { get; set; }

        }


    
        private void ListViewConnected_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Equipments eq = ListViewConnected.SelectedItem as Equipments;
            TextBoxEquipmentId.Text = eq.EquipmentId.ToString();
            TextBoxGarageRoom.Text = eq.GarageRoom;
            TextBoxSerialNo.Text = eq.SerialNo;
            TextBoxManuYear.Text = eq.ManufYear;
        }


        private void ButtonSaveChanges_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                Equipments eq2 = ListViewConnected.SelectedItem as Equipments;
                SqlConnection con = new SqlConnection(_connectionString);
                string sqlExpression = "UPDATE newEquipment SET strManufYear=" + TextBoxManuYear.Text +
                                       ", intGarageRoom=" + TextBoxGarageRoom.Text + ", strSerialNo ='" + TextBoxSerialNo.Text +  "' WHERE intEquipmentID=" +
                                       eq2.EquipmentId + "";
                using (con)
                {
                    con.Open();
                    SqlCommand command = new SqlCommand(sqlExpression, con);
                    int number = command.ExecuteNonQuery();
                    MessageBox.Show("Обновлено объектов" + number);
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void ButtonReports_OnClick(object sender, RoutedEventArgs e)
        {
            Reports rp = new Reports();
            rp.Show();
        }
    }
}
