using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpsManager : MonoBehaviour
{
    [SerializeField] Game gameScript;

    [SerializeField] Color playerColor;

    float initialSpeed;

    [SerializeField] MeshRenderer playerMesh;

    [SerializeField] float timePowerUp = 10f;

    [SerializeField] ObjectPooling objectPooling;

    [SerializeField] PlayerHealth playerHealth;

    [SerializeField] PlayerMovement playerMovement;

    public bool isInvincible = false;

    GameObject player;
    public Vector3 powerupScale;
    Vector3 startScale;


    [SerializeField]
    private Image button;

    public int pressTime=1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle") && isInvincible == true)
        {
            other.gameObject.SetActive(false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        

        initialSpeed = objectPooling.speed;

        playerColor = playerMesh.GetComponent<MeshRenderer>().material.color;

        player = this.gameObject;

        startScale = this.gameObject.transform.localScale;


    }


    public void ChangeGravity() //questa funzione permette di cambiare gravità quando tocco un bottone.
    {
        if (playerMovement.canIPress == true)
        {
            if (playerMovement.isGround)
            {

                playerMovement.changeG = !playerMovement.changeG; //allora cambio changeG da true a false o viceversa
                StartCoroutine(switchColor()); //starto la couroutine per cambiare il colore del bottone
                playerMovement.canIPress = false; //setto CaniPress a false

                if (playerMovement.isTetto == false)
                    playerMovement.GoUp();

                if (playerMovement.isTetto == true)
                    playerMovement.GoDown();
            }
            else
            {
                pressTime++; //se cambio gravità mentre sono in aria (quindi il giocatore non tocca il terreno !isGround) allora aumento "pressTime"
                if (pressTime < 1) //se pressTime super 1 allora il giocatore non potrà più cambiare gravitò.
                {
                    if (playerMovement.canIPress == true) //se canIPress è true
                    {
                        playerMovement.changeG = !playerMovement.changeG; //allora cambio changeG da true a false o viceversa
                        StartCoroutine(switchColor()); //starto la couroutine per cambiare il colore del bottone
                        playerMovement.canIPress = false; //setto CaniPress a false
                    }
                    if (playerMovement.isTetto == false)
                        playerMovement.GoUp();

                    if (playerMovement.isTetto == true)
                        playerMovement.GoDown();
                }
            }
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

    public IEnumerator switchColor() //cambio colore del bottone
    {

        button.color = Color.red; //setto il colore a rosso
        yield return new WaitForSeconds(0.5f); //dopo 1 secondo
        button.color = Color.green; //setto il colore a verde
        playerMovement.canIPress = true; //setto canIPress a true, così il bottone si può ripremere.


    }

    IEnumerator InvinciblePowerUp(MeshRenderer meshToFade)
    {
        playerHealth.canBeHit = false;

        objectPooling.speed = objectPooling.maxSpeed; //per adesso il boost è uguale alla max speed dell'object pooling

        Color fadeColor = meshToFade.material.color;

        fadeColor.a = .1f;
        for (int i = 0; i < timePowerUp; i++)
        {
            yield return new WaitForSeconds(.2f);
            meshToFade.material.color = fadeColor;
            yield return new WaitForSeconds(.2f);
            meshToFade.material.color = playerColor;
        }


        objectPooling.speed = initialSpeed;
        playerHealth.canBeHit = true;
        isInvincible = false;

    }

    public void CallCoroutineMini()
    {
        StartCoroutine(MiniPowerUp());
    }

    IEnumerator MiniPowerUp()
    {

        float elapsedTime = 0f;
        float waitTime = 2f;

        while (elapsedTime < waitTime)
        {
            //this.GetComponent<PlayerMovement>().enabled = false;

            elapsedTime += Time.deltaTime;

            player.transform.localScale = Vector3.Lerp(startScale, powerupScale, elapsedTime / waitTime * 4);

            yield return null;

        }

        // this.GetComponent<PlayerMovement>().enabled = true;

        player.transform.localScale = powerupScale;

        yield return new WaitForSeconds(4f);

        StartCoroutine(ResetScale());

        yield return new WaitForSeconds(0.5f);

        yield return null;


    }

    IEnumerator ResetScale()
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


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            isInvincible = true;
            StartCoroutine(InvinciblePowerUp(playerMesh));

        }

        if (playerMovement.isGround) //Se il giocatore torna a terra resetto il pulsante
        {
            pressTime = -1;
        }

        if (pressTime >= 0) //Se il pulsante è stato premuto X volte allora diventerà giallo
        {
            button.color = Color.yellow; //setto il colore a giallo
        }

        if (button.color == Color.yellow & playerMovement.isGround) //se il pulsante è giallo e il giocatore è a terra.. allora resetto il colore a verde
        {
            button.color = Color.green;
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
