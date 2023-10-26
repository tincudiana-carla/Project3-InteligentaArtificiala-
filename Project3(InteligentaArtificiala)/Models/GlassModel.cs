using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Project3_InteligentaArtificiala_.Models
{
    public class GlassModel
    {
        public int Id { get; set; } 
        public double RI { get; set; } 
        public double Na { get; set; }
        public double Mg { get; set; }
        public double Al { get; set; } 
        public double Si { get; set; } 
        public double K { get; set; } 
        public double Ca { get; set; } 
        public double Ba { get; set; } 
        public double Fe { get; set; } 
        public int Type { get; set; }
        public GlassModel()
        {
        }
        public GlassModel(double ri, double na, double mg, double al, double si, double k, double ca, double ba, double fe, int type)
        {
            RI = ri;
            Na = na;
            Mg = mg;
            Al = al;
            Si = si;
            K = k;
            Ca = ca;
            Ba = ba;
            Fe = fe;
            Type = type;
        }
    }
    public class GlassContext : DbContext
    {
        public DbSet<GlassModel> GlassTable{ get; set; }
        public GlassContext(DbContextOptions<GlassContext> options) : base(options)
        {
        }
    }

}
