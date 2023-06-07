namespace BlazorApp
{
    public class DbService
    {
        public readonly AppDbContext db;

        public DbService(AppDbContext db)
        {
            this.db = db;
        }
    }
}