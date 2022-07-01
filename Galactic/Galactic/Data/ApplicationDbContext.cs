using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Text.RegularExpressions;

namespace Galactic.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public static string ToPostgresqlName(string name) =>
            Regex.Replace(name, "((?<=[a-z])[A-Z]|[A-Z](?=[a-z]))", "_$1").Trim('_').ToLower();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            //Fix table names
            builder.Model.GetEntityTypes()
                .ToList()
#pragma warning disable CS8604 // Possible null reference argument.
                .ForEach(t => t.SetTableName(ToPostgresqlName(t.GetTableName())));
#pragma warning restore CS8604 // Possible null reference argument.

            builder.Model
                .GetEntityTypes()
                .ToList()
                .ForEach(t => t
                    .GetProperties()
                    .ToList()
                    .ForEach(p => p
                        .SetColumnName(ToPostgresqlName(
#pragma warning disable CS8604 // Possible null reference argument.
                            p.GetColumnName(StoreObjectIdentifier.Table(t.GetTableName(), null))))));
#pragma warning restore CS8604 // Possible null reference argument.
        }
    }
}
