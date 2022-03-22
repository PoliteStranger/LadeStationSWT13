
namespace Ladeskab
{
    public class test
    {
        public bool currentdoorstate { get; set; }

        public test(IDoor door)
        {
            door.DoorChangedEvent+=Door_DoorChangedEvent;
        }

        private void Door_DoorChangedEvent(object sender, DTDoorOpenCloseEvent e)
        {
            if (currentdoorstate != e.doorOpen)
            {
                currentdoorstate = e.doorOpen;
                Console.WriteLine("door state is now: {0} \n", currentdoorstate);
            }
            else
            {
                Console.WriteLine("door state is still: {0} \n", currentdoorstate);
            }
        }

        static void Main(string[] args)
        {
            IDoor d1 = new Door();
            test t1 = new test(d1);
            d1.OnDoorOpen();
            d1.DoorLock();
        }
    }
}

