﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalScript : MonoBehaviour
{

    public Shader newshader;
    Shader startShader;

    public GameObject parentTile;

    public MeshRenderer portalMesh;

    int sceneTarget;

    [SerializeField] MakePortalVisible makePortalVisible;

    static int currentScene = 2;


    void Start()
    {
        portalMesh.enabled = false;

        this.sceneTarget = makePortalVisible.sceneTarget;

    }

    private void Awake()
    {

      portalMesh.enabled = false;
    }

    private void OnEnable()
    {
        portalMesh.enabled = false;

    }

  

    private void OnTriggerEnter(Collider other)
    {
          if (other.CompareTag("Player"))
          {

            transform.parent = null;

           
            ObjectPooling.instance.ChangeMatFromTo(GameManager.instance.currentScene, this.sceneTarget);

            Debug.Log("CurrentScene: " + currentScene);
            GameManager.instance.DeactivateScene(currentScene);
            currentScene = sceneTarget;
            

          }

          if (other.CompareTag("PortalTrigger"))
          {

           
             portalMesh.enabled = true;
             
          }
           
         
    }

    /*
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("diocornuto");
            GameManager.instance.DeactivateScene(currentScene);
            currentScene = sceneTarget;
        }
    }
    */

    private void OnDisable()

    {
        portalMesh.enabled = false;
    }


}
