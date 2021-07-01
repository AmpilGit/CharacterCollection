using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
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
using MongoDB.Bson;
using MongoDB.Driver;

namespace project
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            element.Navigate(new SignRegist());
        }
        public static void music(bool play)
        {
            SoundPlayer player = new SoundPlayer();
            player.Stream = Properties.Resources.mu;
            if (play) 
            {
                
                player.Load();
                player.PlayLooping();
            }
            else
            {
                player.Stop();
            }
        }
        public static bool chekingRegist(string name, string password)
        {
            bool mogno = false;
            string connectionString = "mongodb://localhost:27017";
            MongoClient client = new MongoClient(connectionString);
            IMongoDatabase database = client.GetDatabase("characters");
            var collection = database.GetCollection<BsonDocument>("users");
            if (name != "" && password!="")
            {
                var filter = new BsonDocument
                ("$and", new BsonArray
                {
                new BsonDocument("name",name),
                new BsonDocument("password", password)
                }
                );
                var text = collection.Find(filter).ToList();
                try
                {
                    text[0].ToString();
                    MessageBox.Show("Enter another login or password");
                }
                catch (Exception)
                {
                    mogno = true;
                }
                         
            }
            return mogno;
        }
        public static bool regist(string name, string password)
        {
            bool mogno = false;
            string connectionString = "mongodb://localhost:27017";
            MongoClient client = new MongoClient(connectionString);
            IMongoDatabase database = client.GetDatabase("characters");
            var collection = database.GetCollection<BsonDocument>("users");
            if (name != "" && password!="" && chekingRegist(name,password))
            {
                var insert = new BsonDocument
                (new BsonDocument
                {{"name" , name},{"password",password} }
                );
                collection.InsertOne(insert);
                music(false);
                mogno = true;
            }
            return mogno;
        }
        public static bool login(string name, string password)
        {
            bool mogno = false;
            string connectionString = "mongodb://localhost:27017";
            MongoClient client = new MongoClient(connectionString);
            IMongoDatabase database = client.GetDatabase("characters");
            var collection = database.GetCollection<BsonDocument>("users");
            if (name != "" && password != "")
            {
                var filter = new BsonDocument
                ("$and", new BsonArray
                {
                new BsonDocument("name",name),
                new BsonDocument("password", password)
                }
                );
                var text = collection.Find(filter).ToList();
                try
                {
                    text[0].ToString();
                    music(false);
                    mogno = true;
                }
                catch (Exception)
                {
                    mogno = false;
                }
            }
            return mogno;
        }
    }
}
