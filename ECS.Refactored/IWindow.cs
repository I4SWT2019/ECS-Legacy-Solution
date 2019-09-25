using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ECS.Refactored
{
    public interface IWindow
    {
        void Open();
        void Close();
        bool RunSelfTest();
    }
}
