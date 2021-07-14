using Microsoft.EntityFrameworkCore;


namespace CheckingUTP.Models.DataBase
{
    public class Context:DbContext
    {

        public DbSet<UTP> UTPs { get; set; }
        public DbSet<Forma_obuchen> Forma_obuchen { get; set; }
        public DbSet<UTP_type_training_load> UTP_type_training_load { get; set; }
        public DbSet<Type_trainig_load> Type_trainig_load { get; set; }
     
        public Context(DbContextOptions<Context> options) : base(options)
        {
            //Database.EnsureCreated();
            
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }
    }
}
