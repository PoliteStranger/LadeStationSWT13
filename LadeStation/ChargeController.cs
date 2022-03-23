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
        public bool Connected { get; set; }
        public void StartCharge();
        public void StopCharge();


    }

    public class ChargeController : IChargeController
    {
        
        public bool Connected { get; set; }

        private static IDisplay _display;
        private static IUsbCharger _usbCharger;


        public ChargeController(IDisplay display, IUsbCharger usbCharger)
        {
            _display = display;
            _usbCharger = usbCharger;
            Connected = usbCharger.Connected;
            _usbCharger.CurrentValueEvent += HandleCurrentValueEvent;

        }

        public void StartCharge()
        {
            _usbCharger.StartCharge();

        }

        public void StopCharge()
        {
            _usbCharger.StopCharge();
            
        }



        private static void HandleCurrentValueEvent(object sender, CurrentEventArgs e)
        {
            
            //make event to notify display, need help
            if (e.Current <= 500 && e.Current > 5)
            {
                _display.DisplayChargeMessage(IDisplay.ChargeMessages.Charging);
            }
            else if (e.Current <= 5 && e.Current > 0)
            {
                _display.DisplayChargeMessage(IDisplay.ChargeMessages.FullCharge);
            }
            else if (e.Current > 500)
            {
                _usbCharger.StopCharge();
                _display.DisplayChargeMessage(IDisplay.ChargeMessages.ChargeError);
            }
            else if (e.Current == 0)
            {
                _display.DisplayChargeMessage(IDisplay.ChargeMessages.NoConn);
            }
        }



    }







}
