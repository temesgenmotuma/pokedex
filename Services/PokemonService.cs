using MongoDB.Driver;
using PokemonApi.Data;
using PokemonApi.Models;

namespace PokemonApi.Services
{
    public class PokemonService : IPokemonService
    {
        private readonly IMongoCollection<Pokemon> _pokemonCollection;

        public PokemonService(MongoDbContext dbContext)
        {
            _pokemonCollection = dbContext.Pokemons;
        }

        public async Task<List<Pokemon>> GetAllPokemons()
        {
            return await _pokemonCollection.Find(_ => true).ToListAsync();
        }

        public async Task<Pokemon> GetPokemonById(int id)
        {
            return await _pokemonCollection.Find(p => p.Id == id).FirstOrDefaultAsync();
        }

          public async Task<List<Pokemon>> GetPokemonByTypeAsync(string type)
        {
            var filter = Builders<Pokemon>.Filter.Eq(p => p.Type, type);
            return await _pokemonCollection.Find(filter).ToListAsync();
        }

        public async Task AddPokemon(Pokemon pokemon)
        {
            await _pokemonCollection.InsertOneAsync(pokemon);
        }

        public async Task UpdatePokemon(int id, UpdatePokemonRequest request)
        {
            var update = Builders<Pokemon>.Update.Set(p => p.Level, request.level);
            await _pokemonCollection.UpdateOneAsync(p => p.Id == id, update);
        }

        public async Task DeletePokemon(int id)
        {
            await _pokemonCollection.DeleteOneAsync(p => p.Id == id);
        }
    }
}
