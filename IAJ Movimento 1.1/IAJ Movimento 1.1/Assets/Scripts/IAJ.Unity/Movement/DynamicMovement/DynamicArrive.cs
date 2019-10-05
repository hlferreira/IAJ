using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.IAJ.Unity.Movement.DynamicMovement
{
    public class DynamicArrive : DynamicMovement
    {

        public float TargetRadius { get; set; }
        public float SlowRadius { get; set; }
        public float TimeToTarget { get; set; }
        public float MaxSpeed { get; set; }
        public override string Name
        {
            get { return "Arrive"; }
        }

        public DynamicArrive()
        {
            this.TimeToTarget = 0.1f;
            this.SlowRadius = 10;
            this.TargetRadius = 5;
            this.Output = new MovementOutput();
        }

        public override MovementOutput GetMovement()
        {

            Vector3 direction = this.Target.Position - this.Character.Position;

            float distance = direction.sqrMagnitude;


            //find the desired speed
            if (distance < this.TargetRadius * this.TargetRadius)
                return null;

            float TargetSpeed;

            if (distance > this.SlowRadius * this.SlowRadius)
                TargetSpeed = this.MaxSpeed;

            else
                TargetSpeed = this.MaxSpeed * distance / (this.SlowRadius * this.SlowRadius);


            Vector3 TargetVelocity = direction;
            TargetVelocity.Normalize();
            TargetVelocity *= TargetSpeed;

            this.Output.linear = (TargetVelocity - this.Character.velocity) / this.TimeToTarget;

            if (this.Output.linear.sqrMagnitude > this.MaxAcceleration * this.MaxAcceleration)
            {
                this.Output.linear.Normalize();
                this.Output.linear *= this.MaxAcceleration;
            }

            this.Output.angular = 0;
            return this.Output;
        }
    }
}