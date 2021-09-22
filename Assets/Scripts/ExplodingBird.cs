using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodingBird : Bird
{
    public float explosionStrength = 100;

    // sumber https://stackoverflow.com/questions/34250868/unity-addexplosionforce-to-2d
    protected override void OnCollisionEnter2D(Collision2D _other)
    {
        // Debug.Log("colliding2");
        _state = BirdState.HitSomething;
        if (_other.collider.gameObject.tag == "Obstacle" || _other.collider.gameObject.tag == "Enemy")
            _other.rigidbody.AddExplosionForce(explosionStrength, this.transform.position, 500);
    }
}
