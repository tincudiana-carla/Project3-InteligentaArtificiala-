namespace Project3_InteligentaArtificiala_.Models
{
    public class NeuralNetworkModel
    {
        public List<LayerModel> Layers { get; set; }
        public List<double> Errors { get; set; }
       public NeuralNetworkModel(List<int> numberOfNeuronsInLayers)
        {
            Layers = new List<LayerModel>();
            Errors = new List<double>();
            int numberOfConnections = 0;
            foreach (int numberOfNeurons in numberOfNeuronsInLayers)
            {
                Layers.Add(new LayerModel(numberOfNeurons, numberOfConnections));
                numberOfConnections = numberOfNeurons;
            }
        }
        public List<double> GetErrors()
        {
            return Errors;
        }
    }

}
