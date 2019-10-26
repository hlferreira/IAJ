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
            LineSegmentPath segment = new LineSegmentPath(PathPositions[localPathIndex], PathPositions[localPathIndex + 1]);

            if (position.x > this.PathPositions[localPathIndex].x && position.z > this.PathPositions[localPathIndex].z && position.x < this.PathPositions[localPathIndex + 1].x && position.z < this.PathPositions[localPathIndex + 1].z)
            {
                return localPathIndex + segment.GetParam(position, previousParam);
            }
            else
            {
                LineSegmentPath segment2 = new LineSegmentPath(PathPositions[localPathIndex + 1], PathPositions[localPathIndex + 2]);
                return localPathIndex + 1 + segment2.GetParam(position, previousParam);
            }
            
        }

        public override Vector3 GetPosition(float param)
        {
            //TODO: implement latter
            int localPathIndex = (int)Math.Truncate(param);

            LineSegmentPath segment = new LineSegmentPath(PathPositions[localPathIndex], PathPositions[localPathIndex+1]);

            return segment.GetPosition(param);
        }

        public override bool PathEnd(float param)
        {
            //TODO: implement latter
            if ((Length - param) < 0.05)
                return true;
            else return false;
        }
    }
}