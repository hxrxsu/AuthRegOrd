using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace Auth
{
    /// <summary>
    /// Interaction logic for OrdersMenu.xaml
    /// </summary>
    public partial class OrdersMenu : Page
    {
        public OrdersMenu()
        {
            InitializeComponent();
        }

        string filePath = "ordersinfofile.json";
        private void BN_Add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    File.Create(filePath).Close();
                }

                List<OrderInfo> orderdata = JsonConvert.DeserializeObject<List<OrderInfo>>(File.ReadAllText(filePath)) ?? new List<OrderInfo>();
                orderdata.Add(new OrderInfo { name = TB_OrderName.Text, description = TB_Desc.Text,  price = Convert.ToDouble(TB_Price.Text) });
                string json = JsonConvert.SerializeObject(orderdata);
                File.WriteAllText(filePath, json);
                var ordername = orderdata.FirstOrDefault(o => o.name == TB_OrderName.Text);


                var orderData = JsonConvert.DeserializeObject<List<OrderInfo>>(json);
                MessageBox.Show("Заказ добавлен" + ordername.name);

            }
            catch (Exception ex) { MessageBox.Show($"Ошибка! {ex}"); }
        }
    }
}
