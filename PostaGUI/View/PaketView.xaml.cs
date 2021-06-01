using DatabaseModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

namespace PostaGUI.View
{
    /// <summary>
    /// Interaction logic for PaketView.xaml
    /// </summary>
    public partial class PaketView : UserControl
    {
        PostaDbContainer _context;
        CollectionViewSource paketViewSource;
        public PaketView()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

            //  Do not load your data at design time.
            if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            {
                //Load your data here and assign the result to the CollectionViewSource.
                 paketViewSource = (System.Windows.Data.CollectionViewSource)this.Resources["paketViewSource"];

                // Load is an extension method on IQueryable,
                // defined in the System.Data.Entity namespace.
                // This method enumerates the results of the query,
                // similar to ToList but without creating a list.
                // When used with Linq to Entities this method
                // creates entity objects and adds them to the context.
                _context = new PostaDbContainer();
                _context.PostanskeUsluge_Paket.Load();

                // After the data is loaded call the DbSet<T>.Local property
                // to use the DbSet<T> as a binding source.
                paketViewSource.Source = _context.PostanskeUsluge_Paket.Local;
            }
        }
        private void DeleteCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            _context = new PostaDbContainer();
            var cur = paketViewSource.View.CurrentItem as Paket;

            var paket = (from c in _context.PostanskeUsluge_Paket
                         where c.ID_Posiljke == cur.ID_Posiljke
                           select c).FirstOrDefault();

