using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ladeskab
{
    public class DTDoorOpenCloseEvent : EventArgs
    {
        //Dette er lige som "argumentet man sender med i ens eventhandler"
        public bool doorOpen { get; set; }
    }

    //interface for vores door.
    public interface IDoor
    {
        event EventHandler<DTDoorOpenCloseEvent> DoorChangedEvent;
        //public void LockDoor();
        //public void UnlockDoor();
        public void OnDoorOpen();
        public void OnDoorClose();

        public void DoorLock();
        public void DoorUnLock();
    }

    public class Door:IDoor
    {
        //eventhandler med vores "argument" DoorOpenCloseEvent
        public event EventHandler<DTDoorOpenCloseEvent> DoorChangedEvent;
        //bool for vores lås
        private bool doorstate;
        private bool doorlock;

        public Door()
        {
            doorstate = false;
            doorlock = false;
        }

        public void OnDoorOpen()
        {
            if (doorlock == false)
            {
                DoorStateChanged(new DTDoorOpenCloseEvent { doorOpen = true });
                doorstate = true;
            }
        }

        public void OnDoorClose()
        {
            if (doorstate == true)
            {
                DoorStateChanged(new DTDoorOpenCloseEvent { doorOpen = false });
                doorstate = false;
            }
        }

        public void DoorLock()
        {
            if (doorstate == false)
            {
                doorlock = true;
                Console.WriteLine("Døren er låst");
            }
        }

        public void DoorUnLock()
        {
            if (doorlock == true)
            {
                doorlock = false;
                Console.WriteLine("Døren er Ulåst");
            }
        }

        protected virtual void DoorStateChanged(DTDoorOpenCloseEvent e)
        {
            DoorChangedEvent?.Invoke(this, e);
        }
    }
   
}
