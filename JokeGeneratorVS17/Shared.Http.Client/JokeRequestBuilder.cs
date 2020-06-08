namespace Shared.Http.Client
{
    public sealed class JokeRequestBuilder
    {
        private string _firstName;
        private string _lastName;
        private string _category;
        private int _jokesCount;

        public JokeRequestBuilder WithName(string firstName, string lastName)
        {
            _firstName = firstName;
            _lastName = lastName;
            return this;
        }

        public JokeRequestBuilder WithCategory(string category)
        {
            _category = category;
            return this;
        }

        public JokeRequestBuilder WithJokeCount(int count)
        {
            _jokesCount = count;
            return this;
        }

        public JokeRequest BuildRequest()
        {
            return new JokeRequest(_firstName, _lastName, _category, _jokesCount);
        }
    }
}