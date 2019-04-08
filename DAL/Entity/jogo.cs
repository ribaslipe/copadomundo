namespace DAL.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("jogo")]
    public partial class jogo
    {
        [Key]
        public int jogo_id { get; set; }

        public DateTime? jogo_data { get; set; }

        public int? time1_id { get; set; }

        public int? time1_gols { get; set; }

        public int? time2_id { get; set; }

        public int? time2_gols { get; set; }

        public int? temporada_id { get; set; }

        public virtual temporada temporada { get; set; }

        public virtual time time { get; set; }

        public virtual time time1 { get; set; }
    }
}
