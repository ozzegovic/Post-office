﻿using DatabaseModel;
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
    /// Interaction logic for FinansijskaUslugaView.xaml
    /// </summary>
    public partial class FinansijskaUslugaView : UserControl
    {
        PostaDbContainer _context = new PostaDbContainer();
        CollectionViewSource finansijskaUslugaViewSource;


        public FinansijskaUslugaView()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

            //  Do not load your data at design time.
            if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            {
                //Load your data here and assign the result to the CollectionViewSource.
                finansijskaUslugaViewSource = (System.Windows.Data.CollectionViewSource)this.Resources["finansijskaUslugaViewSource"];

                // Load is an extension method on IQueryable,
                // defined in the System.Data.Entity namespace.
                // This method enumerates the results of the query,
                // similar to ToList but without creating a list.
                // When used with Linq to Entities this method
                // creates entity objects and adds them to the context.
                _context.FinansijskaUslugas.Load();

                // After the data is loaded call the DbSet<T>.Local property
                // to use the DbSet<T> as a binding source.
                finansijskaUslugaViewSource.Source = _context.FinansijskaUslugas.Local;
            }
        }
        private void DeleteCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {

            _context = new PostaDbContainer();
            var cur = finansijskaUslugaViewSource.View.CurrentItem as FinansijskaUsluga;

            var finUsluga = (from c in _context.FinansijskaUslugas
                          where c.ID_Uplate == cur.ID_Uplate
                          select c).FirstOrDefault();

            if (finUsluga != null)
            {
                try
                {
                    _context.FinansijskaUslugas.Remove(finUsluga);

                    _context.SaveChanges();
                    _context.FinansijskaUslugas.Load();

                    finansijskaUslugaViewSource.Source = _context.FinansijskaUslugas.Local;
                    finansijskaUslugaViewSource.View.Refresh();


                }
                catch (Exception ex)
                {

                    MessageBox.Show("Trenutno nije moguce obrisati finansijsku uslugu.", "Error");
                    return;
                }

            }

        }
        private void UpdateCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            _context = new PostaDbContainer();
            var cur = finansijskaUslugaViewSource.View.CurrentItem as FinansijskaUsluga;

            var finUsluga = (from c in _context.FinansijskaUslugas
                             where c.ID_Uplate == cur.ID_Uplate
                             select c).FirstOrDefault();

            if (finUsluga != null)
            {
                try
                {
                    finUsluga.Iznos = Convert.ToDouble(iznosTextBox.Text);
                    finUsluga.PosiljalacPrezime = posiljalacImeTextBox.Text;
                    finUsluga.PosiljalacPrezime = posiljalacPrezimeTextBox.Text;
                    finUsluga.PrimalacIme = primalacImeTextBox.Text;
                    finUsluga.PrimalacPrezime = primalacPrezimeTextBox.Text;
                    finUsluga.Grad = gradTextBox.Text;
                    finUsluga.Ulica = ulicaTextBox.Text;
                    finUsluga.Broj = brojTextBox.Text;

                    _context.SaveChanges();
                    _context.FinansijskaUslugas.Load();

                    finansijskaUslugaViewSource.Source = _context.FinansijskaUslugas.Local;
                    finansijskaUslugaViewSource.View.Refresh();


                }
                catch (Exception ex)
                {

                    MessageBox.Show("Trenutno nije moguce izmeniti finansijsku uslugu.", "Error");
                    return;
                }

            }


        }
        private void AddCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {

            _context = new PostaDbContainer();

            FinansijskaUsluga finUsluga = new FinansijskaUsluga();

            if (finUsluga != null)
            {
                try
                {
                    
                    finUsluga.Iznos = Convert.ToDouble(iznosTextBox.Text);
                    finUsluga.PosiljalacPrezime = posiljalacImeTextBox.Text;
                    finUsluga.PosiljalacPrezime = posiljalacPrezimeTextBox.Text;
                    finUsluga.PrimalacIme = primalacImeTextBox.Text;
                    finUsluga.PrimalacPrezime = primalacPrezimeTextBox.Text;
                    finUsluga.Grad = gradTextBox.Text;
                    finUsluga.Ulica = ulicaTextBox.Text;
                    finUsluga.Broj = brojTextBox.Text;

                    _context.FinansijskaUslugas.Add(finUsluga);
                    _context.SaveChanges();
                    _context.FinansijskaUslugas.Load();

                    finansijskaUslugaViewSource.Source = _context.FinansijskaUslugas.Local;
                    finansijskaUslugaViewSource.View.Refresh();


                }
                catch (Exception ex)
                {

                    MessageBox.Show("Trenutno nije moguce dodati finansijsku uslugu.", "Error");
                    return;
                }

            }


        }

    }
}