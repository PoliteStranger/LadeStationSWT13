using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ladeskab
{
    public class RfidUpdateArgs : EventArgs
    {
        public int Id { get; set; }
    }

    public interface IRfidReader
    {
        event EventHandler<RfidUpdateArgs> RfidUpdateEvent;
    }

    internal class RfidReader
    {

    }
}
