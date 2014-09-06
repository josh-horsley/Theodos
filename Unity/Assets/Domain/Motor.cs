using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Motor : MonoBehaviour
{
    public float Power = 500f;
    public float Velocity = 500f;

    List<HingeJoint> rightWheels = new List<HingeJoint>();
    List<HingeJoint> leftWheels = new List<HingeJoint>();

    private bool forward;
    private bool backward;
    private bool right;
    private bool left;

    void Start()
    {
        var components = GetComponentsInChildren<HingeJoint>();

        foreach (var component in components)
        {
            var hinge = component;
            var motor = new JointMotor();
            motor.targetVelocity = Velocity;
            motor.force = 0f;
            hinge.motor = motor;

            if (component.transform.name.Contains("right"))
            {
                rightWheels.Add(hinge);
            }
            else
            {
                leftWheels.Add(hinge);
            }
        }
    }

	void Update ()
    {
        if (forward)
        {
            Forward();
        }
        else if (backward)
        {
            Backward();
        }
        else if (right)
        {
            Right();
        }
        else if (left)
        {
            Left();
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

    private void Forward()
    {
        Move(rightWheels, true);
        Move(leftWheels, true);
    }

    private void Backward()
    {
        Move(rightWheels, false);
        Move(leftWheels, false);
    }

    private void Right()
    {
        Move(rightWheels, false);
        Move(leftWheels, true);
    }

    private void Left()
    {
        Move(rightWheels, true);
        Move(leftWheels, false);
    }

    private void Move(List<HingeJoint> wheels, bool forward)
    {
        var velocity = Velocity * (forward ? 1 : -1);

        foreach (var wheel in wheels)
        {
            var motor = wheel.motor;
            motor.targetVelocity = velocity;
            motor.force = Power;
            wheel.motor = motor;
        }
    }

    private void Stop()
    {
        foreach (var wheel in rightWheels)
        {
            var motor = wheel.motor;
            motor.targetVelocity = 0f;
            motor.force = Power;
            wheel.motor = motor;
        }

        foreach (var wheel in leftWheels)
        {
            var motor = wheel.motor;
            motor.targetVelocity = 0f;
            motor.force = Power;
            wheel.motor = motor;
        }
    }
}
