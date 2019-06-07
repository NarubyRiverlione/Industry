using System;
using System.Collections.Generic;
using System.Text;

namespace IndustryLib {
    public class Equipment : IEquipment {
        public string Name { get; private set; }
        public bool Operational { get;  set; } // TODO: lock down
        public string TypeOfEquipment { get;  set; } // TODO: lock down
        public int Volume { get; private set; }
        public int Content { get; private set; }
        public List<IEquipment> Connections { get; set; }

        public Equipment(string name, int volume) {
            Name = name;
            Operational = true;
            Volume = volume;
            Content = 0;
            Connections = new List<IEquipment>();
        }

     public   virtual void SetFailed() {
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
            foreach (IEquipment connection in Connections) {
                BalanceConnectionContent(connection);
            }
        }

        private void BalanceConnectionContent(IEquipment connection) {
            if (connection != null) {
                int difference = connection.Content - Content;
                // more input then current content = balance difference
                if (difference != 0) {
                    Fill(Content + difference / 2);// add diff 
                    connection.Fill(connection.Content - difference / 2); // take out input
                }
            }
        }
    }
}

