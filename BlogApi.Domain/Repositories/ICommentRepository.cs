using System;
using System.Threading.Tasks;
using BlogApi.Domain.Entities;

namespace BlogApi.Domain.Repositories;

public interface ICommentRepository
{
    Task<Comment> AdicionarAsync(Comment comment);
}
