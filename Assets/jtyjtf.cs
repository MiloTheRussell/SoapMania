using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class humbadie : MonoBehaviour
{
    public float horizontalinput;
    public float speed = 20.0f;

    public GameObject[] projectilePrefabs;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Moves my player
        horizontalinput = Input.GetAxis("Mouse X");
        transform.Translate(Vector3.right * horizontalinput * Time.deltaTime * speed);

        //Shoots a projectile
        if (Input.GetMouseButtonDown(0))
        {
            int foodIndex = Random.Range(0, projectilePrefabs.Length);
            Instantiate(projectilePrefabs[foodIndex], transform.position, projectilePrefabs[foodIndex].transform.rotation);
        }
    }
}
