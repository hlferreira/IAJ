using Assets.Scripts.IAJ.Unity.Pathfinding.Path;
using UnityEngine;

namespace Assets.Scripts.IAJ.Unity.Movement.DynamicMovement
{
    public class DynamicFollowPath : DynamicSeek
    {
        public GlobalPath path { get; set; }
        public float pathOffset { get; set; }
        public float currentParam { get; set; }
        public bool finished { get; set; }

        public override string Name
        {
            get { return "FollowPath"; }
        }

        public DynamicFollowPath()
        {
            this.path = new GlobalPath();
            this.pathOffset = 0.2f;
            this.currentParam = 0;
            this.finished = false;
            //Debug.Log(base.Character.Position);

            this.Output = new MovementOutput();

        }

        public void SetPath(GlobalPath globalPath)
        {
            this.path = globalPath;
            base.Character.Position = globalPath.PathPositions[0];
            base.Target.Position = globalPath.PathPositions[0] + globalPath.GetPosition(pathOffset);
        }

        public override MovementOutput GetMovement()
        {
            if (this.finished == false)
            {
                this.currentParam = path.GetParam(base.Character.Position, currentParam);
           //     Debug.Log("OIOIOIOI 2");
                if (path.PathEnd(this.currentParam))
                {
                   // Debug.Log("FINISHED");
                    this.finished = true;
                    Character.velocity = Vector3.zero;
                    return new MovementOutput();
                }
                float targetParam = currentParam + pathOffset;
                base.Target.Position = path.GetPosition(targetParam);

                return base.GetMovement();

            }
            else return new MovementOutput();
        }
    }
}
