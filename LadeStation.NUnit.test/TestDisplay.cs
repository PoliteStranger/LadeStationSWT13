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

        [TestCase(IDisplay.GuideMessages.ReadRFID)]
        [TestCase(IDisplay.GuideMessages.Free)]
        [TestCase(IDisplay.GuideMessages.ConnectPhone)]
        [TestCase(IDisplay.GuideMessages.Occupied)]
        [TestCase(IDisplay.GuideMessages.RFIDError)]
        [TestCase(IDisplay.GuideMessages.ConnError)]
        [TestCase(IDisplay.GuideMessages.RemovePhone)]
        public void DisplayGuideMessages(IDisplay.GuideMessages n)
        {
            _uut.DisplayGuideMessage(n);
            Assert.That(_uut.GuideStateMessage, Is.EqualTo(n));
        }

        [TestCase(IDisplay.ChargeMessages.FullCharge)]
        [TestCase(IDisplay.ChargeMessages.NoConn)]
        [TestCase(IDisplay.ChargeMessages.ChargeError)]
        [TestCase(IDisplay.ChargeMessages.Charging)]
        public void DisplayChargeMessages(IDisplay.ChargeMessages n)
        {
            _uut.DisplayChargeMessage(n);
            Assert.That(_uut.ChargeStateMessage, Is.EqualTo(n));
        }
    }
}