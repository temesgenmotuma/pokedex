using Microsoft.AspNetCore.Mvc;
using PokemonApi.Models;
using PokemonApi.Repositories;

namespace PokemonApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PokemonController : ControllerBase
    {
        // POST: api/Pokemon
        [HttpPost]
        public IActionResult AddPokemon([FromBody] Pokemon pokemon)
        {
            if (pokemon == null)
            {
                return BadRequest("Pokemon data is null.");
            }

            // Add the Pokemon to the in-memory repository
            PokemonRepository.Pokemons.Add(pokemon);

            return CreatedAtAction(nameof(AddPokemon), new { id = pokemon.Id }, pokemon);
        }

        [HttpGet]
        public IActionResult GetPokemon()
        {
            var pokemons = PokemonRepository.Pokemons;

            if (pokemons.Count == 0)
            {
                return NotFound("No Pokemons found.");
            }

            return Ok(pokemons);
        }

        [HttpGet("{id}")]
        public IActionResult GetPokemonById(int id)
        {
            var pokemon = PokemonRepository.Pokemons.FirstOrDefault(p => p.Id == id);

            if (pokemon == null)
            {
                return NotFound($"Pokemon with ID {id} not found.");
            }

            return Ok(pokemon);
        }

        // PUT: api/Pokemon/{id}
        [HttpPut("{id}")]
        public IActionResult UpdatePokemon(int id, [FromBody] UpdatePokemonRequest request)
        {
            // Find the Pokemon by ID
            var pokemon = PokemonRepository.Pokemons.FirstOrDefault(p => p.Id == id);

            if (pokemon == null)
            {
                return NotFound($"Pokemon with ID {id} not found.");
            }

            if (request.level < 1)
            {
                return BadRequest("Level must be a positive number.");
            }

            // Update the Level field
            pokemon.Level = request.level;

            return Ok(pokemon); // Return the updated Pokemon
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePokemon(int id)
        {
            // Find the Pokemon by ID
            var pokemon = PokemonRepository.Pokemons.FirstOrDefault(p => p.Id == id);

            if (pokemon == null)
            {
                return NotFound($"Pokemon with ID {id} not found.");
            }

            // Remove the Pokemon from the list
            PokemonRepository.Pokemons.Remove(pokemon);

            return NoContent(); 
        }

        /* [HttpPut("{id}")]
        public IActionResult UpdatePokemon(int id, [FromBody] int newLevel)
        {
            // Find the Pokemon by ID
            var pokemon = PokemonRepository.Pokemons.FirstOrDefault(p => p.Id == id);

            if (pokemon == null)
            {
                return NotFound($"Pokemon with ID {id} not found.");
            }

            if (newLevel < 1)
            {
                return BadRequest("Level must be a positive number.");
            }

            // Update the Level field
            pokemon.Level = newLevel;

            return Ok(pokemon); // Return the updated Pokemon
        } */


    }
}
