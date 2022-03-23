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

        [Test]
        public void TestHandleCurrentValueEventCharging()
        {
            
            //usbChargerSimulator.Connected.Returns(true);
            usbChargerSimulator.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs(){Current = 450});

            display.Received().DisplayChargeMessage(IDisplay.ChargeMessages.Charging);
        }

        [Test]
        public void TestHandleCurrentValueEventOverload()
        {
            
            //usbChargerSimulator.Connected.Returns(true);
            usbChargerSimulator.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs() { Current = 750 });

            display.Received().DisplayChargeMessage(IDisplay.ChargeMessages.ChargeError);
        }

        [Test]
        public void TestHandleCurrentValueEventNoConn()
        {
            
            usbChargerSimulator.Connected.Returns(false);
            usbChargerSimulator.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs() { Current = usbChargerSimulator.CurrentValue });

            display.Received().DisplayChargeMessage(IDisplay.ChargeMessages.NoConn);
        }

        [Test]
        public void TestHandleCurrentValueEventFullCharge()
        {

            usbChargerSimulator.Connected.Returns(false);
            usbChargerSimulator.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs() { Current = 2.5 });

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
