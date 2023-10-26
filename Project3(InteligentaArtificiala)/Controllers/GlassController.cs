using System;
using Microsoft.AspNetCore.Mvc;
using Project3_InteligentaArtificiala_.Helper;
using Project3_InteligentaArtificiala_.Models;

namespace Project3_InteligentaArtificiala_.Controllers
{
    public class GlassController : Controller
    {
        private readonly GlassContext _context;
        private readonly CalculatingMINorMaxForEachColumn _calculator;

        public GlassController(GlassContext context, CalculatingMINorMaxForEachColumn calculator)
        {
            _context = context;
            _calculator = calculator;
        }

        public IActionResult Index()
        {
            var glassList = _context.GlassTable.ToList();
            return View(glassList);
        }
    }
}
