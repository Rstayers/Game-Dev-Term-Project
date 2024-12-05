using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyDrop : MonoBehaviour
{
    [SerializeField] private int amount;
    [SerializeField] private float attractionRadius = 5f; // Radius within which the currency is attracted
    [SerializeField] private float attractionSpeed = 5f; // Speed at which the currency moves toward the player
    private GameManager gameManager;
    private Transform player;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        player = GameObject.FindGameObjectWithTag("Player").transform; // Find the player
    }

    private void Update()
    {
        if (player == null) return;

        // Check if the player is within the attraction radius
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer <= attractionRadius)
        {
            // Move the currency toward the player
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * attractionSpeed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player") return;

        // Add currency to the player's total and destroy the object
        gameManager.UpdateCurrency(amount);
        Destroy(gameObject);
    }
}
