using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ladeskab;
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
            display = new Display();
            usbChargerSimulator = new UsbChargerSimulator();
            _uut = new ChargeController(display, usbChargerSimulator);
        }

        //private static void test_HandleCurrentValueEvent(object sender, CurrentEventArgs e)
        //{
        //    //make event to notify display, need help


        //}



    }
}
