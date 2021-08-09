using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class PlayerController : MonoBehaviour
{
    public GameObject buble;
    private float horizontalInput;
    public float verticalInput;
    public float movementSpeed = 10f;
    public float turningSpeed = 60;
    public Rigidbody rbPlayer;
    private GameManager gameManager;
    public ParticleSystem bubleParticle;
    public bool isMoving;
    public AudioSource playerAudio;
    public AudioClip shootSound;
    public AudioClip hitByEnemy;
    public ParticleSystem deathPlayerParticle;


    // Start is called before the first frame update
    void Start()
    {
            gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
            rbPlayer = GetComponent<Rigidbody>();
            playerAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame


    void Update()
    {
        Movement();
        Shooting();
        Bubles();
    
    }

    public void Movement()
    {
        if(gameManager.isGameActive == true)
        {
            horizontalInput = Input.GetAxis("Horizontal");
            verticalInput = Input.GetAxis("Vertical");

            transform.Rotate(Vector3.up, horizontalInput * Time.deltaTime * turningSpeed);

            rbPlayer.AddForce(transform.forward * verticalInput * Time.deltaTime * movementSpeed);
        }
        
    }

    private void Shooting()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(buble, transform.position, transform.rotation);
            playerAudio.PlayOneShot(shootSound, 1.0f);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            playerAudio.PlayOneShot(hitByEnemy, 1.0f);
            deathPlayerParticle.Play();
            Destroy(collision.gameObject);
            gameManager.GameOver();                  
        }
        //if (collision.gameObject.CompareTag("Wall"))
        //{
          //  Vector3 awayFromPlayer = transform.position - collision.gameObject.transform.position;
            //rbPlayer.AddForce(awayFromPlayer * powerUpStrenght, ForceMode.Impulse);
        //}

    }
    private void Bubles()
    {
        if (verticalInput == 0)
        {
            bubleParticle.Play();
        }
    }
}
