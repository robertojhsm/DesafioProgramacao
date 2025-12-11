using System;

namespace BlogApi.Domain.Entities;

public class Comment
{
    public Guid Id { get; private set; }
    public Guid BlogPostId { get; private set; }
    public string Conteudo { get; private set; }
    public string Autor { get; private set; }
    public DateTime DataCriacao { get; private set; }

    public BlogPost BlogPost { get; private set; }

    private Comment() { }

    public Comment(Guid blogPostId, string conteudo, string autor)
    {
        Id = Guid.NewGuid();
        BlogPostId = blogPostId;
        Conteudo = conteudo;
        Autor = autor;
        DataCriacao = DateTime.UtcNow;
    }
}
