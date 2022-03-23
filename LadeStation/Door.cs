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

    public class Door : IDoor
    {
        //eventhandler med vores "argument" DoorOpenCloseEvent
        public event EventHandler<DTDoorOpenCloseEvent> DoorChangedEvent;
        //bool for vores lås
        private bool doorstate;
        private bool doorlock;

        public Door()
        {
            //doorstate 
            //dør åben = true
            //dør lukket = false
            doorstate = false;
              
            doorlock = false;
        }

        public void OnDoorOpen()
        {
            //tjekker om døren er lukket i forvejen
            if (doorlock == false)
            {
                //sender doorOpen true event, da døren nu åbnes
                DoorStateChanged(new DTDoorOpenCloseEvent { doorOpen = true });
                doorstate = true;
            }
        }
        public void OnDoorClose()
        {
            //tjekker om døren er åben i forvejen
            if (doorstate == true)
            {
                //sender doorOpen false event, da døren nu lukkes
                DoorStateChanged(new DTDoorOpenCloseEvent { doorOpen = false });
                doorstate = false;
            }
        }
        public void LockDoor()
        {
            //tjekker om døren er Ulåst
            if (doorstate == false)
            {
                doorlock = true;
            }
        }
        public void UnlockDoor()
        {
            //tjekker om døren er låst
            if (doorlock == true)
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
