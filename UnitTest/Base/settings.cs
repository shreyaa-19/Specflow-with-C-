using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTest.Models;

namespace UnitTest.Base
{
    public class settings
    {
        public Uri BaseUrl { get; set; }
        public RestRequest Request { get; set; }

        public IRestResponse<Posts> Response { get; set; }

        public RestClient RestClient { get; set; } = new RestClient();
    }
    
}
