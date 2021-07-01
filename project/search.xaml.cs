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
    /// Логика взаимодействия для search.xaml
    /// </summary>
    public partial class search : Page
    {
        public search(List<character> list)
        {
            InitializeComponent();
            items.ItemsSource = list;
        }
        private void mouseup(object sender, MouseButtonEventArgs e)
        {
            try {
                character b = (character)items.SelectedItem;
                if (b != null)
                {
                    Surnametext.Text = b.Тайтл.ToString();
                    Nametext.Text = b.Имя.ToString();
                    Agetext.Text = b.Тип.ToString();
                    texttext.Text = b.Описание.ToString();
                    showimage(b);
                }
            } catch (Exception) { }
            
        }
        public void showimage(character b)
        {
            byte[] array = b.Picture;
            ImageBox.Source = ToImage(array);
        }
        public BitmapImage ToImage(byte[] array)
        {
            using (var ms = new System.IO.MemoryStream(array))
            {
                var image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad; // here
                image.StreamSource = ms;
                image.EndInit();
                return image;
            }
        }
    }
}
