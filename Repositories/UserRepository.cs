﻿using Microsoft.EntityFrameworkCore;
using Orange_Portfolio_BackEnd.Data;
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

        public async Task<ICollection<User>> GetAll()
        {
            return await _db.Users.ToListAsync();
        }

        public async Task<User> GetById(int id)
        {
            return await _db.Users.FindAsync(id)!;
        }

        public async Task Add(User user)
        {
            await _db.Users.AddAsync(user);
            await _db.SaveChangesAsync();
        }
        public async Task Update(User user)
        {
            _db.Users.Update(user);
            await _db.SaveChangesAsync();
        }
        public async Task Delete(int id)
        {
            _db.Users.Remove(await GetById(id));
            await _db.SaveChangesAsync();
        }

        public async Task<User> GetByEmail(string email)
        {
            return await _db.Users.FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
