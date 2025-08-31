using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject coinPrefab;    
    public float spawnRangeX = 9f;  // define how far coins can appear along X  axis.
    public float spawnRangeZ = 9f;   // define how far coins can appear along  Z axis.
    public float yPos = 0.7f;  // Y position to keep coins above ground

    private GameObject currentCoin; // kepp track of the current coin

    void Start()
    {
        // spawns the first coin when the game starts using custom method SpawnCoin();
        SpawnCoin();
    }

    public void SpawnCoin()
    {
        // if a coin already exists, do nothing
        if (currentCoin != null) return;

        // it choose random position in range
        float randomX = Random.Range(-spawnRangeX, spawnRangeX);
        float randomZ = Random.Range(-spawnRangeZ, spawnRangeZ);
        Vector3 spawnPos = new Vector3(randomX, yPos, randomZ);

        // spawn a new coin
        currentCoin = Instantiate(coinPrefab, spawnPos, coinPrefab.transform.rotation);
    }

    // calls SpawnCoin() when a coin is collected and create a new coin
    public void CoinCollected()
    {
        currentCoin = null;
        SpawnCoin();
    }
}
