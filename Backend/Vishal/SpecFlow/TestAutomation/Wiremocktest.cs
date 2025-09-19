using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;

namespace TestAutomation
{
    public class Wiremocktest
    {
        static void Main(string[] args)
        {
            WireMockServer server = WireMockServer.Start();
            Console.WriteLine(server.Url);

            //server.Given(Request.Create().WithPath("/home/test").UsingGet()).
            //    RespondWith(Response.Create().WithStatusCode(200).WithHeader("Content-type", "text/plain").
            //    WithBody("Hello everyone"));

            server.Given(Request.Create().WithPath("/home/test").UsingGet()).
                RespondWith(Response.Create().WithStatusCode(200).WithHeader("Content-type", "application/json").
                WithBodyAsJson(new
                {
                    branch = "engineering", designation = "developer"
                }));

            Thread.Sleep(20000);

            server.Stop();
        }
    }
}
