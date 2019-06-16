using System;using System.Collections.Generic;using System.Linq;
using System.Text;namespace IndustryLib {    public abstract class Equipment {        public string Name { get; private set; }        public bool Operational { get; private set; }        public EquipmentCst.Types TypeOfEquipment { get; private set; }        public int Volume { get; private set; }        public int Content { get; private set; }        public int Mark { get; private set; }
        protected internal double Pressure { get; set; }        private List<Equipment> Connections { get; set; }        protected Equipment(EquipmentCst.Types type, string name, int mark,         int volume, int content = 0) {            TypeOfEquipment = type;            Name = name;            Volume = volume;            Mark = mark;            Operational = true;            Content = content <= Volume ? content : volume;
            //     Pressure = content == 0 ? 0 : content / EquipmentCst.PressureContentFactor;
            Connections = new List<Equipment>();        }

        abstract public void CalcPressure(Equipment connectedTo = null);

        // add a connection, also add connection in connected equipment
        public void AddConnection(Equipment connectTo) {
            Connections.Add(connectTo);
            connectTo.Connections.Add(this);
        }        public List<Equipment> GetConnections() {
            return Connections;
        }        public virtual void SetFailed() {            Operational = false;            Content = 0;        }        public virtual void SetRepaired() {            Operational = true;        }        public virtual void Fill(int content) {            if (content > Volume) { content = Volume; }            Content = content;        }


        public void BalanceConnections() {
            foreach (Equipment connection in Connections) {
                BalanceConnectionContent(connection);
            }
        }


        private void BalanceConnectionContent(Equipment connection) {
            CalcPressure(connection);

            if (connection != null) {
                int difference = Content - connection.Content;

                //// more pressure than connection = give content
                //if (Pressure > connection.Pressure) difference = Content;

                //  input difference than current content                
                // ==> balance difference
                int change = difference / EquipmentCst.BalanceFactor;
             
                // connection has greater pressure,never send to,
                // always pull all contentfrom
                if (connection.Pressure > Pressure)       
                    change = - Math.Abs(connection.Content);

                if (connection.Pressure < Pressure)
                    change = Math.Abs(Content);

                //// tank takes everything out connection
                //if (connection.TypeOfEquipment == EquipmentCst.Types.Tank)
                //    change = Content;


                // don't overfill connected
                if (connection.Content + change > connection.Volume)
                    change = Math.Sign(change) * (connection.Volume - connection.Content) ;
                // don't overfill self
                if ((Content - change) > Volume)
                    change = Math.Sign(change)* (Volume - Content) ;

                // change connected       
                connection.Fill(connection.Content + change );

                // change own content          
                // sign if difference determines fill of pull
                Fill(Content - change );

                //}
            }
        }
    }
}