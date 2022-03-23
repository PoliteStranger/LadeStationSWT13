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
        Assert.That(uut.State, Is.EqualTo(StationControl.LadeskabState.Locked));

    }

    [Test]
    public void RfidUpdate_NewId_RfidEventFromAvailableState_ChargerNotConnected()
    {
        //make sure charger isnt connected
        _chargeController.Connected = false;
        //raise rfid event
        _reader.RfidChangedEvent += Raise.EventWith(new DTRfidReaderEvent{RfidId = 1234});

        //assertions
        _display.Received(1).DisplayGuideMessage(IDisplay.GuideMessages.ConnError);
        Assert.That(uut.State, Is.EqualTo(StationControl.LadeskabState.Available));

    }
}