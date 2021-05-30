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
        PostaDbContainer _context = new PostaDbContainer();

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
                System.Windows.Data.CollectionViewSource postanskaUslugaViewSource = (System.Windows.Data.CollectionViewSource)this.Resources["postanskaUslugaViewSource"];

                // Load is an extension method on IQueryable,
                // defined in the System.Data.Entity namespace.
                // This method enumerates the results of the query,
                // similar to ToList but without creating a list.
                // When used with Linq to Entities this method
                // creates entity objects and adds them to the context.
                _context.PostanskeUsluge.Load();

                // After the data is loaded call the DbSet<T>.Local property
                // to use the DbSet<T> as a binding source.
                postanskaUslugaViewSource.Source = _context.PostanskeUsluge.Local;
            }
        }
    }
}
