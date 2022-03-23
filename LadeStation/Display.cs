using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ladeskab
{
    public interface IDisplay
    {

        // Besked typer for opladning (Del 1 af displayet)
        enum ChargeMessages
        {                           // BESKEDER:
            NoConn,                 // Ingen forbindelse = ingen besked!
            FullCharge,             // Telefonen er fuldt opladt
            Charging,               // Telefonen lader
            ChargeError             // Fejl under ladning!
        }

        // Besked typer for guide af ladestationen (del 2 af displayet)
        enum GuideMessages
        {                           // BESKEDER:
            Free,                   // Ladeskabet er ledigt
            ConnectPhone,           // Forbind telefon
            ReadRFID,               // Indlæs RFID
            ConnError,              // Forbindelses fejl
            Occupied,               // Ladeskabet er optaget
            RFIDError,              // RFID fejl v. indlæsning
            RemovePhone             // Fjern telefon
        }

        // Til at afvikle Guide beskeder:
        void DisplayGuideMessage(GuideMessages message);

        // Til at afvikle Charge beskeder:
        void DisplayChargeMessage(ChargeMessages message);
    }

    public class Display : IDisplay
    {
        // Holder nuværende tilstand af displayets beskeder:
        private IDisplay.ChargeMessages _chargeStateMessage;
        private IDisplay.GuideMessages _guideStateMessage;

        // Getters for tilstanden af display beskederne. Til at displayet kan 
        public IDisplay.GuideMessages GuideStateMessage
        {
            get { return _guideStateMessage; }
            set
            {
                _guideStateMessage = value;
                // Her kunne vi evt "Sende" et event el. lign til selve hardwaredelen af displayet!
            }
        }
        public IDisplay.ChargeMessages ChargeStateMessage
        {
            get { return _chargeStateMessage; }
            set
            {
                _chargeStateMessage = value;
                // Her kunne vi evt "Sende" et event el. lign til selve hardwaredelen af displayet!
            }
        }

        // Metoden til at sætte beskeden, samt konsol feedback til test programmet
        public void DisplayGuideMessage(IDisplay.GuideMessages message)
        {
            GuideStateMessage = message;

            switch (message)
            {
                case IDisplay.GuideMessages.Free:
                    Console.WriteLine("Ladeskabet er ledigt.");
                    break;
                case IDisplay.GuideMessages.ConnectPhone:
                    Console.WriteLine("Forbind din telefon til ladestikket.");
                    break;
                case IDisplay.GuideMessages.ReadRFID:
                    Console.WriteLine("Indlæs dit RFID.");
                    break;
                case IDisplay.GuideMessages.ConnError:
                    Console.WriteLine("Der opstod en fejl ved forbindelse af telefonen, prøv igen.");
                    break;
                case IDisplay.GuideMessages.Occupied:
                    Console.WriteLine("Ladeskabet er optaget.");
                    break;
                case IDisplay.GuideMessages.RFIDError:
                    Console.WriteLine("Forkerte RFID.");
                    break;
                case IDisplay.GuideMessages.RemovePhone:
                    Console.WriteLine("Fjern telefonen fra skabet.");
                    break;
            }
        }

        // Metoden til at sætte beskeden, samt konsol feedback til test programmet
        public void DisplayChargeMessage(IDisplay.ChargeMessages message)
        {
            ChargeStateMessage = message;

            switch (message)
            {
                case IDisplay.ChargeMessages.FullCharge:
                    Console.WriteLine("Telefonen er nu fuldt opladt.");
                    break;
                case IDisplay.ChargeMessages.Charging:
                    Console.WriteLine("Telefonen lader...");
                    break;
                case IDisplay.ChargeMessages.ChargeError:
                    Console.WriteLine("En fejl opstod under ladning, ladning er nu stoppet!");
                    break;
            }
        }
    }
}