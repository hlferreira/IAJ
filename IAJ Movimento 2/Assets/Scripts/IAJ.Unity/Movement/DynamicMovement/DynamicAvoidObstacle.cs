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

            Debug.DrawRay(Character.Position, Character.velocity.normalized * lookAhead);
            Debug.DrawRay(Character.Position, MathHelper.Rotate2D(Character.velocity.normalized, MathConstants.MATH_PI / 6) * lookAhead);
            Debug.DrawRay(Character.Position, MathHelper.Rotate2D(Character.velocity.normalized, -(MathConstants.MATH_PI / 6)) * lookAhead);

            RaycastHit hitInfo, LeftHitInfo,RightHitInfo;


            collisionDetector = obstacle.GetComponent<Collider>().Raycast(rayVector, out hitInfo, lookAhead);
            collisionLeftDetector = obstacle.GetComponent<Collider>().Raycast(rayLeftWhisker, out LeftHitInfo, lookAhead);
            collisionRightDetector = obstacle.GetComponent<Collider>().Raycast(rayRightWhisker, out RightHitInfo, lookAhead);

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

//Priority 91, 16 - 10 cones
//Blended 20, 12 (+ red) - 10 cones
//RVO 8, 5 - 10 cones

//Priority 731 - 50 cones (15 sec)