
using NUnit.Framework;
using RestSharp;
using TechTalk.SpecFlow;
using UnitTest.Base;
using UnitTest.Models;

namespace UnitTest.Steps
{
    [Binding]
    public class GetPosts
    {
        // _settings class is used for context injection
        private settings _settings;
        public GetPosts(settings settings1)
        {
            _settings = settings1;
        }
      //  public RestClient client = new RestClient("http://localhost:3000/");
        //accessing through _settings
        //public IRestResponse<Posts> response = new RestResponse<Posts>();


        [Given(@"Perform get operation for ""(.*)""")]
        public void GivenPerformGetOperationFor(string url)
        {
            _settings.Request = new RestRequest(url, Method.GET);
        }

        [When(@"perform operation for post ""(.*)""")]
        public void GivenPerformOperationForPost(int id)
        {
            _settings.Request.AddUrlSegment("postid", id.ToString());

        }

        [Then(@"I should see ""(.*)"" name as ""(.*)""")]
        public void ThenIShouldSeeNameAs(string p0, string p1)
        {
            _settings.Response = _settings.RestClient.Execute<Posts>(_settings.Request);
            var author = p0;
            Assert.That(_settings.Response.Data.author, Is.EqualTo(p1), "NotCorrect");
        }

    }
}
