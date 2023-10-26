using Microsoft.EntityFrameworkCore.Migrations;
using Project3_InteligentaArtificiala_.Models;

namespace Project3_InteligentaArtificiala_.Helper
{
    public class CalculatingMINorMaxForEachColumn
    {
        private readonly GlassContext _context;
        public CalculatingMINorMaxForEachColumn(GlassContext context)
        {
            _context = context;
        }

        public float CalculateMinForRI()
        {
            return (float)_context.GlassTable.Min(g => g.RI);
        }

        public float CalculateMaxForRI()
        {
            return (float)_context.GlassTable.Max(g => g.RI);
        }
        
        public float CalculateMinForNa()
        {
            return (float)_context.GlassTable.Min(g => g.Na);
        }
        public float CalculateMaxForNa()
        {
            return (float)_context.GlassTable.Max(g => g.Na);
        }

        public float CalculateMinForMg()
        {
            return (float)_context.GlassTable.Min(g => g.Mg);
        }

        public float CalculateMaxForMg()
        {
            return (float)_context.GlassTable.Max(g => g.Mg);
        }

        public float CalculateMinForAl()
        {
            return (float)_context.GlassTable.Min(g => g.Al);
        }

        public float CalculateMaxForAl()
        {
            return (float)_context.GlassTable.Max(g => g.Al);
        }

        public float CalculateMinForSi()
        {
            return (float)_context.GlassTable.Min(g => g.Si);
        }

        public float CalculateMaxForSi()
        {
            return (float)_context.GlassTable.Max(g => g.Si);
        }

        public float CalculateMinForK()
        {
            return (float)_context.GlassTable.Min(g => g.K);
        }

        public float CalculateMaxForK()
        {
            return (float)_context.GlassTable.Max(g => g.K);
        }

        public float CalculateMinForCa()
        {
            return (float)_context.GlassTable.Min(g => g.Ca);
        }

        public float CalculateMaxForCa()
        {
            return (float)_context.GlassTable.Max(g => g.Ca);
        }

        public float CalculateMinForBa()
        {
            return (float)_context.GlassTable.Min(g => g.Ba);
        }

        public float CalculateMaxForBa()
        {
            return (float)_context.GlassTable.Max(g => g.Ba);
        }

        public float CalculateMinForFe()
        {
            return (float)_context.GlassTable.Min(g => g.Fe);
        }

        public float CalculateMaxForFe()
        {
            return (float)_context.GlassTable.Max(g => g.Fe);
        }
    }
}
