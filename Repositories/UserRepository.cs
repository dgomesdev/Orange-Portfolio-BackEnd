﻿using Orange_Portfolio_BackEnd.Data;
using Orange_Portfolio_BackEnd.Models;
using Orange_Portfolio_BackEnd.Models.Interfaces;

namespace Orange_Portfolio_BackEnd.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly Context _db;

        public UserRepository(Context db)
        {
            _db = db;
        }

        public List<User> Get()
        {
            return _db.Users.ToList();
        }
        public User Get(int id)
        {
            return _db.Users.Find(id)!;
        }
        public void Add(User user)
        {
            _db.Users.Add(user);
            _db.SaveChanges();
        }
        public void Update(User user)
        {
            _db.Users.Update(user);
            _db.SaveChanges();
        }
        public void Delete(int id)
        {
            _db.Users.Remove(Get(id));
            _db.SaveChanges();
        }

        public ICollection<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public User GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
