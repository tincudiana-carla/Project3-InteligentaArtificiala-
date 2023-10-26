using Microsoft.EntityFrameworkCore;

namespace Project3_InteligentaArtificiala_.Models
{
    public class NormalizedDataGlassModel
    {
        public double Id { get; set; }
        public double RI { get; set; }
        public double Na { get; set; }
        public double Mg { get; set; }
        public double Al { get; set; }
        public double Si { get; set; }
        public double K { get; set; }
        public double Ca { get; set; }
        public double Ba { get; set; }
        public double Fe { get; set; }
        public double Type { get; set; }
        public NormalizedDataGlassModel()
        {
        }
        public NormalizedDataGlassModel(double ri, double na, double mg, double al, double si, double k, double ca, double ba, double fe, double type)
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
    public class NormalizeContext : DbContext
    {
        public DbSet<NormalizedDataGlassModel> NormalizeTable { get; set; }

        public NormalizeContext(DbContextOptions<NormalizeContext> options) : base(options)
        {
        }
    }

}
