using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using UnitTest.Base;

namespace UnitTest.Hooks
{
    [Binding]
    public class TestInitialize
    {
        private settings _settings;
            public TestInitialize(settings settings1)
            {
                _settings = settings1;
            }

        [BeforeScenario]
        public void Testsetup()
        {
            // _settings.BaseUrl = new Uri("http://localhost:3000/");
            _settings.BaseUrl = new Uri(ConfigurationManager.AppSettings["baseUrl"].ToString());
            _settings.RestClient.BaseUrl = _settings.BaseUrl;
        }


    }
}
