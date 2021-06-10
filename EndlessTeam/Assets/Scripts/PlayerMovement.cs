using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Reference al Character controller per muovere il player
    [SerializeField]
    CharacterController controller;

    //Variabile per la velocità di movimento
    [Header("Velocità di movimento")]
    [SerializeField]
    float movSpeed;

    //0 Left, 1 Mid, 2 Right: Posizione delle carreggiate
    [Header("Posizioni delle carreggiate")]
    [SerializeField]
    Transform[] positions;

    /// <summary>
    /// <var name="startPos"> Posizione del tocco iniziale.</var>
    /// <var name="swipeDelta"> Differenza di posizione tra tocco iniziale e posizione corrente del tocco.</var>
    /// </summary>
    Vector2 startPos, swipeDelta;

    /// <summary>
    /// <var name="isTouching"> Booleana per capire se il player sta toccando lo schermo o no.</var>
    /// <var name="canSwipe"> Booleana per decidere se è possibile effettuare lo swipe.</var>
    /// </summary>
    bool isTouching, canSwipe;

    //Enum di Swipe per ogni carreggiata
    enum Swipe
    {
        Mid, Left, Right, Up, Down
    }
    //Carreggiata corrente
    string currentState = "Mid";

    //Inizializziazione dell'enum Swipe
    Swipe swipeEn;

    ///COMMENTI DA INSERIRE
    float gravity = -16.81f;
    float jumpForce = 6;
    Vector3 gravityForce = default;

    [SerializeField]
    Transform groundCheck;

    [SerializeField]
    LayerMask groundMask;

    [SerializeField]
    float crouchHeight;

    private void Start()
    {
        //Assegno di default il valore mid all'enum swipe
        swipeEn = Swipe.Mid;
    }

    void Update()
    {
        //Se si sta toccando lo schermo
        if (Input.touchCount > 0)
        {
            //Prendo la reference del primo tocco
            Touch touch = Input.touches[0];
            //Se è appena iniziato
            if (touch.phase == TouchPhase.Began)
            {
                //Rendo possibile lo swipe
                canSwipe = true;
                //Sto toccando lo schermo quindi
                isTouching = true;
                //Memorizzo la posizione
                startPos = touch.position;
            }
            //Se il tocco è stato tolto
            else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                //Viene chiamato Reset()
                Reset();
                //Non si sta più toccando lo schermo
                isTouching = false;
            }

            //Se si sta toccando lo schermo
            if (isTouching)
            {
                //Calcolo la differenza per effettuare lo swipe e l'assengo a swipeDelta
                swipeDelta = touch.position - startPos;
            }
        }

        //Se lo swipe ha superato la deadzone di 125
        if (swipeDelta.magnitude > 125)
        {
            //Prendo i suoi valori x e y
            float x = swipeDelta.x;
            float y = swipeDelta.y;
            
            //Calcolo verso dove viene effettuato lo swipe, e poii lo assegno all'enum
            //Orizzontale
            if (Mathf.Abs(x) > Mathf.Abs(y))
            {
                //Destra
                if (x > 0)
                    swipeEn = Swipe.Right;
                //Sinistra
                else swipeEn = Swipe.Left;
            }
            //Verticale
            else
            {
                //Su
                if (y > 0)
                    swipeEn = Swipe.Up;
                //Giù
                else swipeEn = Swipe.Down;
            }

            //Se posso effettuare lo swipe
            if (canSwipe)
            {
                if (swipeEn == Swipe.Up)
                {
                    gravityForce.y += Mathf.Sqrt(jumpForce * -2f * gravity);
                }
                else if (swipeEn == Swipe.Down)
                {
                    StopCoroutine("ChangingPosition");
                }
                else
                {
                    //Fermo la coroutine nel caso in cui dovesse essere ancora attiva per la chiamata precedente
                    StopCoroutine("ChangingPosition");
                    //Chiamo la coroutine per cambiare carreggiata e gli passo la stringa dell'enum che servirà da target
                    StartCoroutine(ChangingPosition(swipeEn.ToString()));
                    //Non posso effettuare lo swipe fino all'inizio del prossimo tocco
                }
                canSwipe = false;
            }

           
        }

        ///COMMENTI DA INSERIRE
        gravityForce.y += gravity * Time.deltaTime;

        controller.Move(gravityForce * Time.deltaTime);

        if (Physics.CheckSphere(groundCheck.position, .2f, groundMask))
        {
            if (gravityForce.y < 0) gravityForce.y = -2f;

        }

        //Movimento continuo in avanti
        controller.Move(transform.forward * movSpeed * Time.deltaTime);
    }

    //Resetta la posizione di tocco iniziale e il calcolo della differenza dello lo swipe
    //Viene chiamato quando stacco il dito dallo schermo
    private void Reset()
    {
        startPos = swipeDelta = Vector2.zero;
    }
    
    //Coroutine che si occupa di far cambiare carreggiata al player a seguito di uno swipe
    IEnumerator ChangingPosition(string target)
    {
        //Prendo la posizione iniziale del player
        Vector3 dest = transform.position;
        //Calcolo della posizione finale
        Vector3 finalPos = default;
        //Se il target è sinistra
        if (target == "Left")
        {
            //e sono a destra
            if (currentState == "Right")
            {
                //Vado nel mezzo
                finalPos = positions[1].position;
                currentState = "Mid";
            }
            //Altrimenti mi sposto a sinistra
            else
            {
                finalPos = positions[0].position;
                currentState = "Left";
            }
        }
        //Se il target è destra
        else if (target == "Right")
        {
            //e sono a sinistra
            if (currentState == "Left")
            {
                //Vado nel mezzo
                finalPos = positions[1].position;
                currentState = "Mid";
            }
            //Altrimenti vado a destra
            else
            {
                finalPos = positions[2].position;
                currentState = "Right";
            }
        }

        //Elimino come poisizione finale le y e la z visto che non verranno intaccati dal cambio di carreggiata
        finalPos.y = finalPos.z = 0;

        //Finchè il valore x della posizione non è uguale a quello del target
        while (transform.position.x != finalPos.x)
        {
            //Lerpo la posizione a quella finale
            dest.x = Mathf.Lerp(dest.x, finalPos.x, .1f);
            dest.y = transform.position.y;
            dest.z = transform.position.z;

            //Se manca poco all'arrivo della posizione, viene direttamente messa uguale alla posizione finale
            //In questo modo si evitano loop infiniti
            if (Mathf.Abs(dest.x - finalPos.x) < .2f)
                dest.x = finalPos.x;

            //Assegno continuamente la posizione lerpata a quella effettiva del player
            transform.position = dest;

            //Ritorno null
            yield return null;
        }

    }
    public IEnumerator SlideCor()
    {
        //Finchè l'altezza non è arrivata a quella bassa memorizzata da crouchHeight
        while (controller.height > crouchHeight)
        {
            //Lerpo l'altezza del controller a quella di crouchHeight
            controller.height = Mathf.Lerp(controller.height, crouchHeight, .5f);
            //Se l'altezza è quasi arrivata a destinazione, la setto subito uguale così da evitare rallentamenti prima che finisca la coroutine
            if (controller.height <= crouchHeight + .1f)
                controller.height = crouchHeight;
            yield return null;
        }
    }
}
