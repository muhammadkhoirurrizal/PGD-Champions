using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VechicleMovement : MonoBehaviour
{
    public Vechicle vechicle;
    private Rigidbody rb;

    public WheelCollider FR_L_Wheel, FR_R_Wheel, RE_L_Wheel, RE_R_Wheel;

    private float forwardSpeed=0;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        float h = Input.GetAxis("Horizontal") * 40;
        if (Input.GetAxis("Horizontal") != 0)
        {
            FR_R_Wheel.steerAngle = h;
            FR_L_Wheel.steerAngle = h;
        }
        else
        {
            RE_L_Wheel.brakeTorque = 0;
            RE_R_Wheel.brakeTorque = 0;
        }

        if (forwardSpeed <= 500)
        {
            forwardSpeed += 50;
        }
        Debug.Log(RE_L_Wheel.brakeTorque);

        RE_R_Wheel.motorTorque = forwardSpeed;
        RE_L_Wheel.motorTorque = forwardSpeed;
    }
}
