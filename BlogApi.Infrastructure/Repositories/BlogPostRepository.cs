using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BlogApi.Domain.Entities;
using BlogApi.Domain.Repositories;
using BlogApi.Infrastructure.Persistence;

namespace BlogApi.Infrastructure.Repositories;

public class BlogPostRepository : IBlogPostRepository
{
    private readonly BlogDbContext _context;

    public BlogPostRepository(BlogDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<BlogPost>> ObterTodosAsync()
    {
        return await _context.BlogPosts
            .Include(p => p.Comentarios)
            .ToListAsync();
    }

    public async Task<BlogPost?> ObterPorIdAsync(Guid id)
    {
        return await _context.BlogPosts
            .Include(p => p.Comentarios)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<BlogPost> AdicionarAsync(BlogPost blogPost)
    {
        await _context.BlogPosts.AddAsync(blogPost);
        await _context.SaveChangesAsync();
        return blogPost;
    }

    public async Task AtualizarAsync(BlogPost blogPost)
    {
        _context.BlogPosts.Update(blogPost);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ExisteAsync(Guid id)
    {
        return await _context.BlogPosts.AnyAsync(p => p.Id == id);
    }
}
