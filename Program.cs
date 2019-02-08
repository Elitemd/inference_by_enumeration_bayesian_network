using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InferenceByEnumerationASIA
{
    
    class Program
    {
        static void Main(string[] args)
        {

            ASIABayesianNetwork asia = new ASIABayesianNetwork();

            for (int i = 0; i < asia.Nodes.Length; ++i)
            {
                System.Console.WriteLine("Node " + asia.Nodes[i].Name);
                System.Console.WriteLine("True = " + Math.Round(asia.Nodes[i].currentValues[1]* 100, 2) + 
                                         "% | False = " + Math.Round(asia.Nodes[i].currentValues[0] * 100, 2) + "%");
                System.Console.WriteLine();
            }

        }
    }
    
}
