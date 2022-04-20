using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maskin.Api.Models
{
    public class MachineStore
    {


        public static List<Machine> machines = new List<Machine>
        {
            new Machine{Id = "1", Name = "TestSeed", Data = "text", Status = false},
            new Machine{Id = "2", Name = "TestSeed", Data = "text", Status = false},
        };
    }
}
