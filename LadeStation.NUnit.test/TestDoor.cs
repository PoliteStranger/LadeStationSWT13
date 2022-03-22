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
        public void DoorEventCheckCloseDefaultPositionEqualsNullReference()
        {
            _uut.OnDoorClose();
            Assert.That(() => _doorOpenCloseEvent.doorOpen, Throws.TypeOf<NullReferenceException>());
        }

        [Test]
        public void DoorEventAfterLockAndUnlockEqualTrue()
        {
            _uut.DoorLock();
            _uut.OnDoorOpen();
            _uut.DoorUnLock();
            _uut.OnDoorOpen();
            Assert.That(_doorOpenCloseEvent.doorOpen, Is.True);
        }

        [Test]
        public void DoorEventMessageNotReceivedAfterLockedDoor()
        {
            _uut.DoorLock();
            _uut.OnDoorOpen();

            Assert.That(() => _doorOpenCloseEvent.doorOpen, Throws.TypeOf<NullReferenceException>());
        }

        [Test]
        public void DoorEventUnlockAndCloseEqualNullReference()
        {
            _uut.DoorUnLock();
            _uut.OnDoorClose();

            Assert.That(() => _doorOpenCloseEvent.doorOpen, Throws.TypeOf<NullReferenceException>());
        }

        

        [Test]
        public void DoorEventCheckForUnlockStateEqualsNullReference()
        {
            _uut.DoorUnLock();
            Assert.That(() => _doorOpenCloseEvent.doorOpen, Throws.TypeOf<NullReferenceException>());
        }


    }
}