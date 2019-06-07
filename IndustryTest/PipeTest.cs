using NUnit.Framework;
namespace IndustryLib.Test {
    [TestFixture]
    public class PipeTest {
        [Test]
        public void Create() {
            const string Name = "test pipe";
            const int Volume = 123;
            Pipe pipe = new Pipe(Name, Volume);
            Assert.AreEqual(pipe.Name, Name);
            Assert.AreEqual(pipe.Volume, Volume);
            Assert.AreEqual(pipe.Content, 0);
            Assert.AreEqual(pipe.TypeOfEquipment, "Pipe");
        }

        [Test]
        public void SetContent() {
            const string Name = "test pipe";
            const int Volume = 123;
            Pipe pipe = new Pipe(Name, Volume);
            Assert.AreEqual(pipe.Content, 0);
            pipe.Fill(10);
            Assert.AreEqual(pipe.Content, 10);
        }

        [Test]
        public void SetContentDontOverfill() {
            const string Name = "test pipe";
            const int Volume = 123;
            Pipe pipe = new Pipe(Name, Volume);
            Assert.AreEqual(pipe.Content, 0);
            pipe.Fill(200);
            Assert.AreEqual(pipe.Content, Volume);
        }

        [Test]
        public void AddFromInpunt() {
            Pipe pipeOne = new Pipe("pipe IN", 100);
            Pipe pipeTest = new Pipe("pipe test", 100);
            pipeOne.Fill(70);
            Assert.AreEqual(pipeOne.Content, 70);
            pipeTest.Connections.Add(pipeOne);
            pipeTest.BalanceConnections();
            Assert.AreEqual(pipeOne.Content, 35);
            Assert.AreEqual(pipeTest.Content, 35);
        }

        [Test]
        public void SendToInpunt() {
            Pipe pipeOne = new Pipe("pipe IN", 100);
            Pipe pipeTest = new Pipe("pipe test", 100);
            pipeOne.Fill(10);
            Assert.AreEqual(pipeOne.Content, 10);
            pipeTest.Fill(80);
            Assert.AreEqual(pipeTest.Content, 80);
            pipeTest.Connections.Add(pipeOne);
            pipeTest.BalanceConnections();
            Assert.AreEqual(pipeOne.Content, 45);
            Assert.AreEqual(pipeTest.Content, 45);
        }

        [Test]
        public void Failure() {
            Pipe pipeTest = new Pipe("pipe test", 100);
            pipeTest.Fill(99);
            Assert.IsTrue(pipeTest.Operational);
            Assert.AreEqual(pipeTest.Content, 99);
            pipeTest.SetFailed();
            Assert.IsFalse(pipeTest.Operational);
            Assert.AreEqual(pipeTest.Content, 0);
        }
        [Test]
        public void Repair() {
            Pipe pipeTest = new Pipe("pipe test", 100);
            Assert.IsTrue(pipeTest.Operational);
            pipeTest.SetFailed();
            Assert.IsFalse(pipeTest.Operational);
            pipeTest.SetRepaired();
            Assert.IsTrue(pipeTest.Operational);
        }
    }
}

