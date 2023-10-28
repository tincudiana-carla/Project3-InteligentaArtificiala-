using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Project3_InteligentaArtificiala_.Helper;
using Project3_InteligentaArtificiala_.Models;

namespace Project3_InteligentaArtificiala_.Controllers
{
    public class ChangingInputsAndWeights : Controller
    {
        private readonly GlassContext _context;
        private readonly CalculatingMINorMaxForEachColumn _calculator;
        private readonly NormalizeContext _normalizeContext;
        private readonly NormalizeData _normalizeData;
        private readonly SplittingTheTableInTwoParts _splittingTheTableInTwoParts;
        private readonly TestingContext _testingContext;
        private readonly TrainingContext _trainingContext;
        private static List<LayerModel> _tempLayers = new List<LayerModel>();

        public ChangingInputsAndWeights(GlassContext context, CalculatingMINorMaxForEachColumn calculator, NormalizeContext normalizeContext, NormalizeData normalizeData, TrainingContext trainingContext, TestingContext testingContext, SplittingTheTableInTwoParts splittingTheTableInTwoParts)
        {
            _context = context;
            _calculator = calculator;
            _normalizeContext = normalizeContext;
            _normalizeData = normalizeData;
            _trainingContext = trainingContext;
            _testingContext = testingContext;
            _splittingTheTableInTwoParts = splittingTheTableInTwoParts;
        }
        public void SetInputLayerWithTrainingValues()
        {
            var firstTrainingObject = _trainingContext.TrainingTable2.FirstOrDefault();

            if (firstTrainingObject != null)
            {
                var inputLayer = _tempLayers.FirstOrDefault();
                if (inputLayer != null)
                {
                    inputLayer.Neurons[0].x = Math.Round(firstTrainingObject.RI);
                    inputLayer.Neurons[1].x = firstTrainingObject.Na;
                    inputLayer.Neurons[2].x = firstTrainingObject.Mg;
                    inputLayer.Neurons[3].x = firstTrainingObject.Al;
                    inputLayer.Neurons[4].x = firstTrainingObject.Si;
                    inputLayer.Neurons[5].x = firstTrainingObject.K;
                    inputLayer.Neurons[6].x = firstTrainingObject.Ca;
                    inputLayer.Neurons[7].x = firstTrainingObject.Ba;
                    inputLayer.Neurons[8].x = firstTrainingObject.Fe;

                   
                }
            }
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
