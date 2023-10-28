using Project3_InteligentaArtificiala_.Models;

namespace Project3_InteligentaArtificiala_.Helper
{
    public class ModelHelper
    {
        public List<NormalizedDataGlassModel> NormalizedGlassList { get; set; }
        public List<TestingGlassModel> TestingData { get; set; }
        public NeuralNetworkModel NeuralNetwork { get; set; }

        public ModelHelper()
        {
            NormalizedGlassList = new List<NormalizedDataGlassModel>();
            TestingData = new List<TestingGlassModel>();
            NeuralNetwork = new NeuralNetworkModel(new List<int>());
        }
    }
}
