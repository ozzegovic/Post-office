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
    /// Interaction logic for PostNetView.xaml
    /// </summary>
    public partial class PostNetView : UserControl
    {
        PostaDbContainer _context;
        CollectionViewSource postNetViewSource;

        public PostNetView()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

           
            //  Do not load your data at design time.
            if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            {
                //Load your data here and assign the result to the CollectionViewSource.
                postNetViewSource = (System.Windows.Data.CollectionViewSource)this.Resources["postNetViewSource"];

                // Load is an extension method on IQueryable,
                // defined in the System.Data.Entity namespace.
                // This method enumerates the results of the query,
                // similar to ToList but without creating a list.
                // When used with Linq to Entities this method
                // creates entity objects and adds them to the context.
                _context = new PostaDbContainer();
                _context.PostNets.Load();

                // After the data is loaded call the DbSet<T>.Local property
                // to use the DbSet<T> as a binding source.
                postNetViewSource.Source = _context.PostNets.Local;
            }
        }
        private void DeleteCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            _context = new PostaDbContainer();
            var cur = postNetViewSource.View.CurrentItem as PostNet;

            var postnet = (from c in _context.PostNets
                             where c.ID_Uplate == cur.ID_Uplate
                             select c).FirstOrDefault();

            if (postnet != null)
            {
                try
                {
                    _context.PostNets.Remove(postnet);

                    _context.SaveChanges();
                    _context.PostNets.Load();

                    postNetViewSource.Source = _context.PostNets.Local;
                    postNetViewSource.View.Refresh();


                }
                catch (Exception ex)
                {

                    MessageBox.Show("Trenutno nije moguce obrisati postnet uslugu.", "Error");
                    return;
                }

            }


        }
        private void UpdateCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            _context = new PostaDbContainer();
            var cur = postNetViewSource.View.CurrentItem as PostNet;

            var postnet = (from c in _context.PostNets
                           where c.ID_Uplate == cur.ID_Uplate
                           select c).FirstOrDefault();

            if (postnet != null)
            {
                try
                {
                    postnet.BrojTelefona = Convert.ToInt32(brojTelefonaTextBox.Text);
                    postnet.JMBG_Primaoca = Convert.ToInt32(jMBG_PrimaocaTextBox.Text);
                    _context.SaveChanges();
                    _context.PostNets.Load();

                    postNetViewSource.Source = _context.PostNets.Local;
                    postNetViewSource.View.Refresh();


                }
                catch (Exception ex)
                {

                    MessageBox.Show("Trenutno nije moguce izmeniti postnet uslugu.", "Error");
                    return;
                }

            }


        }
        private void AddCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            _context = new PostaDbContainer();
            PostNet postnet = new PostNet();

            if (postnet != null)
            {
                try
                {
                    postnet.BrojTelefona = Convert.ToInt32(brojTelefonaTextBox.Text);
                    postnet.JMBG_Primaoca = Convert.ToInt32(jMBG_PrimaocaTextBox.Text);
                    _context.PostNets.Add(postnet);
                    _context.SaveChanges();
                    _context.PostNets.Load();

                    postNetViewSource.Source = _context.PostNets.Local;
                    postNetViewSource.View.Refresh();


                }
                catch (Exception ex)
                {

                    MessageBox.Show("Trenutno nije moguce dodati postnet uslugu.", "Error");
                    return;
                }

            }

        }
    }
}
