using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyContainer;
    //[SerializeField]
    //private GameObject _powerUpContainer;
    [SerializeField]
    private GameObject[] powerups;
    [SerializeField]
    private GameObject _SpeedPowerUpPrefab;
    private bool _stopSpawn = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void StartSpawn()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerUpRoutine());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(2.0f);
        while (_stopSpawn == false)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(-9.4f, 9.4f), 5.9f, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, spawnPosition, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(5.0f);
            Debug.Log("Spawn new");
        }
    }

    IEnumerator SpawnPowerUpRoutine()
    {
        yield return new WaitForSeconds(2.0f);
        while (_stopSpawn == false)
        {
            Vector3 spawnPUposition = new Vector3(Random.Range(-9.4f, 9.4f), 5.9f, 0);
            int randomPowerUp = Random.Range(0, 3);
            Instantiate(powerups[randomPowerUp], spawnPUposition, Quaternion.identity);
            yield return new WaitForSeconds(Mathf.Lerp(3.0f, 7.0f,Random.value));
        }
    }

    public void StopSpawn()
    {
        _stopSpawn = true;
    }

}
