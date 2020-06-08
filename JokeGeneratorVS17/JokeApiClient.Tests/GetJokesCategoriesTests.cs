using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace JokeApiClient.Tests
{
    [TestClass]
    public class GetJokesCategoriesTests
    {
        [TestMethod]
        public void GetAllCategoriesTest()
        {
            var options = Substitute.For<IOptions<JokeGeneratorSettings>>();
            options.Value.Returns(new JokeGeneratorSettings
            {
                NamesUrl = "https://api.chucknorris.io",
                ChucknorrisUrl = "https://api.chucknorris.io"
            });
            var categoryProvider = new JokeWebApiClient(options);
            var result = categoryProvider.GetCategories();

            Assert.IsNotNull(result);
            Assert.AreNotEqual(0, result.Count);
        }
    }
}