﻿using UnityEngine;
using UnityEditor;
using Assets.Scripts.IAJ.Unity.Pathfinding.DataStructures;
using Assets.Scripts.IAJ.Unity.Pathfinding.DataStructures.HPStructures;
using Assets.Scripts.IAJ.Unity.Pathfinding.Path;
using Assets.Scripts.IAJ.Unity.Utils;
using RAIN.Navigation.NavMesh;
using System.Collections.Generic;
using RAIN.Navigation.Graph;
using Assets.Scripts.IAJ.Unity.Pathfinding;
using Assets.Scripts.IAJ.Unity.Pathfinding.Heuristics;
using System;

public class IAJMenuItems  {

    [MenuItem("IAJ/Create Cluster Graph")]
    private static void CreateClusterGraph()
    {
        Cluster cluster;
        Gateway gateway;

        //get cluster game objects
        var clusters = GameObject.FindGameObjectsWithTag("Cluster");
        //get gateway game objects
        var gateways = GameObject.FindGameObjectsWithTag("Gateway");
        //get the NavMeshGraph from the current scene
        NavMeshPathGraph navMesh = GameObject.Find("Navigation Mesh").GetComponent<NavMeshRig>().NavMesh.Graph;

        ClusterGraph clusterGraph = ScriptableObject.CreateInstance<ClusterGraph>();

        //create gateway instances for each gateway game object
        for (int i = 0; i < gateways.Length; i++)
        {
            var gatewayGO = gateways[i];
            gateway = ScriptableObject.CreateInstance<Gateway>();
            gateway.Initialize(i,gatewayGO);
            clusterGraph.gateways.Add(gateway);
            clusterGraph.gatewayDistanceTable = new GatewayDistanceTableRow[clusterGraph.gateways.Count];
        }

        //create cluster instances for each cluster game object and check for connections through gateways
        foreach (var clusterGO in clusters)
        {

            cluster = ScriptableObject.CreateInstance<Cluster>();
            cluster.Initialize(clusterGO);
            clusterGraph.clusters.Add(cluster);

            //determine intersection between cluster and gateways and add connections when they intersect
            foreach(var gate in clusterGraph.gateways)
            {
                if (MathHelper.BoundingBoxIntersection(cluster.min, cluster.max, gate.min, gate.max))
                {
                    cluster.gateways.Add(gate);
                    gate.clusters.Add(cluster);
                }
            }
        }

        // Second stage of the algorithm, calculation of the Gateway table

        GlobalPath solution = null;
        float cost;
        Gateway startGate;
        Gateway endGate;

         var pathfindingManager = new PathfindingManager();
        pathfindingManager.Initialize(navMesh, new NodeArrayAStarPathFinding(navMesh, new EuclideanHeuristic()));

        AStarPathfinding pathfinding = new NodeArrayAStarPathFinding(navMesh, new EuclideanHeuristic());

        //TODO implement the rest of the algorithm here, i.e. build the GatewayDistanceTable



        GatewayDistanceTableRow row = new GatewayDistanceTableRow();
        row.entries = new GatewayDistanceTableEntry[clusterGraph.gateways.Count];

        int j = 0;
        foreach (var gate in clusterGraph.gateways)
        {
            int i = 0;
            foreach (var gate2 in clusterGraph.gateways)
            {
                if(!gate.center.Equals(gate2.center))
                {
                    pathfinding.InitializePathfindingSearch(gate.center, gate2.center);
                    while (solution == null)
                    {
                        bool finished = pathfinding.Search(out solution, false);
                    }
                    cost = solution.Length;
                    solution = null;
                    //Debug.Log("======================================        " + gate.center);
                    //Debug.Log("\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\        " + gate2.center);


                    row.entries[i] = new GatewayDistanceTableEntry
                    {
                        startGatewayPosition = gate.center,
                        endGatewayPosition = gate2.center,
                        shortestDistance = cost
                    };
                    i++;
                }
                

            }
            clusterGraph.gatewayDistanceTable[j] = new GatewayDistanceTableRow();
            clusterGraph.gatewayDistanceTable[j] = row;
            j++;
        }

        Debug.Log("aaaaaaaaaaaaaaaaaaaa====== " + clusterGraph.gatewayDistanceTable[0].entries[0].startGatewayPosition);
        Debug.Log("bbbbbbbbbbbbbbbbbbbbbbbb====== " + clusterGraph.gatewayDistanceTable[1].entries[0].startGatewayPosition);

        //foreach (var a in clusterGraph.gatewayDistanceTable)
        //{
        //    foreach(var b in a.entries)
        //    {
        //        Debug.Log("shortestDistance =        " + b.shortestDistance);
        //        Debug.Log("startGatewayPosition =        " + b.startGatewayPosition);
        //        Debug.Log("endGatewayPosition =        " + b.endGatewayPosition);

        //    }
        //    Debug.Log("ACABOU ENTRYYYYYYYYYYYYYYYY\n\n");

        //}

        //create a new asset that will contain the ClusterGraph and save it to disk (DO NOT REMOVE THIS LINE)
        clusterGraph.SaveToAssetDatabase();
    }


    private static List<NavigationGraphNode> GetNodesHack(NavMeshPathGraph graph)
    {
        //this hack is needed because in order to implement NodeArrayA* you need to have full acess to all the nodes in the navigation graph in the beginning of the search
        //unfortunately in RAINNavigationGraph class the field which contains the full List of Nodes is private
        //I cannot change the field to public, however there is a trick in C#. If you know the name of the field, you can access it using reflection (even if it is private)
        //using reflection is not very efficient, but it is ok because this is only called once in the creation of the class
        //by the way, NavMeshPathGraph is a derived class from RAINNavigationGraph class and the _pathNodes field is defined in the base class,
        //that's why we're using the type of the base class in the reflection call
        return (List<NavigationGraphNode>)Assets.Scripts.IAJ.Unity.Utils.Reflection.GetInstanceField(typeof(RAINNavigationGraph), graph, "_pathNodes");
    }
}
