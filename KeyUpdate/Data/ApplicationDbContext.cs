using KeyUpdate.Models;
using Microsoft.EntityFrameworkCore;

namespace KeyUpdate.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Empresa> Empresas { get; set; } = null!;
        public DbSet<Manifest> Manifests { get; set; } = null!;
        public DbSet<KeySisCfg> KeySisCfgs { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Empresa>(entity =>
            {
                entity.ToTable("empresas");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.EmpCodigo).IsRequired();
                entity.HasIndex(e => e.EmpCodigo).IsUnique().HasDatabaseName("uq_emp_codigo");

                entity.Property(e => e.EmpNome).IsRequired().HasMaxLength(120);
                entity.HasIndex(e => e.EmpNome).IsUnique().HasDatabaseName("uq_emp_nome");

                entity.Property(e => e.ExeMode).IsRequired().HasMaxLength(10);
                entity.Property(e => e.Ativo).IsRequired();
                entity.HasIndex(e => e.Ativo).HasDatabaseName("idx_ativo");

                entity.Property(e => e.AtualizadoEm)
                    .IsRequired()
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .ValueGeneratedOnAddOrUpdate();
            });

            modelBuilder.Entity<Manifest>(entity =>
            {
                entity.ToTable("manifest");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.ManifestId).IsRequired();
                entity.HasIndex(e => e.ManifestId).HasDatabaseName("idx_manifest_id");

                entity.Property(e => e.Ativo).IsRequired();
                entity.HasIndex(e => e.Ativo).HasDatabaseName("idx_ativo");
            });

            modelBuilder.Entity<KeySisCfg>(entity =>
            {
                entity.ToTable("keysiscfg");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.EmpCodigo).IsRequired();
                entity.HasIndex(e => e.EmpCodigo).HasDatabaseName("idx_emp_codigo");

                entity.Property(e => e.EmpNome).IsRequired().HasMaxLength(120);
                entity.HasIndex(e => e.EmpNome).HasDatabaseName("idx_emp_nome");

                entity.Property(e => e.Tipo).IsRequired().HasMaxLength(10);

                entity.Property(e => e.Ativo).IsRequired();
                entity.HasIndex(e => e.Ativo).HasDatabaseName("idx_ativo");
            });
        }
    }
}
