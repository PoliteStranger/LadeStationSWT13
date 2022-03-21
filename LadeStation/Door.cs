using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ladeskab
{
   //interface for vores door.
    public interface IDoor
    {
        event EventHandler<DoorOpenCloseEvent> DoorChangedEvent;
        public void LockDoor();
        public void UnlockDoor();
    }

    public class Door:IDoor
    {
        //eventhandler med vores "argument" DoorOpenCloseEvent
        public event EventHandler<DoorOpenCloseEvent> DoorChangedEvent;
        //bool for vores lås
        private bool _lock;
        public void LockDoor()
        {
            //hvis døren er ulåst, så kan den låses. og sende et event
            if (_lock == false)
            {
                DoorStateChanged(new DoorOpenCloseEvent { OpenClose = true });
                _lock = true;
            }
            //ellers er døren fortsat låst.
            else _lock = true;
        }

        public void UnlockDoor()
        {
            //hvis døren er låst, så kan den låses op. og sende et event
            if (_lock == true)
            {
                DoorStateChanged(new DoorOpenCloseEvent { OpenClose = false});
                _lock = false;
            }
            //ellers er døren fortsat ulåst.
            else
            {
                _lock = false;
            }
        }

        protected virtual void DoorStateChanged(DoorOpenCloseEvent e)
        {
            DoorChangedEvent?.Invoke(this, e);
        }
    }
    public class DoorOpenCloseEvent : EventArgs
    {
        //Dette er lige som "argumentet man sender med i ens eventhandler"
        public bool OpenClose { get; set; }
    }
}
