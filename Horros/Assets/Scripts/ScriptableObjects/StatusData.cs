using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StatusData", menuName = "Status/Data")]
public class StatusData : ScriptableObject
{
    public PartyMember member;
    public GameObject gameObject;
    public float[] position = new float[3];
}
