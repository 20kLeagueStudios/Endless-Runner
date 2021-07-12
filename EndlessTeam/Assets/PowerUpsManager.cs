using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpsManager : MonoBehaviour
{
    [SerializeField] Game gameScript;

    [SerializeField] Color playerColor;

    

    [SerializeField] MeshRenderer playerMesh;

    [SerializeField] float timePowerUp = 10f;

    [SerializeField] ObjectPooling objectPooling;

    [SerializeField] PlayerHealth playerHealth;

    [SerializeField] PlayerMovement playerMovement;

    public float powerUpBoostSpeed = default;

    public bool isDashing = false;

    GameObject player;
    public Vector3 powerupScale;
    Vector3 startScale;

    public bool canUsePowerUp = true; // bool generale che diventa false quando un PU è attivo per evitare attivazioni multiple

 
    /// variabili per il powerup slam
    public bool inSlam; ///
    bool firstGrounded; ///
    int groundedTime; /// da rimuovere se la funzione FirstgroundCheck viene rimossa
    public GameObject SlamArea;///

   

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle") && isDashing == true)
        {
            other.gameObject.SetActive(false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        SlamArea.SetActive(false);///
        firstGrounded = false;///
        inSlam = false;///

        //initialSpeed = objectPooling.speed;

        playerColor = playerMesh.GetComponent<MeshRenderer>().material.color;

        player = this.gameObject;

        startScale = this.gameObject.transform.localScale;


    }

    

    public void PowerUpActive(string nomePowerup) //funzione richiamat nello script PowerUpstarter per assegnare ad una piattaforma uno specifico powerup
    {
        Invoke(nomePowerup,0f);
    }

    public void ChangeGravityStarter() //questa funzione permette di cambiare gravità quando tocco un bottone.
    {

        if (playerMovement.isGround)
        {

            playerMovement.changeG = !playerMovement.changeG; //allora cambio changeG da true a false o viceversa
                                                              //StartCoroutine(switchColor()); //starto la couroutine per cambiare il colore del bottone
           // playerMovement.canIPress = false; //setto CaniPress a false

            if (playerMovement.isTetto == false)
                playerMovement.GoUp();

            if (playerMovement.isTetto == true)
                playerMovement.GoDown();
        }
        else
        {

                playerMovement.changeG = !playerMovement.changeG; //allora cambio changeG da true a false o viceversa
                
                if (playerMovement.isTetto == false)
                    playerMovement.GoUp();

                if (playerMovement.isTetto == true)
                    playerMovement.GoDown();
            
        }


    }

    public void ChangeGravityOFF()
    {
        playerMovement.jumpForce = 18;
        //setto la rotazione del giocatore a 0
        playerMovement.playerBody.transform.rotation = Quaternion.Euler(0, 0, 0);
        //setto la gravità a -30
        playerMovement.gravity = -30.81f;
        //metto isTetto a false
        playerMovement.isTetto = false;
        //abbasso il groundCheck
        playerMovement.groundCheck.transform.position = playerMovement.CGPoint1.transform.position;

        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, playerMovement.cam1pos.transform.position, Time.deltaTime * 3f);
        Camera.main.transform.rotation = Quaternion.Slerp(Camera.main.transform.rotation, playerMovement.cam1pos.transform.rotation, Time.deltaTime * 2f);
    }

    public void ChangeGravityON()
    {

        playerMovement.jumpForce = -18;
        //setto la rotazione del giocatore a 180
        playerMovement.playerBody.transform.rotation = Quaternion.Euler(0, 0, 180);
        //alzo il groundCheck
        playerMovement.groundCheck.transform.position = playerMovement.CGPoint2.transform.position;
        //setto la gravità a 30
        playerMovement.gravity = 30.81f;
        //metto isTetto a True
        playerMovement.isTetto = true;
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, playerMovement.cam2pos.transform.position, Time.deltaTime * 3f);
        Camera.main.transform.rotation = Quaternion.Slerp(Camera.main.transform.rotation, playerMovement.cam2pos.transform.rotation, Time.deltaTime * 2f);
    }

    IEnumerator DashPowerUp(MeshRenderer meshToFade)
    {
        isDashing = true;
        playerHealth.canBeHit = false;
        canUsePowerUp = false;

        float tmpSpeed = objectPooling.speed;

        objectPooling.speed += powerUpBoostSpeed; 

        Color fadeColor = meshToFade.material.color;

        fadeColor.a = .1f;
        for (int i = 0; i < timePowerUp; i++)
        {
            yield return new WaitForSeconds(.2f);
            meshToFade.material.color = fadeColor;
            yield return new WaitForSeconds(.2f);
            meshToFade.material.color = playerColor;
        }


        objectPooling.speed = tmpSpeed; //potrebbero esserci dei conflitti?
        playerHealth.canBeHit = true;
        isDashing = false;
        canUsePowerUp = true;


    }


    public void DashStarter()
    {
        StartCoroutine(DashPowerUp(playerMesh));
    }

    /*
    public void CallCoroutineMini()
    {
        StartCoroutine(MiniPowerUp());
    }
    */
    public IEnumerator MiniPowerUp()
    {

        float elapsedTime = 0f;
        float waitTime = 2f;

        while (elapsedTime < waitTime)
        {
            elapsedTime += Time.deltaTime;

            canUsePowerUp = false;

            player.transform.localScale = Vector3.Lerp(startScale, powerupScale, elapsedTime / waitTime * 4);

            yield return null;

        }

        // this.GetComponent<PlayerMovement>().enabled = true;

        player.transform.localScale = powerupScale;

        yield return new WaitForSeconds(2f);

        StartCoroutine(ResetScale());

        yield return new WaitForSeconds(0.5f);

        canUsePowerUp = true;

        yield return null;


    }

    public IEnumerator ResetScale()
    {
        float elapsedTime = 0f;
        float waitTime = 2f;

        while (elapsedTime < waitTime)
        {

            // this.GetComponent<PlayerMovement>().enabled = false;

            elapsedTime += Time.deltaTime;

            player.transform.localScale = Vector3.Lerp(powerupScale, startScale, elapsedTime / waitTime * 4);

            yield return null;

        }

        //this.GetComponent<PlayerMovement>().enabled = true;

        player.transform.localScale = startScale;

        yield return new WaitForSeconds(0.5f);

        yield return null;

    }


    public void MiniStarter()///
    {
        StartCoroutine(MiniPowerUp());
    }

    /*public void CallCoroutineDash()
    {
        StartCoroutine(DashPowerUp(playerMesh));
    }
    */


    /* public IEnumerator Slam() //sistema di Slam, viene chiamato nello script PlayerMovement
     {

         if (!playerMovement.changeG) //in base se sono nel tetto o no, applico una verticalForce
             playerMovement.verticalForce.y = -54f;
         else
             playerMovement.verticalForce.y =  54f;
         if (firstGrounded) //se è la prima volta che tocco il terreno allora faccio lo Slam
         {
             inSlam = true;
             yield return null;
         }
     }
     */ //versione originale skianto

    public IEnumerator Slam() //sistema di Slam, viene chiamato nello script PlayerMovement
    {

        if (!playerMovement.changeG) //in base se sono nel tetto o no, applico una verticalForce
            playerMovement.verticalForce.y = -54f;
        else
            playerMovement.verticalForce.y = 54f;
        
            inSlam = true;
            yield return null;
        
    }

    public IEnumerator SlamTime() //se il giocatore fà lo Slem, allora lo attivo (nel momento in cui tocca il ground) e poi lo disttivo
    {
        SlamArea.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        inSlam = false;
        firstGrounded = false;
    }

    /*
    public void FirstGroundCheck()
    {
        if (playerMovement.isGround) //se è grounded allora aumento di 1 nel tempo groundedTime
        {
            groundedTime += 1;
        }
        else //sennò lo blocco a -1
        {
            groundedTime = -1;
        }
        if (groundedTime > 3) //se groundedTime è maggiore di 3 allora lo blocco a 2
        {
            groundedTime = 2;
        }
        if (groundedTime < 2) //se groundedTime è minore di 2 allora attivo firstGrounded
            firstGrounded = true;
    }
    */

    // Update is called once per frame
    void Update()
    {
        //FirstGroundCheck();

        if (playerMovement.isGround)
        {
            if (inSlam)
            {
                StartCoroutine(SlamTime());
            }
            else
                SlamArea.SetActive(false);
        }
      

        if (playerMovement.changeG == false) ///
        {
            //se ChangeG è false allora richiamo questo metodo
            ChangeGravityOFF();
        }
        else
        {
            //se ChangeG è true allora richiamo questo metodo
            ChangeGravityON();
        }


    }
}
