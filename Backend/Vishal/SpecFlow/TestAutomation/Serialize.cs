//using Newtonsoft.Json;

//namespace TestAutomation
//{ 
//        class dataModel
//        {
//            public string name { get; set; }
//            public string job { get; set; }
//            public object[] addr { get; set; }
//        }

//        class PostalCode
//        {
//            public string code { get; set; }
//            public string sector { get; set; }
//        }


//        class Serialize
//        {
//            static void Main(string[] args)
//            {
//                dataModel data = new dataModel()
//                {
//                    name = "morpheus",
//                    job = "leader",
//                    addr = new object[] { new PostalCode() { code = "1234", sector = "5678" },"belgium","englind" }
//                };

//                var dataser = JsonConvert.SerializeObject(data);
//                Console.WriteLine(dataser);
//            }
//        }
//}
