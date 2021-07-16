using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalScript : MonoBehaviour
{
    int i = 0;
    void Start()
    {
        //transform.parent = null;

        parentTile.transform.GetChild(0).GetComponent<MeshRenderer>().sharedMaterial.shader = startShader;



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
              SceneManager.UnloadSceneAsync(1);

            transform.parent = null;

            if (parentTile!=null)
             parentTile.transform.GetChild(0).GetComponent<MeshRenderer>().sharedMaterial.shader = newshader;  

           // GetComponent<Material>().shader = newshader;

            // SceneManager.MoveGameObjectToScene

            Debug.Log("SCARICA");

          }
      }


    private void OnEnable()
    {
        if (i == 0)
        {
            SceneManager.LoadScene(2, LoadSceneMode.Additive);
            i++;
        }
        

    }
   

    // Update is called once per frame
    void Update()
    {
        
    }
}
