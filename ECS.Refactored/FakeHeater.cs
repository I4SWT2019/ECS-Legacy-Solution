using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECS.Refactored
{
    public class FakeHeater : IHeater
    {
        public void TurnOn()
        {
            System.Console.WriteLine("ON");
        }

        public void TurnOff()
        {
            System.Console.WriteLine("OFF");
        }

        public bool RunSelfTest()
        {
            return true;
        }
    }
}
