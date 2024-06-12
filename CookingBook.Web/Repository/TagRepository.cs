using CookingBook.Web.Data;
using CookingBook.Web.Models.Domena;
using Microsoft.EntityFrameworkCore;

namespace CookingBook.Web.Repository
{
    public class TagRepository : ITagRepository
    {
        private readonly BlogDB blogDB;

        public async Task<IEnumerable<Tag>> GetAllAsync(string? searchQuery)
        {
            var query = blogDB.Tagi.AsQueryable();

            if (string.IsNullOrWhiteSpace(searchQuery) == false)
            {
                query = query.Where(x => x.Nazwa.Contains(searchQuery));
            }

            return await query.ToListAsync();            
            // return await blogDB.Tagi.ToListAsync();
        }

        public TagRepository(BlogDB blogDB)
        {
            this.blogDB = blogDB;
        }

        public async Task<Tag> AddAsync(Tag tag)
        {
            await blogDB.Tagi.AddAsync(tag);
            await blogDB.SaveChangesAsync();
            return tag;
        }

        public async Task<Tag?> DeleteAsync(Guid id)
        {
            var existingTag = await blogDB.Tagi.FindAsync(id);

            if (existingTag != null)
            {
                blogDB.Tagi.Remove(existingTag);
                await blogDB.SaveChangesAsync();
                return existingTag;
            }
            return null;
        }

        public Task<Tag?> GetAsync(Guid id)
        {
            return blogDB.Tagi.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Tag?> UpdateAsync(Tag tag)
        {
            var existingTag = await blogDB.Tagi.FindAsync(tag.Id);

            if (existingTag != null)
            {
                existingTag.Nazwa = tag.Nazwa;
                existingTag.WyświetlanaNazwa = tag.WyświetlanaNazwa;

                await blogDB.SaveChangesAsync();

                return existingTag;
            }
            return null;
        }
    }
}
