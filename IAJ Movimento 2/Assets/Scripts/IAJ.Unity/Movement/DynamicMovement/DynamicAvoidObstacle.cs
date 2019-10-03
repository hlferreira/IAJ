using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.IAJ.Unity.Util;

namespace Assets.Scripts.IAJ.Unity.Movement.DynamicMovement
{
    public class DynamicAvoidObstacle : DynamicSeek
    {
        public bool collisionDetector { get; set; }
        public bool collisionLeftDetector { get; set; }
        public bool collisionRightDetector { get; set; }
        public float avoidDistance { get; set; }
        public float lookAhead { get; set; }
        public GameObject obstacle { get; set; }

        public DynamicAvoidObstacle(GameObject gameObject) {
            this.obstacle = gameObject;
            this.Target = new KinematicData();
        }

        public override MovementOutput GetMovement()
        {
            Ray rayRightWhisker = new Ray(Character.Position, MathHelper.Rotate2D(Character.velocity.normalized, MathConstants.MATH_PI / 6));
            Ray rayLeftWhisker = new Ray(Character.Position, MathHelper.Rotate2D(Character.velocity.normalized, -(MathConstants.MATH_PI / 6)));
            Ray rayVector = new Ray(Character.Position, Character.velocity.normalized);

            Debug.DrawRay(Character.Position, Character.velocity.normalized);
            Debug.DrawRay(Character.Position, MathHelper.Rotate2D(Character.velocity.normalized, MathConstants.MATH_PI / 6));
            Debug.DrawRay(Character.Position, MathHelper.Rotate2D(Character.velocity.normalized, -(MathConstants.MATH_PI / 6)));

            Debug.Log(Character.velocity);
            RaycastHit hitInfo, LeftHitInfo,RightHitInfo;


            collisionDetector = obstacle.GetComponent<Collider>().Raycast(rayVector, out hitInfo, lookAhead);
            collisionLeftDetector = obstacle.GetComponent<Collider>().Raycast(rayLeftWhisker, out LeftHitInfo, lookAhead/2);
            collisionRightDetector = obstacle.GetComponent<Collider>().Raycast(rayRightWhisker, out RightHitInfo, lookAhead/2);

            if (!collisionDetector && !collisionLeftDetector && !collisionRightDetector)
            {
                return new MovementOutput();
            }

            if(collisionDetector)
                base.Target.Position = hitInfo.point + hitInfo.normal * avoidDistance;
            else if(collisionLeftDetector)
                base.Target.Position = LeftHitInfo.point + LeftHitInfo.normal * avoidDistance;
            else
                base.Target.Position = RightHitInfo.point + RightHitInfo.normal * avoidDistance;



            return base.GetMovement();

        }
    }
}
