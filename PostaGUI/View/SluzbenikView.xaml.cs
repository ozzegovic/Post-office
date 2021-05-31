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
    /// Interaction logic for SluzbenikView.xaml
    /// </summary>
    public partial class SluzbenikView : UserControl
    {
        PostaDbContainer _context;
        CollectionViewSource sluzbenikViewSource;

        public SluzbenikView()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {


            //  Do not load your data at design time.
            if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            {
                //Load your data here and assign the result to the CollectionViewSource.
                sluzbenikViewSource = (System.Windows.Data.CollectionViewSource)this.Resources["sluzbenikViewSource"];

                // Load is an extension method on IQueryable,
                // defined in the System.Data.Entity namespace.
                // This method enumerates the results of the query,
                // similar to ToList but without creating a list.
                // When used with Linq to Entities this method
                // creates entity objects and adds them to the context.
                _context = new PostaDbContainer();
                _context.Radnici.Load();
                _context.Radnici_Sluzbenik.Load();

                // After the data is loaded call the DbSet<T>.Local property
                // to use the DbSet<T> as a binding source.
                sluzbenikViewSource.Source = _context.Radnici_Sluzbenik.Local;
            }
        }
        private void DeleteCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            _context = new PostaDbContainer();
            var cur = sluzbenikViewSource.View.CurrentItem as Sluzbenik;

            var radnik = (from c in _context.Radnici_Sluzbenik
                          where c.JMBG_Radnika == cur.JMBG_Radnika
                          select c).FirstOrDefault();

            if (radnik != null)
            {
                try
                {
                    _context.Radnici_Sluzbenik.Remove(radnik);

                    _context.SaveChanges();
                    _context.Radnici_Sluzbenik.Load();

                    sluzbenikViewSource.Source = _context.Radnici_Sluzbenik.Local;
                    sluzbenikViewSource.View.Refresh();


                }
                catch (Exception ex)
                {

                    MessageBox.Show("Trenutno nije moguce obrisati sluzbenika.", "Error");
                    return;
                }

            }

        }
        private void UpdateCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            _context = new PostaDbContainer();
            var cur = sluzbenikViewSource.View.CurrentItem as Sluzbenik;

            var radnik = (from c in _context.Radnici_Sluzbenik
                          where c.JMBG_Radnika == cur.JMBG_Radnika
                         select c).FirstOrDefault() as Sluzbenik;

            if (radnik != null)
            {
                try
                {

                    radnik.Odeljenje = odeljenjeTextBox.Text;
                    radnik.Ime = imeTextBox.Text;
                    radnik.Prezime = prezimeTextBox.Text;

                    _context.SaveChanges();
                    _context.Radnici_Sluzbenik.Load();

                    sluzbenikViewSource.Source = _context.Radnici_Sluzbenik.Local;
                    sluzbenikViewSource.View.Refresh();


                }
                catch (Exception ex)
                {

                    MessageBox.Show("Trenutno nije moguce izmeniti sluzbenika.", "Error");
                    return;
                }

            }

        }
        private void AddCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {

            _context = new PostaDbContainer();

            Sluzbenik sluzbenik = new Sluzbenik();
            if (sluzbenik != null)
            {
                try
                {
                    
                    sluzbenik.Odeljenje = odeljenjeTextBox.Text;
                    sluzbenik.JMBG_Radnika =Convert.ToInt32(jMBG_RadnikaTextBox.Text);
                    sluzbenik.PostaPostanskiBroj = Convert.ToInt32(postanskiBrojTextBox.Text);
                    sluzbenik.Ime = imeTextBox.Text;
                    sluzbenik.Prezime = prezimeTextBox.Text;
                    _context.Radnici_Sluzbenik.Add(sluzbenik);

                    _context.SaveChanges();
                    _context.Radnici_Sluzbenik.Load();

                    sluzbenikViewSource.Source = _context.Radnici_Sluzbenik.Local;
                    sluzbenikViewSource.View.Refresh();


                }
                catch (Exception ex)
                {

                    MessageBox.Show("Trenutno nije moguce dodati sluzbenika.", "Error");
                    return;
                }

            }


        }
    }
}
