using System;

namespace JokeCompany.Utilities.Http
{
    public sealed class QueryBuilder
    {
        private readonly UriBuilder _uriBuilder;

        public QueryBuilder()
        {
            _uriBuilder = new UriBuilder();
        }

        public QueryBuilder AppendToQuery(string key, string value)
        {
            if (string.IsNullOrWhiteSpace(key) || string.IsNullOrWhiteSpace(value))
                return this;

            var queryToAppend = $"{key}={value}";
            if (_uriBuilder.Query != null && _uriBuilder.Query.Length > 1)
                _uriBuilder.Query = _uriBuilder.Query.Substring(1) + "&" + queryToAppend;
            else
                _uriBuilder.Query = queryToAppend;

            return this;
        }

        public string Build()
        {
            return _uriBuilder.Query;
        }
    }
}