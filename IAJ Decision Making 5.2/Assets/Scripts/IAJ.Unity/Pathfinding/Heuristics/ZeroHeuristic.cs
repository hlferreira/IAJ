﻿using RAIN.Navigation.Graph;
using UnityEngine;

namespace Assets.Scripts.IAJ.Unity.Pathfinding.Heuristics
{
    public class ZeroHeuristic : IHeuristic
    {
        public float H(NavigationGraphNode node, NavigationGraphNode goalNode)
        {
            return 0;
        }
        public float H(Vector3 pos1, Vector3 pos2)
        {
            return 0;
        }
    }
}