using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Vehicle", menuName = "Vehicle")]
public class Vechicle : ScriptableObject
{
    public float vehicleSpeed;
    public float vehicleBreak;
    public float vehicleMaxSpeed;
}
