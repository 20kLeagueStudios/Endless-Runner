using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void Scene(int value)
    {
        SaveSystem.Saving(GameManager.instance);
        SceneManager.LoadScene(value);
        
    }
}
