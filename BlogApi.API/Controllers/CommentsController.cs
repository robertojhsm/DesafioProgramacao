using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BlogApi.Application.DTOs;
using BlogApi.Application.UseCases;

namespace BlogApi.API.Controllers;

[ApiController]
[Route("api/posts/{postId}/comments")]
public class CommentsController : ControllerBase
{
    private readonly IAdicionarComentarioUseCase _adicionarComentarioUseCase;

    public CommentsController(IAdicionarComentarioUseCase adicionarComentarioUseCase)
    {
        _adicionarComentarioUseCase = adicionarComentarioUseCase;
    }

    /// <summary>
    /// Adiciona um novo comentário a um post específico
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(ComentarioDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ComentarioDto>> AdicionarComentario(
        Guid postId,
        [FromBody] CriarComentarioDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var comentario = await _adicionarComentarioUseCase.ExecutarAsync(postId, dto);
            return CreatedAtAction(nameof(AdicionarComentario), new { postId, id = comentario.Id }, comentario);
        }
        catch (ArgumentException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
}
