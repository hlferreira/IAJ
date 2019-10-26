using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.IAJ.Unity.Utils;


namespace Assets.Scripts.IAJ.Unity.Movement.DynamicMovement
{
    public class DynamicAvoidCharacter : DynamicSeek
    {
        public KinematicData OtherCharacter { get; set; }

        public float avoidDistance { get; set; }

        public float MaxTimeLookAhead { get; set; }

        public DynamicAvoidCharacter(KinematicData OtherCharacter) {
            this.OtherCharacter = OtherCharacter;
            this.Target = OtherCharacter;
            this.Output = new MovementOutput();
        }

        public override MovementOutput GetMovement()
        {

            var deltaPos = this.OtherCharacter.Position - Character.Position;
            var deltaVel = this.OtherCharacter.velocity - Character.velocity;
            var deltaSqrSpeed = deltaVel.sqrMagnitude;

            if (deltaSqrSpeed == 0) 
                return new MovementOutput();

            var timeToClosest = -Vector3.Dot(deltaPos, deltaVel) / deltaSqrSpeed;

            if (timeToClosest > MaxTimeLookAhead)
                return new MovementOutput();

            var futureDeltaPos = deltaPos + deltaVel * timeToClosest;
            var futureDistance = futureDeltaPos.magnitude;

            if (futureDistance > 2 * avoidDistance)
                return new MovementOutput();

            if (futureDistance <= 0 || deltaPos.magnitude < 2 * avoidDistance)
            {
                // deals with exact or immediate collisions
                this.Output.linear = Character.Position - this.OtherCharacter.Position;
                Debug.Log("Collision");
            }
                
            else
                this.Output.linear = futureDeltaPos * -1;

            this.Output.linear = Output.linear.normalized * MaxAcceleration;
            this.Output.angular = 0;

            return this.Output;

        }
    }
}