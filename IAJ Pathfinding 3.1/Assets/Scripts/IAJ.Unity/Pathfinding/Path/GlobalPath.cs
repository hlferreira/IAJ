using System.Collections.Generic;
using Assets.Scripts.IAJ.Unity.Utils;
using RAIN.Navigation.Graph;
using UnityEngine;
using System;

namespace Assets.Scripts.IAJ.Unity.Pathfinding.Path
{
    public class GlobalPath : Path
    {
        public List<NavigationGraphNode> PathNodes { get; protected set; }
        public List<Vector3> PathPositions { get; protected set; } 
        public bool IsPartial { get; set; }
        public float Length { get; set; }
        public List<LocalPath> LocalPaths { get; protected set; } 


        public GlobalPath()
        {
            this.PathNodes = new List<NavigationGraphNode>();
            this.PathPositions = new List<Vector3>();
            this.LocalPaths = new List<LocalPath>();
        }


        public override float GetParam(Vector3 position, float previousParam)
        {
            //TODO: implement latter
            int localPathIndex = (int)Math.Truncate(previousParam);
            if (localPathIndex + 2 < this.PathPositions.Count)
            {
                Debug.Log("TRYYYYYYYYYYYYYY");
                LineSegmentPath segment = new LineSegmentPath(PathPositions[localPathIndex], PathPositions[localPathIndex + 1]);

                LineSegmentPath segment2 = new LineSegmentPath(PathPositions[localPathIndex + 1], PathPositions[localPathIndex + 2]);

                float param = localPathIndex + segment.GetParam(position, previousParam);

                float param2 = localPathIndex + 1 + segment2.GetParam(position, previousParam);

                if (Math.Abs(param - previousParam) < Math.Abs(param2 - previousParam))
                {
                    return param;
                }
                else return param2;
            }
            else if (localPathIndex == this.PathPositions.Count - 2)
            {
                Debug.Log("catchchchchch");
                LineSegmentPath segment = new LineSegmentPath(PathPositions[localPathIndex], PathPositions[localPathIndex + 1]);

                return localPathIndex + segment.GetParam(position, previousParam);
            }
            else return previousParam;
        }

        public override Vector3 GetPosition(float param)
        {
            //TODO: implement latter
            int localPathIndex = (int)Math.Truncate(param);
            if (localPathIndex + 1 < this.PathPositions.Count)
            {
                LineSegmentPath segment = new LineSegmentPath(PathPositions[localPathIndex], PathPositions[localPathIndex + 1]);
                return segment.GetPosition(param);
            }
            else return this.PathPositions[this.PathPositions.Count - 1];

            
        }

        public override bool PathEnd(float param)
        {
            //TODO: implement latter
            if ((Length - param) < 0.4)
                return true;
            else return false;
        }
    }
}