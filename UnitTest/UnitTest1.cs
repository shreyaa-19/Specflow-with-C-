using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using RestSharp.Serialization.Json;
using UnitTest.Models;

namespace UnitTest
{
    [TestClass]   // Earlier [TestClass] , made it [TestFixture] as we are using Nunit
    public class UnitTest1
    {
        [TestMethod]    // Earlier [TestMethod] , made it [Test] as we are using Nunit
        public void TestMethod1()
        {
            // Method 1
            var client = new RestClient("http://localhost:3000/");
            var request = new RestRequest("posts/{postid}", Method.GET);
            request.AddUrlSegment("postid", 1);
            var content = client.Execute(request).Content;

            //Method-2(Deserialization)
            var response = client.Execute(request);
            var desirialize = new JsonDeserializer();
            var output = desirialize.Deserialize<Dictionary<string, string>>(response);
            var result = output["author"];
            NUnit.Framework.Assert.That(result, Is.EqualTo("Karthik KK"), "Author is not correct"); // Install Nunit 

            // Method-3 (Jobject)
            JObject ob = JObject.Parse(response.Content);   // Install Newtonsoft.json
            NUnit.Framework.Assert.That(ob["author"].ToString(), Is.EqualTo("Karthik KK"), "Author is not correct"); // Install Nunit 

        }
        [TestMethod]
        public void PostWithAnoymousBody()
        {
            var client = new RestClient("http://localhost:3000/");
            var request = new RestRequest("posts/{postid}/comments", Method.POST);

            //   request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(new { body = "Raj" });
            request.AddUrlSegment("postid", 4);

            var response = client.Execute(request);
            var deserialize = new JsonDeserializer();
            var output = deserialize.Deserialize<Dictionary<string, string>>(response);
            var result = output["body"];

            NUnit.Framework.Assert.That(result, Is.EqualTo("Raj"), "Not correct");
        }

        [TestMethod]
        public void PostWithTypeClassBody()
        {
            var client = new RestClient("http://localhost:3000/");
            var request = new RestRequest("posts", Method.POST);
            request.AddJsonBody(new Posts() { id = "15", title = "hARRY", author = "JKRowling" });

            var response = client.Execute(request);
            var deserialize = new JsonDeserializer();
            var output = deserialize.Deserialize<Dictionary<string, string>>(response);
            var result = output["author"];

            NUnit.Framework.Assert.That(result, Is.EqualTo("JKRowling"), "Not correct");
        }

        [TestMethod]
        public void PostWithTypeClassBody2()
        {
            var client = new RestClient("http://localhost:3000/");
            var request = new RestRequest("posts", Method.POST);
            request.AddJsonBody(new Posts() { id = "14", title = "hARRY potter", author = "JKRowling" });

            var response = client.Execute<Posts>(request);  //<Posts> deserialises the body

            NUnit.Framework.Assert.That(response.Data.author, Is.EqualTo("JKRowling"), "Not correct");
        }

        [TestMethod]
        public void PostWithAsync()
        {
            var client = new RestClient("http://localhost:3000/");
            var request = new RestRequest("posts", Method.POST);
            request.AddJsonBody(new Posts() { id = "21", title = "hARRY123 potter", author = "JKRowling" });

            var response = ExecuteAsyncRequest<Posts>(client,request).GetAwaiter().GetResult();  //<Posts> deserialises the body

            NUnit.Framework.Assert.That(response.Data.author, Is.EqualTo("JKRowling"), "Not correct");
        }

        private async Task<IRestResponse<T>> ExecuteAsyncRequest<T>(RestClient client, IRestRequest request) where T : class, new()
        {
            var taskCompletionSource = new TaskCompletionSource<IRestResponse<T>>();
#pragma warning disable CS0618 // Type or member is obsolete
            client.ExecuteAsync<T>(request, restResponse =>
            {
                if (restResponse.ErrorException != null)
                {
                    const string message = "Error receiving response";
                    throw new ApplicationException(message, restResponse.ErrorException);
                }
                taskCompletionSource.SetResult(restResponse);
            });
#pragma warning restore CS0618 // Type or member is obsolete
            return await taskCompletionSource.Task;
        }
    }
}
