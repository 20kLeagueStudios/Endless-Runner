using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalScript : MonoBehaviour
{
    int i = 0;

    MeshRenderer mesh;

    int currentScene;
    void Start()
    {
        //transform.parent = null;

        parentTile.transform.GetChild(0).GetComponent<MeshRenderer>().sharedMaterial.shader = startShader;

        currentScene = gameObject.scene.buildIndex;

        mesh = GetComponent<MeshRenderer>();
        mesh.enabled = false;

    }



    private void Awake()
    {
        startShader = parentTile.transform.GetChild(0).GetComponent<MeshRenderer>().sharedMaterial.shader;
    }

    public Shader newshader;
    Shader startShader;

   public GameObject parentTile;



      private void OnTriggerEnter(Collider other)
      {
          if (other.tag == "Player")
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

        if (other.tag == "PortalTrigger")
        {

            mesh.enabled = true;
        
        }



      }


   
    //private void OnDisable()
    //{
    //    once = true;
    //}



}
