using UnityEngine;
using System.Collections;

public class BotSpawner : MonoBehaviour
{
    public GameObject botPrefab; 
    public Transform spawnPoint; 
    public float respawnTime = 5f; 

    private void Start()
    {
        StartCoroutine(SpawnBots());
    }

    private IEnumerator SpawnBots()
    {
        while (true)
        {
            SpawnBot();
            yield return new WaitForSeconds(respawnTime);
        }
    }

    private void SpawnBot()
    {
        if (spawnPoint != null)
        {
            GameObject bot = Instantiate(botPrefab, spawnPoint.position, spawnPoint.rotation);
            BotController botController = bot.GetComponent<BotController>();
            if (botController != null)
            {
                botController.OnBotDestroyedEvent += HandleBotDestroyed; // Подписываемся на событие
            }
        }
        else
        {
            Debug.LogWarning("Spawn point is not assigned!");
        }
    }

    private void HandleBotDestroyed()
    {
        Debug.Log("Bot destroyed. Handle any logic here.");
    }
}
