using Microsoft.EntityFrameworkCore;

namespace Orange_Portfolio_BackEnd.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {

        }
    }
}
