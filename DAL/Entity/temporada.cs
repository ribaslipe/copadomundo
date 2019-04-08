namespace DAL.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("temporada")]
    public partial class temporada
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public temporada()
        {
            jogo = new HashSet<jogo>();
        }

        [Key]
        public int temporada_id { get; set; }

        [Column("temporada")]
        [StringLength(500)]
        public string temporada1 { get; set; }

        [Column("atual")]
        public bool? atual { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<jogo> jogo { get; set; }
    }
}
