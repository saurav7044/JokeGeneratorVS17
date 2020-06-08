using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace JokeApiClient.Tests
{
    [TestClass]
    public class GetNamesTests
    {
        [DataTestMethod]
        [DataRow(1)]
        public void GetNamesTest(int count)
        {
            var options = GetOptions();
            var provider = new JokeWebApiClient(options);
            var result = provider.GetNames(count);
            Assert.AreEqual(count, result.Count);
        }

        private static IOptions<JokeGeneratorSettings> GetOptions()
        {
            var options = Substitute.For<IOptions<JokeGeneratorSettings>>();
            options.Value.Returns(new JokeGeneratorSettings
            {
                NamesUrl = "https://names.privserv.com/api/",
                ChucknorrisUrl = "https://api.chucknorris.io/"
            });
            return options;
        }
    }
}
