using System;
using System.Collections.Generic;

namespace BlogApi.Application.DTOs;

public class PostDetalheDto
{
    public Guid Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string Conteudo { get; set; } = string.Empty;
    public DateTime DataCriacao { get; set; }
    public DateTime? DataAtualizacao { get; set; }
    public List<ComentarioDto> Comentarios { get; set; } = new();
}
