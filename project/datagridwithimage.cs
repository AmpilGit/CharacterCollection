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
using MongoDB.Bson.Serialization.Attributes;

namespace project
{
    public class datagridwithimage
    {
        [BsonId]
        public ObjectId Id { get; set; }
        [BsonElement("name")]
        public string Имя { get; set; }
        [BsonElement("surname")]
        public string Тайтл { get; set; }
        [BsonElement("age")]
        public string Тип { get; set; }
        [BsonElement("text")]
        public string Описание { get; set; }
        [BsonElement("picture")]
        public BitmapImage Picture { get; set; }
        public datagridwithimage(ObjectId id,string name, string surname, string age, string text, BitmapImage picture)
        {
            Id = id;
            Имя = name;
            Тайтл = surname;
            Тип = age;
            Описание = text;
            Picture = picture;
        }
    }
}
