using System;
using System.IO;
using ECS.Refactored;
using NSubstitute;
using NUnit.Framework;

namespace ECS.Test.Unit
{
    [TestFixture]
    [Author("I4SWT2019GR2")]
    public class ECSUnitTest
    {
        private Refactored.ECS _uut;
        private ITempSensor _tempSensor;
        private IHeater _heater;
        private IWindow _window;

        [SetUp]
        public void Setup()
        {
            int thresholdUpperTemp = 20;
            int thresholdLowerTemp = 10;
            _tempSensor = Substitute.For<ITempSensor>();
            _heater = Substitute.For<IHeater>();
            _window = Substitute.For<IWindow>();
            _uut = new Refactored.ECS(thresholdUpperTemp, thresholdLowerTemp,
                _tempSensor, _heater, _window);
        }

        [Test]
        public void GetUpperThreshold_Is20_Returns20()
        {
            Assert.That(_uut.GetUpperThreshold(), Is.EqualTo(20));
        }

        [Test]
        public void GetLowerThreshold_Is20_Returns20()
        {
            Assert.That(_uut.GetLowerThreshold(), Is.EqualTo(10));
        }


        [TestCase(-20, -20)]
        [TestCase(0, 0)]
        [TestCase(15, 15)]
        public void SetUpperThreshold_SetAboveZeroAtZeroBelowZero_ResultIsCorrect(int threshold, int result)
        {
            _uut.SetUpperThreshold(threshold);
            Assert.That(_uut.GetUpperThreshold(), Is.EqualTo(result));
        }

        [TestCase(-20, -20)]
        [TestCase(0, 0)]
        [TestCase(15, 15)]
        public void SetLowerThreshold_SetAboveZeroAtZeroBelowZero_ResultIsCorrect(int threshold, int result)
        {
            _uut.SetLowerThreshold(threshold);
            Assert.That(_uut.GetLowerThreshold(), Is.EqualTo(result));
        }

        [Test]
        public void GetCurTemp_TempIs10_Return10()
        {
            _tempSensor.GetTemp().Returns(10);
            Assert.That(_uut.GetCurTemp(), Is.EqualTo(10));
        }

        [Test]
        public void RunSelfTest_TestIsOK_ReturnTrue()
        {
            _tempSensor.RunSelfTest().Returns(true);
            _heater.RunSelfTest().Returns(true);
            _window.RunSelfTest().Returns(true);
            Assert.IsTrue(_uut.RunSelfTest());
        }


        [Test]
        public void RunSelfTest_TempSensorFails_SelfTestFails()
        {
            _tempSensor.RunSelfTest().Returns(false);
            _heater.RunSelfTest().Returns(true);  
            _window.RunSelfTest().Returns(true);

            Assert.IsFalse(_uut.RunSelfTest());
        }

        [Test]
        public void RunSelfTest_HeaterFails_SelfTestFails()
        {
            _tempSensor.RunSelfTest().Returns(true);
            _heater.RunSelfTest().Returns(false);
            _window.RunSelfTest().Returns(true);

            Assert.IsFalse(_uut.RunSelfTest());
        }

        [Test]
        public void RunSelfTest_WindowFails_SelfTestFails()
        {
            _tempSensor.RunSelfTest().Returns(true);
            _heater.RunSelfTest().Returns(true);
            _window.RunSelfTest().Returns(false);

            Assert.IsFalse(_uut.RunSelfTest());
        }
    }
}
