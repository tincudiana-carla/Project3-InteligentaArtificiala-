using System;
using System.Collections.Generic;

namespace Project3_InteligentaArtificiala_.Models
{
    public class NeuralNetworkErrorManager
    {
        private List<double> _errorsPerPhase;

        public NeuralNetworkErrorManager()
        {
            _errorsPerPhase = new List<double>();
        }

        public void AddErrorPerPhase(double error)
        {
            _errorsPerPhase.Add(error);
        }

        public void DisplayErrors()
        {
            Console.WriteLine("Errors per Phase:");
            foreach (var error in _errorsPerPhase)
            {
                Console.WriteLine(error);
            }
        }

        public List<double> GetErrorsPerPhase()
        {
            return _errorsPerPhase;
        }
    }
}
