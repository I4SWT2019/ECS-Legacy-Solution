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

        [SetUp]
        public void Setup()
        {
            int thresholdUpperTemp = 20;
            int thresholdLowerTemp = 10;
            var _fakeTempSensor = Substitute.For<ITempSensor>();
            var _fakeHeater = Substitute.For<IHeater>();
            var _fakeWindow = Substitute.For<IWindow>();
            _uut = new Refactored.ECS(thresholdUpperTemp, thresholdLowerTemp,
                _fakeTempSensor, _fakeHeater, _fakeWindow);
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
            Assert.That(_uut.GetUpperThreshold(),Is.EqualTo(result));
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
            _uut.GetCurTemp().Returns(10);
            _uut.Received(10);
        }

        [Test]
        public void RunSelfTest_TestIsOK_ReturnTrue()
        {

            _uut.RunSelfTest().Returns(true);
            _uut.Received(1);
        }

        [Test]
        public void Regulate_TempIs10ThresholdIs20_ReturnsTrue()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                _uut.Regulate();

                string expected = string.Format("ON{0}",Environment.NewLine);

                // Resetting console output
                StreamWriter standardOut = new StreamWriter(Console.OpenStandardOutput());
                standardOut.AutoFlush = true;
                Console.SetOut(standardOut);

                Assert.That(expected, Is.EqualTo(sw.ToString()));
            }
        }

        [Test]
        public void Regulate_TempIs10ThresholdIs5_ReturnsTrue()
        {
            _uut.SetThreshold(5);
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                _uut.Regulate();

                string expected = string.Format("OFF{0}", Environment.NewLine);

                // Resetting console output
                StreamWriter standardOut = new StreamWriter(Console.OpenStandardOutput());
                standardOut.AutoFlush = true;
                Console.SetOut(standardOut);

                Assert.That(expected, Is.EqualTo(sw.ToString()));
            }
        }

        [Test]
        public void Regulate_TempIs10ThresholdIs10_ReturnsTrue()
        {
            _uut.SetThreshold(10);
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                _uut.Regulate();

                string expected = string.Format("OFF{0}", Environment.NewLine);

                // Resetting console output
                StreamWriter standardOut = new StreamWriter(Console.OpenStandardOutput());
                standardOut.AutoFlush = true;
                Console.SetOut(standardOut);

                Assert.That(expected, Is.EqualTo(sw.ToString()));
            }
        }
    }
}