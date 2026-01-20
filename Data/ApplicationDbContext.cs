using Microsoft.EntityFrameworkCore;
using WorkFlowApp.Models;

namespace WorkFlowApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<TekstilStok> TekstilStoklari { get; set; }
        public DbSet<MamulStok> MamulStoklari { get; set; }
        public DbSet<PaketlemeStok> PaketlemeStoklari { get; set; }
        public DbSet<SatinalmaTalep> SatinalmaTalepleri { get; set; }
        public DbSet<RaporTalebi> RaporTalepleri { get; set; }
        public DbSet<Makine> Makineler { get; set; }
        public DbSet<TekstilUretim> TekstilUretimleri { get; set; }
        public DbSet<KaplamaVeri> KaplamaUretimleri { get; set; }
        public DbSet<PazarlamaSiparis> SatisSiparisleri { get; set; }
        public DbSet<IplikStok> IplikStoklari { get; set; }
        public DbSet<MuhasebeKayit> MuhasebeKayitlari { get; set; }
    }
}