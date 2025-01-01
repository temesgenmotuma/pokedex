using PokemonApi.Models;
using System.Collections.Generic;

namespace PokemonApi.Repositories
{
    public static class PokemonRepository
    {
        public static List<Pokemon> Pokemons { get; } = new List<Pokemon>();
    }
}
