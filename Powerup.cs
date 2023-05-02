using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour

{

    [SerializeField]
    private float _powerupspeed = 3.0f;
    [SerializeField] // 0 = Triple, 1 = Speed, 2 = Shield
    private int powerupID;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PowerupMoving();
        DestoryPowerup();
    }

    void PowerupMoving()
    {
        transform.Translate(Vector3.down * _powerupspeed * Time.deltaTime);
    }

    void DestoryPowerup()
    {
        if (transform.position.y <= -4.9f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {
                switch (powerupID)
                {
                    case 0:
                        player.TripleShotActive();
                        break;
                    case 1:
                        player.SpeedBoostActive();
                        break;
                    case 2:
                        player.ShieldActive();
                        break;
                }
            }
            player.PowerUpSound();
            Destroy(this.gameObject);
        }
    }
}
