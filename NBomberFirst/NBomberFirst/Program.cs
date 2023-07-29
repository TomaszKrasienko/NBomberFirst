using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NBomberFirst.DTOs;
using NBomberFirst.Entities;
using NBomberFirst.Persistance;
using NBomberFirst.Persistance.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IMovieRepository, MovieRepository>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddDbContext<MoviesDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString"));
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/getById/{movieId}", async ([FromRoute]Guid movieId, IMovieRepository movieRepository) =>
{
    var movie = await movieRepository.GetByIdAsync(movieId);
    return movie is not null ? Results.Ok(movie) : Results.NotFound();
});

app.MapPost("/add", async ([FromBody]MovieDto movieDto, IMovieRepository movieRepository, IMapper mapper) =>
{
    Movie movie = mapper.Map<Movie>(movieDto);
    await movieRepository.AddAsync(movie);
    return Results.Ok();
});

app.Run();


