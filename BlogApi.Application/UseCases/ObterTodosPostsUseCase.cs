using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogApi.Application.DTOs;
using BlogApi.Domain.Entities;
using BlogApi.Domain.Repositories;

namespace BlogApi.Application.UseCases;

public interface IObterTodosPostsUseCase
{
    Task<IEnumerable<PostResumoDto>> ExecutarAsync();
}

public class ObterTodosPostsUseCase : IObterTodosPostsUseCase
{
    private readonly IBlogPostRepository _blogPostRepository;

    public ObterTodosPostsUseCase(IBlogPostRepository blogPostRepository)
    {
        _blogPostRepository = blogPostRepository;
    }

    public async Task<IEnumerable<PostResumoDto>> ExecutarAsync()
    {
        var posts = await _blogPostRepository.ObterTodosAsync();

        return posts.Select(post => new PostResumoDto
        {
            Id = post.Id,
            Titulo = post.Titulo,
            QuantidadeComentarios = post.ObterQuantidadeComentarios()
        });
    }
}
