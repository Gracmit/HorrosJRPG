using UnityEngine;

[CreateAssetMenu(menuName = "CauldronData", fileName = "CauldronData", order = 0)]
public class CauldronData : ScriptableObject
{
   public float BloodAmount;
   public bool FirstAnimation = true;
   public bool Done;
}