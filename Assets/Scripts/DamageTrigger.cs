using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision) //this method will be called everytime something Collids with the trigger
    {
        //if (collision.tag == "Player")
        //    GameManager.instance.player.KnockBack(); 
        Player player = collision.gameObject.GetComponent<Player>();     //local variabl of the Player, player will have a value only if we find component of <Player> that entered the trigger on the collision
        if (player != null)
            player?.KnockBack(transform.position.x); //same as above but more independant // player? == if (player != null)
    }
}
