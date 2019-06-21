using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public GameObject Player;
    [SerializeField] private string sceneName;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.gameObject.tag == "Player" && Player.GetComponent<HeroController>().keyCheck == true)
        {
            Player.transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            Invoke("NextScene", 3.0f);
        }
    }

    private void NextScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}