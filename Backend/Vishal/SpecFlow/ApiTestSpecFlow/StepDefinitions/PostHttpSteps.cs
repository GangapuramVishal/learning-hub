using ApiTestSpecFlow.Support;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using TechTalk.SpecFlow.Infrastructure;

namespace ApiTestSpecFlow.StepDefinitions
{
    [Binding]
    public class PostHttpSteps
    {
        HttpClient httpClient;
        HttpResponseMessage response;
        string responsebody;
        private readonly ISpecFlowOutputHelper _specFlowOutputHelper;

        public PostHttpSteps(ISpecFlowOutputHelper _specFlowOutputHelper)
        {
            httpClient = new HttpClient();
            this._specFlowOutputHelper = _specFlowOutputHelper;    
        }



        [Given(@"the user sends a post request with url as ""([^""]*)""")]
        public async Task GivenTheUserSendsAPostRequestWithUrlAs(string uri)
        {
            PostData postData = new PostData()
            {
                name = "morpheus",
                job = "leader"
            };

            string data = JsonConvert.SerializeObject(postData);
            var contentdata = new StringContent(data);

            response = await httpClient.PostAsync(uri, contentdata);

            responsebody = await response.Content.ReadAsStringAsync();
            _specFlowOutputHelper.WriteLine("post response is " + responsebody);
        }

        [Then(@"user should get a success response")]
        public void ThenUserShouldGetASuccessResponse()
        {
            Assert.True(response.IsSuccessStatusCode);
        }

    }
}
