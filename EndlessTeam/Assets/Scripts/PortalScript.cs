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

    [SerializeField] MakePortalVisible makePortalVisible;


    void Start()
    {
        portalMesh.enabled = false;

        this.sceneTarget = makePortalVisible.sceneTarget;

        currentScene = gameObject.scene.buildIndex;

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

            if (parentTile!=null)
                parentTile.transform.GetChild(0).GetComponent<MeshRenderer>().sharedMaterial.shader = newshader;

            ObjectPooling.instance.ChangeMatFromTo(GameManager.instance.currentScene, this.sceneTarget);
            GameManager.instance.DeactivateScene(currentScene);

          }

          if (other.CompareTag("PortalTrigger"))
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


}
