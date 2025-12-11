using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BlogApi.Domain.Entities;
using BlogApi.Domain.Repositories;
using BlogApi.Infrastructure.Persistence;

namespace BlogApi.Infrastructure.Repositories;

public class CommentRepository : ICommentRepository
{
    private readonly BlogDbContext _context;

    public CommentRepository(BlogDbContext context)
    {
        _context = context;
    }

    public async Task<Comment> AdicionarAsync(Comment comment)
    {
        await _context.Comments.AddAsync(comment);
        await _context.SaveChangesAsync();
        return comment;
    }
}
