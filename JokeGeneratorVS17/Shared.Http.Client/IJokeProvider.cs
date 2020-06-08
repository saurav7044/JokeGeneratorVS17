using System.Collections.Generic;

namespace Shared.Http.Client
{
    public interface IJokeProvider
    {
        IList<string> GetJokes(JokeRequest jokeRequest);

        IList<KeyValuePair<string, string>> GetNames(int count);

        IList<string> GetCategories();
    }
}