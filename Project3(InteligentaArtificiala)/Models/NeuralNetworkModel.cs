namespace Project3_InteligentaArtificiala_.Models
{
    public class NeuralNetworkModel
    {
        public List<LayerModel> Layers { get; set; }

        public NeuralNetworkModel(List<int> numberOfNeuronsInLayers)
        {
            Layers = new List<LayerModel>();
            int numberOfConnections = 0;
            foreach (int numberOfNeurons in numberOfNeuronsInLayers)
            {
                Layers.Add(new LayerModel(numberOfNeurons, numberOfConnections));
                numberOfConnections = numberOfNeurons;
            }
        }
    }

}
