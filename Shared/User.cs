using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.RegularExpressions;

namespace blzrwasm_d.Shared
{
    [BsonIgnoreExtraElements]
    public class User
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

        [BsonElement("UserId")]
        public int UserId { get; set; }

        [BsonElement("name")]
        public string Name { get; set; } = string.Empty;

        [BsonElement("email")]
        public string Email { get; set; } = string.Empty;

        [BsonElement("role")]
        public string Role { get; set; } = string.Empty;

        [BsonElement("password")]
        public string Password { get; set; } = string.Empty;
    }
}
