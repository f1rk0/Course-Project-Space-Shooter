using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private float _speed = 5.5f;
    [SerializeField]
    private float _speedboost = 9.5f;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private float _fireRate = 0.2f;
    private float _canFire = -1f;
    [SerializeField]
    private int _lives = 3;
    private SpawnManager _spawnManager;
    [SerializeField]
    private GameObject _TripleShotPrefab;
    private bool _isTheTripleActive = false;
    [SerializeField]
    private GameObject _SpeedBoostPrefab;
    private bool _isTheSpeedActive = false;
    [SerializeField]
    private GameObject _ShieldBoostPrefab;
    private bool _isTheShieldActive = false;
    [SerializeField]
    private GameObject _Shield;
    [SerializeField]
    private GameObject _Fire_Right;
    [SerializeField]
    private GameObject _Fire_Left;

    [SerializeField]
    private int _score;

    private UIManager _uiManager;

    [SerializeField]
    private AudioSource _laserSound;
    [SerializeField]
    private AudioSource _powerUpSound;

    void Start()
    {
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        if (_spawnManager == null)
        {
            Debug.LogError("The Spawn Manager is NULL");
        }
        //StartCoroutine(TSPU_Active_Timer());

        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        if (_uiManager == null)
        {
            Debug.LogError("The UI Manager is NULL");
        }

    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();
        }
    }

    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);

        if (_isTheSpeedActive == false)
        {
            transform.Translate(direction * _speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(direction * _speedboost * Time.deltaTime);
        }

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.9f, 5.9f), 0);

        if (transform.position.x >= 11.4f)
        {
            transform.position = new Vector3(-11.4f, transform.position.y, 0);
        }
        else if (transform.position.x <= -11.4f)
        {
            transform.position = new Vector3(11.4f, transform.position.y, 0);
        }
    }

    void FireLaser()
    {
        _canFire = Time.time + _fireRate;
        // Instantiate(_laserPrefab, _laserPrefab.transform.position = new Vector3(transform.position.x, transform.position.y + 0.8f, transform.position.z), Quaternion.identity);
        // Instantiate(_laserPrefab, .transform.position + new Vector3(0, 0.8f, 0), Quaternion.identity); - Udemy code

        if (_isTheTripleActive == true){
            Instantiate(_TripleShotPrefab, transform.position, Quaternion.identity);
        } 
        else { 
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.8f, 0), Quaternion.identity);
        }

        _laserSound.Play();

    }

    public void Damage()
    {
        if (_isTheShieldActive == true)
        {
            _isTheShieldActive = false;
            _Shield.SetActive(false);
            return;
        }
        else
        {
            _lives--;
            _uiManager.UpdateLives(_lives);
        }
        if (_lives == 2)
        {
            _Fire_Right.SetActive(true);
        }
        else if (_lives == 1)
        {
            _Fire_Left.SetActive(true);
        }
        if (_lives == 0)
        {
            _spawnManager.StopSpawn();
            Destroy(this.gameObject);
        }
    }


    public void TripleShotActive()
    {
        _isTheTripleActive = true;
        StartCoroutine(TSPU_Active_Timer());
    }

    IEnumerator TSPU_Active_Timer()
    {
        while (_isTheTripleActive == true)
        {
            yield return new WaitForSeconds(5.0f);
            _isTheTripleActive = false;
        }
    }

    public void SpeedBoostActive()
    {
        _isTheSpeedActive = true;
        StartCoroutine(Speed_Active_Timer());
    }

    IEnumerator Speed_Active_Timer()
    {
        while (_isTheSpeedActive == true)
        {
            yield return new WaitForSeconds(5.0f);
            _isTheSpeedActive = false;
        }
    }

    public void ShieldActive()
    {
        _Shield.SetActive(true); 
        _isTheShieldActive = true;
    }

    public void AddScore(int points)
    {
        _score += points;
        _uiManager.UpdateScore(_score);
    }

    public void PowerUpSound()
    {
        _powerUpSound.Play();
    }
}
