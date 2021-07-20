using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalScript : MonoBehaviour
{

    public Shader newshader;
    Shader startShader;

    public GameObject parentTile;

    MeshRenderer portalMesh;

    int currentScene;


    void Start()
    {
        //transform.parent = null;

        portalMesh = this.gameObject.GetComponent<MeshRenderer>();

        portalMesh.enabled = false;

        parentTile.transform.GetChild(0).GetComponent<MeshRenderer>().sharedMaterial.shader = startShader;

        currentScene = gameObject.scene.buildIndex;



    }

    private void Awake()
    {
        startShader = parentTile.transform.GetChild(0).GetComponent<MeshRenderer>().sharedMaterial.shader;
    }



      private void OnTriggerEnter(Collider other)
      {
          if (other.CompareTag("Player"))
          {
            //SceneManager.UnloadSceneAsync(1);

            transform.parent = null;

            if (parentTile!=null)
                parentTile.transform.GetChild(0).GetComponent<MeshRenderer>().sharedMaterial.shader = newshader;

            // GetComponent<Material>().shader = newshader;

            // SceneManager.MoveGameObjectToScene


            GameManager.instance.DeactivateScene(currentScene);

            Debug.Log("SCARICA");

          }

           if (other.CompareTag("PortalTrigger"))
           {
            portalMesh.enabled = true;
           }
           else if (other.CompareTag("PortalTriggerExit"))
           {
            portalMesh.enabled = false;
           }


      }


   
    //private void OnDisable()
    //{
    //    once = true;
    //}



}
