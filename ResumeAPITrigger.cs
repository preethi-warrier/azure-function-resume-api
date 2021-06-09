using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Net.Http;
using System.Text;

namespace ResumeAPI.Function
{
    public static class ResumeAPITrigger
    {
        [FunctionName("ResumeAPITrigger")]
        public static async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log,ExecutionContext context)
        {
            try
            {
                log.LogInformation("C# http trigger function to process a request.");
                var path = context.FunctionAppDirectory + "/resume.json";
                if(File.Exists(path))
                {
                   var jsonResponse = File.ReadAllText(path);
                   
                    return new HttpResponseMessage(HttpStatusCode.OK){
                            Content = new StringContent(jsonResponse,Encoding.UTF8,"application/json")
                        };
                        
                }
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
            catch(Exception e){
                log.LogInformation(e.Message);
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
            
        }
    }
}
