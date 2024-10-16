using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FruitType { Apple,Banana, Cherry, Kiwi, Melon, Orange, Pineapple, Strawberry}

public class Fruit : MonoBehaviour //could be the same for coins
{
    [SerializeField] private FruitType fruitType;
    [SerializeField] private GameObject pickupVFX;

    private GameManager gameManager;
    private Animator anim;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>(); //InChildren, because Animator is a Child of Fruit gameObject
    }

    private void Start()
    {
        gameManager = GameManager.instance; // we do this in the start and not in the Awake(), because GameManager is assigned in the Awake() and it might Awaker() of this script will be called before the GameManager and might create problems
        SetRandomLookIfNeeded();
    }

    private void SetRandomLookIfNeeded()
    {
        if (gameManager.FruitsHaveRandomLook() == false)
        {
            UpdateFruitVisuals();
            return;
        }
            

        int randomIndex = Random.Range(0, 8); //random integer tha min inclussive and max exclussive => random number from 0-7
        anim.SetFloat("fruitIndex", randomIndex);
    }

    private void UpdateFruitVisuals() => anim.SetFloat("fruitIndex", (int)fruitType); //it will take a fruitType turn it into int and give this value in the <Animator>()

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();

        if (player != null)
        {
            gameManager.AddFruit();
            Destroy(gameObject); // destroys the fruit after picking it up

            GameObject newFX = Instantiate(pickupVFX, transform.position,Quaternion.identity); //create object on runtime, adding transform.position make it so the effect happens where the fruit is, Qiaternion.identy => dont change the identy

            //Destroy(newFX, .5f); // destroying newFX after 5 secs
        }

    }
}
