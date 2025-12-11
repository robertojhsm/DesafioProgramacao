using System;
using System.Linq;
using System.Threading.Tasks;
using BlogApi.Application.DTOs;
using BlogApi.Domain.Entities;
using BlogApi.Domain.Repositories;

namespace BlogApi.Application.UseCases;

public interface ICriarPostUseCase
{
    Task<PostDetalheDto> ExecutarAsync(CriarPostDto dto);
}

public class CriarPostUseCase : ICriarPostUseCase
{
    private readonly IBlogPostRepository _blogPostRepository;

    public CriarPostUseCase(IBlogPostRepository blogPostRepository)
    {
        _blogPostRepository = blogPostRepository;
    }

    public async Task<PostDetalheDto> ExecutarAsync(CriarPostDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Titulo))
            throw new ArgumentException("Título é obrigatório", nameof(dto));

        if (string.IsNullOrWhiteSpace(dto.Conteudo))
            throw new ArgumentException("Conteúdo é obrigatório", nameof(dto));

        var post = new BlogPost(dto.Titulo, dto.Conteudo);
        await _blogPostRepository.AdicionarAsync(post);

        return new PostDetalheDto
        {
            Id = post.Id,
            Titulo = post.Titulo,
            Conteudo = post.Conteudo,
            DataCriacao = post.DataCriacao,
            DataAtualizacao = post.DataAtualizacao,
            Comentarios = new List<ComentarioDto>()
        };
    }
}
