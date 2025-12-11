using System;

namespace BlogApi.Application.DTOs;

public class ComentarioDto
{
    public Guid Id { get; set; }
    public string Conteudo { get; set; } = string.Empty;
    public string Autor { get; set; } = string.Empty;
    public DateTime DataCriacao { get; set; }
}
