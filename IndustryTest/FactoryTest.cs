using NUnit.Framework;

namespace IndustryLib.Test {
    [TestFixture]
    public class FactoryTest {

        [Test]
        public void Create() {
            Factory factory = new Factory();

            Assert.AreEqual(factory.EquipmentCount, 0);
            Assert.IsEmpty(factory.GetEquipment());
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
        public void AddSource() {
            Factory factory = new Factory();
            Assert.AreEqual(factory.EquipmentCount, 0);

            Source source = new Source("source test");
            factory.AddEquipment(source);
            Assert.AreEqual(factory.EquipmentCount, 1);

            Equipment firstEquipment = factory.GetEquipment()[0];
            Assert.AreEqual(firstEquipment, source);
        }

        [Test]
        public void ConnectTankPipe() {
            Tank emptyTank = new Tank("testTank", 123);
            Pipe testPipe = new Pipe("First", 1);

            emptyTank.AddConnection(testPipe);
            Assert.AreEqual(testPipe, emptyTank.GetConnections()[0]);
            Assert.AreEqual(emptyTank, testPipe.GetConnections()[0]);

            Factory factory = new Factory();
            factory.AddEquipment(testPipe);
            factory.AddEquipment(emptyTank);
        }
        [Test]
        public void GetEquipmentByName() {
            const string TankName = "testTank";
            const string PipeName = "First Pipe";
            const string Pipe2Name = "Other pipe";
            const string SourceName = "Source Test";

            Factory factory = new Factory();
            Assert.AreEqual(factory.EquipmentCount, 0);

            Pipe testPipe = new Pipe(PipeName, 1);
            factory.AddEquipment(testPipe);
            Pipe otherPipe = new Pipe(Pipe2Name, 5);
            factory.AddEquipment(otherPipe);

            Tank emptyTank = new Tank(TankName, 123);
            factory.AddEquipment(emptyTank);

            Source source = new Source(SourceName);
            factory.AddEquipment(source);

            Equipment foundTank = factory.GetEquipmentByName(TankName);
            Assert.AreEqual(foundTank.TypeOfEquipment, EquipmentCst.Types.Tank);
            Assert.AreEqual(foundTank.Name, TankName);

            Equipment foundPipe = factory.GetEquipmentByName(PipeName);
            Assert.AreEqual(foundPipe.TypeOfEquipment, EquipmentCst.Types.Pipe);
            Assert.AreEqual(foundPipe.Name, PipeName);
            Assert.AreEqual(foundPipe.Mark, 1);

            Equipment foundSource = factory.GetEquipmentByName(SourceName);
            Assert.AreEqual(foundSource.TypeOfEquipment, EquipmentCst.Types.Source);
            Assert.AreEqual(foundSource.Name, SourceName);
        }

    }

    [TestFixture]
    public class FlowTest {
        Factory factory;
        const string Pipe1Name = "Firs Pipe";
        const string Pipe2Name = "Second Pipe";
        const string Pipe3Name = "Third Pipe";

