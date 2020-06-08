using Shared.Http.Client;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace JokeApiClient.Tests
{
    [TestClass]
    public class GetJokesTests
    {
        private static IOptions<JokeGeneratorSettings> GetOptions()
        {
            var options = Substitute.For<IOptions<JokeGeneratorSettings>>();
            options.Value.Returns(new JokeGeneratorSettings
            {
                NamesUrl = "https://api.chucknorris.io",
                ChucknorrisUrl = "https://api.chucknorris.io"
            });
            return options;
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(5)]
        public void GetJustCategoryDefaultNameJokesTest(int count)
        {
            var options = GetOptions();
            var jokeProvider = new JokeWebApiClient(options);
            var jr = new JokeRequest("animal", count);
            var results = jokeProvider.GetJokes(jr);
            Assert.AreEqual(count, results.Count);
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(5)]
        public void GetAnyRandomJokesTest(int count)
        {
            var options = GetOptions();
            var jokeProvider = new JokeWebApiClient(options);
            var jr = new JokeRequest(count);
            var results = jokeProvider.GetJokes(jr);
            Assert.AreEqual(count, results.Count);
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(5)]
        public void GetJokesWithFirstNameOnlyTest(int count)
        {
            var options = GetOptions();
            var jokeProvider = new JokeWebApiClient(options);
            var jr = new JokeRequest("Thomas", null, "movie", count);
            var results = jokeProvider.GetJokes(jr);
            Assert.AreEqual(count, results.Count);
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(5)]
        public void GetJokesWithLastNameOnlyTest(int count)
        {
            var options = GetOptions();
            var jokeProvider = new JokeWebApiClient(options);
            var jr = new JokeRequest(null, "Crane", "food", count);
            var results = jokeProvider.GetJokes(jr);
            Assert.AreEqual(count, results.Count);
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(5)]
        public void GetBothNamesJokesTest(int count)
        {
            var options = GetOptions();
            var jokeProvider = new JokeWebApiClient(options);
            var jr = new JokeRequest("Thomas", "Crane", "food", count);
            var results = jokeProvider.GetJokes(jr);
            Assert.AreEqual(count, results.Count);
        }
    }
}