using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{

    [SerializeField]
    private float _laserspeed = 8.9f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        LaserMove();
        LaserDestroy();
    }

    void LaserMove()
    {
        transform.Translate(Vector3.up * _laserspeed * Time.deltaTime);
    }

    void LaserDestroy()
    {
        if (transform.position.y >= 7)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(this.gameObject);
        }
    }
}
