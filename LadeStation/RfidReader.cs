using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ladeskab
{
    public class DTRfidReaderEvent : EventArgs
    {
        //Dette er lige som "argumentet man sender med i ens eventhandler"
        public int RfidId { get; set; }
    }

    public interface IRfidReader
    {
        event EventHandler<DTRfidReaderEvent> RfidChangedEvent;
        public void RfidDetected(int id);
    }

    public class RfidReader : IRfidReader
    {
        public event EventHandler<DTRfidReaderEvent> RfidChangedEvent;
        public void RfidDetected(int id)
        {
            OnRfidChangedEvent(new DTRfidReaderEvent { RfidId = id });
        }

        protected virtual void OnRfidChangedEvent(DTRfidReaderEvent e)
        {
            RfidChangedEvent?.Invoke(this, e);
        }
    }
   
}
