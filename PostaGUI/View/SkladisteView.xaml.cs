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
                System.Windows.Data.CollectionViewSource skladistaViewSource = (System.Windows.Data.CollectionViewSource)this.Resources["skladisteViewSource"];

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


    }
}
