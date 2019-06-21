using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeEnemyControl : MonoBehaviour
{
    [SerializeField] private GameObject Bullet;
    private Vector3 PositionBullet;
    private bool FlagAtack = true;

    private void Update()
    {
        Atack();
    }

    private void Atack()
    {
        if(FlagAtack)
        {
            if(gameObject.transform.localScale.x < 0)
            {
                PositionBullet = new Vector3(gameObject.transform.position.x + 0.9f, 
                                        gameObject.transform.position.y, 
                                        gameObject.transform.position.z);
            }
            if(gameObject.transform.localScale.x > 0)
            {
                PositionBullet = new Vector3(gameObject.transform.position.x - 0.9f, 
                                        gameObject.transform.position.y, 
                                        gameObject.transform.position.z);
            }
            FlagAtack = false;
            Invoke("EnableBullet", 2.5f);
            Instantiate(Bullet, PositionBullet, Quaternion.identity);
        }
    }

    private void EnableBullet()
    {
        FlagAtack = true;
    }

}
