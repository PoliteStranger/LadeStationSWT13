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
       
        public void OnDoorOpen();
        public void OnDoorClose();

        public void LockDoor();
        public void UnlockDoor();
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
            if (doorlock == false && doorlock == false)
            {
                DoorStateChanged(new DTDoorOpenCloseEvent { doorOpen = true });
                doorstate = true;
            }
        }

        public void OnDoorClose()
        {
            if (doorstate == true && doorlock == false)
            {
                DoorStateChanged(new DTDoorOpenCloseEvent { doorOpen = false });
                doorstate = false;
            }
        }

        public void LockDoor()
        {
            if (doorstate == false && doorlock == false)
            {
                doorlock = true;
            }
        }

        public void UnlockDoor()
        {
            if (doorlock == true && doorstate == false)
            {
                doorlock = false;
            }
        }

        protected virtual void DoorStateChanged(DTDoorOpenCloseEvent e)
        {
            DoorChangedEvent?.Invoke(this, e);
        }
    }
   
}
