//using Newtonsoft.Json;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net.Http.Headers;
//using System.Text;
//using System.Threading.Tasks;

//namespace TestAutomation
//{
//    public class Datasend
//    {
//        public int id { get; set; }
//        public int quantity { get; set; }
//        public double price { get; set; }
//    }

//    public class PostMethod
//    {
//        static async Task Main(string[] args)
//        {
//            HttpClient client = new HttpClient();
//            HttpRequestMessage request;
//            HttpResponseMessage response;
//            string responseBody;

//            client = new HttpClient();

//            //request payload
//            request = new HttpRequestMessage(HttpMethod.Post, "https://reqbin.com/echo/post/json");
//            var stringData = JsonConvert.SerializeObject(new Datasend() { id = 1568, quantity = 1, price = 20 });
//            var stringContent = new StringContent(stringData, Encoding.UTF8, "application/json");

//            //attech to http request message 
//            request.Content = stringContent;

//            //Adding headers to the rquest
//            List<NameValueHeaderValue> listheaders = new List<NameValueHeaderValue>();
//            listheaders.Add(new NameValueHeaderValue("Encoding", "utf-8"));
//            listheaders.Add(new NameValueHeaderValue("user-agent", "x-machine"));

//            foreach(var header in listheaders)
//            {
//                request.Headers.Add(header.Name, header.Value);
//            }

//            //to send as part of send async
//            response = await client.SendAsync(request);


//            responseBody = await response.Content.ReadAsStringAsync();
//            Console.WriteLine(responseBody);

//            foreach (var item in response.Headers)
//            {
//                Console.WriteLine("Key is " + item.Key + "Value is " + item.Value.FirstOrDefault());
//            }

//            //To see request 
//            foreach (var item in request.Headers)
//            {
//                Console.WriteLine("Key is " + item.Key + "Value is " + item.Value.FirstOrDefault());
//            }
//        }
//    }
//}
