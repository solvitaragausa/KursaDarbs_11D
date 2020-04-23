using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PostGame : MonoBehaviour
{
    public void ReloadLevel()
    {
        SceneManager.LoadScene(0);
        Common_Vertibas.AtlautsSpelet = true;
    }
}
