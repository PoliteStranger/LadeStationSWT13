using System;
using Ladeskab;
using NuGet.Frameworks;
using NUnit.Framework;

namespace LadeStation.NUnit.test
{
    public class Tests
    {
        private Door _uut;
        private Ladeskab.DTDoorOpenCloseEvent _doorOpenCloseEvent;


        [SetUp]
        public void Setup()
        {
            _doorOpenCloseEvent = null;
            _uut = new Door();
            _uut.DoorChangedEvent += (o, args) 
                => { _doorOpenCloseEvent = args; };
        }

        [Test]
        public void DoorEventNotNull()
        {
            _uut.OnDoorOpen();
            Assert.That(_doorOpenCloseEvent, Is.Not.Null);
        }

        [Test]
        public void DoorEventDoorOpenEqualTrue()
        {
            _uut.OnDoorOpen();
            Assert.That(_doorOpenCloseEvent.doorOpen, Is.True);
        }

        [Test]
        public void DoorEventDoorOpenCloseEqualFalse()
        {
            _uut.OnDoorOpen();
            _uut.OnDoorClose();
            Assert.That(_doorOpenCloseEvent.doorOpen, Is.False);
        }

        [Test]
        public void DoorEventMessageNotReceivedAfterLockedDoor()
        {
            _uut.DoorLock();
            _uut.OnDoorOpen();

            Assert.That(() => _doorOpenCloseEvent.doorOpen, Throws.TypeOf<NullReferenceException>());
        }

    }
}