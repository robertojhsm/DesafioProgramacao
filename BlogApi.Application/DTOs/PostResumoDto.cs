using System;
using System.Collections.Generic;

namespace BlogApi.Application.DTOs;

public class PostResumoDto
{
    public Guid Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public int QuantidadeComentarios { get; set; }
}
