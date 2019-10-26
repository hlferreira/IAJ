using RAIN.Navigation.Graph;
using Assets.Scripts.IAJ.Unity.Pathfinding.DataStructures.HPStructures;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Assets.Scripts.IAJ.Unity.Pathfinding.Heuristics
{
    public class GatewayHeuristic : IHeuristic
    {
        private ClusterGraph graph;
        public GatewayHeuristic()
        {
            this.graph = (ClusterGraph) Resources.Load("ClusterGraph");

            //Debug.Log("gsadgadsg  " + this.graph.gatewayDistanceTable[0].entries[0].shortestDistance);
        }

        public float H(NavigationGraphNode node, NavigationGraphNode goalNode)
        {
            Cluster nodeCluster= graph.Quantize(node);
            Cluster nodeGoalCluster = graph.Quantize(node);
            if(nodeCluster.center.Equals(nodeGoalCluster.center))
            {
                return (node.Position - goalNode.Position).magnitude;
            }
            else
            {
                List<Gateway> nodeClusterGateways = nodeCluster.gateways;
                List<Gateway> nodeGoalClusterGateways = nodeGoalCluster.gateways;

                float min = 10000f;

                foreach(Gateway gate in nodeClusterGateways)
                {
                    foreach(Gateway gate2 in nodeGoalClusterGateways)
                    {
                        float distance = graph.gatewayDistanceTable[gate.id].entries[gate2.id].shortestDistance;
                        if (distance < min)
                        {
                            min = distance;
                        }
                    }
                }
                return min;


            }
        }
    }
}
