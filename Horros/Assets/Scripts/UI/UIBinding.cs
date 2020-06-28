using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBinding : MonoBehaviour
{
    IEnumerator Start()
    {
        var player = FindObjectOfType<PlayerMovementController>();
        while (player == null)
        {
            yield return null;
            player = FindObjectOfType<PlayerMovementController>();
        }

        GetComponent<ItemsPanel>().BindInventory(player.GetComponent<Inventory>());
    }
}
