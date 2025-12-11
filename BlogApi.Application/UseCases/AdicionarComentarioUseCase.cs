using System;
using System.Threading.Tasks;
using BlogApi.Application.DTOs;
using BlogApi.Domain.Entities;
using BlogApi.Domain.Repositories;

namespace BlogApi.Application.UseCases;

public interface IAdicionarComentarioUseCase
{
    Task<ComentarioDto> ExecutarAsync(Guid postId, CriarComentarioDto dto);
}

public class AdicionarComentarioUseCase : IAdicionarComentarioUseCase
{
    private readonly IBlogPostRepository _blogPostRepository;
    private readonly ICommentRepository _commentRepository;

    public AdicionarComentarioUseCase(
        IBlogPostRepository blogPostRepository,
        ICommentRepository commentRepository)
    {
        _blogPostRepository = blogPostRepository;
        _commentRepository = commentRepository;
    }

    public async Task<ComentarioDto> ExecutarAsync(Guid postId, CriarComentarioDto dto)
    {
        var postExiste = await _blogPostRepository.ExisteAsync(postId);
        if (!postExiste)
            throw new ArgumentException("Post não encontrado", nameof(postId));

        var comentario = new Comment(postId, dto.Conteudo, dto.Autor);
        await _commentRepository.AdicionarAsync(comentario);

        return new ComentarioDto
        {
            Id = comentario.Id,
            Conteudo = comentario.Conteudo,
            Autor = comentario.Autor,
            DataCriacao = comentario.DataCriacao
        };
    }
}
