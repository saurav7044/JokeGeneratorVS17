using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Shared.Http.Client;
using JokeCompany.Utilities.Http;
using JokeApiClient.Dtos;
using Microsoft.Extensions.Options;

namespace JokeApiClient
{
    /// <summary>
    /// Represent remote facade to communicate with remote server.
    /// </summary>
    public class JokeWebApiClient : IJokeProvider
    {
        private readonly IOptions<JokeGeneratorSettings> _options;

        public JokeWebApiClient(IOptions<JokeGeneratorSettings> options)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
        }

        public virtual IList<string> GetJokes(JokeRequest jokeRequest)
        {
            using (var client = new HttpClient().Build(new Uri(_options.Value.ChucknorrisUrl)))
            {
                var queryString = new QueryBuilder()
                    .AppendToQuery("name", jokeRequest.Name)
                    .AppendToQuery("category", jokeRequest.Category)
                    .Build();
                
                var result = new List<string>(jokeRequest.JokeCount); //Set that capacity like a boss

                for (var i = 0; i < jokeRequest.JokeCount; i++)
                {
                    var jokes = client.GetAsArray<JokeDto>(new Uri($"/jokes/random{queryString}",
                        UriKind.Relative));
                    result.AddRange(jokes.Select(_ => _.value));
                }

                return result;
            }
        }

        public virtual IList<KeyValuePair<string, string>> GetNames(int count)
        {
            if (0 > count)
                throw new ArgumentOutOfRangeException(nameof(count), "Cannot generate a negative number of names");

            using (var httpClient = new HttpClient().Build(new Uri(_options.Value.NamesUrl)))
            {
                var query = new QueryBuilder().AppendToQuery("amount", count.ToString()).Build();
                var response = httpClient.GetAsArray<PersonDto>(query);

                var result = new List<KeyValuePair<string, string>>(count);
                result.AddRange(response.Select(name => new KeyValuePair<string, string>(name.name, name.surname)));
                return result;
            }
        }

        public IList<string> GetCategories()
        {
            using (var client = new HttpClient().Build(new Uri(_options.Value.ChucknorrisUrl)))
            {
                return client.GetAsArray<string>(new Uri($"/jokes/categories", UriKind.Relative));
            }
        }
    }
}