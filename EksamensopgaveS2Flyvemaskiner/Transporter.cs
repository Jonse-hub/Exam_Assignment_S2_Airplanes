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
        //private DateTime dato;
        //private float sum;

        public int Id { get; set; }
        public DateTime Dato { get; set; }
        public Fly Fly { get; set; }
        public Container Container { get; set; }


        /*public Transporter(DateTime dato, Fly fly, Container container)
        {
            Id = fly.Id;
            Dato = dato;
            Fly = fly;
            Container = container;

        }*/

        // dunno, not in Fly.cs. i think remove this. then fix flyveData with set parameters like in readfly
        /*public Transporter(int id, DateTime dato, Fly fly, Container container)
        {
            Id = id;
            Dato = dato;
            Fly = fly;
            Container = container;
        }*/
    }
}
