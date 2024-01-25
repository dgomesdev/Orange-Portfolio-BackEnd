using Orange_Portfolio_BackEnd.Data;
using Orange_Portfolio_BackEnd.Models;
using Orange_Portfolio_BackEnd.Models.Interfaces;

namespace Orange_Portfolio_BackEnd.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly Context _db;

        public TagRepository(Context db)
        {
            _db = db;
        }

        public ICollection<Tag> GetAll()
        {
            return _db.Tags.ToList();
        }

        public Tag GetById(int id)
        {
            return _db.Tags.Find(id)!;
        }

        public void Add(Tag tag)
        {
            _db.Tags.Add(tag);
            _db.SaveChanges();
        }
        public void Update(Tag tag)
        {
            _db.Tags.Update(tag);
            _db.SaveChanges();
        }
        public void Delete(int id)
        {
            _db.Tags.Remove(GetById(id));
            _db.SaveChanges();
        }
    }
}
