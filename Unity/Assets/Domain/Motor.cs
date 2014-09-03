using UnityEngine;
using System.Collections;
using System;

public class Motor : MonoBehaviour
{
    public float Power = 500f;
    public float Velocity = 500f;

    private HingeJoint rightFront;
    private HingeJoint rightBack;
    private HingeJoint leftFront;
    private HingeJoint leftBack;
    private HingeJoint[] wheels;

    JointMotor mRightFront;
    JointMotor mRightBack;
    JointMotor mLeftFront;
    JointMotor mLeftBack;

    private bool forward;
    private bool backward;
    private bool right;
    private bool left;

    void Start()
    {
        wheels = GetComponentsInChildren<HingeJoint>();

        foreach (var wheel in wheels)
        {
            if (wheel.transform.name.Equals("rightFront"))
            {
                rightFront = wheel;
                mRightFront = new JointMotor();
                mRightFront.targetVelocity = Velocity;
                mRightFront.force = 0f;
                rightFront.motor = mRightFront;
            }
            else if (wheel.transform.name.Equals("rightBack"))
            {
                rightBack = wheel;
                mRightBack = new JointMotor();
                mRightBack.targetVelocity = Velocity;
                mRightBack.force = 0f;
                rightBack.motor = mRightBack;
            }
            else if (wheel.transform.name.Equals("leftFront"))
            {
                leftFront = wheel;
                mLeftFront = new JointMotor();
                mLeftFront.targetVelocity = Velocity;
                mLeftFront.force = 0f;
                leftFront.motor = mLeftFront;
            }
            else if (wheel.transform.name.Equals("leftBack"))
            {
                leftBack = wheel;
                mLeftBack = new JointMotor();
                mLeftBack.targetVelocity = Velocity;
                mLeftBack.force = 0f;
                leftBack.motor = mLeftBack;
            }
        }
    }

	void Update ()
    {
        if (forward)
        {
            RightForward();
            LeftForward();
        }
        else if (backward)
        {
            RightBackward();
            LeftBackward();
        }
        else if (right)
        {
            RightBackward();
            LeftForward();
        }
        else if (left)
        {
            RightForward();
            LeftBackward();
        }
        else
        {
            Stop();
        }
    }

    public void Command(string command)
    {
        if (command.Equals("forward"))
        {
            forward = true;
        }
        else if (command.Equals("backward"))
        {
            backward = true;
        }
        else if (command.Equals("right"))
        {
            right = true;
        }
        else if (command.Equals("left"))
        {
            left = true;
        }
        else if (command.Equals("stop"))
        {
            forward = false;
            backward = false;
            right = false;
            left = false;
        }
    }

    private void RightForward()
    {
        mRightFront.force = Power;
        rightFront.motor = mRightFront;

        mRightBack.force = Power;
        rightBack.motor = mRightBack;
    }

    private void RightBackward()
    {
        mRightFront.targetVelocity = -500f;
        mRightFront.force = Power;
        rightFront.motor = mRightFront;

        mRightBack.targetVelocity = -500f;
        mRightBack.force = Power;
        rightBack.motor = mRightBack;
    }

    private void LeftForward()
    {
        mLeftFront.force = Power;
        leftFront.motor = mLeftFront;

        mLeftBack.force = Power;
        leftBack.motor = mLeftBack;
    }

    private void LeftBackward()
    {
        mLeftFront.targetVelocity = -500f;
        mLeftFront.force = Power;
        leftFront.motor = mLeftFront;

        mLeftBack.targetVelocity = -500f;
        mLeftBack.force = Power;
        leftBack.motor = mLeftBack;
    }

    private void Stop()
    {
        mRightFront.targetVelocity = Velocity;
        mRightFront.force = 0f;
        rightFront.motor = mRightFront;

        mRightBack.targetVelocity = Velocity;
        mRightBack.force = 0f;
        rightBack.motor = mRightBack;

        mLeftFront.targetVelocity = Velocity;
        mLeftFront.force = 0f;
        leftFront.motor = mLeftFront;

        mLeftBack.targetVelocity = Velocity;
        mLeftBack.force = 0f;
        leftBack.motor = mLeftBack;
    }
}
