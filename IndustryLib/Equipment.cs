using System;
using System.Collections.Generic;
using System.Text;

namespace IndustryLib {
    public abstract class Equipment {
        public string Name { get; private set; }
        public bool Operational { get; private set; }
        public string TypeOfEquipment { get; private set; }
        public int Volume { get; private set; }
        public int Content { get; private set; }
        public int Mark { get; private set; }

        public List<Equipment> Connections { get; set; }

        public Equipment(string type, string name, int mark,
         int volume, int content = 0) {
            TypeOfEquipment = type;
            Name = name;
            Volume = volume;
            Mark = mark;
            Operational = true;
            Content = content <= Volume ? content : volume;
            Connections = new List<Equipment>();
        }

        public virtual void SetFailed() {
            Operational = false;
            Content = 0;
        }

        public virtual void SetRepaired() {
            Operational = true;
        }

        public void Fill(int content) {
            if (content > Volume) { content = Volume; }
            Content = content;
        }

        public void BalanceConnections() {
            foreach (Equipment connection in Connections) {
                BalanceConnectionContent(connection);
            }
        }

        private void BalanceConnectionContent(Equipment connection) {
            if (connection != null) {
                int difference = connection.Content - Content;
                //  input difference than current content = balance difference
                if (difference != 0) {
                    int change = difference / EquipmentCst.BalanceFactor;
                    // don't overfill
                    if (change + Content > Volume) change = Volume - Content;

                    Fill(Content + change);// add diff 
                    connection.Fill(connection.Content - change); // take out input
                }
            }
        }
    }
}

