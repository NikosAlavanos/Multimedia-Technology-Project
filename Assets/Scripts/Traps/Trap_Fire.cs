using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_Fire : MonoBehaviour
{
    [SerializeField] private float offDuration;
    [SerializeField] private Trap_FireButton fireButton; //making sure there is always a fire button
    private Animator anim;
    private CapsuleCollider2D fireCollider;
    private bool isActive;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        fireCollider = GetComponent<CapsuleCollider2D>();
    }

    private void Start()
    {
        if (fireButton == null)
            Debug.Log("you don;t have fire button on" + gameObject.name + "!");

        SetFire(true);
    }

    public void SwitchOffFire()
    {
        if (isActive == false)
            return;
        
        StartCoroutine(FireCouroutine());
    }

    private IEnumerator FireCouroutine()
    {
        SetFire(false);

        yield return new WaitForSeconds(offDuration);

        SetFire(true);
    }

    private void SetFire(bool active)
    {
        anim.SetBool("active", active);
        fireCollider.enabled = active;
        isActive = true;
    }
}
