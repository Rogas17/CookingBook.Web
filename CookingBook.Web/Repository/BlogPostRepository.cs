using CookingBook.Web.Data;
using CookingBook.Web.Models.Domena;
using Microsoft.EntityFrameworkCore;

namespace CookingBook.Web.Repository
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly BlogDB blogDB;

        public BlogPostRepository(BlogDB blogDB)
        {
            this.blogDB = blogDB;
        }

        public async Task<BlogPost> AddAsync(BlogPost blogPost)
        {
            await blogDB.AddAsync(blogPost);
            await blogDB.SaveChangesAsync();
            return blogPost;
        }

        public async Task<BlogPost?> DeleteAsync(Guid id)
        {
            var existingBlog = await blogDB.Posty.FindAsync(id);

            if (existingBlog != null)
            {
                blogDB.Posty.Remove(existingBlog);
                await blogDB.SaveChangesAsync();
                return existingBlog;
            }
            return null;
        }

        public async Task<IEnumerable<BlogPost>> GetAllAsync(string? searchQuery)
        {
            var query = blogDB.Posty.AsQueryable();

            if (string.IsNullOrWhiteSpace(searchQuery) == false)
            {
                query = query.Where(x => x.Nagłówek.Contains(searchQuery)||
                                         x.KrótkiOpis.Contains(searchQuery));
            }

            return await query.Include(x => x.Tagi).ToListAsync();

            //return await blogDB.Posty.Include(x => x.Tagi).ToListAsync();
        }

        public async Task<BlogPost?> GetAsync(Guid id)
        {
            return await blogDB.Posty.Include(x => x.Tagi).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<BlogPost?> GetByUrlHandleAsync(string urlHandle)
        {
            return await blogDB.Posty.Include(x => x.Tagi)
                .FirstOrDefaultAsync(x => x.UrlHandle == urlHandle);
        }

        public async Task<BlogPost?> UpdateAsync(BlogPost blogPost)
        {
            var existingBlog = await blogDB.Posty.Include(x => x.Tagi).FirstOrDefaultAsync(x => x.Id == blogPost.Id);

            if (existingBlog != null)
            {
                existingBlog.Id = blogPost.Id;
                existingBlog.Nagłówek = blogPost.Nagłówek;
                existingBlog.TytułStrony = blogPost.TytułStrony;
                existingBlog.Treść = blogPost.Treść;
                existingBlog.KrótkiOpis = blogPost.KrótkiOpis;
                existingBlog.Autor = blogPost.Autor;
                existingBlog.UrlZdjęcia = blogPost.UrlZdjęcia;
                existingBlog.UrlHandle = blogPost.UrlHandle;
                existingBlog.Widoczność = blogPost.Widoczność;
                existingBlog.DataPublikacji = blogPost.DataPublikacji;
                existingBlog.Tagi = blogPost.Tagi;

                await blogDB.SaveChangesAsync();
                return existingBlog;
            }
            return null;
        }
    }
}
