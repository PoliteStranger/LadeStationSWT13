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
        public event EventHandler<CurrentEventArgs> CurrentValueToDisplayEvent;

        public bool Connected { get; }

        public void StartCharge()
        {


        }

        public void StopCharge()
        {
            
            
        }



        private static void CurrentValueEvent(object sender, CurrentEventArgs e)
        {
            //make event to notify display, need help



        }



    }







}
