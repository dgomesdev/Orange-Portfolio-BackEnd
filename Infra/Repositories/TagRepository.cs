using Microsoft.EntityFrameworkCore;
using Orange_Portfolio_BackEnd.Domain.Models;
using Orange_Portfolio_BackEnd.Domain.Models.Interfaces;
using Orange_Portfolio_BackEnd.Infra.Data;

namespace Orange_Portfolio_BackEnd.Infra.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly Context _db;

        public TagRepository(Context db)
        {
            _db = db;
        }

        public async Task<ICollection<Tag>> GetAll()
        {
            return await _db.Tags.ToListAsync();
        }

        public async Task<Tag> GetById(int id)
        {
            return await _db.Tags.FindAsync(id)!;
        }

        public async Task Add(Tag tag)
        {
            _db.Tags.Add(tag);
            await _db.SaveChangesAsync();
        }
        public async Task Update(Tag tag)
        {
            _db.Tags.Update(tag);
            await _db.SaveChangesAsync();
        }
        public async Task Delete(int id)
        {
            _db.Tags.Remove(await GetById(id));
            await _db.SaveChangesAsync();
        }
    }
}
