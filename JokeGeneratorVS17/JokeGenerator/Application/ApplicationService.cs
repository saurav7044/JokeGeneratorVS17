using System;
using System.Collections.Generic;
using Shared.Http.Client;
using JokeGenerator.ConsolePresentation;

namespace JokeGenerator.Application
{
    /// <summary>
    /// Basically the responsibility of the class is to manage user actions from UI and to redirect commands to lower layers.
    /// </summary>
    internal sealed class ApplicationService
    {
        private IList<string> _categories;
        private readonly IJokeProvider _jokeProvider;
        private readonly IPresentationBehavior _presentationBehavior;

        /// <summary>
        /// This class doesn't depend of any implementation of remote facade it has and UI implementation by interfaces here is using
        /// </summary>
        public ApplicationService(
            IPresentationBehavior presentationBehavior,
            IJokeProvider jokeProvider)
        {
            _presentationBehavior = presentationBehavior ?? throw new ArgumentNullException(nameof(presentationBehavior));
            _jokeProvider = jokeProvider ?? throw new ArgumentNullException(nameof(jokeProvider));
        }

        internal int Run()
        {
            _presentationBehavior.LoadingInformation();
            if (_categories == null)
            {
                _categories = _jokeProvider.GetCategories();
            }

            try
            {
                var key = ConsoleResults.Question;
                if (key == ConsoleResults.Question)
                {
                    var jokeRequestBuilder = new JokeRequestBuilder();
                    // initialize joke counter to 1 to by deafult display atleast 1 joke
                    jokeRequestBuilder.WithJokeCount(1);

                    while (key != ConsoleResults.Q)
                    {
                        key = _presentationBehavior.MakeChoose();
                        switch (key)
                        {
                            case ConsoleResults.C:
                                _presentationBehavior.WriteCategories(_categories);
                                key = _presentationBehavior.WannaSpecifyCategory();
                                if (key == ConsoleResults.Y)
                                {
                                    var category = _presentationBehavior.EnterCategory(_categories);
                                    jokeRequestBuilder.WithCategory(category);
                                }
                                break;

                            case ConsoleResults.R:
                                var keyValuePairs = _jokeProvider.GetNames(1);
                                jokeRequestBuilder.WithName(
                                    keyValuePairs[0].Key, keyValuePairs[0].Value);
                                _presentationBehavior.SetRandomName(keyValuePairs[0].Key, keyValuePairs[0].Value);
                                break;

                            case ConsoleResults.J:
                                var buildRequest = jokeRequestBuilder.BuildRequest();
                                var jokes = _jokeProvider.GetJokes(buildRequest);
                                _presentationBehavior.WriteJokes(jokes);
                                break;

                            case ConsoleResults.A:
                                jokeRequestBuilder.WithJokeCount(_presentationBehavior.HowManyJokes());
                                break;

                            case ConsoleResults.N:
                                jokeRequestBuilder.WithName(
                                    _presentationBehavior.InputName(),
                                    _presentationBehavior.InputLastName());
                                break;

                            case ConsoleResults.Q:
                                break;
                        }
                    }
                }
            }
            catch (ApplicationException applicationException)
            {
                return _presentationBehavior.ExpectedExceptionMessage(applicationException);
            }
            catch (Exception ex)
            {
                return _presentationBehavior.Exception(ex);
            }
            return 0;
        }
    }
}
