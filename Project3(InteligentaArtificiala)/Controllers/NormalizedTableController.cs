using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
        private readonly SplittingTheTableInTwoParts _splittingTheTableInTwoParts;
        private readonly TestingContext _testingContext;
        private readonly TrainingContext _trainingContext;
        private static List<LayerModel> _tempLayers = new List<LayerModel>();

        public NormalizedTableController(GlassContext context, CalculatingMINorMaxForEachColumn calculator, NormalizeContext normalizeContext, NormalizeData normalizeData, TrainingContext trainingContext , TestingContext testingContext, SplittingTheTableInTwoParts splittingTheTableInTwoParts)
        {
            _context = context;
            _calculator = calculator;
            _normalizeContext = normalizeContext;
            _normalizeData = normalizeData;
            _trainingContext = trainingContext;
            _testingContext = testingContext;
            _splittingTheTableInTwoParts = splittingTheTableInTwoParts; 
        }

        public IActionResult Index()
        {
            var normalizedGlassList = _normalizeContext.NormalizeTable.ToList();
            var testingData = _testingContext.TestingTable.ToList(); 
            _normalizeData.NormalizingData();
            var viewModel = new ModelHelper
            {
                NormalizedGlassList = normalizedGlassList,
                TestingData = testingData
            };

            return View(viewModel);
        }
        public IActionResult ChoosingTestingTable()
        {
            var normalizedGlassList = _normalizeContext.NormalizeTable.ToList();
            var testingData = _testingContext.TestingTable.ToList(); 
            var viewModel = new ModelHelper
            {
                NormalizedGlassList = normalizedGlassList,
                TestingData = testingData
            };
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult ConfigureNeuralNetwork()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ConfigureNeuralNetwork(int numberOfLayers, List<int> neuronsPerLayer)
        {
            var layers = new List<LayerModel>();
            int numberOfConnections = 0; 
            for (int i = 0; i < numberOfLayers; i++)
            {
                int numberOfNeurons = neuronsPerLayer[i];
                layers.Add(new LayerModel(numberOfNeurons, numberOfConnections));
                numberOfConnections = numberOfNeurons; 
            }

            var neuralNetwork = new NeuralNetworkModel(neuronsPerLayer)
            {
                Layers = layers
            };
            _tempLayers = layers;
            return RedirectToAction("ViewNeuronalNetwork", new { neuronsPerLayer = string.Join(",", neuronsPerLayer) });
        }
        public void SetInputLayerWithTrainingValues()
        {
            var firstTrainingObject = _trainingContext.TrainingTable2.FirstOrDefault();

            if (firstTrainingObject != null)
            {
                var inputLayer = _tempLayers.FirstOrDefault();
                if (inputLayer != null)
                {
                    inputLayer.Neurons[0].x = firstTrainingObject.RI;
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
        public IActionResult ViewNeuronalNetwork(string neuronsPerLayer)
        {
            var neuronsList = neuronsPerLayer.Split(',').Select(int.Parse).ToList();
            var viewModel = new NeuralNetworkModel(neuronsList)
            {
                Layers = _tempLayers
            };
            SetInputLayerWithTrainingValues();

            return View(viewModel);
        }





    }
}
