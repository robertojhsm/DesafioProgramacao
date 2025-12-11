using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BlogApi.Domain.Entities;

namespace BlogApi.Domain.Repositories;

public interface IBlogPostRepository
{
    Task<IEnumerable<BlogPost>> ObterTodosAsync();
    Task<BlogPost?> ObterPorIdAsync(Guid id);
    Task<BlogPost> AdicionarAsync(BlogPost blogPost);
    Task AtualizarAsync(BlogPost blogPost);
    Task<bool> ExisteAsync(Guid id);
}
