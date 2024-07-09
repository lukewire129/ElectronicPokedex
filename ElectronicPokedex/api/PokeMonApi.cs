using ElectronicPokedex.Models;
using ElectronicPokedex.Models.Dto;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ElectronicPokedex.api;

public interface IPokeMonApi
{
    Task<List<PokeMonModel>> GetPocketMons(int idx);
    Task<List<PokeMonModel>> GetPocketMons();
}

public class PokeMonApi : IPokeMonApi
{
    int _index = 19;
    public async Task<List<PokeMonModel>> GetPocketMons(int idx)
    {
        var PokeMons = new List<PokeMonModel> ();
        HttpClient client = new HttpClient ()
        {
            BaseAddress = new System.Uri ("https://pokeapi.co")
        };
        for (int i = 1; i <= _index; i++)
        {
            int index = (_index * idx) + i;
            string retpokemon = await client.GetStringAsync ($"/api/v2/pokemon/{index}");
            string retpokemonName = await client.GetStringAsync ($"/api/v2/pokemon-species/{index}");

            var temp1 = JsonConvert.DeserializeObject<pokemon> (retpokemon);
            var temp2 = JsonConvert.DeserializeObject<pokemonName> (retpokemonName);
            PokeMons.Add (new PokeMonModel ()
            {
                idx = index,
                imageUrl = temp1.sprites.versions.generationv.blackwhite.animated.front_default,
                Name = temp2.names.FirstOrDefault (x => x.language.name == "ko").name
            });
        }

        return PokeMons;
    }
    public async Task<List<PokeMonModel>> GetPocketMons()
    {
        var PokeMons = new List<PokeMonModel> ();
        HttpClient client = new HttpClient ()
        {
            BaseAddress = new System.Uri ("https://pokeapi.co")
        };
        for (int i = 1; i <= 30; i++)
        {
            string retpokemon = await client.GetStringAsync ($"/api/v2/pokemon/{i}");
            string retpokemonName = await client.GetStringAsync ($"/api/v2/pokemon-species/{i}");
            if (retpokemon == null || retpokemonName == null)
                return PokeMons;
            var temp1 = JsonConvert.DeserializeObject<pokemon> (retpokemon);
            var temp2 = JsonConvert.DeserializeObject<pokemonName> (retpokemonName);
            PokeMons.Add (new PokeMonModel ()
            {
                idx = i,
                imageUrl = temp1.sprites.versions.generationv.blackwhite.animated.front_default,
                Name = temp2.names.FirstOrDefault (x => x.language.name == "ko").name
            });
        }

        return PokeMons;
    }
}
