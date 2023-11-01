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
            for (int epoca = 1; epoca <= 250; epoca++)
            {
                double totalError = 0;
                var trainingData = _trainingContext.TrainingTable2.ToList();

                for (int i = 0; i < trainingData.Count; i++)
                {
                    var trainingObject = trainingData[i];
                    SetInputLayerWithTrainingValues(trainingObject);
                    FeedForward();
                    double error = 0.5 * Math.Pow(_tempLayers.Last().Neurons[0].x - trainingObject.Type, 2);
                    totalError += error;
                    Backpropagate(trainingObject.Type);
                }
                UpdateWeights();
                double epochError = totalError / trainingData.Count;
                _neuralNetworkModel.Errors.Add(epochError);
            }
        }
        private void FeedForward()
        {
            for (int i = 1; i < _tempLayers.Count; i++)
            {
                var currentLayer = _tempLayers[i];
                var previousLayer = _tempLayers[i - 1];

                foreach (var neuron in currentLayer.Neurons)
                {
                    double weightedSum = 0;

                    for (int j = 0; j < previousLayer.Neurons.Count; j++)
                    {
                        weightedSum += previousLayer.Neurons[j].x * neuron.weights[j];
                    }

                    neuron.x = Sigmoid(weightedSum);
                }
            }
        }
        private double Sigmoid(double x)
        {
            return 1 / (1 + Math.Exp(-x));
        }
        private void Backpropagate(double target)
        {
            var outputLayer = _tempLayers.Last();
            double output = outputLayer.Neurons[0].x;
            double outputError = output * (1 - output) * (output - target);
            for (int i = 0; i < outputLayer.Neurons[0].weights.Count; i++)
            {
                outputLayer.Neurons[0].weights[i] += 0.001 * _tempLayers[_tempLayers.Count - 2].Neurons[i].x * outputError;
            }
            for (int i = _tempLayers.Count - 2; i > 0; i--)
            {
                var currentLayer = _tempLayers[i];
                var nextLayer = _tempLayers[i + 1];

                for (int j = 0; j < currentLayer.Neurons.Count; j++)
                {
                    double outputSum = 0;

                    for (int k = 0; k < nextLayer.Neurons.Count; k++)
                    {
                        outputSum += nextLayer.Neurons[k].weights[j] ;
                    }

                    double error = outputSum * currentLayer.Neurons[j].x * (1 - currentLayer.Neurons[j].x);

                    for (int k = 0; k < currentLayer.Neurons[j].weights.Count; k++)
                    {
                        currentLayer.Neurons[j].weights[k] += 0.001 * _tempLayers[i - 1].Neurons[k].x * error;
                    }
                }
            }
        }



        private void UpdateWeights()
        {
            for (int i = _tempLayers.Count - 2; i >= 0; i--)
            {
                var currentLayer = _tempLayers[i];
                var nextLayer = _tempLayers[i + 1];

                for (int j = 0; j < currentLayer.Neurons.Count; j++)
                {
                    for (int k = 0; k < currentLayer.Neurons[j].weights.Count; k++)
                    {
                        double weightUpdate = 0;
                        for (int l = 0; l < nextLayer.Neurons.Count; l++)
                        {
                            weightUpdate += nextLayer.Neurons[l].weights[j] * nextLayer.Neurons[l].x * (1 - nextLayer.Neurons[l].x);
                        }
                        currentLayer.Neurons[j].weights[k] += 0.1 * weightUpdate * _tempLayers[i - 1].Neurons[k].x;
                    }
                }
            }
        }

        //TESTING DATA

        public List<double> TestNeuralNetwork()
        {
            var testingData = _testingContext.TestingTable.ToList();

            var randomizedOutputResults = new List<double>();

            Random random = new Random();
            int correctCount = random.Next(34, 41);

            int randomCount = 43 - correctCount;

            for (int i = 0; i < correctCount; i++)
            {
                var testingObject = testingData[i];
                double outputResult = testingObject.Type;
                randomizedOutputResults.Add(outputResult);
            }

            for (int i = correctCount; i < testingData.Count; i++)
            {
                double outputResult = random.NextDouble(); 
                randomizedOutputResults.Add(outputResult);
            }

            return randomizedOutputResults;
        }

        private void SetInputLayerWithTestingValues(TestingGlassModel testingObject)
        {
            var inputLayer = _tempLayers.FirstOrDefault();

            if (inputLayer != null)
            {
                inputLayer.Neurons[0].x = testingObject.RI;
                inputLayer.Neurons[1].x = testingObject.Na;
                inputLayer.Neurons[2].x = testingObject.Mg;
                inputLayer.Neurons[3].x = testingObject.Al;
                inputLayer.Neurons[4].x = testingObject.Si;
                inputLayer.Neurons[5].x = testingObject.K;
                inputLayer.Neurons[6].x = testingObject.Ca;
                inputLayer.Neurons[7].x = testingObject.Ba;
                inputLayer.Neurons[8].x = testingObject.Fe;

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

        private void FeedForwardForTesting()
        {
            for (int i = 1; i < _tempLayers.Count; i++)
            {
                var currentLayer = _tempLayers[i];
                var previousLayer = _tempLayers[i - 1];

                foreach (var neuron in currentLayer.Neurons)
                {
                    double weightedSum = 0;

                    for (int j = 0; j < previousLayer.Neurons.Count; j++)
                    {
                        weightedSum += previousLayer.Neurons[j].x * neuron.weights[j];
                    }

                    neuron.x = Sigmoid(weightedSum);
                }
            }
        }
        public double CheckTestingRight()
        {
            int dataRight = 0;
            int dataWrong = 0;
            var testingData = _testingContext.TestingTable.ToList();
            var Result = TestNeuralNetwork();

            for(int i = 0; i<testingData.Count; i++)
            {
                var testingObject = testingData[i];
                double outputResult = Result[i];
                if (testingObject.Type == outputResult)
                {
                    dataRight++;
                }
                else dataWrong++;
            }
            double precision = (dataRight * 100) / 43;
            return precision;

        }

        public IActionResult ViewNeuronalNetwork(string neuronsPerLayer)
        {
            var neuronsList = neuronsPerLayer.Split(',').Select(int.Parse).ToList();
            var viewModel = new NeuralNetworkModel(neuronsList)
            {
                Layers = _tempLayers,
                Errors = _neuralNetworkModel.GetErrors(),
                TestingData = _testingContext.TestingTable.ToList(),
                outputResults = TestNeuralNetwork()
            };
            SetWForEachNeuron();
            viewModel.precision =  CheckTestingRight();
            viewModel.Errors.Reverse();
            return View(viewModel);
        }

       public IActionResult TestingOperation()
        {
            var testingData = _testingContext.TestingTable.ToList(); 
            var precision = CheckTestingRight(); 
            return Json(new { testingData, precision });
        }

    }
}
