using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShotRight : MonoBehaviour
{
    [SerializeField] private float speed;
    Rigidbody2D rbBullet;

    private void Start()
    {
        rbBullet = GetComponent<Rigidbody2D>();
        rbBullet.AddRelativeForce(Vector2.right * speed, ForceMode2D.Impulse);
    }
    private void Update() {
        Dead();
    }

    private void Dead()
    {
        Destroy(gameObject, 5f);
    }
}
