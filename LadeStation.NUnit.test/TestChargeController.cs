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
        public void TestHandleCurrentValueEvent(/*double current*/)
        {
            //make event to notify display, need help
            //raise event with





            Assert.That(true);
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
