using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECS.Refactored
{
    public class FakeTempSensor : ITempSensor
    {
        public int _temp;

        public int GetTemp()
        {
            return _temp;
        }

        public bool RunSelfTest()
        {
            return true;
        }
    }
}
