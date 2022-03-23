using System;
using Ladeskab;
using NSubstitute.ReceivedExtensions;
using NuGet.Frameworks;
using NUnit.Framework;

namespace LadeStation.NUnit.test
{
    
    public class TestRfidReader
    {
        private RfidReader _uut;
        private Ladeskab.DTRfidReaderEvent _dtRfidReaderEvent;

        [SetUp]
        public void Setup()
        {
            _dtRfidReaderEvent = null;
            _uut = new RfidReader();
        }
        
        [Test]
        public void RfidEventId20_equals20()
        {
            _uut.RfidChangedEvent += (o, args)
                => { _dtRfidReaderEvent = args; };

            int id = 20;
            _uut.RfidDetected(id);
            Assert.That(() => _dtRfidReaderEvent.RfidId, Is.EqualTo(20));
        }

        [Test]
        public void RfidEventNobodyIslisting()
        {
            int id = 20;
            _uut.RfidDetected(id);
            Assert.That(() => _dtRfidReaderEvent.RfidId, Throws.TypeOf<NullReferenceException>());
        }

        [Test]
        public void RfidEventIdMinus20_equalsNull()
        {
            _uut.RfidChangedEvent += (o, args)
                => { _dtRfidReaderEvent = args; };
            int id = -20;
            _uut.RfidDetected(id);
            Assert.That(() => _dtRfidReaderEvent.RfidId, Throws.TypeOf<NullReferenceException>());
        }

        [Test]
        public void RfidEventId20ButChangedTo10_equals10()
        {
            _uut.RfidChangedEvent += (o, args)
                => { _dtRfidReaderEvent = args; };
            int id = 20;
            id = 10;
            _uut.RfidDetected(id);
            Assert.That(() => _dtRfidReaderEvent.RfidId, Is.EqualTo(10));
        }

    }
}