using Microsoft.AspNetCore.Mvc;
using Project3_InteligentaArtificiala_.Helper;
using Project3_InteligentaArtificiala_.Models;

namespace Project3_InteligentaArtificiala_.Controllers
{
    public class NormalizedTableController : Controller
    {
        private readonly GlassContext _context;
        private readonly CalculatingMINorMaxForEachColumn _calculator;
        private readonly NormalizeContext _normalizeContext;
        private readonly NormalizeData _normalizeData;
        public NormalizedTableController(GlassContext context, CalculatingMINorMaxForEachColumn calculator, NormalizeContext normalizeContext, NormalizeData normalizeData)
        {
            _context = context;
            _calculator = calculator;
            _normalizeContext = normalizeContext;
            _normalizeData = normalizeData;
        }

        public IActionResult Index()
        {
            var normalizedGlassList = _normalizeContext.NormalizeTable.ToList();
            _normalizeData.NormalizingData();
            return View(normalizedGlassList);
        }
    }
}
