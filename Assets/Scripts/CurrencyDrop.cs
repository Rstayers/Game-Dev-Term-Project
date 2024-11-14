using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyDrop : MonoBehaviour
{
    [SerializeField] private int amount;
    private GameManager gameManager;
    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player") return;
        gameManager.UpdateCurrency(amount);
        Destroy(gameObject);
    }
}
