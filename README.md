<h2>Inference by enumeration for a bayesian network (ASIA)</h2>

The program is made for ASIA network but can be easily adapted for another bayesian network.

Nodes, network topology and probability tables for each node can be set in <i>ASIABayesianNetwork.cs</i> using functions from Node class

You can find the main algorithm functions in <i>InferenceByEnumaration.cs</i>

Almost every line from the code is commented. If you find any issues fell free to share what you have found.

An example of execution without any evidences in network
<img src=https://i.imgur.com/PdxJSFa.png>

You can set your own evidences in file <i>ASIABayesianNetwork.cs</i>, just change evidence array (-1 = no evidence, 1 = true, 0 = false)

You can find the pseudocode used to implement this program here
http://courses.csail.mit.edu/6.034s/handouts/spring12/bayesnets-pseudocode.pdf

The probability distribution table for ASIA network:
<img src=https://i.stack.imgur.com/IaI0b.png>

