using NUnit.Framework;

        /*
        [Test]
            Tank tank = new Tank(Name, Volume) {
                HeatingOn = true,
                FillingNow = true,
                Temperature = Temp
            };
            tank.Fill(Content);

            string output = tank.ToString();
            correct += " and a content of " + Content + " liters on a temperature of " + Temp + " �C";
            correct += " and is currently heating up";
            correct += " and is filling up";
            Assert.AreEqual(output, correct);
        }

        [Test]
        public void ToStringEmpty() {
            Tank tank = new Tank(Name, Volume) ;

            string output = tank.ToString();
            correct += " and is empty";
            Assert.AreEqual(output, correct);
        }

        [Test]
        public void ToStringEmptyHeating() {
            Tank tank = new Tank(Name, Volume) {
                HeatingOn = true,
                Temperature = Temp
            };
            string output = tank.ToString();
            correct += " and is empty";
            correct += " and is currently heating up";
            Assert.AreEqual(output, correct);
        }

            const string Name = "TestTank";
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

        [Test]
            const string Name = "TestTank";
            Tank tank = new Tank(Name, Volume);
            Assert.IsTrue(tank.Operational);
            tank.SetFailed();
            Assert.IsFalse(tank.Operational);
            tank.SetRepaired();
            Assert.IsTrue(tank.Operational);
        }