using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    #region Variabili
    bool isKeyboard = false, wallTouch = false;

    int currentObstacle = default;

    int currentScore = 0, currentMoney = 0;


    int obstacleCount = 0;

    float scoreIncTime;

    [SerializeField]
    TextMeshProUGUI scoreText;

    [SerializeField]
    TextMeshProUGUI moneyText;

    [SerializeField]
    PlayerHealth healthScript;
    [SerializeField]
    ObjectPooling objectPooling;
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

    bool sliding = false;

    //Enum di Swipe per ogni carreggiata
    enum Swipe
    {
        Mid, Left, Right, Up, Down
    }
    //Carreggiata corrente
    string currentState = "Mid";

    //Inizializziazione dell'enum Swipe
    Swipe swipeEn;

    //Gravità, forza di salto e vettore in cui verrà applicato all'asse Y la gravità e il salto

    public float gravity = -30.81f; ////////prima era privata 

    public float jumpForce; ///////////prima era public
    public Vector3 verticalForce = default;

    //Posizione da cui creare una sfera invisibile che controllerà se si tocca il pavimento o no
 
    public Transform groundCheck; ////////prima era privata serializzata

    //Layer del pavimento
    [SerializeField]
    LayerMask groundMask;

    //Altezza che dovrà avere il character controller e la posizione che dovrà avere il punto Y del character controller
    //Quando si utilizzerà lo slide
    [SerializeField]
    float crouchHeight, crouchPos;

    //Stessa cosa dello slide ma per ritornare allo stato di corsa
    float idleHeight, idlePos;

    //Riferimento all'animatore
    [SerializeField]
    Animator animator;

    //ChangeG è una booleane che mi servirà per invertire la gravità
    //canIPress è una booleana che mi servirà a mettere un countdown al bottone
    //mentre Image button mi serve come rifermento al bottone per cambiare colore quando viene premuto
    public bool changeG;/// era priv
    public bool canIPress;/// era priv 

    /*
    [SerializeField]///
    private Image button;
    */

    //is ground mi serve per controllare se il giocatore è Ground
    public bool isGround; ///////////////////prima era privata
    //isTetto mi serve controllare se il giocatore tocca il teto o meno
    public bool isTetto = false;////////////////prima era privata
    //CGPoint 1 e 2 mi servono per cambiare la posizione del GroundCheck
    
    public GameObject CGPoint1; ////////prima era privata serializzata
    public GameObject CGPoint2; ////////prima era privata serializzata

    //playerBody mi serve come rifermineto al corpo del giocatore per poi ruotarlo quando cambia la gravità
   
    public GameObject playerBody;////////prima era privata serializzata

    public  GameObject cam1pos; ////////prima era privata serializzata

    public  GameObject cam2pos; ////////prima era privata serializzata

    string prePoint;
    Vector3 value = default;
    Vector3 initialPoint;
    RaycastHit hit;
    bool rayWall = false;
    Ray ray;
    [SerializeField]
    LayerMask wallMask;

    public PowerUpsManager powerupsManager; ///


    #endregion

    private void OnEnable()
    {
        controller.enabled = false;
        controller.transform.position = new Vector3(positions[1].position.x, transform.position.y, transform.position.z);
    }

    private void Start()
    {
        controller.enabled = false;
        controller.transform.position = new Vector3(positions[1].position.x, transform.position.y, transform.position.z);
        controller.enabled = true;

        Debug.Log("POSIZIONE" + this.gameObject.transform.position);
        
        //characterController.enabled = true;

        //pressTime = 0;
        //setto ChangeG a false, canIPress a true e il colore a green.
        changeG = false; ///
        canIPress = true; ///

        //button.color = Color.green;///

        //Setto l'altezza standard a quella iniziale
        idleHeight = controller.height;
        idlePos = controller.center.y;

        //Assegno di default il valore mid all'enum swipe
        swipeEn = Swipe.Mid;



    }



    void Update()
    {

        if (Time.time - scoreIncTime > .1f)
        {
            IncreaseScore(5);
            scoreIncTime = Time.time;
        }

        //Debug.Log("POSIZIONE"+ controller.transform.position);

        isGround = Physics.CheckSphere(groundCheck.position, .4f, groundMask);

        value.x = positions[1].position.x - positions[0].position.x;

        initialPoint = transform.position - (value) - Vector3.right;
        ray.origin = initialPoint;
        ray.direction = transform.right;


        if (Physics.Raycast(ray.origin, ray.direction, value.x + 8, wallMask))
        {
            rayWall = true;
        }

        if (Physics.Raycast(transform.position, transform.forward, 8f, wallMask))
        {
            rayWall = false;
        }

        //if (Physics.CheckSphere(transform.position, 5f, wallMask))
        //{
        //    rayWall = true;
        //}

        Debug.DrawRay(transform.position, transform.forward * 8f, Color.red);

        /*
        if (isGround) //Se il giocatore torna a terra resetto il pulsante
        {
            pressTime = -1;
        }

        if (pressTime >= 0) //Se il pulsante è stato premuto X volte allora diventerà giallo
        {
            button.color = Color.yellow; //setto il colore a giallo
        }

        if (button.color == Color.yellow & isGround) //se il pulsante è giallo e il giocatore è a terra.. allora resetto il colore a verde
        {
            button.color = Color.green;
        }
        */


        /* ADESSO QUESTO CHECK VIENE FATTO NELL UPDATE DELLO SCRIPT POWERUPMANAGER
        if (changeG == false) ///
        {
            //se ChangeG è false allora richiamo questo metodo
            ChangeGravityOFF();
        }
        else
        {
            //se ChangeG è true allora richiamo questo metodo
            ChangeGravityON();
        }
        */

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
        } //Comandi da tastiera, se premo un tasto orizzontale o verticale
        else if ((Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0) && !isKeyboard && !wallTouch)
        {
            canSwipe = true;

            Debug.Log("Tastiera");
            float x = Input.GetAxisRaw("Horizontal");
            //Debug.Log("X: " + x);

            float y = Input.GetAxisRaw("Vertical");
            //Debug.Log("Y: " + y);
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
                else if (y < 0) swipeEn = Swipe.Down;
            }


            //Se posso effettuare lo swipe
            if (canSwipe)
            {
                if (changeG == false)
                {
                    //Se faccio lo swipe in alto
                    if (swipeEn == Swipe.Up)
                    {
                        //Se tocco il pavimento
                        if (isGround)
                        {

                            //Applico la forza del salto a verticalForce
                            verticalForce.y = jumpForce;
                            //Se sto usando lo sliding lo disattivo chiamando ResetSliding()
                            if (sliding) ResetSliding();


                        }
                    }
                    //Altrimenti se sto effettuando lo swipe in basso
                    else if (swipeEn == Swipe.Down)
                    {
                        //Se sto toccando il pavimento e non sto già usando lo slide attivo la coroutine SlideCor
                        if (isGround && !sliding)
                            StartCoroutine("SlideCor");
                        //Altrimenti applico una forza al verticalForce che spingerà più velocemente in basso il giocatore
                        else if (!isGround)
                        {
                            verticalForce.y = -34f; 

                            powerupsManager.inSlam = true;
                            StartCoroutine(powerupsManager.Slam());


                        }
                        


                    }
                    //Altrimenti se sto facendo uno swipe orizzontale
                    else
                    {
                        //if (!coroutineOn)
                        //{
                        //Fermo la coroutine nel caso in cui dovesse essere ancora attiva per la chiamata precedente
                        StopCoroutine("ChangingPosition");
                        StopAllCoroutines();
                        //Chiamo la coroutine per cambiare carreggiata e gli passo la stringa dell'enum che servirà da target
                        StartCoroutine(ChangingPosition(swipeEn.ToString()));
                        //Non posso effettuare lo swipe fino all'inizio del prossimo tocco
                        //}
                    }
                    //rendo canSwipe a false, in questo modo per fare un nuovo swipe dovrò ripoggiare il dito
                    canSwipe = false;
                    isKeyboard = true;
                }
                else
                {
                    //Se faccio lo swipe in alto
                    if (swipeEn == Swipe.Down)
                    {
                        //Se tocco il pavimento
                        if (isGround)
                        {


                            //Applico la forza del salto a verticalForce
                            verticalForce.y = jumpForce;
                            //Se sto usando lo sliding lo disattivo chiamando ResetSliding()
                            if (sliding) ResetSliding();


                        }
                    }
                    //Altrimenti se sto effettuando lo swipe in basso
                    else if (swipeEn == Swipe.Up)
                    {


                        //Se sto toccando il pavimento e non sto già usando lo slide attivo la coroutine SlideCor
                        if (isGround && !sliding)
                            StartCoroutine("SlideCor");
                        //Altrimenti applico una forza al verticalForce che spingerà più velocemente in basso il giocatore
                        else if (!isGround)
                        {
                            verticalForce.y = 34f; 

                            powerupsManager.inSlam = true;
                            StartCoroutine(powerupsManager.Slam());


                        }
                        


                    }
                    //Altrimenti se sto facendo uno swipe orizzontale
                    else
                    {
                        //if (!coroutineOn)
                        //{
                        //Fermo la coroutine nel caso in cui dovesse essere ancora attiva per la chiamata precedente
                        StopCoroutine("ChangingPosition");
                        StopAllCoroutines();
                        //Chiamo la coroutine per cambiare carreggiata e gli passo la stringa dell'enum che servirà da target

                        StartCoroutine(ChangingPosition(swipeEn.ToString()));
                        //Non posso effettuare lo swipe fino all'inizio del prossimo tocco
                        //}
                    }
                    //rendo canSwipe a false, in questo modo per fare un nuovo swipe dovrò ripoggiare il dito
                    canSwipe = false;
                    isKeyboard = true;

                }

            }
        }
        else if (Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0) isKeyboard = false;



        //Se lo swipe ha superato la deadzone di 125
        if (swipeDelta.magnitude > 10)
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

                if (changeG == false)
                {
                    //Se faccio lo swipe in alto
                    if (swipeEn == Swipe.Up)
                    {
                        //Se tocco il pavimento
                        if (isGround)
                        {
                          
                            Debug.Log(0);
                            //Applico la forza del salto a verticalForce
                            verticalForce.y = jumpForce;
                            //Se sto usando lo sliding lo disattivo chiamando ResetSliding()
                            if (sliding) ResetSliding();

                        }

                      

                    }
                    //Altrimenti se sto effettuando lo swipe in basso
                    else if (swipeEn == Swipe.Down)
                    {
                        //Se sto toccando il pavimento e non sto già usando lo slide attivo la coroutine SlideCor
                        if (isGround && !sliding)
                            StartCoroutine("SlideCor");
                        //Altrimenti applico una forza al verticalForce che spingerà più velocemente in basso il giocatore
                        else if (!isGround)
                        {
                            //verticalForce.y = -34f; // 

                            powerupsManager.inSlam = true;
                            StartCoroutine(powerupsManager.Slam());


                        }


                    }
                    //Altrimenti se sto facendo uno swipe orizzontale
                    else
                    {
                        //Fermo la coroutine nel caso in cui dovesse essere ancora attiva per la chiamata precedente
                        StopAllCoroutines();
                        StopCoroutine("ChangingPosition");
                        //Chiamo la coroutine per cambiare carreggiata e gli passo la stringa dell'enum che servirà da target
                        StartCoroutine(ChangingPosition(swipeEn.ToString()));
                        //Non posso effettuare lo swipe fino all'inizio del prossimo tocco
                    }
                    //rendo canSwipe a false, in questo modo per fare un nuovo swipe dovrò ripoggiare il dito
                    canSwipe = false;
                }
                else
                {
                    //Se faccio lo swipe in alto
                    if (swipeEn == Swipe.Down)
                    {
                        //Se tocco il pavimento
                        if (isGround)
                        {
                            Debug.Log(1);
                            //Applico la forza del salto a verticalForce
                            verticalForce.y = jumpForce;
                            //Se sto usando lo sliding lo disattivo chiamando ResetSliding()
                            if (sliding) ResetSliding();

                        }
                    }
                    //Altrimenti se sto effettuando lo swipe in basso
                    else if (swipeEn == Swipe.Up)
                    {


                        //Se sto toccando il pavimento e non sto già usando lo slide attivo la coroutine SlideCor
                        if (isGround && !sliding)
                            StartCoroutine("SlideCor");
                        //Altrimenti applico una forza al verticalForce che spingerà più velocemente in basso il giocatore
                        else if (!isGround)
                            verticalForce.y = 34f;


                    }
                    //Altrimenti se sto facendo uno swipe orizzontale
                    else
                    {
                        //Fermo la coroutine nel caso in cui dovesse essere ancora attiva per la chiamata precedente
                        StopAllCoroutines();
                        StopCoroutine("ChangingPosition");
                        //Chiamo la coroutine per cambiare carreggiata e gli passo la stringa dell'enum che servirà da target
                        StartCoroutine(ChangingPosition(swipeEn.ToString()));
                        //Non posso effettuare lo swipe fino all'inizio del prossimo tocco
                    }
                    //rendo canSwipe a false, in questo modo per fare un nuovo swipe dovrò ripoggiare il dito
                    canSwipe = false;
                }

            }


        }


        //Applico verticalForce al controller
        controller.Move(verticalForce * Time.deltaTime);

        //Se sto toccando il pavimento e la forza è minore di zero verrà messa di default a -3 così che
        //non raggiunga valori immensi

        if (Physics.CheckSphere(groundCheck.position, .4f, groundMask))
        {
            if (changeG == false)
            {
                if (verticalForce.y < 0) verticalForce.y = -3f;

            }
            else
            {
                if (verticalForce.y > 0) verticalForce.y = 3f;
            }

        }
        else
        {

            //Aggiungo la gravità alla verticalForce
            verticalForce.y += gravity * Time.deltaTime;
        }


        //Movimento continuo in avanti
        controller.Move(transform.forward * movSpeed * Time.deltaTime);
    }



    //Disattivo lo sliding, e setto a false l'animazione dello slide così che esca e ritorni allo stato corsa
    public void ResetSliding()
    {
       // canIPress = true; ////////
        sliding = false;
        animator.SetBool("Slide", false);
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
            //Altrimenti se sono al centro mi sposto a sinistra
            else if (currentState == "Mid")
            {
                finalPos = positions[0].position;
                currentState = "Left";
                //Altrimenti se sono a sinistra mi sposto fuori sinistra
            }
            else if (currentState == "Left")
            {
                finalPos = positions[0].position * 2;
                currentState = "LeftOut";
            }
        }
        //Se il target è destra
        else if (target == "Right")
        {
            //e sono a sinistra mi sposto nel mezzo
            if (currentState == "Left")
            {
                //Vado nel mezzo
                finalPos = positions[1].position;
                currentState = "Mid";
            }
            //Altrimenti se sono al centro vado a destra
            else if (currentState == "Mid")
            {
                finalPos = positions[2].position;
                currentState = "Right";
            }
            //Altrimenti se sono a destra vado fuori destra
            else if (currentState == "Right")
            {
                finalPos = positions[2].position * 2;
                currentState = "RightOut";
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
            if (Mathf.Abs(dest.x - finalPos.x) < .4f)
                dest.x = finalPos.x;

            //Assegno continuamente la posizione lerpata a quella effettiva del player
            transform.position = dest;

            //Ritorno null
            yield return null;
        }
        prePoint = currentState;
    }

    //Si occupa di effettuare lo slide
    public IEnumerator SlideCor()
    {
        //Rendo lo sliding a true così che SlideCor() non può essere chiamato mentre è in esecuzione
        sliding = true;
        //Attivo l'animazione dello slide
        animator.SetBool("Slide", true);
        //Setto la posizione dei vettori che determineranno l'altezza e la posizione in slide del controller
        Vector3 controllerCrouchPos = new Vector3(0, crouchPos, 0);
        Vector3 controllerIdlePos = new Vector3(0, idlePos, 0);
        //Se l'altezza corrente del controller è diversa da quella in slide, significa che dobbiamo assegnarla
        if (controller.height != crouchHeight)
        {
            //Setto l'altezza e la posizione in slide
            controller.height = crouchHeight;
            controller.center = controllerCrouchPos;
            yield return null;
        }
        //Finché slide è vero rimane bloccata la coroutine, sliding verrà messo a true quando faremo lo swipe in alto
        //o quando l'animazione starà quasi per finire
        while (sliding)
        {
            yield return null;
        }
        //Dopo di che l'altezza e la posizione del controller viene messa in modalità corsa
        controller.height = idleHeight;
        controller.center = controllerIdlePos;

        yield return null;
    }

    //Debug del groundCheck
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(groundCheck.position, .4f);
    }

    /*
    public void ChangeGravity() //questa funzione permette di cambiare gravità quando tocco un bottone.
    {
        if (isGround)
        {
            if (canIPress == true) //se canIPress è true
            {
                changeG = !changeG; //allora cambio changeG da true a false o viceversa
                StartCoroutine(switchColor()); //starto la couroutine per cambiare il colore del bottone
                canIPress = false; //setto CaniPress a false
            }
            if (isTetto == false)
                GoUp();

            if (isTetto == true)
                GoDown();
        }
        else
        {
            pressTime++;
            if (pressTime < 2)
            {
                if (canIPress == true) //se canIPress è true
                {
                    changeG = !changeG; //allora cambio changeG da true a false o viceversa
                    StartCoroutine(switchColor()); //starto la couroutine per cambiare il colore del bottone
                    canIPress = false; //setto CaniPress a false
                }
                if (isTetto == false)
                    GoUp();

                if (isTetto == true)
                    GoDown();
            }
        }

    }

    */

    public void GoUp()
    {
        //setto la verticalForce
        verticalForce.y = 30.81f;
        isGround = Physics.CheckSphere(groundCheck.position, .4f, groundMask);
        //Se tocco il pavimento
        if (isGround)
        {
            //Applico la forza del salto a verticalForce
            verticalForce.y = Mathf.Sqrt(jumpForce * -2f * gravity);
            //Se sto usando lo sliding lo disattivo chiamando ResetSliding()
            if (sliding) ResetSliding();
        }
    }

    public void GoDown()
    {
        //setto la vertical force
        verticalForce.y = -30.81f;
        isGround = Physics.CheckSphere(groundCheck.position, .4f, groundMask);
        if (swipeEn == Swipe.Down)
        {
            //Se sto toccando il pavimento e non sto già usando lo slide attivo la coroutine SlideCor
            if (isGround && !sliding)
                StartCoroutine("SlideCor");
            //Altrimenti applico una forza al verticalForce che spingerà più velocemente in basso il giocatore
            else if (!isGround)
                verticalForce.y = 34f;
        }
    }

    /*
    public void ChangeGravityOFF()
    {
        jumpForce = 18;
        //setto la rotazione del giocatore a 0
        playerBody.transform.rotation = Quaternion.Euler(0, 0, 0);
        //setto la gravità a -30
        gravity = -30.81f;
        //metto isTetto a false
        isTetto = false;
        //abbasso il groundCheck
        groundCheck.transform.position = CGPoint1.transform.position;

        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, cam1pos.transform.position, Time.deltaTime * 3f);
        Camera.main.transform.rotation = Quaternion.Slerp(Camera.main.transform.rotation, cam1pos.transform.rotation, Time.deltaTime * 2f);
    }

    public void ChangeGravityON()
    {

        jumpForce = -18;
        //setto la rotazione del giocatore a 180
        playerBody.transform.rotation = Quaternion.Euler(0, 0, 180);
        //alzo il groundCheck
        groundCheck.transform.position = CGPoint2.transform.position;
        //setto la gravità a 30
        gravity = 30.81f;
        //metto isTetto a True
        isTetto = true;
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, cam2pos.transform.position, Time.deltaTime * 3f);
        Camera.main.transform.rotation = Quaternion.Slerp(Camera.main.transform.rotation, cam2pos.transform.rotation, Time.deltaTime * 2f);
    }

    public IEnumerator switchColor() //cambio colore del bottone
    {

        button.color = Color.red; //setto il colore a rosso
        yield return new WaitForSeconds(1f); //dopo 1 secondo
        button.color = Color.green; //setto il colore a verde
        canIPress = true; //setto canIPress a true, così il bottone si può ripremere.


    }
    */

    IEnumerator ChangingPosition(Vector3 wallPos)
    {

        string target = transform.position.x > wallPos.x ? target = "Right" : target = "Left";
        Debug.Log(target);
        //Prendo la posizione iniziale del player
        Vector3 dest = transform.position;
        //Calcolo della posizione finale
        Vector3 finalPos = default;

        //Se il target è a destra
        if (target == "Right")
        {

            //e sono a fuori sinistra
            if (currentState == "LeftOut")
            {
                Debug.Log("Prova");
                //Vado nel sinistro
                finalPos = positions[0].position;
                currentState = "Left";
            }
            //Altrimenti se sono a sinistra mi sposto al centro
            else if (currentState == "Left")
            {
                finalPos = positions[1].position;
                currentState = "Mid";
            }
            else if (currentState == "Mid")
            {
                finalPos = positions[2].position;
                currentState = "Right";
            }
        }
        //Se il target è a sinistra
        else if (target == "Left")
        {
            //e sono fuori destra
            if (currentState == "RightOut")
            {
                //Vado a destra
                finalPos = positions[2].position;
                currentState = "Right";
            }
            //Altrimenti vado al centro
            else if (currentState == "Right")
            {
                finalPos = positions[1].position;
                currentState = "Mid";
            }
            else if (currentState == "Mid")
            {
                finalPos = positions[0].position;
                currentState = "Left";
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
            if (Mathf.Abs(dest.x - finalPos.x) < .3f)
                dest.x = finalPos.x;

            //Assegno continuamente la posizione lerpata a quella effettiva del player
            transform.position = dest;

            //Ritorno null
            yield return null;
        }

        prePoint = currentState;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Money"))
        {
            IncreaseScore(50);
            IncreaseMoney();
            Destroy(other.gameObject);
        }
        if (other.CompareTag("Obstacle"))
        {
            currentObstacle = other.gameObject.GetInstanceID();
        }
        if (other.CompareTag("Point"))
        {

            if (obstacleCount < 5) obstacleCount++;
            else
            {
                obstacleCount = 0;
                UpgradeSpeed();
            }
            if (other.transform.parent.gameObject.GetInstanceID() != currentObstacle) IncreaseScore(200);
            else
            {
                IncreaseScore(-250);
                currentObstacle = -1;
            }

        }

        if (rayWall)
        {
            if (LayerMask.LayerToName(other.gameObject.layer) == "Wall")
            {
                StopCoroutine("ChangingPosition");
                StopAllCoroutines();
                StartCoroutine("ChangingPosition", other.transform.position);

                rayWall = false;
            }
        }
        else
        {
            if (LayerMask.LayerToName(other.gameObject.layer) == "Wall")
            {
                objectPooling.speed = 0;
                healthScript.InstantDeath();
            }
        }
    }

    private void IncreaseScore(int value)
    {
        currentScore += value;
        if (currentScore < 0) currentScore = 0;
        scoreText.text = "Score: " + currentScore.ToString();

    }

    private void IncreaseMoney()
    {
        currentMoney += 1;
        moneyText.text = ": " + currentMoney.ToString();
    }

    void UpgradeSpeed()
    {
        objectPooling.speed += 4;
        if (objectPooling.speed > objectPooling.maxSpeed) objectPooling.speed = objectPooling.maxSpeed;
    }
}
