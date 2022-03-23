using NSubstitute;
using NUnit.Framework;

namespace Ladeskab.NUnit.test;

[TestFixture]
public class TestStationControl
{
    private StationControl uut;
    private IDisplay _display;
    private IRfidReader _reader;
    private ILogger _logger;
    private IChargeController _chargeController;
    private IDoor _door;

    [SetUp]
    public void setup()
    {
        _display = Substitute.For<IDisplay>();
        _reader = Substitute.For<IRfidReader>();
        _logger = Substitute.For<ILogger>();
        _chargeController = Substitute.For<IChargeController>();
        _door = Substitute.For<IDoor>();
        uut = new StationControl(_door, _chargeController, _display, _reader, _logger);
    }

    [Test]
    public void RfidUpdate_NewId_RfidEventFromAvailableState_ChargerConnected()
    {
        //connect charger
        _chargeController.Connected = true;
        //raise rfid event
        _reader.RfidChangedEvent += Raise.EventWith(new DTRfidReaderEvent{RfidId = 1234});

        //assertions
        Assert.Multiple(() =>
        {
            Assert.That(uut.State, Is.EqualTo(StationControl.LadeskabState.Locked));
            _display.Received(1).DisplayGuideMessage(IDisplay.GuideMessages.Occupied);
        });
    }

    [Test]
    public void RfidUpdate_NewId_RfidEventFromAvailableState_ChargerNotConnected()
    {
        //make sure charger isnt connected
        _chargeController.Connected = false;
        //raise rfid event
        _reader.RfidChangedEvent += Raise.EventWith(new DTRfidReaderEvent{RfidId = 1234});

        //assertions
        Assert.Multiple(() => 
        {
            _display.Received(1).DisplayGuideMessage(IDisplay.GuideMessages.ConnError);
            Assert.That(uut.State, Is.EqualTo(StationControl.LadeskabState.Available));
        });        
    }

    [Test]
    public void RfidUpdate_LockedState_CorrectId()
    {
        //make sure charger is connected
        _chargeController.Connected = true;
        //raise rfid event for saving id
        _reader.RfidChangedEvent += Raise.EventWith(new DTRfidReaderEvent{RfidId = 1234});

        
        //access ladestation with correct id
        _reader.RfidChangedEvent += Raise.EventWith(new DTRfidReaderEvent{RfidId = 1234});

        Assert.Multiple(() => 
        {
            _display.Received(1).DisplayGuideMessage(IDisplay.GuideMessages.RemovePhone);
            Assert.That(uut.State, Is.EqualTo(StationControl.LadeskabState.Available));
        });
    }

    [Test]
    public void RfidUpdate_LockedState_IncorrectId()
    {
        //make sure charger is connected
        _chargeController.Connected = true;
        //raise rfid event for saving id
        _reader.RfidChangedEvent += Raise.EventWith(new DTRfidReaderEvent{RfidId = 1234});

        
        //access ladestation with incorrect id
        _reader.RfidChangedEvent += Raise.EventWith(new DTRfidReaderEvent{RfidId = 2345});

        Assert.Multiple(() =>
        {
            _display.Received(1).DisplayGuideMessage(IDisplay.GuideMessages.RFIDError);
            Assert.That(uut.State, Is.Not.EqualTo(StationControl.LadeskabState.Available));
        });
    }

    [Test]
    public void RfidUpdate_OpenDoorState_NewIdButDoorIsOpen()
    {
        //open door
        _door.DoorChangedEvent += Raise.EventWith(new DTDoorOpenCloseEvent { doorOpen = true });

        //raise rfid event for saving id
        _reader.RfidChangedEvent += Raise.EventWith(new DTRfidReaderEvent { RfidId = 1234 });

        //assertions
        Assert.Multiple(() =>
        {
            _display.Received(1).DisplayGuideMessage(IDisplay.GuideMessages.ConnectPhone);
            Assert.That(uut.State, Is.EqualTo(StationControl.LadeskabState.DoorOpen));
        });
    }

    [Test]
    public void RfidUpdate_OpenDoorState_OpeningAndClosingChargerConnected()
    {
        _chargeController.Connected = true;

        //open door
        _door.DoorChangedEvent += Raise.EventWith(new DTDoorOpenCloseEvent { doorOpen = true });

        //close door
        _door.DoorChangedEvent += Raise.EventWith(new DTDoorOpenCloseEvent { doorOpen = false });

        //raise rfid event for saving id
        _reader.RfidChangedEvent += Raise.EventWith(new DTRfidReaderEvent { RfidId = 1234 });

        //assertions
        Assert.Multiple(() =>
        {
            _display.Received(1).DisplayGuideMessage(IDisplay.GuideMessages.ReadRFID);
            Assert.That(uut.State, Is.EqualTo(StationControl.LadeskabState.Locked));
        });
    }

    [Test]
    public void RfidUpdate_OpenDoorState_OpeningAndClosingChargerNotConnected()
    {
        _chargeController.Connected = false;

        //open door
        _door.DoorChangedEvent += Raise.EventWith(new DTDoorOpenCloseEvent { doorOpen = true });

        //close door
        _door.DoorChangedEvent += Raise.EventWith(new DTDoorOpenCloseEvent { doorOpen = false });

        //raise rfid event for saving id
        _reader.RfidChangedEvent += Raise.EventWith(new DTRfidReaderEvent { RfidId = 1234 });

        //assertions
        Assert.Multiple(() =>
        {
            _display.Received(1).DisplayGuideMessage(IDisplay.GuideMessages.Free);
            Assert.That(uut.State, Is.EqualTo(StationControl.LadeskabState.Available));
        });
    }


}