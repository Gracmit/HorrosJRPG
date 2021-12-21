using System;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private List<Item> _offers;

    public event Action<List<Item>> ShopOpened;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ShopOpened?.Invoke(_offers);
        }
    }
}