    using Ladeskab;
    using UsbSimulator;

    class Program
    {
        static void Main(string[] args)
        {
				// Assemble your system here from all the classes
                IDoor door = new Door();
                IDisplay display = new Display();
                IRfidReader rfidReader = new RfidReader();
                IUsbCharger usbcharger = new UsbChargerSimulator();
                IChargeController charger = new ChargeController(display, usbcharger);
                ILogger logger = new Logger();
                StationControl s1 = new StationControl(door, charger, display, rfidReader, logger);
                bool finish = false;

                do
                {
                string input;
                System.Console.WriteLine("Indtast E, O, C, R: ");
                input = Console.ReadLine();
                if (string.IsNullOrEmpty(input)) continue;

                switch (input[0])
                {
                    case 'E':
                        finish = true;
                        break;

                    case 'O':
                        door.OnDoorOpen();
                        break;

                    case 'C':
                        door.OnDoorClose();
                        break;

                    case 'R':
                        System.Console.WriteLine("Indtast RFID id: ");
                        string idString = System.Console.ReadLine();

                        int id = Convert.ToInt32(idString);
                        rfidReader.RfidDetected(id);
                        break;

                    default:
                        break;
                }

            } while (!finish);
        }
    }

