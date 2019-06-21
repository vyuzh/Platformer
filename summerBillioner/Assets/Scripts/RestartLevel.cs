using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartLevel : MonoBehaviour
{
    [SerializeField] private string sceneName;

    public void RestarScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
