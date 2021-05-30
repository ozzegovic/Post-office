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
    /// Interaction logic for UplatnicaView.xaml
    /// </summary>
    public partial class UplatnicaView : UserControl
    {
        PostaDbContainer _context;
        CollectionViewSource uplatnicaViewSource;

        public UplatnicaView()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {


            //  Do not load your data at design time.
            if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            {
                //Load your data here and assign the result to the CollectionViewSource.
                uplatnicaViewSource = (System.Windows.Data.CollectionViewSource)this.Resources["uplatnicaViewSource"];

                // Load is an extension method on IQueryable,
                // defined in the System.Data.Entity namespace.
                // This method enumerates the results of the query,
                // similar to ToList but without creating a list.
                // When used with Linq to Entities this method
                // creates entity objects and adds them to the context.
                _context = new PostaDbContainer();
                _context.Uplatnicas.Load();

                // After the data is loaded call the DbSet<T>.Local property
                // to use the DbSet<T> as a binding source.
                uplatnicaViewSource.Source = _context.Uplatnicas.Local;
            }
        }
        private void DeleteCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            _context = new PostaDbContainer();
            var cur = uplatnicaViewSource.View.CurrentItem as Uplatnica;

            var uplatnica = (from c in _context.Uplatnicas
                             where c.ID_Uplate == cur.ID_Uplate
                             select c).FirstOrDefault();

            if (uplatnica != null)
            {
                try
                {
                    _context.Uplatnicas.Remove(uplatnica);

                    _context.SaveChanges();
                    _context.Uplatnicas.Load();

                    uplatnicaViewSource.Source = _context.Uplatnicas.Local;
                    uplatnicaViewSource.View.Refresh();


                }
                catch (Exception ex)
                {

                    MessageBox.Show("Trenutno nije moguce obrisati uplatnicu.", "Error");
                    return;
                }

            }


        }
        private void UpdateCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            _context = new PostaDbContainer();
            var cur = uplatnicaViewSource.View.CurrentItem as Uplatnica;

            var uplatnica = (from c in _context.Uplatnicas
                             where c.ID_Uplate == cur.ID_Uplate
                             select c).FirstOrDefault();

            if (uplatnica != null)
            {
                try
                {
                    uplatnica.BrojRacuna = Convert.ToInt32(brojRacunaTextBox.Text);


                    _context.SaveChanges();
                    _context.Uplatnicas.Load();

                    uplatnicaViewSource.Source = _context.Uplatnicas.Local;
                    uplatnicaViewSource.View.Refresh();


                }
                catch (Exception ex)
                {

                    MessageBox.Show("Trenutno nije moguce izmeniti uplatnicu.", "Error");
                    return;
                }

            }

        }
        private void AddCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            _context = new PostaDbContainer();
            Uplatnica uplatnica = new Uplatnica();
            if (uplatnica != null)
            {
                try
                {
                    uplatnica.BrojRacuna = Convert.ToInt32(brojRacunaTextBox.Text);
                    

                    _context.SaveChanges();
                    _context.Uplatnicas.Load();

                    uplatnicaViewSource.Source = _context.Uplatnicas.Local;
                    uplatnicaViewSource.View.Refresh();


                }
                catch (Exception ex)
                {

                    MessageBox.Show("Trenutno nije moguce dodati uplatnicu.", "Error");
                    return;
                }

            }


        }
    }
}
