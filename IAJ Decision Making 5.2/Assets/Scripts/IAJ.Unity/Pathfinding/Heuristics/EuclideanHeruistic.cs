using RAIN.Navigation.Graph;
using UnityEngine;

namespace Assets.Scripts.IAJ.Unity.Pathfinding.Heuristics
{
    public class EuclideanHeuristic : IHeuristic
    {
        public float H(NavigationGraphNode node, NavigationGraphNode goalNode)
        {
            return (node.Position - goalNode.Position).magnitude;
        }

        public float H(Vector3 pos1, Vector3 pos2)
        {
            return (pos1 - pos2).magnitude;
        }
    }
}
