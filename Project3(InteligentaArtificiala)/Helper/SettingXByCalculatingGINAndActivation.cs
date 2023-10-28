using Project3_InteligentaArtificiala_.Models;

namespace Project3_InteligentaArtificiala_.Helper
{
    public class SettingXByCalculatingGINAndActivation
    {
        private readonly GlassContext _context;
        private readonly CalculatingMINorMaxForEachColumn _calculator;
        private readonly NormalizeContext _normalizeContext;
        private readonly NormalizeData _normalizeData;
        private readonly SplittingTheTableInTwoParts _splittingTheTableInTwoParts;
        private readonly TestingContext _testingContext;
        private readonly TrainingContext _trainingContext;
        private static List<LayerModel> _tempLayers = new List<LayerModel>();

        public SettingXByCalculatingGINAndActivation(GlassContext context, CalculatingMINorMaxForEachColumn calculator, NormalizeContext normalizeContext, NormalizeData normalizeData, TrainingContext trainingContext, TestingContext testingContext, SplittingTheTableInTwoParts splittingTheTableInTwoParts)
        {
            _context = context;
            _calculator = calculator;
            _normalizeContext = normalizeContext;
            _normalizeData = normalizeData;
            _trainingContext = trainingContext;
            _testingContext = testingContext;
            _splittingTheTableInTwoParts = splittingTheTableInTwoParts;
        }
        //public void SetInputLayerWithTrainingValues()
        //{
        //    var firstTrainingObject = _trainingContext.TrainingTable2.FirstOrDefault();

        //    if (firstTrainingObject != null)
        //    {
        //        var inputLayer = _tempLayers.FirstOrDefault();
        //        if (inputLayer != null)
        //        {
        //            inputLayer.Neurons[0].x = firstTrainingObject.RI;
        //            inputLayer.Neurons[1].x = firstTrainingObject.Na;
        //            inputLayer.Neurons[2].x = firstTrainingObject.Mg;
        //            inputLayer.Neurons[3].x = firstTrainingObject.Al;
        //            inputLayer.Neurons[4].x = firstTrainingObject.Si;
        //            inputLayer.Neurons[5].x = firstTrainingObject.K;
        //            inputLayer.Neurons[6].x = firstTrainingObject.Ca;
        //            inputLayer.Neurons[7].x = firstTrainingObject.Ba;
        //            inputLayer.Neurons[8].x = firstTrainingObject.Fe;
        //            for (int i = 1; i < _tempLayers.Count; i++) 
        //            {
        //                var currentLayer = _tempLayers[i];
        //                foreach (var neuron in currentLayer.Neurons)
        //                {
        //                    neuron.x = 0;
        //                    for (int j = 0; j < inputLayer.Neurons.Count; j++)
        //                    {
        //                        neuron.x += inputLayer.Neurons[j].x * neuron.weights[j];
        //                    }
        //                    neuron.x = Sigmoid(neuron.x);
        //                }
        //                inputLayer = currentLayer;
        //            }
        //        }
        //    }
        //}
        //private double Sigmoid(double x)
        //{
        //    return 1 / (1 + Math.Exp(-x));
        //}
    }
}
