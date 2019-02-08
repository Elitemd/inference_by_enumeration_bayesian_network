using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InferenceByEnumerationASIA
{
    class InferenceByEnumaration
    {

        public Node[] Nodes;
        private int domain = 2; // number of distinct values that a node can have
                                // in our case there are only 2 - True and False

        public int[] network_evidente;

        public InferenceByEnumaration (Node[] n, int[] e)
        {
            Nodes = n;
            network_evidente = e;
        }

        // normalization function that makes the sum of Q equal to 1
        public double[] Normalization(double[] Q)
        {
            double sum = Q.Sum();
            Q[0] /= sum;
            Q[1] /= sum;
            return Q;
        }

        // enumerationAsk function that gets the node id and returns its distribution
        public double[] EnumerationAsk(int NodeId)
        {
            double[] Q = new double[2];
            // Bayes network (ASIA in our case) nodes sorted in topological order
            var vars = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7 };

            // initialEvidence vector (-1 = no evidence, 0 = true, 1 = false)
            // initialEvidence[0] = -1 => no evidence for node 0
            int[] initialEvidence = (int[])network_evidente.Clone();

            // if the truth value of NodeId is set in initialEvidence then we return it immediately
            if (initialEvidence[NodeId] != -1)
            {
                Q[initialEvidence[NodeId]] = 1;
                return Normalization(Q);
            }

            // iteration through possible values of possible values from NodeId domain
            for (int i = 0; i < domain; ++i)
            {
                // here we suppose that the truth value of NodeId is equal to a certain value from domain
                initialEvidence[NodeId] = i;
                Q[i] = EumerateAll(vars, initialEvidence);
            }

            // we need the sum of Q (distribution over domain) to be equal to 1
            return Normalization(Q);
        }

        // recursive function that iterates through all nodes from real_vars list
        // with respect to evidences from real_evidence list 
        public double EumerateAll(List<int> real_vars, int[] real_evidence)
        {
            // a copy for real_vars and real_evidence to solve isolation issues
            List<int> vars = new List<int>(real_vars);
            int[] evidence = (int[])real_evidence.Clone();

            // if there are no variables left in real_vars we return 1
            // returning from recursion
            if (vars.Count == 0)
                return 1.0;

            // extracting the first variable from real_vars for next processing
            int Y = vars[0];
            vars.RemoveAt(0);

            // if there is some evidence for the extracted variable
            if (evidence[Y] != -1)
            {
                // default evidences for parents are unknown => -1
                int j = 0, e1 = -1, e2 = -1;

                // iterating through parents of Y
                foreach (int parent in Nodes[Y].Parents)
                {
                    if (evidence[parent] != -1)
                    {
                        if (j == 0)
                        {
                            e1 = evidence[parent];
                            j = 1;
                        }
                        else
                        {
                            e2 = evidence[parent];
                        }
                    }
                }
                // returning the probability that node Y has value equal to evidence of Y
                // with respect to known evidences of his parents which were processed before
                // multiplied by EnumareteAll with remained variables and the same evidence
                return Nodes[Y].GetTruthTableProbability(evidence[Y], e1, e2) * EumerateAll(vars, evidence);
            }
            else
            {
                // if we don't have any evidence for Y
                // we are going to suppose that possible values from domain
                // are true one by one

                double sum = 0.0;

                // we need a copy of the current evidence to work with new_evidence
                // that is not the real evidence we have from the start
                int[] new_evidence = (int[])evidence.Clone();

                // iterating through possible values of domain and supposing that each value is true
                for (int i = 0; i < domain; ++i)
                {
                    new_evidence[Y] = i;
                    int j = 0, e1 = -1, e2 = -1;

                    foreach (int parent in Nodes[Y].Parents)
                    {
                        if (evidence[parent] != -1)
                        {
                            if (j == 0)
                            {
                                e1 = evidence[parent];
                                j = 1;
                            }
                            else
                            {
                                e2 = evidence[parent];
                            }
                        }
                    }

                    // returning the sum of products of the probability that Y equals to each value from domain
                    // with respect to evidences assigned to Y parents
                    // multiplied by EumerateAll of remained vars and the new evidence
                    sum += Nodes[Y].GetTruthTableProbability(i, e1, e2) * EumerateAll(vars, new_evidence);
                }

                return sum;
            }


        }

    }
}
