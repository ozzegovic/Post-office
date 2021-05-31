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
    /// Interaction logic for PostanskaUslugaView.xaml
    /// </summary>
    public partial class PostanskaUslugaView : UserControl
    {
        PostaDbContainer _context;
        CollectionViewSource postanskaUslugaViewSource;

        public PostanskaUslugaView()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {


            //  Do not load your data at design time.
            if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            {
                //Load your data here and assign the result to the CollectionViewSource.
                postanskaUslugaViewSource = (System.Windows.Data.CollectionViewSource)this.Resources["postanskaUslugaViewSource"];

                // Load is an extension method on IQueryable,
                // defined in the System.Data.Entity namespace.
                // This method enumerates the results of the query,
                // similar to ToList but without creating a list.
                // When used with Linq to Entities this method
                // creates entity objects and adds them to the context.
                _context = new PostaDbContainer();
                _context.PostanskeUsluge.Load();

                // After the data is loaded call the DbSet<T>.Local property
                // to use the DbSet<T> as a binding source.
                postanskaUslugaViewSource.Source = _context.PostanskeUsluge.Local;
            }
        }
        private void DeleteCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            _context = new PostaDbContainer();
            var cur = postanskaUslugaViewSource.View.CurrentItem as PostanskaUsluga;

            var postanskaUsluga = (from c in _context.PostanskeUsluge
                         where c.ID_Posiljke == cur.ID_Posiljke
                         select c).FirstOrDefault();

            if (postanskaUsluga != null)
            {
                try
                {
                    _context.PostanskeUsluge.Remove(postanskaUsluga);

                    _context.SaveChanges();
                    _context.PostanskeUsluge.Load();

                    postanskaUslugaViewSource.Source = _context.PostanskeUsluge.Local;
                    postanskaUslugaViewSource.View.Refresh();


                }
                catch (Exception ex)
                {

                    MessageBox.Show("Trenutno nije moguce izbrisati postansku uslugu.", "Error");
                    return;
                }

            }


        }
        private void UpdateCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            _context = new PostaDbContainer();
            var cur = postanskaUslugaViewSource.View.CurrentItem as PostanskaUsluga;

            var postanskaUsluga = (from c in _context.PostanskeUsluge
                                   where c.ID_Posiljke == cur.ID_Posiljke
                                   select c).FirstOrDefault();

            if (postanskaUsluga != null)
            {
                try
                {
                    postanskaUsluga.PostanskiBrojOdredista = Convert.ToInt32(postanskiBrojOdredistaTextBox.Text);
                    postanskaUsluga.PosiljalacPrezime = posiljalacImeTextBox.Text;
                    postanskaUsluga.PosiljalacPrezime = posiljalacPrezimeTextBox.Text;
                    postanskaUsluga.PrimalacIme = primalacImeTextBox.Text;
                    postanskaUsluga.PrimalacPrezime = primalacPrezimeTextBox.Text;
                    postanskaUsluga.Grad = gradTextBox.Text;
                    postanskaUsluga.Ulica = ulicaTextBox.Text;
                    postanskaUsluga.Broj = brojTextBox.Text;
                    postanskaUsluga.SluzbenikJMBG_Radnika = Convert.ToInt32(sluzbenikJMBG_RadnikaTextBox.Text);
                    postanskaUsluga.SluzbenikPostanskiBroj = Convert.ToInt32(sluzbenikPostanskiBrojTextBox.Text);
                    postanskaUsluga.PostarJMBG_Radnika = Convert.ToInt32(postarJMBG_RadnikaTextBox.Text);
                    postanskaUsluga.PostarPostanskiBroj = Convert.ToInt32(postarPostanskiBrojTextBox.Text);
                    postanskaUsluga.SkladisteId_Skladiste = Convert.ToInt32(skladisteId_SkladisteTextBox.Text);
                    _context.SaveChanges();
                    _context.PostanskeUsluge.Load();

                    postanskaUslugaViewSource.Source = _context.PostanskeUsluge.Local;
                    postanskaUslugaViewSource.View.Refresh();


                }
                catch (Exception ex)
                {

                    MessageBox.Show("Trenutno nije moguce izmeniti postansku uslugu.", "Error");
                    return;
                }

            }

        }
        private void AddCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            _context = new PostaDbContainer();

            //PostanskaUsluga postanskaUsluga = new PostanskaUsluga();

            //if (postanskaUsluga != null)
            //{
            //    try
            //    {
            //        postanskaUsluga.PostanskiBrojOdredista = Convert.ToInt32(postanskiBrojOdredistaTextBox.Text);
            //        postanskaUsluga.PosiljalacPrezime = posiljalacImeTextBox.Text;
            //        postanskaUsluga.PosiljalacPrezime = posiljalacPrezimeTextBox.Text;
            //        postanskaUsluga.PrimalacIme = primalacImeTextBox.Text;
            //        postanskaUsluga.PrimalacPrezime = primalacPrezimeTextBox.Text;
            //        postanskaUsluga.Grad = gradTextBox.Text;
            //        postanskaUsluga.Ulica = ulicaTextBox.Text;
            //        postanskaUsluga.Broj = brojTextBox.Text;

            //        _context.PostanskeUsluge.Add(postanskaUsluga);
            //        _context.SaveChanges();
            //        _context.PostanskeUsluge.Load();

            //        postanskaUslugaViewSource.Source = _context.PostanskeUsluge.Local;
            //        postanskaUslugaViewSource.View.Refresh();


            //    }
            //    catch (Exception ex)
            //    {

            //        MessageBox.Show("Trenutno nije moguce dodati postansku uslugu.", "Error");
            //        return;
            //    }

            //}

        }
    }
}
