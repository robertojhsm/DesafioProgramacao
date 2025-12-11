using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BlogApi.Application.DTOs;
using BlogApi.Application.UseCases;

namespace BlogApi.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PostsController : ControllerBase
{
    private readonly IObterTodosPostsUseCase _obterTodosPostsUseCase;
    private readonly ICriarPostUseCase _criarPostUseCase;
    private readonly IObterPostPorIdUseCase _obterPostPorIdUseCase;

    public PostsController(
        IObterTodosPostsUseCase obterTodosPostsUseCase,
        ICriarPostUseCase criarPostUseCase,
        IObterPostPorIdUseCase obterPostPorIdUseCase)
    {
        _obterTodosPostsUseCase = obterTodosPostsUseCase;
        _criarPostUseCase = criarPostUseCase;
        _obterPostPorIdUseCase = obterPostPorIdUseCase;
    }

    /// <summary>
    /// Obtém todos os posts com título e quantidade de comentários
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<PostResumoDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<PostResumoDto>>> ObterTodos()
    {
        var posts = await _obterTodosPostsUseCase.ExecutarAsync();
        return Ok(posts);
    }

    /// <summary>
    /// Cria um novo post no blog
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(PostDetalheDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PostDetalheDto>> Criar([FromBody] CriarPostDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var post = await _criarPostUseCase.ExecutarAsync(dto);
            return CreatedAtAction(nameof(ObterPorId), new { id = post.Id }, post);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Obtém um post específico por ID com título, conteúdo e lista de comentários
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(PostDetalheDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PostDetalheDto>> ObterPorId(Guid id)
    {
        var post = await _obterPostPorIdUseCase.ExecutarAsync(id);
        
        if (post == null)
            return NotFound(new { message = "Post não encontrado" });

        return Ok(post);
    }
}
