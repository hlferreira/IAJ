using Assets.Scripts.IAJ.Unity.Utils;
using UnityEngine;
using System;

namespace Assets.Scripts.IAJ.Unity.Pathfinding.Path
{
    public class LineSegmentPath : LocalPath
    {
        protected Vector3 LineVector;
        public LineSegmentPath(Vector3 start, Vector3 end)
        {
            this.StartPosition = start;
            this.EndPosition = end;
            this.LineVector = end - start;
        }

        public override Vector3 GetPosition(float param)
        {
            //TODO: implement latter
            float decimalParam = param - (float)Math.Truncate(param);
            return this.StartPosition + decimalParam * this.LineVector;
        }

        public override bool PathEnd(float param)
        {
            //TODO: implement latter
            
            if (1 - param < 0.05)
            {
                return true;
            }
            else return false;
        }

        public override float GetParam(Vector3 position, float lastParam)
        {
            //TODO: implement latter
            float param = MathHelper.closestParamInLineSegmentToPoint(this.StartPosition, this.EndPosition, position);
            
            return param;
        }
    }
}