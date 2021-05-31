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
    /// Interaction logic for PismoView.xaml
    /// </summary>
    public partial class PismoView : UserControl
    {
        PostaDbContainer _context;
        CollectionViewSource pismoViewSource;

        public PismoView()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {


            //  Do not load your data at design time.
            if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            {
                //Load your data here and assign the result to the CollectionViewSource.
                 pismoViewSource = (System.Windows.Data.CollectionViewSource)this.Resources["pismoViewSource"];

                // Load is an extension method on IQueryable,
                // defined in the System.Data.Entity namespace.
                // This method enumerates the results of the query,
                // similar to ToList but without creating a list.
                // When used with Linq to Entities this method
                // creates entity objects and adds them to the context.
                _context = new PostaDbContainer();
                _context.PostanskeUsluge_Pismo.Load();

                // After the data is loaded call the DbSet<T>.Local property
                // to use the DbSet<T> as a binding source.
                pismoViewSource.Source = _context.PostanskeUsluge_Pismo.Local;
            }
        }
        private void DeleteCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            _context = new PostaDbContainer();
            var cur = pismoViewSource.View.CurrentItem as Pismo;

            var pismo = (from c in _context.PostanskeUsluge_Pismo
                         where c.ID_Posiljke == cur.ID_Posiljke
                         select c).FirstOrDefault();

            if (pismo != null)
            {
                try
                {
                    _context.PostanskeUsluge_Pismo.Remove(pismo);
                    _context.SaveChanges();
                    _context.PostanskeUsluge_Pismo.Load();

                    pismoViewSource.Source = _context.PostanskeUsluge_Pismo.Local;
                    pismoViewSource.View.Refresh();


                }
                catch (Exception ex)
                {

                    MessageBox.Show("Trenutno nije moguce izbrisati pismo.", "Error");
                    return;
                }

            }


        }
        private void UpdateCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            _context = new PostaDbContainer();
            var cur = pismoViewSource.View.CurrentItem as Pismo;

            var pismo = (from c in _context.PostanskeUsluge_Pismo
                         where c.ID_Posiljke == cur.ID_Posiljke
                         select c).FirstOrDefault();

            if (pismo != null)
            {
                try
                {
                    pismo.Preporuceno = preporucenoCheckBox.IsChecked ?? false;

                    //nisam sigurna da li sme da se menja odavde bilo sta sto nije samo za pismo, ali mi je lakse da imam ovde
                    // oni polja koja su od postanske usluge se mogu meenjati iz postanske usluge i te promene ce se videti i ovde
                    pismo.PostanskiBrojOdredista = Convert.ToInt32(postanskiBrojOdredistaTextBox.Text);
                    pismo.PosiljalacPrezime = posiljalacImeTextBox.Text;
                    pismo.PosiljalacPrezime = posiljalacPrezimeTextBox.Text;
                    pismo.PrimalacIme = primalacImeTextBox.Text;
                    pismo.PrimalacPrezime = primalacPrezimeTextBox.Text;
                    pismo.Grad = gradTextBox.Text;
                    pismo.Ulica = ulicaTextBox.Text;
                    pismo.Broj = brojTextBox.Text;
                    pismo.SluzbenikJMBG_Radnika = Convert.ToInt32(sluzbenikJMBG_RadnikaTextBox.Text);
                    pismo.SluzbenikPostanskiBroj = Convert.ToInt32(sluzbenikPostanskiBrojTextBox.Text);
                    pismo.PostarJMBG_Radnika = Convert.ToInt32(postarJMBG_RadnikaTextBox.Text);
                    pismo.PostarPostanskiBroj = Convert.ToInt32(postarPostanskiBrojTextBox.Text);
                    pismo.SkladisteId_Skladiste = Convert.ToInt32(skladisteId_SkladisteTextBox.Text);

                    _context.SaveChanges();
                    _context.PostanskeUsluge_Pismo.Load();

                     pismoViewSource.Source = _context.PostanskeUsluge_Pismo.Local;
                    pismoViewSource.View.Refresh();


                }
                catch (Exception ex)
                {

                    MessageBox.Show("Trenutno nije moguce izmeniti pismo.", "Error");
                    return;
                }

            }

        }
        private void AddCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            _context = new PostaDbContainer();
            Pismo pismo = new Pismo();

            if (pismo != null)
            {
                try
                {
                    pismo.Preporuceno = preporucenoCheckBox.IsChecked ?? false;
                    pismo.PostanskiBrojOdredista = Convert.ToInt32(postanskiBrojOdredistaTextBox.Text);
                    pismo.PosiljalacPrezime = posiljalacImeTextBox.Text;
                    pismo.PosiljalacPrezime = posiljalacPrezimeTextBox.Text;
                    pismo.PrimalacIme = primalacImeTextBox.Text;
                    pismo.PrimalacPrezime = primalacPrezimeTextBox.Text;
                    pismo.Grad = gradTextBox.Text;
                    pismo.Ulica = ulicaTextBox.Text;
                    pismo.Broj = brojTextBox.Text;
                    pismo.SluzbenikJMBG_Radnika = Convert.ToInt32(sluzbenikJMBG_RadnikaTextBox.Text);
                    pismo.SluzbenikPostanskiBroj = Convert.ToInt32(sluzbenikPostanskiBrojTextBox.Text);
                    pismo.PostarJMBG_Radnika = Convert.ToInt32(postarJMBG_RadnikaTextBox.Text);
                    pismo.PostarPostanskiBroj = Convert.ToInt32(postarPostanskiBrojTextBox.Text);
                    pismo.SkladisteId_Skladiste = Convert.ToInt32(skladisteId_SkladisteTextBox.Text);

                    _context.PostanskeUsluge_Pismo.Add(pismo);
                    _context.SaveChanges();
                    _context.PostanskeUsluge_Pismo.Load();

                    pismoViewSource.Source = _context.PostanskeUsluge_Pismo.Local;
                    pismoViewSource.View.Refresh();


                }
                catch (Exception ex)
                {

                    MessageBox.Show("Trenutno nije moguce dodati pismo.", "Error");
                    return;
                }

            }


        }
    }
}
