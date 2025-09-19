using System.Net.Http.Headers;
using System;
using System.Net.Http;
//using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Text.Json.Nodes;
using System.Text.Json;
using System.Text;

namespace TeamAdiDemo
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            // Replace with your Microsoft Graph access token
            string accessToken = "";

            // Microsoft Graph Calendar API endpoint
            string endpoint = "https://graph.microsoft.com/v1.0/me/events";

            try
            {
                // Create an HttpClient instance
                using (HttpClient client = new HttpClient())
                {
                    // Set the authorization header
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                    // Make the GET request to the Graph API
                    HttpResponseMessage response = await client.GetAsync(endpoint);

                    // Check if the request was successful
                    if (response.IsSuccessStatusCode)
                    {
                        string responseData = await response.Content.ReadAsStringAsync();
                        Console.WriteLine("Calendar Events:");
                        Console.WriteLine(responseData);

                        var clientInfo = JsonSerializer.Deserialize<CalendarEvent>(responseData);
                        var events = new List<EventDetails>();

                        foreach (var info in clientInfo.value)
                        {
                            var subject = info.subject;
                            if (subject.Contains("Herman", StringComparison.OrdinalIgnoreCase))
                            {
                                EventDetails newEventDetails = new()
                                {
                                    Name = subject,
                                    InvoiceDate = info.start.dateTime,
                                };

                                events.Add(newEventDetails);
                            }

                            if (subject.Contains("Johnathan", StringComparison.OrdinalIgnoreCase))
                            {
                                EventDetails newEventDetails = new()
                                {
                                    Name = subject,
                                    InvoiceDate = info.start.dateTime,
                                };

                                events.Add(newEventDetails);
                            }
                        }

                        var token = "";

                        foreach (var calenderEvent in events)
                        {
                            var hermanPayload =string.Empty;
                            var jonathanPayload = string.Empty;
                            if (calenderEvent.Name.Contains("Herman", StringComparison.OrdinalIgnoreCase))
                            {
                                hermanPayload = $@"
{{
  ""buyerId"": ""3131935c-535b-4231-b00c-0c34ecea6676"",
  ""creditDays"": 0,
  ""description"": ""Herman wants to sell these books"",
  ""discount"": 0,
  ""invoiceDate"": ""2025-01-17T10:36:13.203Z"",
  ""invoiceLines"": [
    {{
      ""description"": ""bahubali\r\n"",
      ""delivered"": 1,
      ""discount"": 0,
      ""lineIndex"": 1,
      ""productId"": ""1157fab6-63f6-4cf4-a0de-33d4d0ba516a"",
      ""totalQuantity"": 1,
      ""unitPrice"": 206.61,
      ""orderLineId"": ""f7f92a09-5664-4883-892a-981ea7100fd2"",
      ""isUnitPriceExclusiveVat"": true
    }}
  ],
  ""orderDate"": ""2025-01-17T10:34:49.352Z"",
  ""paymentReference"": """",
  ""sellerId"": ""ac149e39-4e0a-42a0-9137-8d27a2f9cde7"",
  ""templateId"": ""d4c1d9cb-03c4-44f4-9294-3bda5d835b65"",
  ""type"": ""DirectInvoice"",
  ""customerInfo"": {{
    ""shippingCustomer"": {{
      ""customerPublicId"": ""3131935c-535b-4231-b00c-0c34ecea6676"",
      ""name"": ""Herman SnelStart"",
      ""contactPerson"": """",
      ""street"": ""Alkmaar"",
      ""streetNumber"": 0,
      ""affix"": """",
      ""postalCode"": """",
      ""city"": """",
      ""countryId"": ""7272777a-f817-41d5-b49e-624bad9de6c6"",
      ""countryName"": ""NLD""
    }},
    ""invoicingCustomer"": {{
      ""customerPublicId"": ""3131935c-535b-4231-b00c-0c34ecea6676"",
      ""name"": ""Herman SnelStart"",
      ""contactPerson"": """",
      ""street"": ""Alkmaar"",
      ""streetNumber"": 0,
      ""affix"": """",
      ""postalCode"": """",
      ""city"": """",
      ""countryId"": ""7272777a-f817-41d5-b49e-624bad9de6c6"",
      ""countryName"": ""NLD""
    }}
  }},
  ""financialInfo"": {{
    ""paymentTermType"": ""None"",
    ""paymentConditionDiscountPercentage"": 0,
    ""paymentCondtionDays"": 0,
    ""costCenterIdentifier"": null,
    ""amountA"": 0,
    ""amountB"": 0,
    ""gAccountPercentage"": 0
  }},
  ""subscription"": {{}}
}}";
                            }

                            if (calenderEvent.Name.Contains("Johnathan", StringComparison.OrdinalIgnoreCase))
                            {
                                
                                     jonathanPayload = $@"
{{
  ""buyerId"": ""7bf861dd-672e-4c2d-b05a-9f4816119ba2"",
  ""creditDays"": 0,
  ""description"": ""15 books for sale"",
  ""discount"": 0,
  ""invoiceDate"": ""2025-01-17T10:36:13.203Z"",
  ""invoiceLines"": [
    {{
      ""description"": ""THE COLLAB"",
      ""delivered"": 1,
      ""discount"": 0,
      ""lineIndex"": 1,
      ""productId"": ""ab19220b-3245-4269-a46a-c0734f0cbe3e"",
      ""totalQuantity"": 1,
      ""unitPrice"": 123.97,
      ""orderLineId"": ""6a74e051-d3fd-4fb5-8e9e-ab29c624c594"",
      ""isUnitPriceExclusiveVat"": true
    }}
  ],
  ""orderDate"": ""{calenderEvent.InvoiceDate:yyyy-MM-ddTHH:mm:ss.fffZ}"",
  ""paymentReference"": """",
  ""sellerId"": ""ac149e39-4e0a-42a0-9137-8d27a2f9cde7"",
  ""templateId"": ""d4c1d9cb-03c4-44f4-9294-3bda5d835b65"",
  ""type"": ""DirectInvoice"",
  ""customerInfo"": {{
    ""shippingCustomer"": {{
      ""customerPublicId"": ""7bf861dd-672e-4c2d-b05a-9f4816119ba2"",
      ""name"": ""Jonathan SnelStart"",
      ""contactPerson"": """",
      ""street"": """",
      ""streetNumber"": 0,
      ""affix"": """",
      ""postalCode"": """",
      ""city"": """",
      ""countryId"": ""7272777a-f817-41d5-b49e-624bad9de6c6"",
      ""countryName"": ""NLD""
    }},
    ""invoicingCustomer"": {{
      ""customerPublicId"": ""7bf861dd-672e-4c2d-b05a-9f4816119ba2"",
      ""name"": ""Jonathan SnelStart"",
      ""contactPerson"": """",
      ""street"": """",
      ""streetNumber"": 0,
      ""affix"": """",
      ""postalCode"": """",
      ""city"": """",
      ""countryId"": ""7272777a-f817-41d5-b49e-624bad9de6c6"",
      ""countryName"": ""NLD""
    }}
  }},
  ""financialInfo"": {{
    ""paymentTermType"": ""None"",
    ""paymentConditionDiscountPercentage"": 0,
    ""paymentCondtionDays"": 0,
    ""costCenterIdentifier"": null,
    ""amountA"": 0,
    ""amountB"": 0,
    ""gAccountPercentage"": 0
  }},
  ""subscription"": {{}}
}}";
                            }




                            var apiUrl = "https://api-tst.snelstart.nl/product/payment/v1/invoice/create-invoice";

                            using var httpClient = new HttpClient();

                            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                            // Call API with Herman's payload
                            var hermanResponse = await PostInvoiceAsync(httpClient, apiUrl, hermanPayload);
                            Console.WriteLine($"Herman Response: {hermanResponse}");

                            // Call API with Jonathan's payload
                            var jonathanResponse = await PostInvoiceAsync(httpClient, apiUrl, jonathanPayload);
                            Console.WriteLine($"Jonathan Response: {jonathanResponse}");
                            // You can now use the clientInfo object
                            // Console.WriteLine($"Name: {clientInfo.Name}, Email: {clientInfo.Email}");
                        }
                    }

                    else
                    {
                        Console.WriteLine($"Error: {response.StatusCode}");
                        string errorData = await response.Content.ReadAsStringAsync();
                        Console.WriteLine($"Details: {errorData}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred:");
                Console.WriteLine(ex.Message);
            }
        }

        private static async Task<string> PostInvoiceAsync(HttpClient client, string url, string payload)
        {
            var content = new StringContent(payload, Encoding.UTF8, "application/json");

            try
            {
                var response = await client.PostAsync(url, content);
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }
    }

    public class ClientInfo
    {
        public string Name { get; set; }
        public string Email { get; set; }
        // Add other properties as needed
    }

    public class EventDetails
    {
        public string Id { get; set; }
        public string Name { get; set;}
        public DateTime InvoiceDate { get; set; }
    }

}
