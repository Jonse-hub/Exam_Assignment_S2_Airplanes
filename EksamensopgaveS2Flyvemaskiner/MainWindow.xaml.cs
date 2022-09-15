using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EksamensopgaveS2Flyvemaskiner
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        FlyveFunc func = new FlyveFunc();

        public List<Container> ContainerListe = new List<Container>();
        public List<Container> ContainerListe_old = new List<Container>();

        public List<Fly> FlyListe = new List<Fly>();
        public List<Transporter> TransportListe = new List<Transporter>();

        bool isEditing_Container = false;
        bool isEditing_Fly = false;
        bool isEditing_Transport = false;

        public MainWindow()
        {
            InitializeComponent();
            //func.CreateContainer(new Container { Bruttovægt = 22, Lufthavn = "Dumb id file lufthavn1" }); // virker nu
            func.ReadContainere(ContainerListe);
            //func.CreateFly(new Fly { MaxConatinerVægt = 22, Registreringsnummeret = "s" });
            func.ReadFly(FlyListe);
            //ContainerListe_old = ContainerListe;

            DG_Container.ItemsSource = ContainerListe;
            DG_Fly.ItemsSource = FlyListe;
            DG_Transport.ItemsSource = FlyListe;

            

        }


        #region container
        private void Btn_Container_Rediger_Click(object sender, RoutedEventArgs e)
        {
            if(DG_Container.SelectedItem != null)
            {
                
                if (isEditing_Container == true)
                {
                    //update


                    //func.UpdateContainer(new Container { Id =  Convert.ToInt32(TBl_Container_Id.Text), Bruttovægt = Convert.ToInt32(TB_Container_Bruttovægt.Text), Lufthavn = TB_Container_Lufthavn.Text });

                    //ContainerListe[DG_Container.SelectedIndex] = new Container { Bruttovægt = Convert.ToInt32(TB_Container_Bruttovægt.Text), Lufthavn = TB_Container_Lufthavn.Text };
                    func.UpdateContainer(new Container { Id = Convert.ToInt32(TBl_Container_Id.Text), Bruttovægt = Convert.ToInt32(TB_Container_Bruttovægt.Text), Lufthavn = TB_Container_Lufthavn.Text });
                    func.ReadContainere(ContainerListe);
                    DG_Container.Items.Refresh();
                    Btn_Container_Rediger.Content = "Rediger";
                    isEditing_Container = false;
                }
                else
                {
                    // ædret texten i gui til man kan ændre
                    TBl_Container_Id.Text = (DG_Container.SelectedItem as Container).Id.ToString();
                    TB_Container_Bruttovægt.Text = (DG_Container.SelectedItem as Container).Bruttovægt.ToString();
                    TB_Container_Lufthavn.Text = (DG_Container.SelectedItem as Container).Lufthavn;

                    Btn_Container_Rediger.Content = "Lav Ændring";
                    isEditing_Container = true;
                }
            }
            
        }

        private void Btn_Container_Slet_Click(object sender, RoutedEventArgs e)
        {
            //func.CreateContainer(new Container { Id = 0, Bruttovægt = Convert.ToInt32(TB_Container_Bruttovægt.Text), Lufthavn = TB_Container_Lufthavn.Text });
            if (DG_Container.SelectedItem != null)
            {
                func.DeleteContainer(DG_Container.SelectedItem as Container);
                func.ReadContainere(ContainerListe);
                //ContainerListe.Remove(DG_Container.SelectedItem as Container);
                DG_Container.Items.Refresh();
            }
        }

        private void Btn_Container_Ny_Click(object sender, RoutedEventArgs e)
        {
            //ContainerListe.Add(new Container { Bruttovægt = Convert.ToInt32(TB_Container_Bruttovægt.Text), Lufthavn = TB_Container_Lufthavn.Text });
            func.CreateContainer(new Container { Bruttovægt = Convert.ToInt32(TB_Container_Bruttovægt.Text), Lufthavn = TB_Container_Lufthavn.Text });
            func.ReadContainere(ContainerListe);
            DG_Container.Items.Refresh();
        }



        /*private void Btn_Container_Gem_Click(object sender, RoutedEventArgs e) // ignore for now..
        {
            *//*foreach (Container container in ContainerListe_old) // sletter gamle bestillinger i database. ikke nødvændig da jeg har fixed id dubletter
            {
                //bestillingsListe.Remove(bestil);
                func.DeleteContainer(container); // delete all


                //bestillingsListe.Add(bestil); // not working
            }*/

        /*int i = 0;
        foreach (Container container in ContainerListe_old) //  sletter dem der skal
        {
            if (container.Id == ContainerListe[i].Id)
            {


            }
            else

            i++;
        }*//*

        for (int i = 0; i < ContainerListe_old.Count; i++) // gemmer bestillinger i database.
        {
            if (ContainerListe[i].Id == ContainerListe_old[i].Id)
            {
                if (ContainerListe[i].Bruttovægt != ContainerListe_old[i].Bruttovægt || ContainerListe[i].Lufthavn != ContainerListe_old[i].Lufthavn)
                {
                    func.UpdateContainer(new Container { Id = ContainerListe[i].Id, Bruttovægt = ContainerListe[i].Bruttovægt, Lufthavn = ContainerListe[i].Lufthavn });

                }
                else
                {
                    // nothing
                }
            }
            else
            {
                    func.DeleteContainer(ContainerListe_old[i]);
            }
            i++;
        }
        foreach (Container container in ContainerListe) // gemmer bestillinger i database.
        {
            if (ContainerListe_old.Contains(container))
            {
                //nothing
            }
            else
            {
                func.CreateContainer(container);
            }
            ContainerListe_old = ContainerListe;

        }

            //ContainerListe_old = ContainerListe; // ikke nødvændig da jeg har fixed id dubletter
    }*/

        #endregion

        #region Fly
        private void Btn_Fly_Rediger_Click(object sender, RoutedEventArgs e)
        {
            if (isEditing_Fly == true)
            {
                func.UpdateFly(new Fly { Id = Convert.ToInt32(TBl_Fly_Id.Text), MaxConatinerVægt = Convert.ToInt32(TB_Fly_MaxConatinerVægt.Text), Registreringsnummeret = TB_Fly_Registreringsnummeret.Text });
                func.ReadFly(FlyListe);
                DG_Fly.Items.Refresh();
                Btn_Fly_Rediger.Content = "Rediger";
                isEditing_Fly = false;

            }
            else if(isEditing_Fly == false)
            {
                TBl_Fly_Id.Text = (DG_Fly.SelectedItem as Fly).Id.ToString();
                TB_Fly_MaxConatinerVægt.Text = (DG_Fly.SelectedItem as Fly).MaxConatinerVægt.ToString();
                TB_Fly_Registreringsnummeret.Text = (DG_Fly.SelectedItem as Fly).Registreringsnummeret.ToString();
                Btn_Fly_Rediger.Content = "Lav Ændring";
                isEditing_Fly = true;
            }
        }

        private void Btn_Fly_Slet_Click(object sender, RoutedEventArgs e)
        {
            if(DG_Fly.SelectedItem != null)
            {
                func.DeleteFly(DG_Fly.SelectedItem as Fly);
                func.ReadFly(FlyListe);
                DG_Fly.Items.Refresh();
            }
        }

        private void Btn_Fly_Ny_Click(object sender, RoutedEventArgs e)
        {
            func.CreateFly(new Fly { MaxConatinerVægt = Convert.ToInt32(TB_Fly_MaxConatinerVægt.Text), Registreringsnummeret = TB_Fly_Registreringsnummeret.Text });
            func.ReadFly(FlyListe);
            DG_Fly.Items.Refresh();
        }
        #endregion

        #region Tranporter

        private void Btn_Transport_Rediger_Click(object sender, RoutedEventArgs e)
        {
            if (isEditing_Transport == true)
            {
                func.UpdateFly(new Fly { Id = Convert.ToInt32(TBl_Fly_Id.Text), MaxConatinerVægt = Convert.ToInt32(TB_Fly_MaxConatinerVægt.Text), Registreringsnummeret = TB_Fly_Registreringsnummeret.Text });
                func.ReadFly(FlyListe);
                DG_Fly.Items.Refresh();
                Btn_Fly_Rediger.Content = "Rediger";
                isEditing_Transport = false;

            }
            else if (isEditing_Transport == false)
            {
                TBl_Fly_Id.Text = (DG_Fly.SelectedItem as Fly).Id.ToString();
                TB_Fly_MaxConatinerVægt.Text = (DG_Fly.SelectedItem as Fly).MaxConatinerVægt.ToString();
                TB_Fly_Registreringsnummeret.Text = (DG_Fly.SelectedItem as Fly).Registreringsnummeret.ToString();
                Btn_Fly_Rediger.Content = "Lav Ændring";
                isEditing_Transport = true;
            }
        }

        private void Btn_Transport_Slet_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Btn_Transport_Ny_Click(object sender, RoutedEventArgs e)
        {

        }
        #endregion
    }
}
