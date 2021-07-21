using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Masso : MonoBehaviour
{
    [SerializeField]
    Transform finalPos;

    IEnumerator FallCor()
    {
        Vector3 temp = transform.position;
        while(transform.position.y != finalPos.position.y)
        {
            temp.x = transform.position.x;
            temp.z = transform.position.z;
            temp.y = Mathf.Lerp(temp.y, finalPos.position.y, .1f);

            if (Mathf.Abs(temp.y - finalPos.position.y ) < .001f)
                temp.y = finalPos.position.y;

            transform.position = temp;

            yield return null;

            ////Finchè il valore x della posizione non è uguale a quello del target
            //while (transform.position.x != finalPos.x)
            //{
            //    //Lerpo la posizione a quella finale
            //    dest.x = Mathf.Lerp(dest.x, finalPos.x, .1f);
            //    dest.y = transform.position.y;
            //    dest.z = transform.position.z;

            //    //Se manca poco all'arrivo della posizione, viene direttamente messa uguale alla posizione finale
            //    //In questo modo si evitano loop infiniti
            //    if (Mathf.Abs(dest.x - finalPos.x) < .4f)
            //        dest.x = finalPos.x;

            //    //Assegno continuamente la posizione lerpata a quella effettiva del player
            //    transform.position = dest;

            //    //Ritorno null
            //    yield return null;
            }

        yield return null;
    }

    public void CallFallCor()
    {
        StartCoroutine("FallCor");
    }
}
