using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ladeskab;
using NSubstitute;
using NUnit.Framework;
using UsbSimulator;


namespace LadeStation.NUnit.test
{
    public class TestChargeController
    {
        private ChargeController _uut;
        private Display display;
        private UsbChargerSimulator usbChargerSimulator;

        [SetUp]
        public void Setup()
        {
            display = Substitute.For<Display>();
            usbChargerSimulator = Substitute.For<UsbChargerSimulator>();
            _uut = new ChargeController(display, usbChargerSimulator);
        }

        //private static void test_HandleCurrentValueEvent(object sender, CurrentEventArgs e)
        //{
        //    //make event to notify display, need help


        //}
        [Test]
        public void TestStartCharge()
        {
            _uut.StartCharge();
            usbChargerSimulator.Received(1).StartCharge();
        }

        [Test]
        public void TestStopCharge()
        {
            _uut.StartCharge();
            usbChargerSimulator.Received(1).StopCharge();
        }




    }
}
