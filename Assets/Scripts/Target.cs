using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Rigidbody targetRb;
    private GameManager gameManager;

    private float maxSpeed = 12.0f;
    private float minSpeed = 16.0f;
    private float maxTorque = 10.0f;
    private float xRange = 4.0f;
    private float spawnPosY = -2.0f;

    public int pointValue;
    public ParticleSystem explosionEffect;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        targetRb = GetComponent<Rigidbody>();
        targetRb.AddForce( RandomForce(), ForceMode.Impulse); //Random force controls upwards movement speed
        targetRb.AddTorque( RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse); // Random Torque for the x, y and z axis of a Vector3, and then a Force mode
        transform.position = SpawnPosition(); // Random spawnpos, z = 0.
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if (gameManager.isGameActive) 
        {
            Destroy(gameObject);
            Instantiate(explosionEffect, transform.position, explosionEffect.transform.rotation);
            gameManager.UpDateScore(pointValue);
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        if (!gameObject.CompareTag("Bad"))
        {
            gameManager.GameOver();
        }
    }

    Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(minSpeed, maxSpeed);
    }

    float RandomTorque()
    {
        return Random.Range(-maxTorque, maxTorque);
    }

    Vector3 SpawnPosition()
    {
        return new Vector3(Random.Range(-xRange, xRange), spawnPosY);
    }
}
