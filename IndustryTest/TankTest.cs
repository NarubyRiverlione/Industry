using NUnit.Framework;namespace IndustryLib.Test {    [TestFixture]    public class TankTest {        [Test]        public void Create() {            const string Name = "TestTank";            const int Volume = 1234567;            Tank tank = new Tank(Name, Volume);            Assert.AreEqual(tank.Name, Name);            Assert.IsTrue(tank.Operational);            Assert.AreEqual(tank.TypeOfEquipment, "Tank");            Assert.AreEqual(tank.Volume, Volume);            Assert.AreEqual(tank.Content, 0);            Assert.AreEqual(tank.Temperature, 20);            Assert.IsFalse(tank.HeatingOn);            Assert.IsFalse(tank.FillingNow);        }

        /*        [Test]        public void CreateWithContent() {            const string Name = "TestTank";            const int Volume = 1234567;            const int Content = 99;            Tank tank = new Tank(Name, Volume, Content);            Assert.AreEqual(tank.Name, Name);            Assert.IsTrue(tank.Operational);            Assert.AreEqual(tank.TypeOfEquipment, "Tank");            Assert.AreEqual(tank.Content, Content);            Assert.AreEqual(tank.Volume, Volume);            Assert.AreEqual(tank.Temperature, 20);            Assert.IsFalse(tank.HeatingOn);            Assert.IsFalse(tank.FillingNow);        }        [Test]        public void CreateWithContentAndTemp() {            const string Name = "TestTank";            const int Volume = 1234567;            const int Content = 99;            const int Temp = 656;            Tank tank = new Tank(Name, Volume, Content, Temp);            Assert.AreEqual(tank.Name, Name);            Assert.IsTrue(tank.Operational);            Assert.AreEqual(tank.TypeOfEquipment, "Tank");            Assert.AreEqual(tank.Volume, Volume);            Assert.AreEqual(tank.Content, Content);            Assert.AreEqual(tank.Temperature, Temp);            Assert.IsFalse(tank.HeatingOn);            Assert.IsFalse(tank.FillingNow);        }        [Test]        public void CreateDontOverflow() {            const string Name = "TestTank";            const int Volume = 100;            const int Content = 200;            Tank tank = new Tank(Name, Volume, Content);            Assert.AreEqual(tank.Name, Name);            Assert.IsTrue(tank.Operational);            Assert.AreEqual(tank.TypeOfEquipment, "Tank");            Assert.AreEqual(tank.Volume, Volume);            Assert.AreEqual(tank.Content, Volume);            Assert.AreEqual(tank.Temperature, 20);            Assert.IsFalse(tank.HeatingOn);            Assert.IsFalse(tank.FillingNow);        }        */
        [Test]        public void Fill() {            const string Name = "TestTank";            const int Volume = 100;            Tank tank = new Tank(Name, Volume);            Assert.AreEqual(tank.Content, 0);            tank.Fill(20);            Assert.AreEqual(tank.Content, 20);        }        [Test]        public void FillDontOverfloww() {            const string Name = "TestTank";            const int Volume = 100;            Tank tank = new Tank(Name, Volume);            Assert.AreEqual(tank.Content, 0);            tank.Fill(200);            Assert.AreEqual(tank.Content, Volume);        }        [Test]        public void ToStringComplete() {            const string Name = "TestTank";            const int Volume = 1234567;            const int Content = 99;            const int Temp = 656;            const bool Operational = true;
            Tank tank = new Tank(Name, Volume) {
                HeatingOn = true,
                FillingNow = true,
                Temperature = Temp
            };
            tank.Fill(Content);

            string output = tank.ToString();            string correct = "Tank " + Name + " has volume of " + Volume.ToString() + " liter and is " + (Operational ? "operational" : "not operational");
            correct += " and a content of " + Content + " liters on a temperature of " + Temp + " °C";
            correct += " and is currently heating up";
            correct += " and is filling up";
            Assert.AreEqual(output, correct);
        }

        [Test]
        public void ToStringEmpty() {            const string Name = "TestTank";            const int Volume = 1234567;
            Tank tank = new Tank(Name, Volume) ;

            string output = tank.ToString();            string correct = "Tank " + Name + " has volume of " + Volume.ToString() + " liter and is operational";
            correct += " and is empty";
            Assert.AreEqual(output, correct);
        }

        [Test]
        public void ToStringEmptyHeating() {            const string Name = "TestTank";            const int Volume = 1234567;            const int Temp = 656;
            Tank tank = new Tank(Name, Volume) {
                HeatingOn = true,
                Temperature = Temp
            };
            string output = tank.ToString();            string correct = "Tank " + Name + " has volume of " + Volume.ToString() + " liter and is operational";
            correct += " and is empty";
            correct += " and is currently heating up";
            Assert.AreEqual(output, correct);
        }
        [Test]        public void Failure() {
            const string Name = "TestTank";            const int Volume = 1234567;            const int Content = 100;            const int Temp = 656;
            Tank tank = new Tank(Name, Volume) {
                HeatingOn = true,
                FillingNow = true,
                Temperature = Temp
            };
            tank.Fill(Content);
            Assert.IsTrue(tank.Operational);
            Assert.AreEqual(tank.Content, Content);
            tank.SetFailed();
            Assert.IsFalse(tank.Operational);
            Assert.AreEqual(tank.Content, 0);
        }

        [Test]        public void Repaired() {
            const string Name = "TestTank";            const int Volume = 1234567;
            Tank tank = new Tank(Name, Volume);
            Assert.IsTrue(tank.Operational);
            tank.SetFailed();
            Assert.IsFalse(tank.Operational);
            tank.SetRepaired();
            Assert.IsTrue(tank.Operational);
        }    }}