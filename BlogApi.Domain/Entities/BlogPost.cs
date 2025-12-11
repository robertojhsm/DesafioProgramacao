using System;
using System.Collections.Generic;
using System.Linq;

namespace BlogApi.Domain.Entities;

public class BlogPost
{
    public Guid Id { get; private set; }
    public string Titulo { get; private set; }
    public string Conteudo { get; private set; }
    public DateTime DataCriacao { get; private set; }
    public DateTime? DataAtualizacao { get; private set; }
    
    private readonly List<Comment> _comentarios = new();
    public IReadOnlyCollection<Comment> Comentarios => _comentarios.AsReadOnly();

    private BlogPost() { }

    public BlogPost(string titulo, string conteudo)
    {
        Id = Guid.NewGuid();
        Titulo = titulo;
        Conteudo = conteudo;
        DataCriacao = DateTime.UtcNow;
    }

    public void Atualizar(string titulo, string conteudo)
    {
        Titulo = titulo;
        Conteudo = conteudo;
        DataAtualizacao = DateTime.UtcNow;
    }

    public void AdicionarComentario(Comment comentario)
    {
        if (comentario == null)
            throw new ArgumentNullException(nameof(comentario));

        _comentarios.Add(comentario);
    }

    public int ObterQuantidadeComentarios()
    {
        return _comentarios.Count;
    }
}
