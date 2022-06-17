using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.RegularExpressions;

namespace blzrwasm_d.Shared
{
    [BsonIgnoreExtraElements]
    public class Product : IEquatable<Product>
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        private string _id = string.Empty;
        public string Id
        {
            get { return _id; }
            set
            {
                string newId = value ?? string.Empty;
                if (newId.Length == 24) { _id = newId; }
                else
                {
                    _id = Guid.NewGuid().ToString();
                    _id = Regex.Replace(_id, "[^0-9]", string.Empty, RegexOptions.None);
                    _id = Regex.Match(_id, "[0-9]{24}").Value;
                }
            }
        }

        [BsonElement("Code")]
        public string Code { get; }
        
        [BsonElement("Name")]
        public string Name { get; set; }

        [BsonElement("AlternateName")]
        public string AlternateName { get; set; } = string.Empty;

        [BsonElement("Description")]
        public string Description { get; }

        [BsonElement("HasSerial")]
        public bool HasSerial { get; set; }

        [BsonElement("Care")]
        public string Care { get; set; } = string.Empty;

        [BsonElement("Price")]
        public double Price { get; }

        [BsonElement("URL")]
        public string URL { get; set; }

        [BsonElement("ImageURL")]
        public string ImageURL { get; set; }

        [BsonElement("Time")]
        public TimeSpan Time { get; set; }

        [BsonElement("Materials")]
        public List<string> Materials { get; set; } = new List<string>();

        [BsonElement("Tools")]
        public List<string> Tools { get; set; } = new List<string>();

        public Product(string code, string name, string description, double price, string url, string imageUrl)
        {
            Id = Guid.NewGuid().ToString().Replace("-", string.Empty);
            Code = code;
            Name = name;
            Description = description;
            Price = price;
            URL = url;
            ImageURL = imageUrl;
        }

        public override int GetHashCode() { return Id.GetHashCode() ^ Materials.GetHashCode() ^ Tools.GetHashCode(); }
        public override bool Equals(object? other) => other is Product otherProduct && Equals(otherProduct);
        public bool Equals(Product? other) => Id == other?.Id;
        public override string ToString() => $"{Code} {Name} · Materials [{Materials.Count}] · Tools [{Tools.Count}]";
    }
}
