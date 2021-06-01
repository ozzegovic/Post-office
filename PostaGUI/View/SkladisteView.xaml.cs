using DatabaseModel;
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
using System.Windows.Shapes;
using System.Data.Entity;

namespace PostaGUI.View
{
    /// <summary>
    /// Interaction logic for SkladisteView.xaml
    /// </summary>
    public partial class SkladisteView : UserControl
    {

        PostaDbContainer _context = new PostaDbContainer();
        CollectionViewSource skladistaViewSource;
        public SkladisteView()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

            //  Do not load your data at design time.
            if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            {
                //Load your data here and assign the result to the CollectionViewSource.
                skladistaViewSource = (System.Windows.Data.CollectionViewSource)this.Resources["skladisteViewSource"];

                // Load is an extension method on IQueryable,
                // defined in the System.Data.Entity namespace.
                // This method enumerates the results of the query,
                // similar to ToList but without creating a list.
                // When used with Linq to Entities this method
                // creates entity objects and adds them to the context.
                _context.Skladista.Load();

                // After the data is loaded call the DbSet<T>.Local property
                // to use the DbSet<T> as a binding source.
                skladistaViewSource.Source = _context.Skladista.Local;
            }
        }
        private void DeleteCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {

            var cur = skladistaViewSource.View.CurrentItem as Skladiste;

            var skladiste = (from c in _context.Skladista
                             where c.Id_Skladiste == cur.Id_Skladiste
                             select c).FirstOrDefault();

            if (skladiste != null)
            {
                _context.Skladista.Remove(skladiste);
                //  context.Customers.Remove(cust);
            }
            _context.SaveChanges();
            skladistaViewSource.View.Refresh();

        }
        private void UpdateCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {

            var cur = skladistaViewSource.View.CurrentItem as Skladiste;

            var skladiste = (from c in _context.Skladista
                             where c.Id_Skladiste == cur.Id_Skladiste
                             select c).FirstOrDefault();

            if (skladiste != null)
            {
                skladiste.Grad = gradTextBox.Text;
                skladiste.Ulica = ulicaTextBox.Text;
                skladiste.Broj = brojTextBox.Text;
                if (!String.IsNullOrEmpty(postaPostanskiBrojTextBox.Text))
                {
                    int postanskibr = Convert.ToInt32(postaPostanskiBrojTextBox.Text);
                    var posta = _context.Poste.Any(c => c.PostanskiBroj == postanskibr);

                    if (posta)
                    {
                        var p = _context.Poste.Where(c => c.PostanskiBroj == postanskibr).FirstOrDefault();
                        skladiste.PostaPostanskiBroj = p.PostanskiBroj;

                    }
                    else
                    {
                        skladiste.PostaPostanskiBroj = null;

                    }
                }
                else
                {
                    skladiste.PostaPostanskiBroj = null;
                }
            }
            _context.SaveChanges();
            skladistaViewSource.View.Refresh();
        }
        private void AddCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {

            Skladiste skladiste = new Skladiste();
            skladiste.Grad = gradTextBox.Text;
            skladiste.Ulica = ulicaTextBox.Text;
            skladiste.Broj = brojTextBox.Text;
            //skladiste moze bez poste, ali ako se ubaci postanski broj, mora da bude taj koji vec postoji
            if (!String.IsNullOrEmpty(postaPostanskiBrojTextBox.Text))
            {
                int postanskibr = Convert.ToInt32(postaPostanskiBrojTextBox.Text);
                var posta = _context.Poste.Any(c => c.PostanskiBroj == postanskibr);

                if (posta)
                {
                    var p = _context.Poste.Where(c => c.PostanskiBroj == postanskibr).FirstOrDefault();
                    skladiste.PostaPostanskiBroj = p.PostanskiBroj;

                }
                else
                {
                    skladiste.PostaPostanskiBroj = null;

                }
            }
            else
            {
                skladiste.PostaPostanskiBroj = null;
            }

            _context.Skladista.Add(skladiste);
            _context.SaveChanges();
            skladistaViewSource.View.Refresh();

        }

        public bool Validacija()
        {
            string message = "";
            bool result = true;
            if (String.IsNullOrEmpty(gradTextBox.Text))
            {
                result = false;
                message += "Morate uneti naziv  grada.";

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

            if (Int32.Parse(postaPostanskiBrojTextBox.Text) <= 0)
            {
                result = false;
                message += "Morate uneti ispravan postanski broj poste.";

            }


            if (!String.IsNullOrEmpty(message))
            {
                MessageBox.Show(message, "Error");
            }

            return result;
        }
        public void ClearFields()
        {
            gradTextBox.Text = "";
            ulicaTextBox.Text = "";
            brojTextBox.Text = "";

            postaPostanskiBrojTextBox.Text = "";


        }
    }
}
