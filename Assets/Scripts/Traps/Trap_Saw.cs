using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.UIElements;

public class Trap_Saw : MonoBehaviour
{
    private Animator anim;
    private SpriteRenderer sr;

    [SerializeField] private float moveSpeed = 3;
    [SerializeField] private float cooldown = 1;
    [SerializeField] private Transform[] wayPoint;

    private Vector3[] wayPointPosition;

    public int wayPointIndex = 1;
    public int moveDirection = 1;
    private bool canMove = true;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        UpdateWaypointsInfo();
        transform.position = wayPointPosition[0]; //the object will snap to the first position in the begining of the game
    }

    private void UpdateWaypointsInfo()
    {
        List<Trap_SawWaypoint>  wayPointList = new List<Trap_SawWaypoint>(GetComponentsInChildren<Trap_SawWaypoint>()); //create a new list as a local variable with the objects that we get components in children

        if (wayPointList.Count != wayPoint.Length)
        {
            wayPoint = new Transform[wayPointList.Count];

            for (int i = 0; i < wayPointList.Count; i++)
            {
                wayPoint[i] = wayPointList[i].transform;
            }

        }

        wayPointPosition = new Vector3[wayPoint.Length]; //creates a new Array with the length of the first Vector(Transform)

        for (int i = 0; i < wayPoint.Length; i++)
        {
            wayPointPosition[i] = wayPoint[i].position;
        }
    }

    private void Update()
    {
        anim.SetBool("active", canMove);

        if (canMove == false)
            return;

        transform.position = Vector2.MoveTowards(transform.position, wayPointPosition[wayPointIndex], moveSpeed * Time.deltaTime); // moves towards the current [osition to the target position, Time.deltaTime is used to make it smooth and frame rate independant

        if (Vector2.Distance(transform.position, wayPointPosition[wayPointIndex]) < .1f)
        {
            if (wayPointIndex == wayPointPosition.Length - 1 || wayPointIndex == 0) //finding the index of the last element || it reached the start
            {
                moveDirection = moveDirection * -1; //it will change its direction when reaching the last element of the array
                StartCoroutine(StopMovement(cooldown));
            }
                

            wayPointIndex = wayPointIndex + moveDirection; //it will go +1 everytime we move foreward
        }
    }

    private IEnumerator StopMovement(float delay)
    {
        canMove = false;

        yield return new WaitForSeconds(delay);

        canMove = true;
        sr.flipX = !sr.flipX;
    }

}
