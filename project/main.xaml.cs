using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media.Converters;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using System.IO;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Drawing;
using System.Drawing.Imaging;
using System.Media;
using System.Threading;

namespace project
{
    /// <summary>
    /// Логика взаимодействия для main.xaml
    /// </summary>
    public partial class main
    {
        public main()
        {
            InitializeComponent();
            vivod();

        }
        public async void vivod()
        {
            try
            {
                string connectionString = "mongodb://localhost:27017";
                MongoClient client = new MongoClient(connectionString);
                IMongoDatabase database = client.GetDatabase("characters");
                var collection = database.GetCollection<character>("characters");
                List<character> list = await collection.AsQueryable().ToListAsync<character>();
                List<datagridwithimage> newlist = new List<datagridwithimage>();
                foreach (var p in list)
                {
                    ObjectId id = p.Id;
                    string name = p.Имя;
                    string title = p.Тайтл;
                    string type = p.Тип;
                    string text = p.Описание;
                    BitmapImage picture = ToImage(p.Picture);
                    datagridwithimage data1 = new datagridwithimage(id, name, title, type, text, picture);
                    newlist.Add(data1);
                }
                items.ItemsSource = newlist;
            }
            catch (Exception) { }

        }
        public void addcharacter(object sender, RoutedEventArgs e)
        {//adding
            try
            {
                string name = nametext.Text;
                string surname = taitltext.Text;
                string age = typetext.Text;
                string text = texttext.Text;
                name = deletespace(name);
                age = deletespace(age);
                surname = deletespace(surname);
                text = deletespace(text);
                if (name != "" && age != "" && surname != "" && text != "")
                {
                    BitmapImage image1 = new BitmapImage();
                    OpenFileDialog file = new OpenFileDialog();
                    Nullable<bool> buttonOK = file.ShowDialog();
                    string _link = null;
                    string item = file.FileName;
                    _link += item;
                    image1 = new BitmapImage(new Uri(_link));
                    string connectionString = "mongodb://localhost:27017";
                    MongoClient client = new MongoClient(connectionString);
                    IMongoDatabase database = client.GetDatabase("characters");
                    var collection = database.GetCollection<character>("characters");
                    byte[] imagearray = ImageToByteArray(_link);
                    var insert = new BsonDocument();
                    character pers = new character(name, surname, age, text, imagearray);
                    collection.InsertOne(pers);
                    nametext.Text = "";
                    taitltext.Text = "";
                    typetext.Text = "";
                    texttext.Text = "";
                    vivod();
                }
            }
            catch (Exception) { };


        }
        public byte[] ImageToByteArray(string fileName)
        {
            Bitmap bitmap = new Bitmap(fileName);
            MemoryStream stream = new MemoryStream();
            bitmap.Save(stream, ImageFormat.Bmp);
            return stream.ToArray();
        }
        private void mouseup(object sender, MouseButtonEventArgs e)
        {
            datagridwithimage b = (datagridwithimage)items.SelectedItem;
            if (b != null)
            {
                idtext.Text = b.Id.ToString();
                taitltext.Text = b.Тайтл.ToString();
                nametext.Text = b.Имя.ToString();
                typetext.Text = b.Тип.ToString();
                texttext.Text = b.Описание.ToString();
            }

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

        private void update(object sender, RoutedEventArgs e)
        {
            //updating
            try
            {
                string name = nametext.Text;
                string surname = taitltext.Text;
                string age = typetext.Text;
                string text = texttext.Text;
                name = deletespace(name);
                age = deletespace(age);
                surname = deletespace(surname);
                text = deletespace(text);
                if (name != "" && age != "" && surname != "" && text != "")
                {
                    ObjectId id = ObjectId.Parse(idtext.Text);
                    string connectionString = "mongodb://localhost:27017";
                    MongoClient client = new MongoClient(connectionString);
                    IMongoDatabase database = client.GetDatabase("characters");
                    var collection = database.GetCollection<character>("characters");
                    var filter = new BsonDocument("_id", id);
                    var picture = collection.Find(filter).ToList<character>();
                    byte[] imagearray = picture[0].Picture;
                    var insert = new BsonDocument();
                    insert.Add("name", name);
                    insert.Add("surname", surname);
                    insert.Add("age", age);
                    insert.Add("text", text);
                    insert.Add("picture", imagearray);
                    var newcollection = database.GetCollection<BsonDocument>("characters");
                    var replaceall =
                    newcollection.ReplaceOne(new BsonDocument("_id", id), insert);
                    vivod();
                }
            }
            catch (Exception)
            {
            }
        }
        public string deletespace(string name)
        {
            name = name.Trim();
            return name;
        }
        private void updatewithimage(object sender, RoutedEventArgs e)
        {
            try
            {

                string name = nametext.Text;
                string surname = taitltext.Text;
                string age = typetext.Text;
                string text = texttext.Text;
                name = deletespace(name);
                surname = deletespace(surname);
                age = deletespace(age);
                text = deletespace(text);
                if (name != "" && age != "" && surname != "" && text != "")
                {
                    string _link = null;
                    string connectionString = "mongodb://localhost:27017";
                    MongoClient client = new MongoClient(connectionString);
                    IMongoDatabase database = client.GetDatabase("characters");
                    var collection = database.GetCollection<BsonDocument>("characters");
                    BitmapImage image1 = new BitmapImage();
                    OpenFileDialog file = new OpenFileDialog();
                    Nullable<bool> buttonOK = file.ShowDialog();
                    string item = file.FileName;
                    _link += item;
                    image1 = new BitmapImage(new Uri(_link));
                    byte[] imagearray = ImageToByteArray(_link);
                    ObjectId id = ObjectId.Parse(idtext.Text);
                    var insert = new BsonDocument();
                    insert.Add("name", name);
                    insert.Add("surname", surname);
                    insert.Add("age", age);
                    insert.Add("text", text);
                    insert.Add("picture", imagearray);
                    var replaceall =
                    collection.ReplaceOne(new BsonDocument("_id", id), insert);
                    Thread.Sleep(500);
                    vivod();
                }
            }
            catch (Exception) { }
        }

        private void delete(object sender, RoutedEventArgs e)
        {
            //deleting
            try
            {
                string connectionString = "mongodb://localhost:27017";
                MongoClient client = new MongoClient(connectionString);
                IMongoDatabase database = client.GetDatabase("characters");
                var collection = database.GetCollection<BsonDocument>("characters");
                ObjectId id = ObjectId.Parse(idtext.Text);
                var filter = Builders<BsonDocument>.Filter.Eq("_id", id);
                var delete = collection.DeleteOneAsync(filter);
                vivod();
            }
            catch (Exception) { }

        }

        public void search(object sender, RoutedEventArgs e)
        {
            //searching
            try
            {
                string name = nametext.Text;
                string surname = taitltext.Text;
                string age = typetext.Text;
                string text = texttext.Text;

                string connectionString = "mongodb://localhost:27017";
                MongoClient client = new MongoClient(connectionString);
                IMongoDatabase database = client.GetDatabase("characters");
                var collection = database.GetCollection<character>("characters");
                var filter = new BsonDocument("$or", new BsonArray{

            new BsonDocument("name",name),
            new BsonDocument("surname", surname),
            new BsonDocument("age",age),
            new BsonDocument("text",text)
            });
                var info = collection.Find(filter).ToList<character>();
                bool v = NavigationService.Navigate(new search(info));
            }
            catch (Exception) { }

        }

        private void clear(object sender, RoutedEventArgs e)
        {
            nametext.Text = "";
            taitltext.Text = "";
            typetext.Text = "";
            texttext.Text = "";
            idtext.Text = "";
        }
    }
}
