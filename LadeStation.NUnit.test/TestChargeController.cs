using Ladeskab;
using NSubstitute;
using NUnit.Framework;
using UsbSimulator;


namespace LadeStation.NUnit.test
{
    public class TestChargeController
    {
        private ChargeController _uut;
        private IDisplay display;
        private IUsbCharger usbChargerSimulator;

        [SetUp]
        public void Setup()
        {
            display = Substitute.For<IDisplay>();
                        
            usbChargerSimulator = Substitute.For<IUsbCharger>();

            _uut = new ChargeController(display, usbChargerSimulator);
        }

        [TestCase(5.1)]
        [TestCase(500)]
        public void TestHandleCurrentValueEventCharging(double current)
        {
            usbChargerSimulator.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs(){Current = current });

            display.Received().DisplayChargeMessage(IDisplay.ChargeMessages.Charging);
        }

        [TestCase(500.1)]
        public void TestHandleCurrentValueEventOverload(double current)
        {
            
            usbChargerSimulator.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs() { Current = current });
            Assert.Multiple(() =>
            {
                usbChargerSimulator.Received().StopCharge();
                display.Received().DisplayChargeMessage(IDisplay.ChargeMessages.ChargeError);
            });
            
        }

        [Test]
        public void TestHandleCurrentValueEventNoConn()
        {
            
            usbChargerSimulator.Connected.Returns(false);
            usbChargerSimulator.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs() { Current = usbChargerSimulator.CurrentValue });

            display.Received().DisplayChargeMessage(IDisplay.ChargeMessages.NoConn);
        }

        [TestCase(0.1)]
        [TestCase(5)]
        public void TestHandleCurrentValueEventFullCharge(double current)
        {
            usbChargerSimulator.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs() { Current = current });

            display.Received().DisplayChargeMessage(IDisplay.ChargeMessages.FullCharge);
        }

        [Test]
        public void TestStartCharge()
        {
            _uut.StartCharge();
            usbChargerSimulator.Received().StartCharge();
        }

        [Test]
        public void TestStopCharge()
        {
            _uut.StopCharge();
            usbChargerSimulator.Received().StopCharge();
        }




    }
}
