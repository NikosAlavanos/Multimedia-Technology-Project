using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Player")]
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform respawnPoint;
    [SerializeField] private float respawnDelay;
    public Player player; //referance to the Player

    [Header("Fruits Managment")]
    public bool fruitsAreRandom;
    public int fruitsCollected;
    public int totalFruits;

    [Header("Checkpoints")]
    public bool canReactivate;

    [Header("Traps")]
    public GameObject arrowPrefab;


    private void Awake()
    {
        if(instance == null) //we dont have an instance yet
            instance = this;
        else
            Destroy(gameObject); //making sure we only have one GameManager

        instance = this;
    }

    private void Start()
    {
        CollectFruitsInfo();
    }

    private void CollectFruitsInfo()
    {
        Fruit[] allFruits = FindObjectsOfType<Fruit>(); //in 2023 FindObjectsByType<Fruit>(FindObjectsSortMode.None); //it will find all the oobjects that have the components of a Fruit and put them in the Array
        totalFruits = allFruits.Length;
    }

    public void UpdateRespawnPosition(Transform newRespawnPoint) => respawnPoint = newRespawnPoint;

    public void RespawnPlayer() => StartCoroutine(RespawnCourutine()); //because Courutine is private we call it from this public method

    private IEnumerator RespawnCourutine() //respawn player with the Courutine
    {
        yield return new WaitForSeconds(respawnDelay);

        GameObject newPlayer = Instantiate(playerPrefab, respawnPoint.position, Quaternion.identity);
        player = newPlayer.GetComponent<Player>();
    }

    public void AddFruit() => fruitsCollected++; // adding fruits in the total
    public bool FruitsHaveRandomLook() => fruitsAreRandom;
    
    public void CreateObject(GameObject prefab, Transform target, float delay = 0)
    {
        StartCoroutine(CreateObjectCouroutine(prefab, target, delay));
    }


    private IEnumerator CreateObjectCouroutine(GameObject prefab, Transform target, float delay)
    {
        Vector3 newPosition = target.position; //otherwise we might destroy the object with the transform and not be able to use it, thats why we store it to a Vector 3 position
        
        yield return new WaitForSeconds(delay);

        GameObject newObject = Instantiate(prefab, newPosition, Quaternion.identity);
    }
}
