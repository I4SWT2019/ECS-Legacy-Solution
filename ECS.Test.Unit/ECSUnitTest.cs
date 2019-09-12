using System;
using System.IO;
using ECS.Refactored;
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
            int thresholdTemp = 20;
            _uut = new Refactored.ECS(thresholdTemp, new FakeTempSensor(), new FakeHeater());
        }

        [Test]
        public void GetThreshold_Is20_Returns20()
        {
            Assert.That(_uut.GetThreshold(), Is.EqualTo(20));
        }

        [TestCase(-20, -20)]
        [TestCase(0, 0)]
        [TestCase(15, 15)]
        public void SetThreshold_SetAboveZeroAtZeroBelowZero_ResultIsCorrect(int threshold, int result)
        {
            _uut.SetThreshold(threshold);
            Assert.That(_uut.GetThreshold(),Is.EqualTo(result));
        }

        [Test]
        public void GetCurTemp_TempIs10_Return10()
        {
            Assert.That(_uut.GetCurTemp(),Is.EqualTo(10));
        }

        [Test]
        public void RunSelfTest_TestIsOK_ReturnTrue()
        {
            Assert.That(_uut.RunSelfTest(),Is.True);
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