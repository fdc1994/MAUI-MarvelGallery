using MarvelGallery.DataModels;
using Refit;

namespace MarvelGallery.Services
{
    public interface IMarvelApiService
    {
        [Get("/v1/public/characters")]
        Task<IMarvelApiResponse<Character>> GetCharacters(
        [Query] string nameStartsWith,
        [Query] int? limit = null,
        [Query] int? offset = null);
    }
}
