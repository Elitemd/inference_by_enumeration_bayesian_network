using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InferenceByEnumerationASIA
{
    class Node
    {
        public List<int> Parents;
        public string Name { get; set; }
        public double[,,] truthTable;
        public double[] currentValues { get; set; }

        // suppose we want to get the probability of E given B and D
        // then P(E | B, D) = x, x is the probability we were searching for
        // truthTable is a 3D matrix having the following template: 
        // index 1 - truth value of E [0 = False, 1 = True]
        // index 2 - truth value of B [0 = False, 1 = True]
        // index 3 - truth value of D [0 = False, 1 = True]
        // the value of this tuple is the probability (x)

        public Node(string name)
        {
            this.Name = name;
            this.Parents = new List<int>();
            this.currentValues = new double[2];
            truthTable = new double[10, 10, 10];
        }

        public void SetTruthTableProbabilities(double p, int p1, int p2 = 0, int p3 = 0)
        {
            truthTable[p1, p2, p3] = p;
        }

        public double GetTruthTableProbability(int p1, int p2 = 0, int p3 = 0)
        {
            // if there are no evidences we set indexes to zero
            if (p2 < 0) p2 = 0;
            if (p3 < 0) p3 = 0;
            return truthTable[p1, p2, p3];
        }
    }
}
