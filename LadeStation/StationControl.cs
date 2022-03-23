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
        public enum LadeskabState
        {
            Available,
            Locked,
            DoorOpen
        };

        
        // Her mangler flere member variable
        private LadeskabState _state;

        public LadeskabState State
        {
            get { return _state; }
        }
        private IChargeController _charger;
        private IDisplay _display;          // Vis beskeder på displayet
        private int _oldId;
        private IDoor _door;
        private IRfidReader _reader;
        private ILogger _logger;

        // Her mangler constructor
        public StationControl(IDoor door, IChargeController charger, IDisplay display, IRfidReader reader, ILogger logger)
        {
            _door = door;
            door.DoorChangedEvent += HandleDoorChangedEvent;

            _display = display;
            _reader = reader;
            _reader.RfidChangedEvent += HandleIncomingRfId;

            _logger = logger;
            _charger = charger;

            _state = LadeskabState.Available;

        }
        //Eventhandler for door updates
        private void HandleDoorChangedEvent(object sender, DTDoorOpenCloseEvent e)
        {
            DoorUpdate(e.doorOpen);
        }
        //eventhandler for RFIDReader
        private void HandleIncomingRfId(object sender, DTRfidReaderEvent e)
        {
            RfidDetected(e.RfidId);
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
                        _door.LockDoor();
                        _charger.StartCharge();
                        _oldId = id;

                        _logger.Log($"Skab låst med RFID: {id}");


                        _display.DisplayGuideMessage(IDisplay.GuideMessages.Occupied);
                        

                        _state = LadeskabState.Locked;
                    }
                    else
                    {
                        _display.DisplayGuideMessage(IDisplay.GuideMessages.ConnError);
                        
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
                        _door.UnlockDoor();
                        //USE LOG CLASS
                        _logger.Log($"Skab låst op med RFID: {id}");


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

        private void DoorUpdate(bool doorState)
        {
            if (doorState)
            {
                if (_state == LadeskabState.Available)
                {
                    //display "Tilslut telefon"
                    _display.DisplayGuideMessage(IDisplay.GuideMessages.ConnectPhone);
                }
                else
                {
                    //display error
                }
            }
            else
            {
                if (_charger.Connected)
                {
                    //display "Indlæs RFID"

                }
                else
                {
                    //no phone, therefore free
                    _display.DisplayGuideMessage(IDisplay.GuideMessages.Free);
                }
            }
        }

    }
}
