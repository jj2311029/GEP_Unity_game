using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpwanController : MonoBehaviour
{
    public int initialEnemiesPerWave = 10;
    public int currentEnemiesPerWave;

    public float spawnDelay = 1f;

    public int currentWave = 0;
    public float waveCooldown = 20.0f;

    public bool inCooldown;
    public float cooldownCounter = 0;

    public List<Gang> currentEnemiesAlive;

    public GameObject enemyPrefab;

    private void Start()
    {
        currentEnemiesPerWave = initialEnemiesPerWave;

        StartNextWave();
    }


    private void StartNextWave()
    {
        currentEnemiesAlive.Clear();

        currentWave++;
        StartCoroutine(SpawnWave());
    }

    private IEnumerator SpawnWave()
    {
        for(int i=0;i<currentEnemiesPerWave; i++) 
        {
            Vector3 spawnOffset = new Vector3(Random.Range(-2f, 2f), 0f, Random.Range(-2f, 2f));
            Vector3 spawnPosition=transform.position+spawnOffset;

            var zombie = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

            Gang EnemyScript = zombie.GetComponent<Gang>();

            currentEnemiesAlive.Add(EnemyScript);

            yield return new WaitForSeconds(spawnDelay);
        }
    }
    private void Update()
    {
        List<Gang> enemiesToRemove = new List<Gang>();
        foreach (Gang enemy in currentEnemiesAlive)
        {
            if (enemy.isDead)
            {
                enemiesToRemove.Add(enemy);
            }
        }

        foreach (Gang enemy in enemiesToRemove)
        {
            currentEnemiesAlive.Remove(enemy);
        }

        enemiesToRemove.Clear();

        if (currentEnemiesAlive.Count == 0 && inCooldown == false)
        {
            StartCoroutine(WaveCooldown());
        }

        if (inCooldown)
        {
            cooldownCounter -= Time.deltaTime;
        }
        else
        {
            cooldownCounter = waveCooldown;
        }

        
    }
    private IEnumerator WaveCooldown()
    {
        inCooldown = true;
        yield return new WaitForSeconds(waveCooldown);

        inCooldown = false;
        currentEnemiesPerWave += 3;
        StartNextWave();
    }
}
