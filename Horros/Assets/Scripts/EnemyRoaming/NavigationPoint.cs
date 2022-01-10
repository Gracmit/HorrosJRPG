using System;
using UnityEngine;

public class NavigationPoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            var roaming = other.GetComponent<EnemyRoaming>();
            roaming.NextDestination();
        }
    }
}
