using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace RazorLight.Sandbox
{
    class Program
    {
        public static async Task Main()
        {
            var engine = new RazorLightEngineBuilder()
                .UseMemoryCachingProvider()
                //				.UseEmbeddedResourcesProject(typeof(Program))
                .Build();

            string template = "Hello, @Model.Name. Welcome to RazorLight repository";
            var jsonModel =
                @"{""Name"":""InfoTrack%20Template%20Service"",""Tasks"":[""Razor%20Engin"",""Jason%20Model"",""Cache%20Templates""],%20""Description"":%20""Product%20of%20InfoTrack%20Platform%20Team!""}";
            var model = JsonConvert.DeserializeObject(jsonModel);

            string result = await engine.CompileRenderAsync<string>("templateKey", jsonModel, null);

            //            string result = await engine.CompileRenderAsync<string>("Views.Subfolder.A", null, null);
            Console.WriteLine(result);

            Console.WriteLine("Finished");
        }

        private static readonly object locker = new object();

        private static int _j;
        public static int j
        {
            get
            {
                lock (locker)
                {
                    return _j;
                }
            }
            set
            {
                lock (locker)
                {
                    _j = value;
                }
            }
        }
    }
}
