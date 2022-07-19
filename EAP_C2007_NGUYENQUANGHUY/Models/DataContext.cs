using System.Data.Entity;


namespace EAP_C2007_NGUYENQUANGHUY.Models
{
    public class DataContext : DbContext
    {
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public DataContext()
        {
            Database.SetInitializer<DataContext>(new DataInitializer());
            //Database.Initialize(true);
        }
    }
}