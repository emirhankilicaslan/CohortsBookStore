using AutoMapper;
using CohortsBookStore.Context;
using CohortsBookStore.DTOs.GenreDtos;
using CohortsBookStore.Entities;
using CohortsBookStore.Services.Abstract;
using CohortsBookStore.Validation.GenreValidator;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace CohortsBookStore.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GenresController : Controller
{
    private readonly IGenreService _genreService;

    public GenresController(IGenreService genreService)
    {
        _genreService = genreService;
    }

    [HttpGet]
    public async Task<IActionResult> GetGenres()
    {
        var genres = await _genreService.GetAllGenres();
        return Ok(genres);

    }

    [HttpGet("{genreId}")]
    public async Task<IActionResult> GetGenresById(int genreId)
    {
        var genre = await _genreService.GetGenreById(genreId);
        return Ok(genre);
    }

    [HttpPost]
    public async Task<IActionResult>  AddGenre(CreateGenreDto createGenreDto)
    {
        await _genreService.CreateGenre(createGenreDto);
        return Ok("Genre created successfully !");
    }

    [HttpPut]
    public async Task<IActionResult>  UpdateGenre(UpdateGenreDto updateGenreDto)
    {
        await _genreService.UpdateGenre(updateGenreDto);
        return Ok("Genre updated successfully !");
    }

    [HttpDelete("{genreId}")]
    public async Task<IActionResult>  DeleteGenre(int genreId)
    {
        await _genreService.DeleteGenre(genreId);
        return Ok("Genre deleted successfully !");
    }
}