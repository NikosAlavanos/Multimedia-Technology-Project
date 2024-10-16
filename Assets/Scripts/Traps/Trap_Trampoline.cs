using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Trap_Trampoline : MonoBehaviour
{
    private Animator anim; //we use protected so it can be available in this class as well to the class that inherits from it

    [SerializeField] private float pushPower;
    [SerializeField] private float duration = .5f;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();

        if (player != null)
        {
            player.Push(transform.up *pushPower,duration);
            anim.SetTrigger("activate");
        }
    }
}
