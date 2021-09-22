using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    public float Health = 50f;
    public UnityAction<GameObject> OnEnemyDestroyed = delegate { };
    private bool _isHit = false;

    void OnDestroy()
    {
        if (_isHit)
        {
            OnEnemyDestroyed(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        // if collided object has doesnt have rigidbody
        if (col.gameObject.GetComponent<Rigidbody2D>() == null) return;
        // kalau burung, langsung mati
        if (col.gameObject.tag == "Bird")
        {
            _isHit = true;
            Destroy(gameObject);
        }
        // kalau obstacle, drain hp
        else if (col.gameObject.tag == "Obstacle")
        {
            //Hitung damage yang diperoleh
            float damage = col.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude * 10;
            Health -= damage;

            if (Health <= 0)
            {
                _isHit = true;
                Destroy(gameObject);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
