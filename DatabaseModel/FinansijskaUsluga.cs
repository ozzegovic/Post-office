//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DatabaseModel
{
    using System;
    using System.Collections.ObjectModel;
    
    public partial class FinansijskaUsluga
    {
        public int ID_Uplate { get; set; }
        public int SluzbenikJMBG_Radnika { get; set; }
        public decimal SluzbenikPostanskiBroj { get; set; }
        public string PrimalacIme { get; set; }
        public string PrimalacPrezime { get; set; }
        public string PosiljalacIme { get; set; }
        public string PosiljalacPrezime { get; set; }
        public string Grad { get; set; }
        public string Ulica { get; set; }
        public string Broj { get; set; }
        public double Iznos { get; set; }
    
        public virtual Sluzbenik Sluzbenik { get; set; }
        public virtual Uplatnica Uplatnica { get; set; }
        public virtual PostNet PostNet { get; set; }
    }
}
