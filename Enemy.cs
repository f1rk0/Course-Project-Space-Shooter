using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _enemyspeed = 4f;
    [SerializeField]
    private GameObject _enemyPrefab;

    Animator _animation;

    private Player _player;


    private AudioSource _explosionSound;

    [SerializeField]
    private GameObject _explosionPrefab;

    //Start is called before the first frame update
    void Start()
    {
        _explosionSound = GetComponent<AudioSource>();

        if (_explosionSound == null)
        {
            Debug.LogError("Enemy Audio is null");
        }

        _player = GameObject.Find("Player").GetComponent<Player>();

        if (_player == null)
        {
            Debug.LogError("Player missing");
        }

        _animation = GetComponent<Animator>();

        if (_animation == null)
        {
            Debug.LogError("Animation missing");
        }

    }

    // Update is called once per frame
    void Update()
    {
        EnemyMovement();
        EnemyRespawn();
    }

    void EnemyMovement()
    {
        transform.Translate(Vector3.down * _enemyspeed * Time.deltaTime);
        //_enemyPrefab.transform.Translate(Vector3.down * _enemyspeed * Time.deltaTime);
    }

    void EnemyRespawn()
    {
        if (transform.position.y <= -4.9f)
        {
            float randomX = Random.Range(-9.4f, 9.4f);
            transform.position = new Vector3(randomX, 5.9f, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Instantiate(_enemyPrefab, transform.position = new Vector3(Random.Range(-9.4f, 9.4f), 5.9f, 0), Quaternion.identity);
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();

            if (player != null)
            {
                player.Damage();
            }
            _enemyspeed = 0;
            _animation.SetTrigger("OnEnemyDeath");
            _explosionSound.Play();
            Destroy(this.gameObject, 2.8f);
            

        }
        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);
            _enemyspeed = 0;
            _animation.SetTrigger("OnEnemyDeath");
            _explosionSound.Play();
            Destroy(this.gameObject, 2.8f);
            if (_player != null)
            {
                _player.AddScore(10);
            }

        }
        
    }
}
