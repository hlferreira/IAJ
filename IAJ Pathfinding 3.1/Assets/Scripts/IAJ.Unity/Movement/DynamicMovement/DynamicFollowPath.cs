using Assets.Scripts.IAJ.Unity.Pathfinding.Path;
using UnityEngine;

namespace Assets.Scripts.IAJ.Unity.Movement.DynamicMovement
{
    public class DynamicFollowPath : DynamicSeek
    {
        public GlobalPath path { get; set; }
        public float pathOffset { get; set; }
        public float currentParam { get; set; }

        public override string Name
        {
            get { return "FollowPath"; }
        }

        public DynamicFollowPath()
        {
            this.path = new GlobalPath();
            this.pathOffset = 0.2f;
            this.currentParam = 0;
            //Debug.Log(base.Character.Position);

            this.Output = new MovementOutput();

        }

        public void setPath(GlobalPath globalPath)
        {
            this.path = globalPath;
            base.Character.Position = globalPath.PathPositions[0];
        }

        public override MovementOutput GetMovement()
        {
            this.currentParam = path.GetParam(base.Character.Position, currentParam);
            float targetParam = currentParam + pathOffset;
            Debug.Log("targetParam =  " + targetParam);
            base.Target.Position = path.GetPosition(targetParam);
            Debug.Log("TARGET" + this.Target.Position);
            Debug.Log("CHARACTER" + this.Character.Position);


            return base.GetMovement();
        }
    }
}
