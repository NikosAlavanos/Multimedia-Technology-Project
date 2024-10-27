using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    // Shared methods
    public virtual void Hurt()
    {
        Debug.Log($"{name} is hurt!");
    }

    public virtual void Death()
    {
        Debug.Log($"{name} has died!");
        // Additional death logic here
    }
}
