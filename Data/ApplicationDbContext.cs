using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebMatrixUploader.Data.DataForMatrix;

namespace WebMatrixUploader.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public DbSet<UserFile> UsersFiles { get; set; }
    public DbSet<CurveData> CurveData { get; set; }
    public DbSet<Abscissa> Abscissa { get; set; }
    public DbSet<Ordinate> Ordinates { get; set; }
}