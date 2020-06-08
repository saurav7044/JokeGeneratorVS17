using System;

namespace Shared.Http.Client
{
    /// <summary>
    ///     Immutable data structure to represent the request for one or more Joke(s);
    /// </summary>
    [Serializable]
    public sealed class JokeRequest
    {
        public JokeRequest(int jokeCount)
        {
            if (0 > jokeCount)
                throw new ArgumentOutOfRangeException(nameof(jokeCount));
            
            JokeCount = jokeCount;
        }

        public JokeRequest(string category, int jokeCount)
            : this(jokeCount)
        {
            Category = category;
        }

        public JokeRequest(string firstName, string lastName, string category, int jokeCount)
            : this(category, jokeCount)
        {
            Name = string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(lastName)
                ? lastName
                : string.IsNullOrEmpty(lastName) && !string.IsNullOrEmpty(firstName)
                    ? firstName
                    : $"{firstName} {lastName}";
        }

        public string Name { get; private set; }
        public int JokeCount { get; private set; }
        public string Category { get; private set; }
    }
}