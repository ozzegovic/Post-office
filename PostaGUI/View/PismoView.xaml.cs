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
                _context.Pismoes.Load();

                // After the data is loaded call the DbSet<T>.Local property
                // to use the DbSet<T> as a binding source.
                pismoViewSource.Source = _context.Pismoes.Local;
            }
        }
        private void DeleteCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            _context = new PostaDbContainer();
            var cur = pismoViewSource.View.CurrentItem as Pismo;

            var pismo = (from c in _context.Pismoes
                         where c.ID_Posiljke == cur.ID_Posiljke
                         select c).FirstOrDefault();

            if (pismo != null)
            {
                try
                {
                    _context.Pismoes.Remove(pismo);
                    _context.SaveChanges();
                    _context.Pismoes.Load();

                    pismoViewSource.Source = _context.Pismoes.Local;
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

            var pismo = (from c in _context.Pismoes
                         where c.ID_Posiljke == cur.ID_Posiljke
                         select c).FirstOrDefault();

            if (pismo != null)
            {
                try
                {
                    pismo.Preporuceno = preporucenoCheckBox.IsChecked ?? false;
                    _context.SaveChanges();
                    _context.Pismoes.Load();

                    pismoViewSource.Source = _context.Pismoes.Local;
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
                    _context.Pismoes.Add(pismo);
                    _context.SaveChanges();
                    _context.Pismoes.Load();

                    pismoViewSource.Source = _context.Pismoes.Local;
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
