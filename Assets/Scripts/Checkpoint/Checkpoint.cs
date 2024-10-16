using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private Animator anim => GetComponent<Animator>(); //the issue is that every time you call an Animator it will try to get a component, but since its a checkpoint and it gets it only once its ok
    private bool active;

    [SerializeField] private bool canBeReactivated;

    public void Start()
    {
        canBeReactivated = GameManager.instance.canReactivate;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (active && canBeReactivated == false) //checks if it is active now
            return;

        Player player = collision.GetComponent<Player>(); //GetComponent of the <Player>

        if (player != null) //player entered the trigger
            ActivateCheckpoint();
    }

    private void ActivateCheckpoint()
    {
        active = true;
        anim.SetTrigger("activate");
        GameManager.instance.UpdateRespawnPosition(transform); //we pass checkpoint it self
    }
}
