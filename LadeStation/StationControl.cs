using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ladeskab
{
    public class StationControl
    {
        // Enum med tilstande ("states") svarende til tilstandsdiagrammet for klassen
        private enum LadeskabState
        {
            Available,
            Locked,
            DoorOpen
        };

        // Her mangler flere member variable
        private LadeskabState _state;
        private IChargeControl _charger;
        private Display _display;
        private int _oldId;
        private IDoor _door;
        
        private string logFile = "logfile.txt"; // Navnet på systemets log-fil

        // Her mangler constructor
        public StationControl(Display display)
        {
            // Dependency injection af Display objektet!
            _display = display;
        }

        // Eksempel på event handler for eventet "RFID Detected" fra tilstandsdiagrammet for klassen
        private void RfidDetected(int id)
        {
            switch (_state)
            {
                case LadeskabState.Available:
                    // Check for ladeforbindelse
                    if (_charger.Connected)
                    {

                        //_door.LockDoor();   <- virker ikke?!?
                        _charger.StartCharge();
                        _oldId = id;
                        using (var writer = File.AppendText(logFile))
                        {
                            writer.WriteLine(DateTime.Now + ": Skab låst med RFID: {0}", id);
                        }

                        _display.DisplayChargeMessage(IDisplay.ChargeMessages.Charging);
                        

                        _state = LadeskabState.Locked;
                    }
                    else
                    {
                        _display.DisplayChargeMessage(IDisplay.ChargeMessages.ChargeError);
                        
                    }

                    break;

                case LadeskabState.DoorOpen:
                    // Ignore
                    break;

                case LadeskabState.Locked:
                    // Check for correct ID
                    if (id == _oldId)
                    {
                        _charger.StopCharge();
                        //_door.UnlockDoor();   <- virker ikke?!?
                        using (var writer = File.AppendText(logFile))
                        {
                            writer.WriteLine(DateTime.Now + ": Skab låst op med RFID: {0}", id);
                        }


                        _display.DisplayGuideMessage(IDisplay.GuideMessages.RemovePhone);
                        _state = LadeskabState.Available;
                    }
                    else
                    {
                        _display.DisplayGuideMessage(IDisplay.GuideMessages.RFIDError);
                    }

                    break;
            }
        }

        // Her mangler de andre trigger handlere
    }
}
