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
        private readonly SettingXByCalculatingGINAndActivation _settingXByCalculatingGINAndActivation;
        private NeuralNetworkErrorManager _errorManager;
        private readonly NeuralNetworkModel _neuralNetworkModel;

        public NormalizedTableController(GlassContext context, CalculatingMINorMaxForEachColumn calculator, NormalizeContext normalizeContext, NormalizeData normalizeData, TrainingContext trainingContext , TestingContext testingContext, SplittingTheTableInTwoParts splittingTheTableInTwoParts, SettingXByCalculatingGINAndActivation settingXByCalculatingGINAndActivation, NeuralNetworkErrorManager errorManager, NeuralNetworkModel neuralNetworkModel)
        {
            _context = context;
            _calculator = calculator;
            _normalizeContext = normalizeContext;
            _normalizeData = normalizeData;
            _trainingContext = trainingContext;
            _testingContext = testingContext;
            _splittingTheTableInTwoParts = splittingTheTableInTwoParts;
            _settingXByCalculatingGINAndActivation = settingXByCalculatingGINAndActivation;
            _errorManager = errorManager;
            _neuralNetworkModel = neuralNetworkModel;
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
        public void SetInputLayerWithTrainingValues(TrainingGlassModel trainingObject)
        {
            trainingObject = _trainingContext.TrainingTable2.FirstOrDefault(obj => obj.Id == trainingObject.Id);
            double outputResult;

            if (trainingObject != null)
            {
                var inputLayer = _tempLayers.FirstOrDefault();

                if (inputLayer != null)
                {
                    inputLayer.Neurons[0].x = trainingObject.RI;
                    inputLayer.Neurons[1].x = trainingObject.Na;
                    inputLayer.Neurons[2].x = trainingObject.Mg;
                    inputLayer.Neurons[3].x = trainingObject.Al;
                    inputLayer.Neurons[4].x = trainingObject.Si;
                    inputLayer.Neurons[5].x = trainingObject.K;
                    inputLayer.Neurons[6].x = trainingObject.Ca;
                    inputLayer.Neurons[7].x = trainingObject.Ba;
                    inputLayer.Neurons[8].x = trainingObject.Fe;

                    for (int i = 1; i < _tempLayers.Count; i++)
                    {
                        var currentLayer = _tempLayers[i];

                        foreach (var neuron in currentLayer.Neurons)
                        {
                            neuron.x = 0;

                            for (int j = 0; j < inputLayer.Neurons.Count; j++)
                            {
                                neuron.x += inputLayer.Neurons[j].x * neuron.weights[j];
                               
                            }

                            neuron.x = Sigmoid(neuron.x);
                            

                        }

                        inputLayer = currentLayer;
                    }
                }
            }
        }
     

        public void SetWForEachNeuron()
        {
            for (int epoca = 1; epoca <= 100; epoca++)
            {
                    double errorEpoca = 0;
                    var trainingData = _trainingContext.TrainingTable2.ToList();
                    double outputResult = 0;
                    for (int i = 1; i < trainingData.Count; i++)
                    {
                        var trainingObject = trainingData[i];
                        SetInputLayerWithTrainingValues(trainingObject);
                        double errorStep = 0;
                        var outputLayer = _tempLayers.Last();
                        double sigma = 0;
                        for (int j = 0; j < outputLayer.Neurons.Count; j++)
                        {
                            var neuron = outputLayer.Neurons[j];
                            outputResult = neuron.x;
                            double errorNeuron = neuron.x * (1 - neuron.x) * (neuron.x - trainingObject.Type);
                            for (int k = 0; k < neuron.weights.Count; k++)
                            {
                                neuron.weights[k] += -0.1 * _tempLayers[_tempLayers.Count - 2].Neurons[k].x * errorNeuron;
                            }
                            sigma = errorNeuron;
                        }

                    for (int j = _tempLayers.Count - 2; j > 0; j--)
                    {
                        var currentLayer = _tempLayers[j];
                        var nextLayer = _tempLayers[j + 1];
                        foreach (var neuron in currentLayer.Neurons)
                        {
                            double error = neuron.x * (1 - neuron.x);
                            double sum = 0;

                            for (int l = 0; l < nextLayer.Neurons.Count; l++)
                            {
                                sum += nextLayer.Neurons[l].weights[currentLayer.Neurons.IndexOf(neuron)] * error;
                            }

                            error = sum;

                            for (int l = 0; l < neuron.weights.Count; l++)
                            {
                                neuron.weights[l] += -0.1 * _tempLayers[j - 1].Neurons[l].x * error;
                            }
                        }
                    }
                    errorStep = 0.5 * (outputResult - trainingObject.Type) * (outputResult - trainingObject.Type);
                    errorEpoca += errorStep;
                    }
                errorEpoca /= 171;
                _neuralNetworkModel.Errors.Add(errorEpoca);
            }
            
        }



        private double Sigmoid(double x)
        {
            return 1 / (1 + Math.Exp(-x));
        }

        public IActionResult ViewNeuronalNetwork(string neuronsPerLayer)
        {
            var neuronsList = neuronsPerLayer.Split(',').Select(int.Parse).ToList();
            var viewModel = new NeuralNetworkModel(neuronsList)
            {
                Layers = _tempLayers,
                Errors = _neuralNetworkModel.GetErrors()
            };
            SetWForEachNeuron();
            return View(viewModel);
        }





    }
}