        private void PipesTestSetup() {
            Pipe pipe1 = new Pipe(Pipe1Name, 1);
            Pipe pipe2 = new Pipe(Pipe2Name, 1);
            Pipe pipe3 = new Pipe(Pipe3Name, 1);

            pipe1.AddConnection(pipe2);
            pipe2.AddConnection(pipe3);

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
            PipesTestSetup();
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
        [Test]
        // pipe 1  & 2 empty, 3 full
        public void PipeEndToStart() {
            PipesTestSetup();
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
        [Test]
        // pipe 1  & 3 empty, 2 full
        public void PipeMiddle() {
            PipesTestSetup();
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
        [Test]
        // pipe 1 : 25, pipe 2 = 10, pipe 3 = 75
        public void PipeBalanceAll() {
            PipesTestSetup();
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

        [Test]
        //  factory with one tank - pipe 1 - pipe 2 -pipe 3
        public void TankTo3pipes() {
            const int tankStartContent = 800;
            Tank tank = new Tank("test tank", 1000, tankStartContent);

            Pipe pipeAtTank = new Pipe("pipeAtTank", 1);
            Pipe pipeMiddle = new Pipe("pipeMiddle", 1);
            Pipe pipeEnd = new Pipe("pipeEnd", 1);

            pipeAtTank.AddConnection(tank);
            pipeAtTank.AddConnection(pipeMiddle);
            pipeMiddle.AddConnection(pipeEnd);

            Factory facWithTank = new Factory();
            facWithTank.AddEquipment(tank);
            facWithTank.AddEquipment(pipeAtTank);
            facWithTank.AddEquipment(pipeMiddle);
            facWithTank.AddEquipment(pipeEnd);

            Assert.AreEqual(tank.Content, tankStartContent);
            Assert.AreEqual(0, pipeAtTank.Content);
            Assert.AreEqual(0, pipeMiddle.Content);
            Assert.AreEqual(0, pipeEnd.Content);

            facWithTank.BalanceContents(); // 700,50,25,25
            Assert.AreEqual(tankStartContent - pipeAtTank.Volume, tank.Content);
            Assert.AreEqual(pipeAtTank.Volume / EquipmentCst.BalanceFactor, pipeAtTank.Content);
            Assert.AreEqual(pipeMiddle.Volume / (EquipmentCst.BalanceFactor * 2), pipeMiddle.Content);
            Assert.AreEqual(pipeEnd.Volume / (EquipmentCst.BalanceFactor * 2), pipeEnd.Content);

            int count = 0;
            do {
                facWithTank.BalanceContents();
                count++;
            } while (pipeAtTank.Content != pipeAtTank.Volume);

            Assert.AreEqual(tankStartContent - pipeAtTank.Volume * 3 , tank.Content);
            Assert.AreEqual(pipeAtTank.Volume, pipeAtTank.Content);
            Assert.AreEqual(pipeMiddle.Volume , pipeMiddle.Content);
            Assert.AreEqual(pipeMiddle.Volume , pipeEnd.Content);

        }
        [Test]
        // factory with  pipe 1 : 100 - pipe 2 : 100, tank : empty
        // no pressure = balance content
        public void PipesWithoutPressureToFillTank() {
            const int tankStartContent = 0;
            Tank tank = new Tank("test tank", 1000, tankStartContent);

            Pipe pipeStart = new Pipe("pipeStart", 1, 100);
            Pipe pipeAtTank = new Pipe("pipeAtTank", 1, 100);

            pipeStart.AddConnection(pipeAtTank);
            pipeAtTank.AddConnection(tank);

            Factory facWithTank = new Factory();
            facWithTank.AddEquipment(tank);
            facWithTank.AddEquipment(pipeAtTank);
            facWithTank.AddEquipment(pipeStart);

            Assert.AreEqual(tank.Content, tankStartContent);
            Assert.AreEqual(pipeAtTank.Content, 100);
            Assert.AreEqual(pipeStart.Content, 100);

            facWithTank.BalanceContents();
            Assert.AreEqual(69, pipeStart.Content);
            Assert.AreEqual(69, pipeAtTank.Content);
            Assert.AreEqual(62, tank.Content);             
        }

        [Test]
        // unlimited source direct connected to tank = instant filled
        public void SourceToFillTank() {
            Source source = new Source("test source");
            Tank tank = new Tank("test tank", 1000);
            source.AddConnection(tank);
            tank.AddConnection(source);

            Factory fac = new Factory();
            fac.AddEquipment(source);
            fac.AddEquipment(tank);

            Assert.AreEqual(tank.Content, 0);

            fac.BalanceContents();
            Assert.AreEqual(tank.Content, tank.Volume);
        }

        [Test]
        // unlimited source  connected via 1 pipe to tank 
        public void SourceToPipeTOFillTank() {
            Source source = new Source("test source");
            Pipe pipe = new Pipe("test pipe", 5); // 500
            Tank tank = new Tank("test tank", 1500);
            source.AddConnection(pipe);
            pipe.AddConnection(tank);

            Factory fac = new Factory();
            fac.AddEquipment(source);
            fac.AddEquipment(pipe);
            fac.AddEquipment(tank);

            Assert.AreEqual(0, tank.Content);
            Assert.AreEqual(0, pipe.Content);

            // P500,T0
            // P0,T500
            fac.BalanceContents();
            Assert.AreEqual(0, pipe.Content);// pipe.Volume / EquipmentCst.BalanceFactor);
            Assert.AreEqual(500, tank.Content); // pipe.Volume / EquipmentCst.BalanceFactor);

            int count = 0;
            do {
                fac.BalanceContents();
                count++;
            } while (tank.Content != tank.Volume);
            //(pipe.Content !=pipe.Volume);

            Assert.GreaterOrEqual(tank.Content, tank.Volume);
        }

        [Test]
        // connect 2 tanks: tank 1 = 600, pipe = 0 , tank 2 = 300 ==> 400,100,400
        public void Connect2Tanks() {
            const int tank1Start = 600;
            const int tank2Start = 300;
            Tank tank1 = new Tank("tank 600", 1000, tank1Start);
            Pipe pipe = new Pipe("pipe", 1);
            Tank tank2 = new Tank("tank 300", 1000, tank2Start);

            tank1.AddConnection(pipe);
            tank2.AddConnection(pipe);

            Factory fac = new Factory();
            fac.AddEquipment(tank1);
            fac.AddEquipment(pipe);
            fac.AddEquipment(tank2);

            Assert.AreEqual(tank1.Content, tank1Start);
            Assert.AreEqual(pipe.Content, 0);
            Assert.AreEqual(tank2.Content, tank2Start);

            fac.BalanceContents();//500, 0, 400
            Assert.AreEqual(tank1.Content, tank1Start - pipe.Volume);
            Assert.AreEqual(pipe.Content, 0);
            Assert.AreEqual(tank2.Content, tank2Start + pipe.Volume);

                fac.BalanceContents();
            // 400, 100, 400          
            Assert.AreEqual(400, tank1.Content);
            Assert.AreEqual(100, pipe.Content);
            Assert.AreEqual(400, tank2.Content);

            fac.BalanceContents();
            // 400, 100, 400          
            Assert.AreEqual(400, tank1.Content);
            Assert.AreEqual(100, pipe.Content);
            Assert.AreEqual(400, tank2.Content);

            fac.BalanceContents();
            // 400, 100, 400          
            Assert.AreEqual(400, tank1.Content);
            Assert.AreEqual(100, pipe.Content);
            Assert.AreEqual(400, tank2.Content);
        }
    }
}
