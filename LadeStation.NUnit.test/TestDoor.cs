using System;
using Ladeskab;
using NuGet.Frameworks;
using NUnit.Framework;

namespace LadeStation.NUnit.test
{
    public class TestDoor
    {
        private Door _uut;
        private Ladeskab.DTDoorOpenCloseEvent _doorOpenCloseEvent;

        [SetUp]
        public void Setup()
        {
            _doorOpenCloseEvent = null;
            _uut = new Door();
        }

        [Test] 
        public void DoorCloseEventNobodyListing()
        {
            _uut.OnDoorOpen();
            Assert.That(() => _doorOpenCloseEvent.doorOpen, Throws.TypeOf<NullReferenceException>());
        }

        [Test]
        public void DoorEventNotNull()
        {
            _uut.DoorChangedEvent += (o, args)
                => { _doorOpenCloseEvent = args; };
            _uut.OnDoorOpen();
            Assert.That(_doorOpenCloseEvent, Is.Not.Null);
        }

        [Test]
        public void DoorEventDoorOpenEqualTrue()
        {
            _uut.DoorChangedEvent += (o, args)
                => { _doorOpenCloseEvent = args; };
            _uut.OnDoorOpen();
            Assert.That(_doorOpenCloseEvent.doorOpen, Is.True);
        }

        [Test]
        public void DoorEventDoorOpenCloseEqualFalse()
        {
            _uut.DoorChangedEvent += (o, args)
                => { _doorOpenCloseEvent = args; };
            _uut.OnDoorOpen();
            _uut.OnDoorClose();
            Assert.That(_doorOpenCloseEvent.doorOpen, Is.False);
        }

        [Test]
        public void DoorEventCheckCloseDefaultPositionEqualsNullReference()
        {
            _uut.DoorChangedEvent += (o, args)
                => { _doorOpenCloseEvent = args; };
            _uut.OnDoorClose();
            Assert.That(() => _doorOpenCloseEvent.doorOpen, Throws.TypeOf<NullReferenceException>());
        }

        [Test]
        public void DoorEventAfterLockAndUnlockEqualTrue()
        {
            _uut.DoorChangedEvent += (o, args)
                => { _doorOpenCloseEvent = args; };
            _uut.LockDoor();
            _uut.OnDoorOpen();
            _uut.UnlockDoor();
            _uut.OnDoorOpen();
            Assert.That(_doorOpenCloseEvent.doorOpen, Is.True);
        }

        [Test]
        public void DoorEventMessageNotReceivedAfterLockedDoor()
        {
            _uut.DoorChangedEvent += (o, args)
                => { _doorOpenCloseEvent = args; };
            _uut.LockDoor();
            _uut.OnDoorOpen();

            Assert.That(() => _doorOpenCloseEvent.doorOpen, Throws.TypeOf<NullReferenceException>());
        }

        [Test]
        public void DoorEventUnlockAndCloseEqualNullReference()
        {
            _uut.DoorChangedEvent += (o, args)
                => { _doorOpenCloseEvent = args; };
            _uut.UnlockDoor();
            _uut.OnDoorClose();

            Assert.That(() => _doorOpenCloseEvent.doorOpen, Throws.TypeOf<NullReferenceException>());
        }

        [Test]
        public void DoorEventCheckForUnlockStateEqualsNullReference()
        {
            _uut.DoorChangedEvent += (o, args)
                => { _doorOpenCloseEvent = args; };
            _uut.UnlockDoor();
            Assert.That(() => _doorOpenCloseEvent.doorOpen, Throws.TypeOf<NullReferenceException>());
        }
    }
}