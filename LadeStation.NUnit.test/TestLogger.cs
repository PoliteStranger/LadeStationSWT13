using NUnit.Framework;
using Ladeskab;

namespace LadeStation.NUnit.test
{
    public class TestLogger
    {

        private Logger _uut;


        [SetUp]
        public void Setup()
        {
            _uut = new Logger();
        }

        [TestCase]
        public void CreateLogEntry()
        {
            _uut.Log("Message");
            Assert.That(_uut.LogWritten, Is.EqualTo(true));
        }


    }
}