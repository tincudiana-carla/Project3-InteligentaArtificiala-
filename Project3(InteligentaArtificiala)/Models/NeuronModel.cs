namespace Project3_InteligentaArtificiala_.Models
{
    public class NeuronModel
    {
        public double x { get; set; } 
        public List<double> weights { get; set; }
        public string jsonWeights { get; set; }
        public NeuronModel(int numberOfConnections)
        {
            weights = new List<double>(numberOfConnections);
            Random rand = new Random();

            for (int i = 0; i < numberOfConnections; i++)
            {
                double randomWeight = rand.NextDouble() * 2 - 1;
                weights.Add(randomWeight);
            }
        }
    }

}
