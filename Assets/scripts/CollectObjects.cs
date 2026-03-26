using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectObjects : MonoBehaviour
{
    [SerializeField] private int itemsCollected = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Collectible"))
        {
            itemsCollected++;
            Debug.Log("Items collected: " + itemsCollected);
            Destroy(other.gameObject);
        }
    }
}
