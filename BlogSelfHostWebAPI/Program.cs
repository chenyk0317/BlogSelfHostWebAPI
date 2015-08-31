
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Web.Http;
using System.Web.Http.SelfHost;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace BlogSelfHostWebAPI
{
    class Program
    {
        static string str = "http://0.0.0.0:8989/";
        static readonly Uri _baseAddress = new Uri(str);
        static void Main(string[] args)
        {
            HttpSelfHostServer server = null;
            try
            {
                // 设置selfhost
                HttpSelfHostConfiguration config = new HttpSelfHostConfiguration(_baseAddress);

                //配置(config);
                //清除xml格式，设置Json格式
                config.Formatters.XmlFormatter.SupportedMediaTypes.Clear();
                config.Formatters.JsonFormatter.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
                //设置路由
                config.Routes.MapHttpRoute(
                    name: "DefaultApi",
                    routeTemplate: "api/{controller}/{id}",
                    defaults: new { id = RouteParameter.Optional }
                );
                // 创建服务
                server = new HttpSelfHostServer(config);

                // 开始监听
                server.OpenAsync().Wait();
                Console.WriteLine("Listening on " + _baseAddress);
                Console.WriteLine("Web API host started...");
                //输入exit按Enter結束httpServer
                string line = null;
                do
                {
                    line = Console.ReadLine();
                }
                while (line != "exit");
                //结束连接
                server.CloseAsync().Wait();
            }
            catch (Exception e)
            {
                Console.WriteLine("Could not start server: {0}", e.GetBaseException().Message);
                Console.WriteLine("Hit ENTER to exit...");
                Console.ReadLine();
            }
            finally
            {
                if (server != null)
                {
                    // Stop listening
                    server.CloseAsync().Wait();
                }
            }
        }
    }
}
