using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.IO;
using System.Threading;

namespace IssueReproduction.DefaultValueIgnored
{
    class Program
    {
        static void Main(string[] args)
        {
            string json = @"{}";

            while (true)
            {
                var jsonSerializer = new JsonSerializer() { ContractResolver = new DefaultContractResolver() };
                var threads = new Thread[2];
                Exception exception = null;

                for (int i = 0; i < threads.Length; i++)
                {
                    var thread = new Thread(() =>
                    {
                        try
                        {
                            using (var jsonTextReader = new JsonTextReader(new StringReader(json)))
                            {
                                DummyModel result = jsonSerializer.Deserialize<DummyModel>(jsonTextReader);
                            }
                        }
                        catch (Exception e)
                        {
                            exception = e;
                        }
                    });
                    threads[i] = thread;
                }

                threads[0].Start();
                threads[1].Start();

                threads[0].Join();
                threads[1].Join();

                if (exception != null)
                {
                    Console.WriteLine(exception.ToString());
                    break;
                }
            }
        }
    }
}
