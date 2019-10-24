using System;
using System.Collections.Generic;
using RAIN.Navigation.Graph;

namespace Assets.Scripts.IAJ.Unity.Pathfinding.DataStructures
{
    public class NodeRecordArray : IOpenSet, IClosedSet
    {
        private NodeRecord[] NodeRecords { get; set; }
        private List<NodeRecord> SpecialCaseNodes { get; set; }
        private NodePriorityHeap Open { get; set; }

        public NodeRecordArray(List<NavigationGraphNode> nodes)
        {
            //this method creates and initializes the NodeRecordArray for all nodes in the Navigation Graph
            this.NodeRecords = new NodeRecord[nodes.Count];

            for (int i = 0; i < nodes.Count; i++)
            {
                var node = nodes[i];
                node.NodeIndex = i; //we're setting the node Index because RAIN does not do this automatically
                this.NodeRecords[i] = new NodeRecord { node = node, status = NodeStatus.Unvisited };
            }

            this.SpecialCaseNodes = new List<NodeRecord>();

            this.Open = new NodePriorityHeap();
        }

        public NodeRecord GetNodeRecord(NavigationGraphNode node)
        {
            //do not change this method
            //here we have the "special case" node handling
            if (node.NodeIndex == -1)
            {
                for (int i = 0; i < this.SpecialCaseNodes.Count; i++)
                {
                    if (node == this.SpecialCaseNodes[i].node)
                    {
                        return this.SpecialCaseNodes[i];
                    }
                }
                return null;
            }
            else
            {
                return this.NodeRecords[node.NodeIndex];
            }
        }

        public void AddSpecialCaseNode(NodeRecord node)
        {
            this.SpecialCaseNodes.Add(node);
        }

        void IOpenSet.Initialize()
        {
            this.Open.Initialize();
            //we want this to be very efficient (that's why we use for)
            for (int i = 0; i < this.NodeRecords.Length; i++)
            {
                this.NodeRecords[i].status = NodeStatus.Unvisited;
            }

            this.SpecialCaseNodes.Clear();
        }

        void IClosedSet.Initialize()
        {
           //TODO implement
           
        }

        public void AddToOpen(NodeRecord nodeRecord)
        {
            //TODO implement
            NodeRecords[nodeRecord.node.NodeIndex].status = NodeStatus.Open;
            Open.AddToOpen(nodeRecord);
            //throw new NotImplementedException();
        }

        public void AddToClosed(NodeRecord nodeRecord)
        {
            //TODO implement
            NodeRecords[nodeRecord.node.NodeIndex].status = NodeStatus.Closed;
            //throw new NotImplementedException();
        }

        public NodeRecord SearchInOpen(NodeRecord nodeRecord)
        {
            //TODO implement
            return Open.SearchInOpen(nodeRecord);
            //throw new NotImplementedException();
        }

        public NodeRecord SearchInClosed(NodeRecord nodeRecord)
        {
            //TODO implement
            if (NodeRecords[nodeRecord.node.NodeIndex].status == NodeStatus.Closed)
            {
                return NodeRecords[nodeRecord.node.NodeIndex];
            }
            else return null;
            //throw new NotImplementedException();
        }

        public NodeRecord GetBestAndRemove()
        {
            //TODO implement
            return Open.GetBestAndRemove();
            //throw new NotImplementedException();
        }

        public NodeRecord PeekBest()
        {
            //TODO implement
            return Open.PeekBest();
            //throw new NotImplementedException();
        }

        public void Replace(NodeRecord nodeToBeReplaced, NodeRecord nodeToReplace)
        {
            //TODO implement
            Open.Replace(nodeToBeReplaced, nodeToReplace);
            NodeRecords[nodeToReplace.node.NodeIndex].fValue = nodeToBeReplaced.fValue;
            NodeRecords[nodeToReplace.node.NodeIndex].gValue = nodeToBeReplaced.gValue;
            NodeRecords[nodeToReplace.node.NodeIndex].hValue = nodeToBeReplaced.hValue;
            NodeRecords[nodeToReplace.node.NodeIndex].parent = nodeToBeReplaced.parent;
            //throw new NotImplementedException();
        }

        public void RemoveFromOpen(NodeRecord nodeRecord)
        {
            //TODO implement
            Open.RemoveFromOpen(nodeRecord);
            //throw new NotImplementedException();
        }

        public void RemoveFromClosed(NodeRecord nodeRecord)
        {
            //TODO implement
            NodeRecords[nodeRecord.node.NodeIndex].status = NodeStatus.Open;
            //throw new NotImplementedException();
        }

        ICollection<NodeRecord> IOpenSet.All()
        {
            return Open.All();
            //TODO implement
            //throw new NotImplementedException();
        }

        Dictionary<NavigationGraphNode, NodeRecord> IClosedSet.All()
        {
            //TODO implement
            Dictionary<NavigationGraphNode, NodeRecord> ClosedDic = new Dictionary<NavigationGraphNode, NodeRecord>();
            for (int i = 0; i < this.NodeRecords.Length; i++)
            {
                if(NodeRecords[i].status == NodeStatus.Closed)
                {
                    ClosedDic.Add(NodeRecords[i].node, NodeRecords[i]);
                }
            }
 
            return ClosedDic;
            //throw new NotImplementedException();
        }

        public int CountOpen()
        {
            //TODO implement
            return Open.CountOpen();
            //throw new NotImplementedException();
        }
    }
}
