using UnityEngine;
using System.Collections;

public class RearWheelDrive : MonoBehaviour {

    public ControlManager controlManager;

	public WheelCollider[] wheels;
    public Transform[] Tires;

    public int bhp;
    public float torque;
    public int brakeTorque;
    public float maxAngle = 30;
	public float maxTorque = 300;

    public float SteerAngle;

    public float[] gearRatio;
    public float currentSpeed;
    public int currentGear;
    public float engineRPM;
    public float gearUpRPM;
    public float gearDownRPM;

    public int maxSpeed;
    public int maxRevSpeed;
    private GameObject COM;


    // here we find all the WheelColliders down in the hierarchy
    public void Start()
	{
        COM = GameObject.Find("Col");
        GetComponent<Rigidbody>().centerOfMass = new Vector3(COM.transform.localPosition.x * transform.localScale.x, COM.transform.localPosition.y * transform.localScale.y, COM.transform.localPosition.z * transform.localScale.z);
        controlManager = GameObject.FindObjectOfType<ControlManager>();
    }

    public void Update()
	{
        Accelerate();
        Stear();
        AutoGear();

        currentSpeed = GetComponent<Rigidbody>().velocity.magnitude * 3.6f;
        engineRPM = Mathf.Round((wheels[3].rpm * gearRatio[currentGear]));
        torque = bhp * gearRatio[currentGear];

        if (controlManager.breakHold > 0)
        {
            HandBrakes();
        }

        if (Input.GetKey(KeyCode.R))
        {
            transform.position.Set(transform.position.x, transform.position.y + 5f, transform.position.z);
            transform.rotation.Set(0, 0, 0, 0);
        }
    }

    void Stear()
    {
        if (currentSpeed < 100)
        {
            SteerAngle = 13 - (currentSpeed / 10);
        }else
        {
            SteerAngle = 2;
        }
        wheels[0].steerAngle = SteerAngle * controlManager.turnHold;
        wheels[1].steerAngle = SteerAngle * controlManager.turnHold;

        if (controlManager.turnHold != 0)
        {
            maxSpeed = 15;
        }
        else
        {
            maxSpeed = 20;
        }
    }

    void Accelerate()
    {
        if (currentSpeed < maxSpeed && currentSpeed > maxRevSpeed && engineRPM <= gearUpRPM)
        {
            wheels[3].motorTorque = torque * 5;
            wheels[2].motorTorque = torque * 5;
            wheels[3].brakeTorque = 0;
            wheels[2].brakeTorque = 0;
        }
        else
        {
            wheels[3].motorTorque = 0;
            wheels[2].motorTorque = 0;
            wheels[3].brakeTorque = brakeTorque;
            wheels[2].brakeTorque = brakeTorque;
        }

        if (engineRPM > 0 && controlManager.turnHold < 0 && engineRPM <= gearUpRPM)
        {
            wheels[0].brakeTorque = brakeTorque;
            wheels[1].brakeTorque = brakeTorque;
        }
        else
        {
            wheels[0].brakeTorque = 0;
            wheels[1].brakeTorque = 0;
        }
    }
    void AutoGear()
    {
        int AppropriateGear = currentGear;

        if (engineRPM >= gearUpRPM)
        {
            for (var i = 0; i < gearRatio.Length; i++)
            {
                if (wheels[3].rpm * gearRatio[i] < gearUpRPM)
                {
                    AppropriateGear = i;
                    break;
                }
            }
            currentGear = AppropriateGear;
        }
        if (engineRPM <= gearDownRPM)
        {
            AppropriateGear = currentGear;
            for (var i = gearRatio.Length -1; i >=0; i--)
            {
                if (wheels[3].rpm * gearRatio[i] > gearDownRPM)
                {
                    AppropriateGear = i;
                    break;
                }
            }
            currentGear = AppropriateGear;
        }
    }

    void HandBrakes()
    {
        wheels[3].brakeTorque = brakeTorque * controlManager.breakHold;
        wheels[2].brakeTorque = brakeTorque * controlManager.breakHold;
        wheels[0].brakeTorque = brakeTorque * controlManager.breakHold;
        wheels[1].brakeTorque = brakeTorque * controlManager.breakHold;
    }
}
