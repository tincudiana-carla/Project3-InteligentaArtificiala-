using Project3_InteligentaArtificiala_.Models;
using System.Collections.Generic;

public class LayerModel
{
    public List<NeuronModel> Neurons { get; set; } 

    public LayerModel(int numberOfNeurons, int numberOfConnections)
    {
        Neurons = new List<NeuronModel>();
        for (int i = 0; i < numberOfNeurons; i++)
        {
            Neurons.Add(new NeuronModel(numberOfConnections));
        }
    }
}
