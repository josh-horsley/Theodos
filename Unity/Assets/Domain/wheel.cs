using UnityEngine;
using System.Collections;

public class wheel : MonoBehaviour
{
    HingeJoint joint;
    JointMotor motor;

	void Start ()
    {
        joint = GetComponent<HingeJoint>();
        motor = new JointMotor();
        motor.force = 600;
        motor.targetVelocity = 500;
        joint.motor = motor;
	}
	
	void FixedUpdate ()
    {
        motor.force += 1;
        joint.motor = motor;
	}
}
