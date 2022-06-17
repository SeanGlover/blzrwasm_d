using blzrwasm_d.Server.Models;
using blzrwasm_d.Shared;
using MongoDB.Driver;

namespace blzrwasm_d.Server.Services
{
    public interface IUserService
    {
        List<User> Get();
        User? Get(string id);
        User? Get(int userId);
        User Create(User user);
        void Replace(string id, User user);
        void Update(string id, User user);
        void Remove(string id);
    }
    public class UserService : IUserService
    {
        private readonly IMongoCollection<User> _users;
        public static readonly List<User> users = new();
        public UserService(IUserStoreDatabaseSettings settings, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _users = database.GetCollection<User>(settings.CollectionName);
            foreach (var user in _users.Find(user => true).ToList()) { users.Add(user); }
        }
        public User Create(User user)
        {
            _users.InsertOne(user);
            return user;
        }
        public List<User> Get() { return _users.Find(user => true).ToList(); }
        public User? Get(string id)
        {
            List<User> users = Get();
            User? user = users.Where(e => e.Id == id).FirstOrDefault();
            return user;
        }
        public User Get(int userId) { return _users.Find(user => user.UserId == userId).FirstOrDefault(); }
        public void Remove(string id) { _users.DeleteOne(user => user.Id == id); }
        public void Replace(string id, User user) { _users.ReplaceOne(usr => usr.Id == id, user); }
        public void Update(string id, User user) { _users.ReplaceOne(usr => usr.Id == id, user); }
    }
}
