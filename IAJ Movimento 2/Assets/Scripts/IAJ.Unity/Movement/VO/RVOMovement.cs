﻿//class adapted from the HRVO library http://gamma.cs.unc.edu/HRVO/
//adapted to IAJ classes by João Dias

using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.IAJ.Unity.Movement.DynamicMovement;
using Assets.Scripts.IAJ.Unity.Util;
using UnityEngine;

namespace Assets.Scripts.IAJ.Unity.Movement.VO
{
    public class RVOMovement : DynamicMovement.DynamicVelocityMatch
    {
        public override string Name
        {
            get { return "RVO"; }
        }

        protected List<KinematicData> Characters { get; set; }
        protected List<StaticData> Obstacles { get; set; }
        public float CharacterSize { get; set; }
        public float IgnoreDistance { get; set; }
        public float MaxSpeed { get; set; }
        public int NumSamples { get; set; }

        protected DynamicMovement.DynamicMovement DesiredMovement { get; set; }

        public RVOMovement(DynamicMovement.DynamicMovement goalMovement, List<KinematicData> movingCharacters, List<StaticData> obstacles)
        {
            this.DesiredMovement = goalMovement;
            this.Characters = movingCharacters;
            this.Obstacles = obstacles;
            base.Target = new KinematicData();
            this.NumSamples = 100;
            this.CharacterSize = 1.0f;
            this.IgnoreDistance = 4.0f;
        }

        public Vector3 GetBestSample(Vector3 DesiredVelocity, List<Vector3> samples) {
            
           
            Vector3 bestSample = Vector3.zero;
            float minimumPenalty = Mathf.Infinity;


            foreach (var sample in samples)
            {
                float distancePenalty = (DesiredVelocity - sample).magnitude;
                float maximumTimePenalty = 0;
                UnityEngine.Profiling.Profiler.BeginSample("CharacterLoop");
                //Debug.Log("This code is being profiled");
                foreach (var c in Characters)
                {
                    Vector3 deltaP = c.Position - Character.Position;

                    if (deltaP.magnitude > IgnoreDistance)
                        continue;

                    UnityEngine.Profiling.Profiler.BeginSample("Time to collision");
                    Vector3 rayVector = 2 * sample - Character.velocity - c.velocity;
                    float time = MathHelper.TimeToCollisionBetweenRayAndCircle(Character.Position, rayVector, c.Position, 2 * CharacterSize);
                    UnityEngine.Profiling.Profiler.EndSample();


                    UnityEngine.Profiling.Profiler.BeginSample("first");
                    float timePenalty;
                    if (time > 0)
                    {
                        timePenalty = 20 / time;
                        if (timePenalty > maximumTimePenalty)
                        {
                            maximumTimePenalty = timePenalty;
                        }
                    }
                    else if (time.Equals(0.0f))
                    {
                        timePenalty = Mathf.Infinity;
                        maximumTimePenalty = timePenalty;
                        break;
                    }
                    else
                    {
                        timePenalty = 0;
                    }
                    UnityEngine.Profiling.Profiler.EndSample();

                    /*UnityEngine.Profiling.Profiler.BeginSample("last");
                    if (timePenalty > maximumTimePenalty)
                    {
                        maximumTimePenalty = timePenalty;
                    }
                    UnityEngine.Profiling.Profiler.EndSample();*/
                }
                UnityEngine.Profiling.Profiler.EndSample();

                UnityEngine.Profiling.Profiler.BeginSample("ObstacleLoop");
                //Debug.Log("This code is being profiled");
                foreach (var o in Obstacles)
                {
                    Vector3 deltaP = o.Position - Character.Position;

                    if (deltaP.magnitude > 2*IgnoreDistance)
                        continue;

                    Vector3 rayVector = 2 * sample - Character.velocity;
                    float time = MathHelper.TimeToCollisionBetweenRayAndCircle(Character.Position, rayVector, o.Position, 2 * CharacterSize);

                    float timePenalty;
                    if (time > 0)
                    {
                        timePenalty = 30 / time;
                        if (timePenalty > maximumTimePenalty)
                        {
                            maximumTimePenalty = timePenalty;
                        }
                    }
                    else if (time.Equals(0.0f))
                    {
                        timePenalty = Mathf.Infinity;
                        maximumTimePenalty = timePenalty;
                        break;
                    }
                    else
                    {
                        timePenalty = 0;
                    }

                    /*if (timePenalty > maximumTimePenalty)
                    {
                        maximumTimePenalty = timePenalty;
                    }*/
                }
                UnityEngine.Profiling.Profiler.EndSample();

                float penalty = distancePenalty + maximumTimePenalty;


                if (penalty < minimumPenalty)
                {
                    minimumPenalty = penalty;
                    bestSample = sample;
                }
            }

            Debug.DrawLine(Character.Position, Character.Position + bestSample, Color.magenta);
            
            return bestSample;
         
        }

        public override MovementOutput GetMovement()
        {
            var desiredOutput = this.DesiredMovement.GetMovement();
            //verificar
            Vector3 desiredVelocity = Character.velocity + desiredOutput.linear*Time.deltaTime;

            if (desiredVelocity.magnitude > MaxSpeed)
            {
                desiredVelocity.Normalize();
                desiredVelocity *= MaxSpeed;

            }

            List<Vector3> samples = new List<Vector3>();

            samples.Add(desiredVelocity);
            for(int i = 0; i< NumSamples; i++)
            {
                float angle = RandomHelper.RandomBinomial(MathConstants.MATH_PI)+ MathConstants.MATH_PI;
             //   Debug.Log("ANGLE");
               // Debug.Log(angle);
                float magnitude = RandomHelper.RandomBinomial(MaxSpeed / 2) + MaxSpeed/2;
                Vector3 velocitySample = MathHelper.ConvertOrientationToVector(angle) * magnitude;
                samples.Add(velocitySample);
            }

            base.Target.velocity = this.GetBestSample(desiredVelocity,samples);

            return base.GetMovement();
        }
    }
}
