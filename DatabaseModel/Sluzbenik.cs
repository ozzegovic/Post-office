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
    
    public partial class Sluzbenik
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Sluzbenik()
        {
            this.PostanskaUslugas = new ObservableCollection<PostanskaUsluga>();
            this.FinansijskaUslugas = new ObservableCollection<FinansijskaUsluga>();
        }
    
        public int JMBG_Radnika { get; set; }
        public decimal PostanskiBroj { get; set; }
        public string Odeljenje { get; set; }
    
        public virtual Radnik Radnik { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ObservableCollection<PostanskaUsluga> PostanskaUslugas { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ObservableCollection<FinansijskaUsluga> FinansijskaUslugas { get; set; }
    }
}
