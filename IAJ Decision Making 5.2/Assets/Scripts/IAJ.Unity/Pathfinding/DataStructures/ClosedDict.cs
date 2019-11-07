using System.Collections.Generic;
using System.Linq;
using RAIN.Navigation.Graph;


namespace Assets.Scripts.IAJ.Unity.Pathfinding.DataStructures
{
    //very simple (and unefficient) implementation of the open/closed sets
    public class ClosedDict : IClosedSet
    {
        private Dictionary<NavigationGraphNode, NodeRecord> Dict { get; set; }

        public ClosedDict()
        {
            this.Dict = new Dictionary<NavigationGraphNode, NodeRecord>();
        }

        public void Initialize()
        {
            this.Dict.Clear();
        }

        public int CountOpen()
        {
            return this.Dict.Count;
        }

        public void AddToClosed(NodeRecord nodeRecord)
        {
            this.Dict.Add(nodeRecord.node, nodeRecord);
        }

        public void RemoveFromClosed(NodeRecord nodeRecord)
        {
            this.Dict.Remove(nodeRecord.node);
        }

        public NodeRecord SearchInClosed(NodeRecord nodeRecord)
        {
            //here I cannot use the == comparer because the nodeRecord will likely be a different computational object
            //and therefore pointer comparison will not work, we need to use Equals
            //LINQ with a lambda expression
            if (Dict.ContainsKey(nodeRecord.node))
            {
                return this.Dict[nodeRecord.node];
            }
            else return null;
        }

        public Dictionary<NavigationGraphNode, NodeRecord> All()
        {
            return this.Dict;
        }
    }
}
