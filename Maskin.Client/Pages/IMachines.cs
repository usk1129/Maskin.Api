using Maskin.Client.Models;

namespace Maskin.Client.Pages
{
    public interface IMachines
    {
        machine[] _machines { get; set; }
        string DisplayMessage { get; set; }
        string MachineData { get; set; }
        string MachineName { get; set; }
        bool MachineStatus { get; set; }

        Task AddMachine();
        Task DeleteMachine(string id);
        Task PopulateTable();
    }
}