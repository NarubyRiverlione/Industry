using NUnit.Framework;namespace IndustryLib.Test {    [TestFixture]
    public class ValveTest {

        [Test]
        public void TwoPipeOpenValve() {
            Pipe startPipe = new Pipe("start pipe", 1, 100);
            Valve valve = new Valve("test valve");
            Pipe endPipe = new Pipe("end pipe", 1, 0);
            Assert.IsTrue(valve.Open);

            startPipe.AddConnection(valve);
            valve.AddConnection(endPipe);

            Factory fac = new Factory();
            fac.AddEquipment(startPipe);
            fac.AddEquipment(valve);
            fac.AddEquipment(endPipe);

            Assert.AreEqual(100, startPipe.Content);
            Assert.AreEqual(0, valve.Content);
            Assert.AreEqual(0, endPipe.Content);

            fac.BalanceContents();
            Assert.AreEqual(50, startPipe.Content);
            Assert.AreEqual(25, valve.Content);
            Assert.AreEqual(25, endPipe.Content);
        }
        [Test]
        public void TwoPipeClosedValve() {
            Pipe startPipe = new Pipe("start pipe", 1, 100);
            Valve valve = new Valve("test valve", 1, 0, false);
            Pipe endPipe = new Pipe("end pipe", 1, 0);

            Assert.IsFalse(valve.Open);

            startPipe.AddConnection(valve);
            valve.AddConnection(endPipe);

            Factory fac = new Factory();
            fac.AddEquipment(startPipe);
            fac.AddEquipment(valve);
            fac.AddEquipment(endPipe);

            Assert.AreEqual(100, startPipe.Content);
            Assert.AreEqual(0, valve.Content);
            Assert.AreEqual(0, endPipe.Content);

            fac.BalanceContents();
            Assert.AreEqual(100, startPipe.Content);
            Assert.AreEqual(0, valve.Content);
            Assert.AreEqual(0, endPipe.Content);
        }

        [Test]
        public void TwoPipeCloseValveAfterCreation() {
            // close valve after creation
            Pipe startPipe = new Pipe("start pipe", 1, 100);
            Valve valve = new Valve("test valve");
            Pipe endPipe = new Pipe("end pipe", 1, 0);

            Assert.IsTrue(valve.Open);
            valve.SetClosed();
            Assert.IsFalse(valve.Open);

            startPipe.AddConnection(valve);
            valve.AddConnection(endPipe);

            Factory fac = new Factory();
            fac.AddEquipment(startPipe);
            fac.AddEquipment(valve);
            fac.AddEquipment(endPipe);

            Assert.AreEqual(100, startPipe.Content);
            Assert.AreEqual(0, valve.Content);
            Assert.AreEqual(0, endPipe.Content);

            fac.BalanceContents();
            Assert.AreEqual(100, startPipe.Content);
            Assert.AreEqual(0, valve.Content);
            Assert.AreEqual(0, endPipe.Content);
        }

        [Test]
        public void Reopen() {
            Valve valve = new Valve("test valve");

            Assert.IsTrue(valve.Open);
            valve.SetClosed();
            Assert.IsFalse(valve.Open);
            valve.SetOpen();
            Assert.IsTrue(valve.Open);
        }
    }
}