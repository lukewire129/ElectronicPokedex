using MauiReactorTest.Models;
using MauiReactorTest.Models.Dto;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MauiReactorTest.api;

public interface IPokeMonApi
{
    Task<List<PokeMonModel>> GetPocketMons(int idx);
}

public class PokeMonApi : IPokeMonApi
{
    public async Task<List<PokeMonModel>> GetPocketMons(int idx)
    {
        var PokeMons = new List<PokeMonModel> ();
        HttpClient client = new HttpClient ()
        {
            BaseAddress = new System.Uri ("https://pokeapi.co")
        };
        for (int i = 1; i <= 56; i++)
        {
            int index = (56 * idx) + i;
            string retpokemon = await client.GetStringAsync ($"/api/v2/pokemon/{index}");
            string retpokemonName = await client.GetStringAsync ($"/api/v2/pokemon-species/{index}");

            var temp1 = JsonConvert.DeserializeObject<pokemon> (retpokemon);
            var temp2 = JsonConvert.DeserializeObject<pokemonName> (retpokemonName);
            PokeMons.Add (new PokeMonModel ()
            {
                imageUrl = temp1.sprites.front_default,
                Name = temp2.names.FirstOrDefault (x => x.language.name == "ko").name
            });
        }

        return PokeMons;
    }
}
