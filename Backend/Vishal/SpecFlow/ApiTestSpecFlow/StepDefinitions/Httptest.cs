using ApiTestSpecFlow.Support;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TechTalk.SpecFlow.Infrastructure;

namespace ApiTestSpecFlow.StepDefinitions
{
    [Binding]
    public class Httptest
    {
        HttpClient httpClient;
        HttpResponseMessage response;
        string responseBody;
        private readonly ISpecFlowOutputHelper _specFlowOutputHelper;

        public Httptest(ISpecFlowOutputHelper _specFlowOutputHelper)
        {
            httpClient = new HttpClient();
            this._specFlowOutputHelper = _specFlowOutputHelper;
        }
        [Given(@"The user send a get result with url as ""([^""]*)""")]
        public async Task GivenTheUserSendAGetResultWithUrlAs(string uri)
        {
            response = await httpClient.GetAsync(uri);
            response.EnsureSuccessStatusCode();
            responseBody = await response.Content.ReadAsStringAsync();
            _specFlowOutputHelper.WriteLine(responseBody);

            var desData = JsonConvert.DeserializeObject<DataModel>(responseBody);
            _specFlowOutputHelper.WriteLine("After deserialization value is " + desData.page.ToString());
            foreach(var item in desData.data)
            {
                _specFlowOutputHelper.WriteLine(item.id.ToString());
            }




        }

        [Then(@"request should be a success with (.*) Statuscode")]
        public void ThenRequestShouldBeASuccessWithStatuscode(int p0)
        {
            Assert.True(response.IsSuccessStatusCode);
        }

    }
}
