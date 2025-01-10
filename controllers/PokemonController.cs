using Microsoft.AspNetCore.Mvc;
using PokemonApi.Models;
using PokemonApi.Services;

namespace PokemonApi.Controllers
{
    [ApiController]
    [Route("api/pokemon")]
    public class PokemonController : ControllerBase
    {
        private readonly IPokemonService _pokemonService;

        public PokemonController(IPokemonService pokemonService)
        {
            _pokemonService = pokemonService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPokemon()
        {
            try
            {
                var pokemons = await _pokemonService.GetAllPokemons();
                if (pokemons.Count == 0)
                {
                    return NotFound("No Pokemons found.");
                }
                return Ok(pokemons);
            }
            catch (Exception e)
            {
                return StatusCode(500, $"An error occurred: {e.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPokemonById(int id)
        {
            try
            {
                var pokemon = await _pokemonService.GetPokemonById(id);
                if (pokemon == null)
                {
                    return NotFound($"Pokemon with ID {id} not found.");
                }
                return Ok(pokemon);
            }
            catch (Exception e)
            {
                return StatusCode(500, $"An error occurred: {e.Message}");
            }

        }

        [HttpGet("type/{type}")]
        public async Task<IActionResult> GetPokemonByType(string type)
        {
            try
            {
                var pokemons = await _pokemonService.GetPokemonByTypeAsync(type);
                if (pokemons == null || pokemons.Count == 0)
                {
                    return NotFound(new { Message = $"No Pokémon found with type '{type}'." });
                }

                return Ok(pokemons);
            }
            catch (Exception e)
            {
                return StatusCode(500, $"An error occurred: {e.Message}");
            }

        }


        [HttpPost]
        public async Task<IActionResult> AddPokemon([FromBody] Pokemon pokemon)
        {
            try
            {
                if (pokemon == null)
                {
                    return BadRequest("Pokemon data is null.");
                }
                await _pokemonService.AddPokemon(pokemon);
                return CreatedAtAction(nameof(GetPokemonById), new { id = pokemon.Id }, pokemon);
            }
            catch (Exception e)
            {
                return StatusCode(500, $"An error occurred: {e.Message}");

            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePokemon(int id, [FromBody] UpdatePokemonRequest request)
        {
            try
            {

                if (request.level < 1)
                {
                    return BadRequest("Level must be a positive number.");
                }
                await _pokemonService.UpdatePokemon(id, request);
                return Ok();

            }
            catch (Exception e)
            {
                return StatusCode(500, $"An error occurred: {e.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePokemon(int id)
        {
            try
            {
                await _pokemonService.DeletePokemon(id);
                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode(500, $"An error occurred: {e.Message}");
            }
        }
    }
}
