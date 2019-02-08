using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InferenceByEnumerationASIA
{
    class ASIABayesianNetwork
    {
        public Node[] Nodes { get; }
        public ASIABayesianNetwork()
        {

            // creating the topology of ASIA Bayesian network
            // and setting truth table probabilities values for each node

            Nodes = new Node[8];

            Nodes[0] = new Node("Visit to asia");
            Nodes[1] = new Node("Smoker");
            Nodes[2] = new Node("Has tuberculosis");
            Nodes[3] = new Node("Has lung cancer");
            Nodes[4] = new Node("Has bronchitis");
            Nodes[5] = new Node("Tuberculosis or Cancer");
            Nodes[6] = new Node("X-Ray result");
            Nodes[7] = new Node("Dyspnea");

            Nodes[0].SetTruthTableProbabilities(0.01, 1);
            Nodes[0].SetTruthTableProbabilities(0.99, 0);

            Nodes[1].SetTruthTableProbabilities(0.5, 1);
            Nodes[1].SetTruthTableProbabilities(0.5, 0);

            Nodes[2].Parents.Add(0);
            Nodes[2].SetTruthTableProbabilities(0.05, 1, 1);
            Nodes[2].SetTruthTableProbabilities(0.95, 0, 1);
            Nodes[2].SetTruthTableProbabilities(0.01, 1, 0);
            Nodes[2].SetTruthTableProbabilities(0.99, 0, 0);


            Nodes[3].Parents.Add(1);
            Nodes[3].SetTruthTableProbabilities(0.1, 1, 1);
            Nodes[3].SetTruthTableProbabilities(0.9, 0, 1);
            Nodes[3].SetTruthTableProbabilities(0.01, 1, 0);
            Nodes[3].SetTruthTableProbabilities(0.99, 0, 0);

            Nodes[4].Parents.Add(1);
            Nodes[4].SetTruthTableProbabilities(0.6, 1, 1);
            Nodes[4].SetTruthTableProbabilities(0.4, 0, 1);
            Nodes[4].SetTruthTableProbabilities(0.3, 1, 0);
            Nodes[4].SetTruthTableProbabilities(0.7, 0, 0);

            Nodes[5].Parents.Add(2);
            Nodes[5].Parents.Add(3);
            Nodes[5].SetTruthTableProbabilities(1, 1, 1, 1);
            Nodes[5].SetTruthTableProbabilities(0, 0, 1, 1);
            Nodes[5].SetTruthTableProbabilities(1, 1, 1, 0);
            Nodes[5].SetTruthTableProbabilities(0, 0, 1, 0);
            Nodes[5].SetTruthTableProbabilities(1, 1, 0, 1);
            Nodes[5].SetTruthTableProbabilities(0, 0, 0, 1);
            Nodes[5].SetTruthTableProbabilities(0, 1, 0, 0);
            Nodes[5].SetTruthTableProbabilities(1, 0, 0, 0);

            Nodes[6].Parents.Add(5);
            Nodes[6].SetTruthTableProbabilities(0.98, 1, 1);
            Nodes[6].SetTruthTableProbabilities(0.02, 0, 1);
            Nodes[6].SetTruthTableProbabilities(0.05, 1, 0);
            Nodes[6].SetTruthTableProbabilities(0.95, 0, 0);

            Nodes[7].Parents.Add(5);
            Nodes[7].Parents.Add(4);
            Nodes[7].SetTruthTableProbabilities(0.9, 1, 1, 1);
            Nodes[7].SetTruthTableProbabilities(0.1, 0, 1, 1);
            Nodes[7].SetTruthTableProbabilities(0.7, 1, 1, 0);
            Nodes[7].SetTruthTableProbabilities(0.3, 0, 1, 0);
            Nodes[7].SetTruthTableProbabilities(0.8, 1, 0, 1);
            Nodes[7].SetTruthTableProbabilities(0.2, 0, 0, 1);
            Nodes[7].SetTruthTableProbabilities(0.1, 1, 0, 0);
            Nodes[7].SetTruthTableProbabilities(0.9, 0, 0, 0);

            int[] evidence = { -1, -1, -1, -1, -1, -1, -1, -1 };

            InferenceByEnumaration inference = new InferenceByEnumaration(Nodes, evidence);
            
            for (int i = 0; i < Nodes.Length; ++i)
            {
                double[] result = inference.EnumerationAsk(i);
                Nodes[i].currentValues[0] = result[0]; 
                Nodes[i].currentValues[1] = result[1]; 
            }
        }
    }

}
