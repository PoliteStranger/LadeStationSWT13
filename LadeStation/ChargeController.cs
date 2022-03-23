using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsbSimulator;

namespace Ladeskab
{


    public interface IChargeController
    {
        public bool Connected { get; }
        public void StartCharge();
        public void StopCharge();


    }

    public class ChargeController
    {
        
        public bool Connected { get; }

        private static Display _display;
        private static UsbChargerSimulator _usbCharger;


        public ChargeController(Display display, UsbChargerSimulator usbCharger)
        {
            Connected = false;
            _display = display;
            _usbCharger = usbCharger;

        }

        public void StartCharge()
        {
            _usbCharger.StartCharge();

        }

        public void StopCharge()
        {
            _usbCharger.StopCharge();
            
        }



        private static void CurrentValueEvent(object sender, CurrentEventArgs e)
        {
            //make event to notify display, need help

            switch (_usbCharger.CurrentValue)
            {
                case 500:
                    _display.DisplayChargeMessage(IDisplay.ChargeMessages.Charging);
                    break;
                case 750:
                    _display.DisplayChargeMessage(IDisplay.ChargeMessages.ChargeError);
                    break;
                case 0.0:
                    _display.DisplayChargeMessage(IDisplay.ChargeMessages.NoConn);
                    break;
                case 2.5:
                    _display.DisplayChargeMessage(IDisplay.ChargeMessages.FullCharge);
                    break;
            }




        }



    }







}
