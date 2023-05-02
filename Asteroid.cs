using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private float _rotate = 15.5f;
    [SerializeField]
    private GameObject _explosion;

    private SpawnManager _spawnManager;

    // Start is called before the first frame update
    void Start()
    {
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();

        if (_spawnManager == null)
        {
            Debug.LogError("Spawn Manager is null");
        }

    }

    // Update is called once per frame
    void Update()
    {
        AsteroidRotate();
    }

    void AsteroidRotate()
    {
        transform.Rotate(Vector3.forward * _rotate * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {
            //Vector3 explosion = new Vector3(this.gameObject, transform.position, Quaternion.identity);
            GameObject cloneExplosion = Instantiate(_explosion, transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            Destroy(cloneExplosion, 2.8f);
            _spawnManager.StartSpawn();
            Destroy(this.gameObject, 0.2f);
        }
    }
}
