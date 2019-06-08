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
        [Test]
        public void AddPipe() {
            Factory factory = new Factory();
            Assert.AreEqual(factory.EquipmentCount, 0);

            Pipe testPipe = new Pipe("First", 1);

            factory.AddEquipment(testPipe);
            Assert.AreEqual(factory.EquipmentCount, 1);

            Equipment firstEquipment = factory.GetEquipment()[0];
            Assert.AreEqual(firstEquipment, testPipe);
        }
        [Test]
        public void GetEquipmentByName() {
            const string TankName = "testTank";
            const string PipeName = "First Pipe";
            const string Pipe2Name = "Other pipe";

            Factory factory = new Factory();
            Assert.AreEqual(factory.EquipmentCount, 0);

            Pipe testPipe = new Pipe(PipeName, 1);
            factory.AddEquipment(testPipe);
            Pipe otherPipe = new Pipe(Pipe2Name, 5);
            factory.AddEquipment(otherPipe);

            Tank emptyTank = new Tank(TankName, 123);
            factory.AddEquipment(emptyTank);

            Equipment foundTank = factory.GetEquipmentByName(TankName);
            Assert.AreEqual(foundTank.TypeOfEquipment, EquipmentCst.TankType);
            Assert.AreEqual(foundTank.Name, TankName);

            Equipment foundPipe = factory.GetEquipmentByName(PipeName);
            Assert.AreEqual(foundPipe.TypeOfEquipment, EquipmentCst.PipeType);
            Assert.AreEqual(foundPipe.Name, PipeName);
            Assert.AreEqual(foundPipe.Mark, 1);
        }

    }

    [TestFixture]
    public class FlowTest {
        Factory factory;
        const string Pipe1Name = "Firs Pipe";
        const string Pipe2Name = "Second Pipe";
        const string Pipe3Name = "Thirt Pipe";


        [SetUp]
        public void Pipes() {
            Pipe pipe1 = new Pipe(Pipe1Name, 1);
            Pipe pipe2 = new Pipe(Pipe2Name, 1);
            Pipe pipe3 = new Pipe(Pipe3Name, 1);

            pipe1.Connections.Add(pipe2);
            pipe2.Connections.Add(pipe1);
            pipe2.Connections.Add(pipe3);
            pipe3.Connections.Add(pipe2);

            factory = new Factory();
            factory.AddEquipment(pipe1);
            factory.AddEquipment(pipe2);
            factory.AddEquipment(pipe3);
        }
        private void TestPipeFlow() {
            Pipe startPipe = (Pipe)factory.GetEquipmentByName(Pipe1Name);
            Pipe middlePipe = (Pipe)factory.GetEquipmentByName(Pipe2Name);
            Pipe endPipe = (Pipe)factory.GetEquipmentByName(Pipe3Name);

            int pipe1Content = startPipe.Content;
            int pipe2Content = middlePipe.Content;
            int pipe3Content = endPipe.Content;

            factory.BalanceContents();


            int expectPipe1 = pipe1Content - (pipe1Content - pipe2Content) / EquipmentCst.BalanceFactor;

            int expectPipe2 = pipe2Content + (pipe1Content - pipe2Content) / EquipmentCst.BalanceFactor;

            int expectPipe3 = pipe3Content + (expectPipe2 - pipe3Content) / EquipmentCst.BalanceFactor;
            expectPipe2 = expectPipe2 - (expectPipe2 - pipe3Content) / EquipmentCst.BalanceFactor;

            Assert.AreEqual(startPipe.Content, expectPipe1);
            Assert.AreEqual(middlePipe.Content, expectPipe2);
            Assert.AreEqual(endPipe.Content, expectPipe3);
        }

        [Test]
        // pipe 1 is full, 2 & 3 empty
        public void PipeStartToEnd() {
            // 100,0,0
            Pipe startPipe = (Pipe)factory.GetEquipmentByName(Pipe1Name);
            startPipe.Fill(startPipe.Volume);
            Assert.AreEqual(startPipe.Content, startPipe.Volume);

            Pipe middlePipe = (Pipe)factory.GetEquipmentByName(Pipe2Name);
            Assert.AreEqual(middlePipe.Content, 0);

            Pipe endPipe = (Pipe)factory.GetEquipmentByName(Pipe3Name);
            Assert.AreEqual(endPipe.Content, 0);

            // pipe 1 balance  -> 2 : 50,50,0
            // pipe 2, balance -> 3 : 50,25,25
            TestPipeFlow();

            // pipe 1 balance  -> 2 : 38,37,25
            // pipe 2, balance -> 3 : 38,31,31
            TestPipeFlow();

            // pipe 1 balance  -> 2 : 35,34,25
            // pipe 2, balance -> 3 : 35,33,32
            TestPipeFlow();

            // pipe 1 balance  -> 2 : 34,34,32
            // pipe 2, balance -> 3 : 34,33,33
            TestPipeFlow();
        }


        // pipe 1  & 2 empty, 3 full
        [Test]
        public void PipeEndToStart() {
            // 0,0,100
            Pipe startPipe = (Pipe)factory.GetEquipmentByName(Pipe1Name);
            Assert.AreEqual(startPipe.Content, 0);

            Pipe middlePipe = (Pipe)factory.GetEquipmentByName(Pipe2Name);
            Assert.AreEqual(middlePipe.Content, 0);

            Pipe endPipe = (Pipe)factory.GetEquipmentByName(Pipe3Name);
            endPipe.Fill(endPipe.Volume);
            Assert.AreEqual(endPipe.Content, endPipe.Volume);


            // pipe 1 balance  -> 2 :  0,0,100
            // pipe 2, balance -> 3 : 0,50,50
            TestPipeFlow();

            // pipe 1 balance  -> 2 : 25,25,50
            // pipe 2, balance -> 3 : 25,37,38
            TestPipeFlow();

            // pipe 1 balance  -> 2 : 25,37,38
            // pipe 2, balance -> 3 : 31,34,35
            TestPipeFlow();

            // pipe 1 balance  -> 2 : 31,34,34
            // pipe 2, balance -> 3 : 32,34,34
            TestPipeFlow();

            // pipe 1 balance  -> 2 : 31,34,34
            // pipe 2, balance -> 3 : 33,33,34
            TestPipeFlow();
        }

        // pipe 1  & 3 empty, 2 full
        [Test]
        public void PipeMiddle() {
            // 0,0,100
            Pipe startPipe = (Pipe)factory.GetEquipmentByName(Pipe1Name);
            Assert.AreEqual(startPipe.Content, 0);

            Pipe middlePipe = (Pipe)factory.GetEquipmentByName(Pipe2Name);
            middlePipe.Fill(middlePipe.Volume);
            Assert.AreEqual(middlePipe.Content, middlePipe.Volume);

            Pipe endPipe = (Pipe)factory.GetEquipmentByName(Pipe3Name);
            Assert.AreEqual(endPipe.Content, 0);


            // pipe 1 balance  -> 2 : 50,50,0
            // pipe 2, balance -> 3 : 50,25,25
            TestPipeFlow();

            // pipe 1 balance  -> 2 : 38,37,25
            // pipe 2, balance -> 3 : 38,31,31
            TestPipeFlow();

            // pipe 1 balance  -> 2 : 34,34,31
            // pipe 2, balance -> 3 : 35,32,32
            TestPipeFlow();

            // pipe 1 balance  -> 2 : 34,33,31
            // pipe 2, balance -> 3 : 34,33,33
            TestPipeFlow();
        }

        // pipe 1 : 25, pipe 2 = 10, pipe 3 = 75
        [Test]
        public void PipeBalanceAll() {
            Pipe startPipe = (Pipe)factory.GetEquipmentByName(Pipe1Name);
            startPipe.Fill(25);
            Assert.AreEqual(startPipe.Content, 25);

            Pipe middlePipe = (Pipe)factory.GetEquipmentByName(Pipe2Name);
            middlePipe.Fill(10);
            Assert.AreEqual(middlePipe.Content, 10);

            Pipe endPipe = (Pipe)factory.GetEquipmentByName(Pipe3Name);
            endPipe.Fill(75);
            Assert.AreEqual(endPipe.Content, 75);

            TestPipeFlow(); // 18,46,46
            TestPipeFlow(); // 32,39,39
            TestPipeFlow(); // 35,37,37
            TestPipeFlow(); // 36,37,37
        }

        //  factory with one tank - pipe 1 - pipe 2 -pipe 3
        [Test]
        public void TankTo3pipes() {
            const int tankStartContent = 800;
            Tank tank = new Tank("test tank", 1000, tankStartContent);

            Pipe pipeAtTank = new Pipe("pipeAtTank", 1);
            Pipe pipeMiddle = new Pipe("pipeMiddle", 1);
            Pipe pipeEnd = new Pipe("pipeEnd", 1);

            pipeAtTank.Connections.Add(pipeMiddle);
            pipeAtTank.Connections.Add(tank);

            pipeMiddle.Connections.Add(pipeAtTank);
            pipeMiddle.Connections.Add(pipeEnd);

            pipeEnd.Connections.Add(pipeMiddle);

            Factory facWithTank = new Factory();
            facWithTank.AddEquipment(tank);
            facWithTank.AddEquipment(pipeAtTank);
            facWithTank.AddEquipment(pipeMiddle);
            facWithTank.AddEquipment(pipeEnd);

            Assert.AreEqual(tank.Content, tankStartContent);
            Assert.AreEqual(pipeAtTank.Content, 0);
            Assert.AreEqual(pipeMiddle.Content, 0);
            Assert.AreEqual(pipeEnd.Content, 0);

            facWithTank.BalanceContents(); // 700,50,25,25
            Assert.AreEqual(tank.Content, tankStartContent - pipeAtTank.Volume);
            Assert.AreEqual(pipeAtTank.Content, pipeAtTank.Volume / EquipmentCst.BalanceFactor);
            Assert.AreEqual(pipeMiddle.Content, pipeMiddle.Volume / (EquipmentCst.BalanceFactor * 2));
            Assert.AreEqual(pipeEnd.Content, pipeEnd.Volume / (EquipmentCst.BalanceFactor * 2));

            int count = 0;
            do {
                facWithTank.BalanceContents();
                count++;
            } while (pipeEnd.Content < 98);

            Assert.AreEqual(tank.Content, 505);
            Assert.AreEqual(pipeAtTank.Content, 99);
            Assert.AreEqual(pipeMiddle.Content, 98);
            Assert.AreEqual(pipeEnd.Content, 98);
            Assert.AreEqual(count, 11);

        }


        [Test]

        public void PipesToFillTank() {       // factory with  pipe 1 : 100 - pipe 2 : 100, tank : empty
            const int tankStartContent = 0;
            Tank tank = new Tank("test tank", 1000, tankStartContent);

            Pipe pipeStart = new Pipe("pipeStart", 1, 100);
            Pipe pipeAtTank = new Pipe("pipeAtTank", 1, 100);

            pipeStart.Connections.Add(pipeAtTank);
            pipeAtTank.Connections.Add(tank);

            Factory facWithTank = new Factory();
            facWithTank.AddEquipment(tank);
            facWithTank.AddEquipment(pipeAtTank);
            facWithTank.AddEquipment(pipeStart);


            Assert.AreEqual(tank.Content, tankStartContent);
            Assert.AreEqual(pipeAtTank.Content, 100);
            Assert.AreEqual(pipeStart.Content, 100);


            facWithTank.BalanceContents();
            Assert.AreEqual(tank.Content, 50);
            Assert.AreEqual(pipeAtTank.Content, 75);
            Assert.AreEqual(pipeStart.Content, 75);

            int count = 0;
            do {
                facWithTank.BalanceContents();
                count++;
            } while (tank.Content < 65);

            Assert.AreEqual(pipeAtTank.Content, 67);
            Assert.AreEqual(pipeStart.Content, 68);
            Assert.AreEqual(count, 2);

        }
    }
}
