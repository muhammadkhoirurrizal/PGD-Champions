using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Suspension : MonoBehaviour
{

    public Transform wheelMesh;
    public WheelCollider wheelCollider;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(wheelCollider.transform.position, -wheelCollider.transform.up, out hit, wheelCollider.suspensionDistance + wheelCollider.radius))
        {
            wheelMesh.position = hit.point + wheelCollider.transform.up * wheelCollider.radius;
        }
        else
        {
            wheelMesh.position = wheelCollider.transform.position - (wheelCollider.transform.up * wheelCollider.suspensionDistance);
        }
        if (wheelCollider.rpm >= 1  || wheelCollider.rpm <= -1)
        {
            wheelMesh.transform.Rotate(wheelCollider.rpm / 60 * 360 * Time.deltaTime - 1, 0, 0);
            wheelMesh.localEulerAngles = new Vector3(wheelMesh.transform.localEulerAngles.x, wheelCollider.steerAngle - wheelMesh.localEulerAngles.z, wheelMesh.transform.localEulerAngles.z);
        }
    }
}
