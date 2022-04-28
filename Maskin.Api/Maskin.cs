using Maskin.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maskin.Api
{
    public static class Maskin
    {
        [FunctionName("get_machines")]
        public static async Task<IActionResult> GetMachines(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "machine")] HttpRequest req,
            ILogger log)
        {


            return new OkObjectResult(MachineStore.machines);
        }
        
        [FunctionName("get_machines_by_id")]
        public static async Task<IActionResult> GetMachinesById(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "machine/{id}")] HttpRequest req,
            ILogger log, string id)
        {
            var machine = MachineStore.machines.FirstOrDefault(f => f.Id.Equals(id));
            if (machine == null)
            {
                return new NotFoundResult();
            }

            return new OkObjectResult(machine);
        }

        [FunctionName("add_machine")]
        public static async Task<IActionResult> AddMachine(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "machine")] HttpRequest req,
            ILogger log)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            var machine = JsonConvert.DeserializeObject<Machine>(requestBody);

            MachineStore.machines.Add(machine);

            return new OkObjectResult(machine);
        }

        [FunctionName("delete_machine")]
        public static async Task<IActionResult> DeleteMachine(
           [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "machine/{id}")] HttpRequest req,
           ILogger log, string id)
        {
            var machine = MachineStore.machines.FirstOrDefault(f => f.Id.Equals(id));

            if (machine == null)
            {
                return new NotFoundResult();
            }

            MachineStore.machines.Remove(machine);
            return new OkResult();
        }


    }
}
