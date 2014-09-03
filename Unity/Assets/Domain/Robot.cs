using UnityEngine;
using System.Collections;

public class Robot : MonoBehaviour {

    private NodeClient _nodeClient;
    private Motor motor;
    private Sensor sensor;

	void Start ()
    {
        _nodeClient = GetComponent<NodeClient>();
        motor = GetComponentInChildren<Motor>();
        sensor = GetComponentInChildren<Sensor>();

        _nodeClient.client.On("GetDistance", (data) =>
        {
            _nodeClient.client.Emit("distance", sensor.GetDistance());
        });

        _nodeClient.client.On("motor", (data) =>
        {
            motor.Command(data.Json.args[0].ToString());
        });
	}

    void SendMessage(string message)
    {
        _nodeClient.Send(message);
    }
}
