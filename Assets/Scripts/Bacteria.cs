using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bacteria : MonoBehaviour
{
    public float zSpawnRange = 12f;
    public float xSpawnRange = 12f;
    public float speed = 1f;
    public GameObject player;
    public bool isDead = false;
    private GameManager gameManager;
    public int pointValue = 0;
    public AudioSource enemyAudio;
    public AudioClip deathSound;
    public ParticleSystem deathParticle;
    private Collider bacteriaCollider;
    private SkinnedMeshRenderer bacteriaRenderer;
    public GameObject rendererObject;
    //public float minSpawnDistance = 5f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        //transform.position = RandomSpawnPos();

        RandomSpawnPos();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        enemyAudio = GetComponent<AudioSource>();
        bacteriaCollider = GetComponent<Collider>();
        bacteriaRenderer = rendererObject.GetComponent<SkinnedMeshRenderer>();
        //if ((RandomSpawnPos() - player.transform.position).magnitude <= minSpawnDistance)
        //{
            //transform.position = RandomSpawnPos();
        //}
    }

    // Update is called once per frame
    void Update()
    {
        if(isDead == false && gameManager.isGameActive == true)
        {
            transform.LookAt(player.transform.position);
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Projectile") && isDead == false)
        {
            bacteriaRenderer.enabled = false;
            bacteriaCollider.enabled = false;
            isDead = true;
            deathParticle.Play();
            enemyAudio.PlayOneShot(deathSound, 1.0f);
            Destroy(other.gameObject);
            gameManager.UpdateScore(pointValue);
            StartCoroutine(WaitForDeath());          
        }
    }

    IEnumerator WaitForDeath()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }

    //public Vector3 RandomSpawnPos()
    //{
        //float randomXPos = Random.Range(-xSpawnRange, xSpawnRange);
        //float randomZPos = Random.Range(-zSpawnRange, zSpawnRange);
        //return new Vector3(randomXPos, transform.position.y, randomZPos );
    //}
    public void RandomSpawnPos()
    {
        Vector3 spawnPositon = new Vector3(Random.Range(-xSpawnRange, xSpawnRange), transform.position.y, Random.Range(-zSpawnRange, zSpawnRange));
        while (Vector3.Distance(spawnPositon, player.transform.position) < 8)
        {
            spawnPositon = new Vector3(Random.Range(-xSpawnRange, xSpawnRange), transform.position.y, Random.Range(-zSpawnRange, zSpawnRange));
        }
        transform.position = spawnPositon;
       
    }
        

}
