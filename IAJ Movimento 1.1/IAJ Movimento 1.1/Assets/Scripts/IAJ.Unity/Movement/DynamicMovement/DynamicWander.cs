using Assets.Scripts.IAJ.Unity.Util;
using UnityEngine;
using System;

namespace Assets.Scripts.IAJ.Unity.Movement.DynamicMovement
{

    public class DynamicWander : DynamicSeek
    {
        public float WanderOffset { get; set; }
        public float WanderRadius { get; set; }
        public float WanderRate { get; set; }
        public float WanderOrientation { get; set; }
        public float WanderAngle { get; protected set; }

        public Vector3 CircleCenter { get; private set; }

        public GameObject DebugTarget { get; set; }

        public DynamicWander()
        {
            this.Target = new KinematicData();
            this.WanderAngle = 0;
            this.WanderRate = 0.1f;
            this.WanderRadius = 5.0f;
            this.WanderOrientation = this.Target.Orientation;
        }

        public override string Name
        {
            get { return "Wander"; }
        }
 

        public override MovementOutput GetMovement()
        {

            Debug.Log("EDER");

            WanderOrientation += RandomHelper.RandomBinomial() * WanderRate;

            Debug.Log(WanderOrientation);

            this.Target.Orientation = WanderOrientation + this.Character.Orientation;

            this.Target.Position = this.Character.Position + WanderOffset * this.Character.GetOrientationAsVector();

            this.Target.Position += WanderRadius * this.Target.GetOrientationAsVector();

            MovementOutput steering = base.GetMovement();

            //steering.linear = MaxAcceleration * this.Character.GetOrientationAsVector();

            if (this.DebugTarget != null)
            {
                this.DebugTarget.transform.position = this.Target.Position;
                
            }
            return steering;
        }
    }
}
