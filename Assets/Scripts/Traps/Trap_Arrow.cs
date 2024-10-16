using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_Arrow : Trap_Trampoline //instead of MonoBehaviour we use Trap_Trampoline to inherite behaviour
{
    [Header("Additional info")]
    [SerializeField] private float cooldown;
    [SerializeField] private bool rotatingRight;
    [SerializeField] private float rotationSpeed = 120;
    private int direction = -1;
    [Space]
    [SerializeField] private float scaleUpSpeed = 10;
    [SerializeField] private Vector3 targetScale;

    private void Start()
    {
        transform.localScale = new Vector3(.3f, .3f, .3f); //changing scale of the transform with the new Vector
    }

    private void Update()
    {
        HandleScaleUp();
        HandleRotation();
    }

    private void HandleScaleUp()
    {
        if (transform.localScale.x < targetScale.x)
            transform.localScale = Vector3.Lerp(transform.localScale, targetScale, scaleUpSpeed * Time.deltaTime);
    }

    private void HandleRotation()
    {
        direction = rotatingRight ? -1 : 1;
        transform.Rotate(0, 0, (rotationSpeed * direction) * Time.deltaTime);
    }

    private void DestoryMe()
    {
        GameObject arrowPrefab = GameManager.instance.arrowPrefab;
        GameManager.instance.CreateObject(arrowPrefab, transform, cooldown);

        Destroy(gameObject);
    }
}
