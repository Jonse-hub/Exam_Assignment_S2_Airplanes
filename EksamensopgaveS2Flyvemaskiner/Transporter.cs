using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EksamensopgaveS2Flyvemaskiner
{
    public class Transporter
    {
        //private float price;
        private DateTime dato;
        //private float sum;

        public int Id { get; set; }
        public Fly Fly { get; set; }
        public Container Container { get; set; }

        public DateTime Dato { get; set; }
        /*public int Quantity
        {
            get { return quantity; }
            set
            {
                if (quantity != value)
                {
                    quantity = value;
                    sum = Quantity * price;
                }
            }
        }
        public float Sum
        {
            get { return sum; }
            set
            {
                if (sum != value)
                {
                    sum = value;
                }
            }
        }*/


        public Transporter(int id, DateTime dato, Fly fly, Container container)
        {
            Id = id;
            Dato = dato;
            Fly = fly;
            Container = container;
        }
    }
}
