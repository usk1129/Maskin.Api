using Maskin.Client.Models;
using System.Net.Mime;
using System.Text;
using System.Text.Json;

namespace Maskin.Client.Pages
{
    public partial class Machines : IMachines
    {

        public string MachineName { get; set; }
        public string MachineData { get; set; }
        public bool MachineStatus { get; set; }
        public string DisplayMessage { get; set; }
        public machine[] _machines { get; set; }

        public async Task AddMachine()
        {

            HttpClient client = new HttpClient { BaseAddress = new Uri(" http://localhost:7071/api/machine") };

            HttpResponseMessage response = null;

            var payload = new machine()
            {
                Name = MachineName,
                Data = MachineData,
                Status = MachineStatus,

            };

            var payloadString = new StringContent(System.Text.Json.JsonSerializer.Serialize(payload), Encoding.UTF8, MediaTypeNames.Application.Json);

            response = await client.PostAsync(client.BaseAddress, payloadString);

            if (response.IsSuccessStatusCode)
            {
                await PopulateTable();
                DisplayMessage = "Successfully added!";
            }
            else
            {
                DisplayMessage = response.StatusCode.ToString() + "\n" + client.BaseAddress.ToString();
            }
        }
        protected override async Task OnInitializedAsync()
        {
            DisplayMessage = "hello!";
            await PopulateTable();
        }

        public async Task PopulateTable()
        {

            HttpClient client = new HttpClient { BaseAddress = new Uri(" http://localhost:7071/api/machine") };

            HttpResponseMessage response = null;

            response = await client.GetAsync(client.BaseAddress);

            if (response.Content is object && response.Content.Headers.ContentType.MediaType == "application/json")
            {
                var content = await response.Content.ReadAsStringAsync();
                _machines = JsonSerializer.Deserialize<machine[]>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }

        }

        public async Task DeleteMachine(string id)
        {
            HttpClient client = new HttpClient { BaseAddress = new Uri(" http://localhost:7071/api/machine/" + id) };

            HttpResponseMessage response = null;

            response = await client.DeleteAsync(client.BaseAddress);

            if (response.IsSuccessStatusCode)
            {
                DisplayMessage = "We deleted: " + id;
            }

            await PopulateTable();
        }
    }
}
