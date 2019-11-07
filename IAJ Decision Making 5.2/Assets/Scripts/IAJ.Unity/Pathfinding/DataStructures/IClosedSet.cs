using System.Collections.Generic;
using RAIN.Navigation.Graph;

namespace Assets.Scripts.IAJ.Unity.Pathfinding.DataStructures
{
    public interface IClosedSet
    {
        void Initialize();
        void AddToClosed(NodeRecord nodeRecord);
        void RemoveFromClosed(NodeRecord nodeRecord);
        //should return null if the node is not found
        NodeRecord SearchInClosed(NodeRecord nodeRecord);
        Dictionary<NavigationGraphNode, NodeRecord> All();
    }
}
