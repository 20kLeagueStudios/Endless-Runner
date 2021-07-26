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
        portalMesh = this.gameObject.GetComponent<MeshRenderer>();

        portalMesh.enabled = false;
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
            if (this.enabled)
            {
                Debug.Log("ENTRATA PORT");
                portalMesh.enabled = true;
            }

           }
           
           
 

      }



    private void OnDisable()
    {
        portalMesh.enabled = false;
    }

    private void Update()
    {
        Debug.Log("PORT"+portalMesh.enabled);
    }


}
