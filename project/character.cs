using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace project
{
    public class character
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
        public byte[] Picture { get; set; }
        public character(string name,string surname,string age, string text, byte[] picture)
        {
            Имя = name;
            Тайтл = surname;
            Тип = age;
            Описание = text;
            Picture = picture;
        }
    }
}
