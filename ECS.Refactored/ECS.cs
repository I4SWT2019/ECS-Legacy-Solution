using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECS.Refactored
{
    public class ECS
    {
        private int _thresholdUpper;
        private int _thresholdLower;
        private readonly ITempSensor _tempSensor;
        private readonly IHeater _heater;
        private readonly IWindow _window;

        public ECS(int thrUp, int thrLo, ITempSensor tempSensorObj, IHeater heaterObj, IWindow windowObj)
        {
            SetUpperThreshold(thrUp);
            SetLowerThreshold(thrLo);
            _tempSensor = tempSensorObj;
            _heater = heaterObj;
            _window = windowObj;
        }

        public void Regulate()
        {
            var t = _tempSensor.GetTemp();
            if (t < _thresholdLower)
            {
                _heater.TurnOn();
                _window.Close();
            }
            else if (t > _thresholdUpper)
            {
                _heater.TurnOff();
                _window.Open();
            }
            else
            {
                _heater.TurnOff();
                _window.Close();
            }

        }

        public void SetUpperThreshold(int thr)
        {
            _thresholdUpper = thr;
        }

        public void SetLowerThreshold(int thr)
        {
            _thresholdLower = thr;
        }

        public int GetUpperThreshold()
        {
            return _thresholdUpper;
        }

        public int GetLowerThreshold()
        {
            return _thresholdLower;
        }

        public int GetCurTemp()
        {
            return _tempSensor.GetTemp();
        }

        public bool RunSelfTest()
        {
            return _tempSensor.RunSelfTest() && _heater.RunSelfTest() && _window.RunSelfTest();
        }
    }
}
