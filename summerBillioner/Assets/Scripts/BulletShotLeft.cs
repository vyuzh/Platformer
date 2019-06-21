using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShotLeft : MonoBehaviour
{
    [SerializeField] private float speed;
    Rigidbody2D rbBullet;

    private void Start()
    {
        rbBullet = GetComponent<Rigidbody2D>();
        rbBullet.AddRelativeForce(Vector2.left * speed, ForceMode2D.Impulse);
    }
    private void Update() {
        Dead();
    }

    private void Dead()
    {
        Destroy(gameObject, 5f);
    }
}
