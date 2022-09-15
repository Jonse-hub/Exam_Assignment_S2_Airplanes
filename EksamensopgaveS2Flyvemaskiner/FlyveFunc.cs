using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EksamensopgaveS2Flyvemaskiner
{
    public class FlyveFunc
    {
        FlyveData data = new FlyveData();

        #region containere
        public void ReadContainere(List<Container> containerListe)
        {
            data.ReadContainere(containerListe);
        }

        public void UpdateContainer(Container container)
        {
            data.UpdateContainer(container);
        }

        public void DeleteContainer(Container container)
        {
            data.DeleteContainer(container);
        }
        public void CreateContainer(Container container)
        {
            data.CreateContainer(container);
        }
        #endregion

        #region Fly
        public void ReadFly(List<Fly> flyListe)
        {
            data.ReadFly(flyListe);
        }
        public void UpdateFly(Fly fly)
        {
            data.UpdateFly(fly);
        }

        public void DeleteFly(Fly fly)
        {
            data.DeleteFly(fly);
        }
        public void CreateFly(Fly fly)
        {
            data.CreateFly(fly);
        }
        #endregion

        public void ReadTransporter(List<Transporter> transporterListe, List<Fly> flyListe, List<Container> containerListe)
        {
            data.ReadTransporter(transporterListe, flyListe, containerListe);
        }
        public void UpdateTransporter(Transporter fly)
        {
            data.UpdateTransporter(fly);
        }

        public void DeleteTransporter(Transporter transporter)
        {
            data.DeleteTransporter(transporter);
        }
        public void CreateTransporter(Transporter transporter)
        {
            data.CreateTransporter(transporter);
        }
    }
}
