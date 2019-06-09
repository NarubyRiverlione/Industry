using System;using System.Collections.Generic;using System.Text;namespace IndustryLib {    public abstract class Equipment {        public string Name { get; private set; }        public bool Operational { get; private set; }        public EquipmentCst.Types TypeOfEquipment { get; private set; }        public int Volume { get; private set; }        public int Content { get; private set; }        public int Mark { get; private set; }        private List<Equipment> Connections { get; set; }        protected Equipment(EquipmentCst.Types type, string name, int mark,         int volume, int content = 0) {            TypeOfEquipment = type;            Name = name;            Volume = volume;            Mark = mark;            Operational = true;            Content = content <= Volume ? content : volume;            Connections = new List<Equipment>();        }

        // add a connection, also add connection in connected equipment
        public void AddConnection(Equipment connectTo) {
            Connections.Add(connectTo);
            connectTo.Connections.Add(this);
        }        public List<Equipment> GetConnections() {
            return Connections;
        }        public virtual void SetFailed() {            Operational = false;            Content = 0;        }        public virtual void SetRepaired() {            Operational = true;        }        public void Fill(int content) {            if (content > Volume) { content = Volume; }            Content = content;        }        public void BalanceConnections() {            foreach (Equipment connection in Connections) {                BalanceConnectionContent(connection);            }        }        private void BalanceConnectionContent(Equipment connection) {            if (connection != null) {                int difference = Content - connection.Content;
                //  input difference than current content = balance difference
                if (difference != 0) {                    int change = difference / EquipmentCst.BalanceFactor;

                    // don't overfill connected
                    if (connection.Content + change > connection.Volume)
                        change = (connection.Volume - connection.Content)*Math.Sign(change); 
                    // don't overfill self
                    if ((Content - change) > Volume)
                        change = (Volume - Content)*Math.Sign(change);

                    // change connected, unless it's a Source
                    if (connection.TypeOfEquipment != EquipmentCst.Types.Source) {                        connection.Fill(connection.Content + change);                    }

                    // change own content, unless it's a Source
                    if (TypeOfEquipment != EquipmentCst.Types.Source)
                        // sign if difference determines fill of pull
                        Fill(Content - change);
                }            }        }    }}