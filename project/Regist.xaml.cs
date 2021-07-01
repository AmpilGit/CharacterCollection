using System;
using System.Collections.Generic;
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

namespace project
{
    /// <summary>
    /// Логика взаимодействия для Regist.xaml
    /// </summary>
    public partial class Regist : Page
    {
        public Regist()
        {
            InitializeComponent();
        }

        public void Button_Click(object sender, RoutedEventArgs e)
        {
            string name = Name.Text;
            string password = Password.Password;
            name = name.Trim();
            password = password.Trim();
            if(name!="" && password != "")
            {
                if(MainWindow.regist(name, password))
                {
                    NavigationService.Navigate(new main());
                }
            }
            else { MessageBox.Show("заполните поля"); }
        }
    }
}
