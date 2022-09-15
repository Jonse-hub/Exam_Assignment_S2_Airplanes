using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EksamensopgaveS2Flyvemaskiner
{
    using EksamensopgaveS2Flyvemaskiner.FlyvDataSetTableAdapters;
    using static EksamensopgaveS2Flyvemaskiner.FlyvDataSet;
    public class FlyveData
    {
        // classe adgang til datasettet i sql {
        FlyvDataSet flyvDataset = new FlyvDataSet();
        // Field. Benyttes for at kalde metoder i klassen IsLagerDataSet herfra.
        TableAdapterManager AdapterManager = new TableAdapterManager();
        // Field. Benyttes for at kalde metoder i klassen TableAdapterManager herfra.

        public FlyveData()
        // Konstruktor. Benyttes til at oprette instanser(objekter) af vores adapters.
        {
            AdapterManager.ContainerTableAdapter = new ContainerTableAdapter();
            AdapterManager.FlyTableAdapter = new FlyTableAdapter();
        }
        // }}classe adgang til datasettet i sql 
        public void ReadContainere(List<Container> ContainerListe) // Method. Henter alle varer fra databasen.
        {
            ContainerListe.Clear();  // Tøm listen før vi fylder den. På den måde forhindrer vi dubletter.
            ContainerDataTable ContainereRows = new ContainerDataTable();
            AdapterManager.ContainerTableAdapter.Fill(ContainereRows);

        foreach (ContainerRow row in ContainereRows)
            {
                if (row.Bruttovægt >= 0)  // varer med negativ pris betragter vi som logisk slettet
                {

                    Container container = new Container()
                    {
                        Id = row.Id,
                        Bruttovægt = row.Bruttovægt,
                        Lufthavn = row.Lufthavn,
                    };
                    ContainerListe.Add(container);
                }
            }
        }

        //
        /*public float GetVarePrice(List<Vare> vareListe, int Vare.Id)
        // Find bestillingens varepris. Her lavet med en løkke, 
        // andre mere avancerede metoder kan anvendes.
        // Man kan fx bruge Find-metoden på listen eller man kan anvende LINQ.

        {
            foreach(Vare vare in vareListe)
            {
                    if(vare.Id == Vare.Id)
                    {
                        return vare.Price;
                    }
            }
            return -1.0f;
        } */

        public Container GetContainere(List<Container> ContainerListe, int ContainerId)
        // Find varen med en bestemt vareId. Her lavet med en løkke, andre mere avancerede metoder kan anvendes.
        // Man kunne fx bruge Find-metoden på listen anvende LINQ.
        {
            foreach (Container containere in ContainerListe)
            {
                if (containere.Id == ContainerId)
                {
                    return containere;
                }
            }
            return null;  // ingen vare i vareListe har den Id vi ledte efter
        }

        public void ReadFly(List<Fly> FlyListe) // Method. Henter alle varer fra databasen.
        {
            FlyListe.Clear();  // Tøm listen før vi fylder den. På den måde forhindrer vi dubletter.
            FlyDataTable FlyRows = new FlyDataTable();
            AdapterManager.FlyTableAdapter.Fill(FlyRows);

            foreach (FlyRow row in FlyRows)
            {
                if (row.MaxConatinerVægt >= 0)  // varer med negativ pris betragter vi som logisk slettet
                {

                    Fly fly = new Fly()
                    {
                        Id = row.Id,
                        MaxConatinerVægt = row.MaxConatinerVægt,
                        Registreringsnummeret = row.Registreringsnummeret,
                    };
                    FlyListe.Add(fly);
                }
            }
        }

        public Fly GetFly(List<Fly> FlyListe, int FlyId)
        // Find varen med en bestemt vareId. Her lavet med en løkke, andre mere avancerede metoder kan anvendes.
        // Man kunne fx bruge Find-metoden på listen anvende LINQ.
        {
            foreach (Fly fly in FlyListe)
            {
                if (fly.Id == FlyId)
                {
                    return fly;
                }
            }
            return null;  // ingen vare i vareListe har den Id vi ledte efter
        }

        public void ReadTransporter(List<Transporter> transporterListe, List<Fly> flyListe,  List<Container> containerListe)
        // Method. Henter alle bestillinger fra databasen.
        {
            transporterListe.Clear();
            // Tøm listen før vi fylder den. På den måde forhindrer vi dubletter.
            TransporterDataTable transporterRows = new TransporterDataTable();
            AdapterManager.TransporterTableAdapter.Fill(transporterRows);

            foreach (TransporterRow row in transporterRows)
            {
                Transporter transporter = new Transporter(row.Id, row.Dato, GetFly(flyListe, row.FlyId), GetContainere(containerListe, row.ContainerId));
                transporterListe.Add(transporter);
            }
        }

        public void CreateContainer(Container container) // gemme indholdet af vare i database
        {
            ContainerRow row = flyvDataset.Container.NewContainerRow();
            row.Bruttovægt = container.Bruttovægt;
            row.Lufthavn = container.Lufthavn;
            RækkeTilDatabase(row);
        }

        private void RækkeTilDatabase(ContainerRow row) // tilføjer dataset til database
        {
            flyvDataset.Container.Rows.Add(row);
            AdapterManager.ContainerTableAdapter.Update(flyvDataset.Container);
        }
        public void CreateFly(Fly fly) // Fly - gemme indholdet af vare i database
        {
            FlyRow row = flyvDataset.Fly.NewFlyRow();
            row.MaxConatinerVægt = fly.MaxConatinerVægt;
            row.Registreringsnummeret = fly.Registreringsnummeret;
            RækkeTilDatabase(row);
        }

        private void RækkeTilDatabase(FlyRow row) // Fly - tilføjer dataset til database
        {
            flyvDataset.Fly.Rows.Add(row);
            AdapterManager.FlyTableAdapter.Update(flyvDataset.Fly);
        }

        public void CreateTransporter(Transporter transporter) // ny bestilling til sql database
        {
            TransporterRow row = flyvDataset.Transporter.NewTransporterRow();
            row.Dato = transporter.Dato;
            row.FlyId = transporter.Fly.Id;
            row.ContainerId = transporter.Container.Id;
            RækkeTilDatabase(row);
        }

        private void RækkeTilDatabase(TransporterRow row)  // Overloading, samme metode forskellig signatur
        {
            flyvDataset.Transporter.Rows.Add(row);
            AdapterManager.TransporterTableAdapter.Update(flyvDataset.Transporter);
        }


        public void UpdateContainer(Container container) // præcis række skal opdateres
        {
            ContainerDataTable containerRows = new ContainerDataTable();
            AdapterManager.ContainerTableAdapter.Fill(containerRows);  // Hent database rows
            ContainerRow row = containerRows.FindById(container.Id); // Find row
            row.Bruttovægt = container.Bruttovægt;  // Opdater row
            row.Lufthavn = container.Lufthavn;
            AdapterManager.ContainerTableAdapter.Update(containerRows); // Gem forandringer
        }
        public void UpdateFly(Fly fly) // præcis række skal opdateres
        {
            FlyDataTable flyRows = new FlyDataTable();
            AdapterManager.FlyTableAdapter.Fill(flyRows);  // Hent database rows
            FlyRow row = flyRows.FindById(fly.Id); // Find row
            row.MaxConatinerVægt = fly.MaxConatinerVægt;  // Opdater row
            row.Registreringsnummeret = fly.Registreringsnummeret;
            AdapterManager.FlyTableAdapter.Update(flyRows); // Gem forandringer
        }

        public void UpdateTransporter(Transporter transporter)  // præcis række skal opdateres
        {
            TransporterDataTable transporterRows = new TransporterDataTable();
            AdapterManager.TransporterTableAdapter.Fill(transporterRows);  // Hent database rows
            TransporterRow row = transporterRows.FindById(transporter.Id); // Find row
            row.Dato = transporter.Dato;  // Opdater row
            row.FlyId = transporter.Fly.Id;
            row.ContainerId = transporter.Container.Id;
            AdapterManager.TransporterTableAdapter.Update(transporterRows); // Gem forandringer
        }// præcis række skal opdateres

        public void DeleteTransporter(Transporter transporter) // slet en bestilling
        {
            AdapterManager.TransporterTableAdapter.Delete(transporter.Id, transporter.Dato, transporter.Fly.Id, transporter.Container.Id);
        }

        public void DeleteContainerLogisk(Container container) // hvis slettet Container hvis Bruttovægt er negativ
        {
            container.Bruttovægt = -99;
            UpdateContainer(container);
        }
        public void DeleteFlyLogisk(Fly fly) // hvis slettet fly hvis max vægt er negativ
        {
            fly.MaxConatinerVægt = -99;
            UpdateFly(fly);
        }


        // udkommenter 3-2 linjer ad gangen og du kan få error selvom koden er rigtig. test hvad man ikke kan.
        /*public void TestDatabase(List<Bestilling> bestillingsListe, List<Vare> vareListe)
        {
            //CreateVare(vareListe[0]);
            //bestillingsListe.Add(new Bestilling(vareListe[0], 6));
            //CreateBestilling(bestillingsListe[0]);

            //vareListe[0].Price = 19.98f;
            //UpdateVare(vareListe[0]);

            //ReadVarer(vareListe);
            //DeleteVareLogisk(vareListe[0]);
            //ReadVarer(vareListe);

            //ReadBestillinger(bestillingsListe, vareListe);
            //CreateBestilling(bestillingsListe[0]);

            //ReadBestillinger(bestillingsListe, vareListe);
            //bestillingsListe[2].Quantity = 1;
            //UpdateBestilling(bestillingsListe[2]);

            //DeleteBestilling(bestillingsListe[7]);
        }*/

        public void DeleteContainer(Container container) // slet en bestilling, selv tilføjet
        {
            AdapterManager.ContainerTableAdapter.Delete(container.Id, container.Bruttovægt); // ikke text (container.Lufthavn)
        }

        public void DeleteFly(Fly fly) // slet en bestilling, selv tilføjet
        {
            AdapterManager.FlyTableAdapter.Delete(fly.Id, fly.MaxConatinerVægt);
        }

    }
    


}
