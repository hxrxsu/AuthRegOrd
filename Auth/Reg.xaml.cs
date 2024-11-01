using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Newtonsoft.Json;

namespace Auth
{
    /// <summary>
    /// Interaction logic for Regxaml.xaml
    /// </summary>
    public partial class Reg : Page
    {
        public Reg()
        {
            InitializeComponent();
        }

        string filePath = "users.json";


        private void BN_Reg_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!File.Exists(filePath)) 
                {
                    File.Create(filePath).Close();
                }




                List<Person> userdata = JsonConvert.DeserializeObject<List<Person>>(File.ReadAllText(filePath)) ?? new List<Person>();
                userdata.Add(new Person{ login = TB_Login.Text, password = TB_Pass.Text  });
                string json = JsonConvert.SerializeObject(userdata);
                File.WriteAllText(filePath, json);

                MessageBox.Show("Вы зарегистрировались!");
            }
            catch(Exception ex) { MessageBox.Show($"Ошибка! {ex}"); }
        }

        private void BN_Auth_Click(object sender, RoutedEventArgs e)
        {
            List<Person> userdata = JsonConvert.DeserializeObject<List<Person>>(File.ReadAllText(filePath)) ?? new List<Person>();
            var user = userdata.FirstOrDefault(u => u.login == TB_Login.Text && u.password == TB_Pass.Text);
            
            if(user != null)
            {
                MessageBox.Show("Вы вошли " +  user.login);

                NavigationService.Navigate(new OrdersMenu());
            }
            else
            {
                MessageBox.Show("Неправильный логин или пароль");
            }
        }
    }
}
