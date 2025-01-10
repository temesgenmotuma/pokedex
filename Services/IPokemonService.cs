using PokemonApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PokemonApi.Services
{
    public interface IPokemonService
    {
        Task<List<Pokemon>> GetAllPokemons();
        Task<Pokemon> GetPokemonById(int id);
        Task<List<Pokemon>> GetPokemonByTypeAsync(string type);
        Task AddPokemon(Pokemon pokemon);
        Task UpdatePokemon(int id, UpdatePokemonRequest request);
        Task DeletePokemon(int id);
    }
}
