﻿using RAIN.Navigation.Graph;
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
            //AssetDatabase.LoadAssetAtPath("Assets/Textures/texture.jpg")
            //this.graph = (ClusterGraph) AssetDatabase.LoadAssetAtPath("Assets/Resources/ClusterGraph.asset", typeof(ClusterGraph));
            //Resources.Load("ClusterGraph");
            this.graph = Resources.Load<ClusterGraph>("ClusterGraph");
        }

        public float H(NavigationGraphNode node, NavigationGraphNode goalNode)
        {
            Cluster nodeCluster= graph.Quantize(node);
            Cluster nodeGoalCluster = graph.Quantize(goalNode);
            //Debug.Log("DISTS====" + nodeCluster.center + nodeGoalCluster.center);
            bool check = nodeCluster.center.Equals(nodeGoalCluster.center);
            if (check)
            {
                return (node.Position - goalNode.Position).magnitude;
            }
            else
            {
                List<Gateway> nodeClusterGateways = nodeCluster.gateways;
                List<Gateway> nodeGoalClusterGateways = nodeGoalCluster.gateways;

                float min = 10000f;
                Gateway gateMin = new Gateway();
                Gateway gate2Min = new Gateway();

                foreach (Gateway gate in nodeClusterGateways)
                {
                    foreach(Gateway gate2 in nodeGoalClusterGateways)
                    {
                        float distance = graph.gatewayDistanceTable[gate.id].entries[gate2.id].shortestDistance;
                        if (distance < min)
                        {
                            min = distance;
                            gateMin = gate;
                            gate2Min = gate2;
                        }
                    }
                }
                
                float hNode = (node.Position - gateMin.center).magnitude;
                float hGoal = (goalNode.Position - gate2Min.center).magnitude;

                return hNode + min + hGoal;


            }
        }
    }
}