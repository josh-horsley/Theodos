using UnityEngine;
using System.Collections;

public class Tilt : MonoBehaviour
{
    private Vector3 tilt { get; set; }

    void FixedUpdate()
    {
        tilt = transform.rotation.eulerAngles;
    }

    public Vector3 GetTilt()
    {
        return tilt;
    }
}
