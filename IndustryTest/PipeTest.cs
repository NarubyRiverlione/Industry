﻿using NUnit.Framework;

namespace IndustryLib.Test {
    [TestFixture]
    public class PipeTest {

        const string Name = "test pipe";
        const int Mark = 1;

        [Test]
        public void CreateMark_1() {
            Pipe testPipe = new Pipe(Name, Mark);
            Assert.AreEqual(testPipe.Name, Name);
            Assert.AreEqual(testPipe.Mark, Mark);
            Assert.AreEqual(testPipe.Volume, EquipmentCst.PipeBasicVolume);
            Assert.AreEqual(testPipe.Content, 0);
            Assert.AreEqual(testPipe.TypeOfEquipment, EquipmentCst.Types.Pipe);
        }
        [Test]
        public void CreateMark_4() {
            Pipe testPipe = new Pipe(Name, 4);
            Assert.AreEqual(testPipe.Name, Name);
            Assert.AreEqual(testPipe.Mark, 4);
            Assert.AreEqual(testPipe.Volume, 4 * EquipmentCst.PipeBasicVolume);
            Assert.AreEqual(testPipe.Content, 0);
            Assert.AreEqual(testPipe.TypeOfEquipment, EquipmentCst.Types.Pipe);
        }

        [Test]
        public void CreateWithContent() {
            Pipe testPipe = new Pipe(Name, Mark, EquipmentCst.PipeBasicVolume / 2);
            Assert.AreEqual(testPipe.Name, Name);
            Assert.AreEqual(testPipe.Mark, Mark);
            Assert.AreEqual(testPipe.Content, EquipmentCst.PipeBasicVolume / 2);
        }

        [Test]
        public void SetContent() {
            Pipe testPipe = new Pipe(Name, Mark);
            Assert.AreEqual(testPipe.Content, 0);
            testPipe.Fill(10);
            Assert.AreEqual(testPipe.Content, 10);
        }
        [Test]
        public void SetContentDontOverfill() {
            Pipe testPipe = new Pipe(Name, Mark);
            Assert.AreEqual(testPipe.Content, 0);
            testPipe.Fill(testPipe.Volume * 5);
            Assert.AreEqual(testPipe.Content, testPipe.Volume);
        }


        [Test]
        public void Failure() {
            Pipe pipeTest = new Pipe(Name, Mark);
            pipeTest.Fill(99);
            Assert.IsTrue(pipeTest.Operational);
            Assert.AreEqual(pipeTest.Content, 99);
            pipeTest.SetFailed();
            Assert.IsFalse(pipeTest.Operational);
            Assert.AreEqual(pipeTest.Content, 0);
        }
        [Test]
        public void Repair() {
            Pipe pipeTest = new Pipe("testPipe test", 1);
            Assert.IsTrue(pipeTest.Operational);
            pipeTest.SetFailed();
            Assert.IsFalse(pipeTest.Operational);
            pipeTest.SetRepaired();
            Assert.IsTrue(pipeTest.Operational);
        }

        [Test]
        public void AddFromConnectedPipe() {
            Pipe pipeOne = new Pipe("testPipe IN", 1, 70);
            Assert.AreEqual(pipeOne.Content, 70);

            Pipe pipeTest = new Pipe("testPipe test", 1);
            pipeTest.AddConnection(pipeOne);

            pipeTest.BalanceConnections();
            Assert.AreEqual(pipeOne.Content, 35);
            Assert.AreEqual(pipeTest.Content, 35);
        }

        [Test]
        public void SendToConnectedPipe() {
            Pipe pipeOne = new Pipe("testPipe IN", 1, 10);
            Assert.AreEqual(pipeOne.Content, 10);

            Pipe pipeTest = new Pipe("testPipe test", 1, 80);
            Assert.AreEqual(pipeTest.Content, 80);
            pipeTest.AddConnection(pipeOne);

            pipeTest.BalanceConnections();
            Assert.AreEqual(pipeOne.Content, 45);
            Assert.AreEqual(pipeTest.Content, 45);
        }

        [Test]
        public void FromGreaterPipe() {
            Pipe pipeOne = new Pipe("testPipe IN", 2, 150);
            Assert.AreEqual(pipeOne.Content, 150);
            Assert.AreEqual(pipeOne.Mark, 2);

            Pipe pipeTest = new Pipe("testPipe test", 1);
            Assert.AreEqual(pipeTest.Content, 0);
            pipeTest.AddConnection(pipeOne);

            pipeTest.BalanceConnections();
            Assert.AreEqual(pipeOne.Content, 75);
            Assert.AreEqual(pipeTest.Content, 75);
        }
        [Test]
        public void FromGreaterPipeDontOverFill() {
            Pipe pipeOne = new Pipe("Greater pipe 300", 3, 300);
            Assert.AreEqual(pipeOne.Content, 300);
            Assert.AreEqual(pipeOne.Mark, 3);

            Pipe pipeTest = new Pipe("normal pipe 100", 1);
            Assert.AreEqual(pipeTest.Content, 0);
            pipeTest.AddConnection(pipeOne);

            pipeTest.BalanceConnections();
            Assert.AreEqual(pipeOne.Content, 200);
            Assert.AreEqual(pipeTest.Content, 100);
        }

    }

}

