using UnityEngine;
using System.Collections;
using System;

public class Sensor : MonoBehaviour
{
    private float _distance;

	void FixedUpdate ()
    {
        RaycastHit hit;
        Vector3 fwd = transform.TransformDirection(Vector3.right);
        Ray forwardRay = new Ray(transform.position, fwd);

        if (Physics.Raycast(forwardRay, out hit))
        {
            _distance = hit.distance;
        }
        else
        {
            _distance = -1;
        }
	}

    public float GetDistance()
    {
        return _distance;
    }
}
