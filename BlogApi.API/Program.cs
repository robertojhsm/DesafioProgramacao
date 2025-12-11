using Microsoft.EntityFrameworkCore;
using BlogApi.Infrastructure.Persistence;
using BlogApi.Domain.Repositories;
using BlogApi.Infrastructure.Repositories;
using BlogApi.Application.UseCases;
using BlogApi.API.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { 
        Title = "Blog API", 
        Version = "v1",
        Description = "API RESTful para gerenciamento de posts de blog e comentários"
    });
    
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        c.IncludeXmlComments(xmlPath);
    }
});

builder.Services.AddDbContext<BlogDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection") 
        ?? "Data Source=blog.db"));

builder.Services.AddScoped<IBlogPostRepository, BlogPostRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<IObterTodosPostsUseCase, ObterTodosPostsUseCase>();
builder.Services.AddScoped<ICriarPostUseCase, CriarPostUseCase>();
builder.Services.AddScoped<IObterPostPorIdUseCase, ObterPostPorIdUseCase>();
builder.Services.AddScoped<IAdicionarComentarioUseCase, AdicionarComentarioUseCase>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Blog API v1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseMiddleware<ErrorHandlingMiddleware>();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<BlogDbContext>();
    dbContext.Database.EnsureCreated();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
