using System.ComponentModel.DataAnnotations;

namespace BlogApi.Application.DTOs;

public class CriarComentarioDto
{
    [Required(ErrorMessage = "Conteúdo é obrigatório")]
    [StringLength(500, ErrorMessage = "Conteúdo não pode exceder 500 caracteres")]
    public string Conteudo { get; set; } = string.Empty;

    [Required(ErrorMessage = "Autor é obrigatório")]
    [StringLength(100, ErrorMessage = "Autor não pode exceder 100 caracteres")]
    public string Autor { get; set; } = string.Empty;
}
