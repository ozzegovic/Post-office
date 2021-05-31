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
    /// Interaction logic for PostarView.xaml
    /// </summary>
    public partial class PostarView : UserControl
    {
        PostaDbContainer _context;
        CollectionViewSource postarViewSource;

        public PostarView()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {


            //  Do not load your data at design time.
            if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            {
                //Load your data here and assign the result to the CollectionViewSource.
                postarViewSource = (System.Windows.Data.CollectionViewSource)this.Resources["postarViewSource"];

                // Load is an extension method on IQueryable,
                // defined in the System.Data.Entity namespace.
                // This method enumerates the results of the query,
                // similar to ToList but without creating a list.
                // When used with Linq to Entities this method
                // creates entity objects and adds them to the context.
                _context = new PostaDbContainer();
                _context.Radnici_Postar.Load();

                // After the data is loaded call the DbSet<T>.Local property
                // to use the DbSet<T> as a binding source.
                postarViewSource.Source = _context.Radnici_Postar.Local;
            }
        }
        private void DeleteCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            _context = new PostaDbContainer();
            var cur = postarViewSource.View.CurrentItem as Postar;

            var postar = (from c in _context.Radnici_Postar
                          where c.JMBG_Radnika == cur.JMBG_Radnika
                          select c).FirstOrDefault();

            if (postar != null)
            {
                try
                {
                    _context.Radnici_Postar.Remove(postar);

                    _context.SaveChanges();
                    _context.Radnici_Postar.Load();

                    postarViewSource.Source = _context.Radnici_Postar.Local;
                    postarViewSource.View.Refresh();


                }
                catch (Exception ex)
                {

                    MessageBox.Show("Trenutno nije moguce obrisati postara.", "Error");
                    return;
                }

            }

        }
        private void UpdateCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            _context = new PostaDbContainer();
            var cur = postarViewSource.View.CurrentItem as Postar;

            var postar = (from c in _context.Radnici_Postar
                          where c.JMBG_Radnika == cur.JMBG_Radnika
                          select c).FirstOrDefault();

            if (postar != null)
            {
                try
                {

                    postar.DeoGrada = deoGradaTextBox.Text;
                    postar.Ime = imeTextBox.Text;
                    postar.Prezime = prezimeTextBox.Text;
                    postar.JMBG_Radnika = Convert.ToInt32(jMBG_RadnikaTextBox.Text);
                    postar.PostaPostanskiBroj = Convert.ToInt32(postanskiBrojTextBox.Text);

                    _context.SaveChanges();
                    _context.Radnici_Postar.Load();

                    postarViewSource.Source = _context.Radnici_Postar.Local;
                    postarViewSource.View.Refresh();


                }
                catch (Exception ex)
                {

                    MessageBox.Show("Trenutno nije moguce izmeniti postara.", "Error");
                    return;
                }

            }

        }
        private void AddCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {

            _context = new PostaDbContainer();

            Postar postar = new Postar();
            if (postar != null)
            {
                try
                {

                    postar.DeoGrada = deoGradaTextBox.Text;
                    postar.JMBG_Radnika = Convert.ToInt32(jMBG_RadnikaTextBox.Text);
                    postar.PostaPostanskiBroj = Convert.ToInt32(postanskiBrojTextBox.Text);
                    postar.Ime = imeTextBox.Text;
                    postar.Prezime = prezimeTextBox.Text;

                    _context.Radnici_Postar.Add(postar);
                    _context.SaveChanges();
                    _context.Radnici_Postar.Load();
                 
                    postarViewSource.Source = _context.Radnici_Postar.Local;
                    postarViewSource.View.Refresh();


                }
                catch (Exception ex)
                {

                    MessageBox.Show("Trenutno nije moguce dodati postara.", "Error");
                    return;
                }

            }


        }
    }
}
