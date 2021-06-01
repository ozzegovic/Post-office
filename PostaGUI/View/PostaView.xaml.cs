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
    /// Interaction logic for PostaView.xaml
    /// </summary>
    public partial class PostaView : UserControl
    {
        PostaDbContainer _context;
        CollectionViewSource postaViewSource;

        public PostaView()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

          
            //  Do not load your data at design time.
            if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            {
                //Load your data here and assign the result to the CollectionViewSource.
                postaViewSource = (System.Windows.Data.CollectionViewSource)this.Resources["postaViewSource"];

                // Load is an extension method on IQueryable,
                // defined in the System.Data.Entity namespace.
                // This method enumerates the results of the query,
                // similar to ToList but without creating a list.
                // When used with Linq to Entities this method
                // creates entity objects and adds them to the context.
                _context = new PostaDbContainer();
                _context.Poste.Load();

                // After the data is loaded call the DbSet<T>.Local property
                // to use the DbSet<T> as a binding source.
                postaViewSource.Source = _context.Poste.Local;
            }
        }
        private void DeleteCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            _context = new PostaDbContainer();
            var cur = postaViewSource.View.CurrentItem as Posta;

            var posta = (from c in _context.Poste
                             where c.PostanskiBroj == cur.PostanskiBroj
                             select c).FirstOrDefault();

            if (posta != null)
            {
                try
                {
                    _context.Poste.Remove(posta);

                    _context.SaveChanges();
                    _context.Poste.Load();

                    postaViewSource.Source = _context.Poste.Local;
                    postaViewSource.View.Refresh();


                }
                catch (Exception ex)
                {
                   
                   // postaViewSource.View.Refresh();
                    MessageBox.Show("Trenutno nije moguce obrisati postu.", "Error");
                    return;
                }

            }
            

        }
        private void UpdateCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            _context = new PostaDbContainer();
            var cur = postaViewSource.View.CurrentItem as Posta;

            var posta = (from c in _context.Poste
                         where c.PostanskiBroj == cur.PostanskiBroj
                         select c).FirstOrDefault();

            if (posta != null && Validacija())
            {
                try
                {
                    posta.Grad = gradTextBox.Text;
                    posta.Ulica = ulicaTextBox.Text;
                    posta.Broj = brojTextBox.Text;

                    _context.SaveChanges();
                    _context.Poste.Load();

                    postaViewSource.Source = _context.Poste.Local;
                    postaViewSource.View.Refresh();


                }
                catch (Exception ex)
                {

                    MessageBox.Show("Trenutno nije moguce izmeniti postu.", "Error");
                    return;
                }

            }

        }
        private void AddCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {

            _context = new PostaDbContainer();

            Posta posta = new Posta();

            if (posta != null && Validacija())
            {
                try
                {
                    posta.PostanskiBroj = Convert.ToDecimal(postanskiBrojTextBox.Text);
                    posta.Grad = gradTextBox.Text;
                    posta.Ulica = ulicaTextBox.Text;
                    posta.Broj = brojTextBox.Text;

                    _context.Poste.Add(posta);
                    _context.SaveChanges();
                    _context.Poste.Load();

                    postaViewSource.Source = _context.Poste.Local;
                    postaViewSource.View.Refresh();


                }
                catch (Exception ex)
                {

                    MessageBox.Show("Trenutno nije moguce dodati postu.", "Error");
                    return;
                }

            }
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
          
            if (Int32.Parse(postanskiBrojTextBox.Text) <= 0)
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
    }
}
