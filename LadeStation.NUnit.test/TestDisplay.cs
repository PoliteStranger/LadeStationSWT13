using NUnit.Framework;
using Ladeskab;

namespace LadeStation.NUnit.test
{
    public class TestDisplay
    {

        private Display _uut;


        [SetUp]
        public void Setup()
        {
            _uut = new Display();
        }

        [Test]
        public void DisplayMessageFree()
        {
            _uut.DisplayGuideMessage(IDisplay.GuideMessages.Free);
            Assert.That(_uut.GuideStateMessage, Is.EqualTo(IDisplay.GuideMessages.Free));
        }
    }
}