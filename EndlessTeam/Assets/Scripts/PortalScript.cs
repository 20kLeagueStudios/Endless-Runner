using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalScript : MonoBehaviour
{

    public Shader newshader;
    Shader startShader;

    public GameObject parentTile;

    public MeshRenderer portalMesh;

    int currentScene;

    int sceneTarget;


    void Start()
    {
        this.sceneTarget = transform.GetChild(0).GetComponent<MakePortalVisible>().sceneTarget;

        //portalMesh = this.gameObject.GetComponent<MeshRenderer>();

       // portalMesh.enabled = false;

        //parentTile.transform.GetChild(0).GetComponent<MeshRenderer>().sharedMaterial.shader = startShader;

        currentScene = gameObject.scene.buildIndex;



    }

    private void Awake()
    {
        //portalMesh = this.gameObject.GetComponent<MeshRenderer>();

      portalMesh.enabled = false;
      //  startShader = parentTile.transform.GetChild(0).GetComponent<MeshRenderer>().sharedMaterial.shader;
    }

    private void OnEnable()
    {
        portalMesh.enabled = false;

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

            ObjectPooling.instance.ChangeMatFromTo(GameManager.instance.currentScene, this.sceneTarget);


            GameManager.instance.DeactivateScene(currentScene);

            Debug.Log("SCARICA");

          }

          else if (other.CompareTag("PortalTrigger"))
           {

            //  Debug.Log("ENTRATA PORT");
             portalMesh.enabled = true;

          //  tifotto = true;
            

           }
           
           
 

      }



    private void OnDisable()
    {
        portalMesh.enabled = false;
    }

    public bool tifotto = false;

    private void Update()
    {
        /*
        if (tifotto)
        {
            Debug.Log("PAPARA");
            portalMesh.enabled = true;
        }
        */
    }


}
