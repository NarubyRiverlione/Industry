using NUnit.Framework;

namespace IndustryLib.Test {
    [TestFixture]
    public class FactoryTest {

        [Test]
        public void Create() {
            Factory factory = new Factory();

            Assert.AreEqual(factory.EquipmentCount, 0);
        }


        [Test]
        public void AddTank() {
            Factory factory = new Factory();
            Assert.AreEqual(factory.EquipmentCount, 0);

            Tank emptyTank = new Tank("testTank", 123);

            factory.AddEquipment(emptyTank);
            Assert.AreEqual(factory.EquipmentCount, 1);

            Equipment firstEquipment = factory.GetEquipment()[0];
            Assert.AreEqual(firstEquipment, emptyTank);
        }
    }
}