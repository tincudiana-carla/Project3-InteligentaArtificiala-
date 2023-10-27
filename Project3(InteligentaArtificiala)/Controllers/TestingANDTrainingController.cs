using Microsoft.AspNetCore.Mvc;
using Project3_InteligentaArtificiala_.Helper;
using Project3_InteligentaArtificiala_.Models;

namespace Project3_InteligentaArtificiala_.Controllers
{
    public class TestingANDTrainingController : Controller
    {
        private readonly NormalizeContext _normalizeContext;
        private readonly NormalizeData _normalizeData;
        private readonly TestingContext _testingContext;
        private readonly TrainingContext _trainingContext;
        public TestingANDTrainingController(NormalizeContext normalizeContext, NormalizeData normalizeData, TestingContext testingContext, TrainingContext trainingContext)
        {
            _normalizeContext = normalizeContext;
            _normalizeData = normalizeData;
            _testingContext = testingContext;
            _trainingContext = trainingContext;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
