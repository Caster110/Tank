using System;
using UnityEngine;
using UnityEngine.AI;

namespace DefaultNamespace
{
    public class NavMeshVehicleMovement : MonoBehaviour
    {
        private NavMeshAgent navMeshAgent;

        public bool overrideMovementCtrl;
        public bool moveByAnim;

        
        public bool isTurnRound;
        private bool hasSetTurnRoundDes = false;

        public float turnRadius = 4;
        public int turnDegreeStepValue = 120;
        public int turnAngleThreshold = 90;// turn when angle>this variable

        public void Start()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
        }
        
        
        private Vector3 realDest;//the point you clicked
        private Vector3 d1;//the temporary turn point
       

        public void Update()
        {

            if (overrideMovementCtrl == false || navMeshAgent.enabled == false || HasArrived() ||
                navMeshAgent.isStopped)
                return;


            Vector3 targetDir = navMeshAgent.destination - transform.position;
            if (Vector3.Angle(targetDir, transform.forward) > turnAngleThreshold && isTurnRound == false)//if destination is in the back and not in TurnRound Status
            {
                realDest = navMeshAgent.destination;
                isTurnRound = true;//execute this segment  only once; 
                navMeshAgent.autoBraking = false;//just try to set this to true and you will know why
            }


            if (isTurnRound)
            {
                if (hasSetTurnRoundDes == false) //set the first temporary turn point to enable the loop
                {
                    d1 = FindTurnPoint(realDest);
                    hasSetTurnRoundDes = true;
                    navMeshAgent.SetDestination(d1);
                }

                if (Vector3.Distance(transform.position, d1) <= navMeshAgent.stoppingDistance*1.2f)//set next turn point when arrived turn point
                {
                    if (Vector3.Angle(realDest - transform.position, transform.forward) > turnAngleThreshold)// if angle > threshold,find next turn point
                    {
                        d1 = FindTurnPoint(realDest);
                        navMeshAgent.SetDestination(d1);
                    }
                }

                if (Vector3.Angle(realDest - transform.position, transform.forward) < turnAngleThreshold)//End the turn when the Angle is less than 90 degrees
                {
                    navMeshAgent.SetDestination(realDest);
                    hasSetTurnRoundDes = false;
                    isTurnRound = false;
                    navMeshAgent.autoBraking = true;
                }
            }

            Vector3 FindTurnPoint(Vector3 realDest)//Find temporary turn point;
            {
                Vector3 direction = realDest - transform.position;
                var cross = Vector3.Cross(transform.forward, direction);
                //The cross product determines whether the target location is to the left or to the right and determines which way to rotate
                Vector3 targetPos;
                NavMeshHit navMeshHit;
                if (cross.y < 0) //left side
                {
                    targetPos = transform.position - transform.right * turnRadius -
                                transform.right * (turnRadius * Mathf.Cos((180 - turnDegreeStepValue) * Mathf.Deg2Rad)) +
                                transform.forward * (turnRadius * Mathf.Sin((180 - turnDegreeStepValue) * Mathf.Deg2Rad));
                }
                else //right side
                {
                    targetPos = transform.position + transform.right * turnRadius +
                                transform.right * (turnRadius * Mathf.Cos(turnDegreeStepValue * Mathf.Deg2Rad)) +
                                transform.forward * (turnRadius * Mathf.Sin(turnDegreeStepValue * Mathf.Deg2Rad));
                }

                //SamplePosition
                //The radius should not be too large
                if (NavMesh.SamplePosition(targetPos, out navMeshHit, 2f, -1))
                {
                    targetPos = navMeshHit.position; 
                }
                else // cant find valid pos on NavMesh,return realDest
                {
                    targetPos = this.realDest;
                }

                return targetPos;
            }
        }

        public void SetRealDest(Vector3 pos)//the real destination is the position you clicked;
        {
            realDest = pos;
        }

       

        private bool HasArrived()
        {
            float remainingDistance;
            if (navMeshAgent.pathPending)
            {
                remainingDistance = float.PositiveInfinity;
            }
            else
            {
                remainingDistance = navMeshAgent.remainingDistance;
            }

            return remainingDistance <= navMeshAgent.stoppingDistance;
        }
    }
}