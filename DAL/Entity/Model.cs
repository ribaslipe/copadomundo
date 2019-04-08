namespace DAL.Entity
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model : DbContext
    {
        public Model()
            : base("name=Conexao")
        {
        }

        public virtual DbSet<jogo> jogo { get; set; }
        public virtual DbSet<temporada> temporada { get; set; }
        public virtual DbSet<time> time { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<time>()
                .HasMany(e => e.jogo)
                .WithOptional(e => e.time)
                .HasForeignKey(e => e.time1_id);

            modelBuilder.Entity<time>()
                .HasMany(e => e.jogo1)
                .WithOptional(e => e.time1)
                .HasForeignKey(e => e.time2_id);
        }
    }
}
