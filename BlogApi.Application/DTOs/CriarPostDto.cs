using System.ComponentModel.DataAnnotations;

namespace BlogApi.Application.DTOs;

public class CriarPostDto
{
    [Required(ErrorMessage = "Título é obrigatório")]
    [StringLength(200, ErrorMessage = "Título não pode exceder 200 caracteres")]
    public string Titulo { get; set; } = string.Empty;

    [Required(ErrorMessage = "Conteúdo é obrigatório")]
    public string Conteudo { get; set; } = string.Empty;
}