            if (paket != null)
            {
                try
                {
                    _context.PostanskeUsluge_Paket.Remove(paket);
                    _context.SaveChanges();
                    _context.PostanskeUsluge_Paket.Load();

                    paketViewSource.Source = _context.PostanskeUsluge_Paket.Local;
                    paketViewSource.View.Refresh();


                }
                catch (Exception ex)
                {

                    MessageBox.Show("Trenutno nije moguce obrisati paket.", "Error");
                    return;
                }

            }

        }
        private void UpdateCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            _context = new PostaDbContainer();
            var cur = paketViewSource.View.CurrentItem as Paket;

            var paket = (from c in _context.PostanskeUsluge_Paket
                         where c.ID_Posiljke == cur.ID_Posiljke
                         select c).FirstOrDefault();

            if (paket != null && Validacija())
            {
                try
                {
                    paket.Tezina = Convert.ToInt32(tezinaTextBox.Text);
                    paket.PostanskiBrojOdredista = Convert.ToInt32(postanskiBrojOdredistaTextBox.Text);
                    paket.PosiljalacIme = posiljalacImeTextBox.Text;
                    paket.PosiljalacPrezime = posiljalacPrezimeTextBox.Text;
                    paket.PrimalacIme = primalacImeTextBox.Text;
                    paket.PrimalacPrezime = primalacPrezimeTextBox.Text;
                    paket.Grad = gradTextBox.Text;
                    paket.Ulica = ulicaTextBox.Text;
                    paket.Broj = brojTextBox.Text;
                    paket.SluzbenikJMBG_Radnika = Convert.ToInt32(sluzbenikJMBG_RadnikaTextBox.Text);
                    paket.SluzbenikPostanskiBroj = Convert.ToInt32(sluzbenikPostanskiBrojTextBox.Text);
                    paket.PostarJMBG_Radnika = Convert.ToInt32(postarJMBG_RadnikaTextBox.Text);
                    paket.PostarPostanskiBroj = Convert.ToInt32(postarPostanskiBrojTextBox.Text);
                    paket.SkladisteId_Skladiste = Convert.ToInt32(skladisteId_SkladisteTextBox.Text);

                    _context.SaveChanges();
                    _context.PostanskeUsluge_Paket.Load();

                    paketViewSource.Source = _context.PostanskeUsluge_Paket.Local;
                    paketViewSource.View.Refresh();


                }
                catch (Exception ex)
                {

                    MessageBox.Show("Trenutno nije moguce izmeniti paket.", "Error");
                    return;
                }

            }

        }
        private void AddCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            _context = new PostaDbContainer();
            Paket paket = new Paket();

            if (paket != null && Validacija())
            {
                try
                {
                    paket.Tezina = Convert.ToInt32(tezinaTextBox.Text);
                    paket.PostanskiBrojOdredista = Convert.ToInt32(postanskiBrojOdredistaTextBox.Text);
                    paket.PosiljalacIme = posiljalacImeTextBox.Text;
                    paket.PosiljalacPrezime = posiljalacPrezimeTextBox.Text;
                    paket.PrimalacIme = primalacImeTextBox.Text;
                    paket.PrimalacPrezime = primalacPrezimeTextBox.Text;
                    paket.Grad = gradTextBox.Text;
                    paket.Ulica = ulicaTextBox.Text;
                    paket.Broj = brojTextBox.Text;
                    paket.SluzbenikJMBG_Radnika = Convert.ToInt32(sluzbenikJMBG_RadnikaTextBox.Text);
                    paket.SluzbenikPostanskiBroj = Convert.ToInt32(sluzbenikPostanskiBrojTextBox.Text);
                    paket.PostarJMBG_Radnika = Convert.ToInt32(postarJMBG_RadnikaTextBox.Text);
                    paket.PostarPostanskiBroj = Convert.ToInt32(postarPostanskiBrojTextBox.Text);
                    paket.SkladisteId_Skladiste = Convert.ToInt32(skladisteId_SkladisteTextBox.Text);

                    _context.PostanskeUsluge_Paket.Add(paket);
                    _context.SaveChanges();
                    _context.PostanskeUsluge_Paket.Load();

                    paketViewSource.Source = _context.PostanskeUsluge_Paket.Local;
                    paketViewSource.View.Refresh();


                }
                catch (Exception ex)
                {

                    MessageBox.Show("Trenutno nije moguce dodati paket.", "Error");
                    return;
                }

            }

        }
        public bool Validacija()
        {
            string message = "";
            bool result = true;
            if (Int32.Parse(tezinaTextBox.Text) <= 0)
            {
                message += "Morate uneti tezinu paketa.";
                result = false;
            }


            if (Int32.Parse(postanskiBrojOdredistaTextBox.Text) <= 0)
            {
                message += "Morate uneti validan postanski broj odredista.";
                result = false;
            }

            if (String.IsNullOrEmpty(posiljalacImeTextBox.Text))
            {
                message += "Morate uneti ime posiljaoca.";
                result = false;
            }
            if (String.IsNullOrEmpty(posiljalacPrezimeTextBox.Text))
            {
                message += "Morate uneti prezime posiljaoca.";
                result = false;
            }
            if (String.IsNullOrEmpty(primalacImeTextBox.Text))
            {
                message += "Morate uneti ime primaoca.";
                result = false;

            }
            if (String.IsNullOrEmpty(primalacPrezimeTextBox.Text))
            {
                message += "Morate uneti prezime primaoca.";
                result = false;
            }
            if (String.IsNullOrEmpty(gradTextBox.Text))
            {
                message += "Morate uneti grad.";

                result = false;

            }
            if (String.IsNullOrEmpty(ulicaTextBox.Text))
            {
                result = false;
                message += "Morate uneti ulicu.";


            }
            if (String.IsNullOrEmpty(brojTextBox.Text))
            {
                result = false;
                message += "Morate uneti broj.";

            }
            if (Int32.Parse(sluzbenikJMBG_RadnikaTextBox.Text) <= 0)
            {
                result = false;
                message += "Morate uneti ispravan jmbg sluzbenika.";

            }
            if (Int32.Parse(sluzbenikPostanskiBrojTextBox.Text) <= 0)
            {
                result = false;
                message += "Morate uneti ispravan postanski broj sluzbenika.";

            }

            if (Int32.Parse(postarJMBG_RadnikaTextBox.Text) <= 0)
            {
                result = false;
                message += "Morate uneti ispravan jmbg postara.";

            }
            if (Int32.Parse(postarPostanskiBrojTextBox.Text) <= 0)
            {
                result = false;
                message += "Morate uneti ispravan postanski broj postara.";

            }
            if (Int32.Parse(skladisteId_SkladisteTextBox.Text) <= 0)
            {
                result = false;
                message += "Morate uneti ispravan id skladista.";

            }

            if (!String.IsNullOrEmpty(message))
            {
                MessageBox.Show(message, "Error");
            }

            return result;
        }
    }
}
