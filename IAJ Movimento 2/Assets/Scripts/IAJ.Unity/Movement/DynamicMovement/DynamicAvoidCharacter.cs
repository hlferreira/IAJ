using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.IAJ.Unity.Util;


namespace Assets.Scripts.IAJ.Unity.Movement.DynamicMovement
{
    public class DynamicAvoidCharacter : DynamicSeek
    {
        public KinematicData OtherCharacter { get; set; }

        public DynamicAvoidCharacter(KinematicData OtherCharacter) {
            this.OtherCharacter = OtherCharacter;
        }

        public override MovementOutput GetMovement()
        {
            float TimeToTarget = 5000;

            var relativePos = Target.Position - Character.Position;
            var relativeVel = Target.velocity - Target.velocity;
            var relativeSpeed = relativeVel.sqrMagnitude();
            var timetoCollision = (relativePos * relativeVel)/ (relativeSpeed* relativeSpeed)
            return base.GetMovement();
        }
    }
}