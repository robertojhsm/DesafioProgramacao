using System;
using System.Linq;
using System.Threading.Tasks;
using BlogApi.Application.DTOs;
using BlogApi.Domain.Repositories;

namespace BlogApi.Application.UseCases;

public interface IObterPostPorIdUseCase
{
    Task<PostDetalheDto?> ExecutarAsync(Guid id);
}

public class ObterPostPorIdUseCase : IObterPostPorIdUseCase
{
    private readonly IBlogPostRepository _blogPostRepository;

    public ObterPostPorIdUseCase(IBlogPostRepository blogPostRepository)
    {
        _blogPostRepository = blogPostRepository;
    }

    public async Task<PostDetalheDto?> ExecutarAsync(Guid id)
    {
        var post = await _blogPostRepository.ObterPorIdAsync(id);

        if (post == null)
            return null;

        return new PostDetalheDto
        {
            Id = post.Id,
            Titulo = post.Titulo,
            Conteudo = post.Conteudo,
            DataCriacao = post.DataCriacao,
            DataAtualizacao = post.DataAtualizacao,
            Comentarios = post.Comentarios.Select(c => new ComentarioDto
            {
                Id = c.Id,
                Conteudo = c.Conteudo,
                Autor = c.Autor,
                DataCriacao = c.DataCriacao
            }).ToList()
        };
    }
}
