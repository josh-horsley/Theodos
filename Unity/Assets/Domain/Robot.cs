using UnityEngine;
using System.Collections;

public class Robot : MonoBehaviour {

    private NodeClient _nodeClient;
    private Motor motor;
    private Sensor sensor;
    private Tilt tilt;

	void Start ()
    {
        _nodeClient = GetComponent<NodeClient>();
        motor = GetComponentInChildren<Motor>();
        sensor = GetComponentInChildren<Sensor>();
        tilt = GetComponentInChildren<Tilt>();

        _nodeClient.client.On("GetDistance", (data) =>
        {
            _nodeClient.client.Emit("distance", sensor.GetDistance());
        });

        _nodeClient.client.On("GetTilt", (data) =>
        {
            var angle = tilt.GetTilt();
            _nodeClient.client.Emit("tilt", new { x = angle.x, y = angle.y, z = angle.z });
        });

        _nodeClient.client.On("motor", (data) =>
        {
            motor.Command(data.Json.args[0].ToString());
        });
	}
}
