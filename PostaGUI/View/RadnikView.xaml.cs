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
using System.Windows.Shapes;

namespace PostaGUI.View
{
    /// <summary>
    /// Interaction logic for RadnikView.xaml
    /// </summary>
    public partial class RadnikView : UserControl
    {
        PostaDbContainer _context;
        CollectionViewSource radnikViewSource;

        public RadnikView()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

           
            //  Do not load your data at design time.
            if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            {
                //Load your data here and assign the result to the CollectionViewSource.
                 radnikViewSource = (System.Windows.Data.CollectionViewSource)this.Resources["radnikViewSource"];

                // Load is an extension method on IQueryable,
                // defined in the System.Data.Entity namespace.
                // This method enumerates the results of the query,
                // similar to ToList but without creating a list.
                // When used with Linq to Entities this method
                // creates entity objects and adds them to the context.
                _context = new PostaDbContainer(); 
                _context.Radnici.Load();

                // After the data is loaded call the DbSet<T>.Local property
                // to use the DbSet<T> as a binding source.
                radnikViewSource.Source = _context.Radnici.Local;
            }
        }
        private void DeleteCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            _context = new PostaDbContainer();
            var cur = radnikViewSource.View.CurrentItem as Radnik;

            var radnik = (from c in _context.Radnici
                         where c.JMBG_Radnika == cur.JMBG_Radnika
                          select c).FirstOrDefault();

            if (radnik != null)
            {
                try
                {
                    _context.Radnici.Remove(radnik);

                    _context.SaveChanges();
                    _context.Radnici.Load();

                    radnikViewSource.Source = _context.Radnici.Local;
                    radnikViewSource.View.Refresh();


                }
                catch (Exception ex)
                {

                    MessageBox.Show("Trenutno nije moguce obrisati radnika.", "Error");
                    return;
                }

            }

        }
        private void UpdateCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            _context = new PostaDbContainer();
            var cur = radnikViewSource.View.CurrentItem as Radnik;

            var radnik = (from c in _context.Radnici
                         where c.JMBG_Radnika == cur.JMBG_Radnika
                          select c).FirstOrDefault();

            if (radnik != null)
            {
                try
                {
                    radnik.Ime = imeTextBox.Text;
                    radnik.Prezime = prezimeTextBox.Text;
                    radnik.JMBG_Radnika = Convert.ToInt32(jMBG_RadnikaTextBox.Text);
                    radnik.PostaPostanskiBroj = Convert.ToInt32(postaPostanskiBrojTextBox.Text);

                    _context.SaveChanges();
                    _context.Radnici.Load();

                    radnikViewSource.Source = _context.Radnici.Local;
                    radnikViewSource.View.Refresh();


                }
                catch (Exception ex)
                {

                    MessageBox.Show("Trenutno nije moguce izmeniti radnika.", "Error");
                    return;
                }

            }


        }
        private void AddCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            _context = new PostaDbContainer();

            Radnik radnik = new Radnik();

            if (radnik != null)
            {
                try
                {
                    radnik.Ime = imeTextBox.Text;
                    radnik.Prezime = prezimeTextBox.Text;
                    radnik.JMBG_Radnika = Convert.ToInt32(jMBG_RadnikaTextBox.Text);
                    radnik.PostaPostanskiBroj = Convert.ToInt32(postaPostanskiBrojTextBox.Text);

                    _context.Radnici.Add(radnik);
                    _context.SaveChanges();
                    _context.Radnici.Load();

                    radnikViewSource.Source = _context.Radnici.Local;
                    radnikViewSource.View.Refresh();


                }
                catch (Exception ex)
                {

                    MessageBox.Show("Trenutno nije moguce dodati radnika.", "Error");
                    return;
                }

            }


        }
    }
}
