using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using UnitTest.Base;
using UnitTest.Models;

namespace UnitTest.Steps
{
    [Binding]
    public class PostProfile
    {
        private settings _settings;
        public PostProfile(settings settings1)
        {
            _settings = settings1;
        }

        [Given(@"Perform Post operation for ""(.*)"" with body")]
        public void GivenPerformPostOperationForWithBody(string url, Table table)
        {
            dynamic data = table.CreateDynamicInstance();
            _settings.Request = new RestRequest(url, Method.POST);
            _settings.Request.AddJsonBody (new {  id = data.id.ToString() });
          //  _settings.Request.AddUrlSegment("id", ((int)data.postid).ToString());

            _settings.Response = _settings.RestClient.Execute<Posts>(_settings.Request);
        }

    }
}
