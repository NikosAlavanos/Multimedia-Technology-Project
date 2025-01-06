using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    private Vector2 startPos; // Store both x and y starting positions
    public GameObject cam;
    public Vector2 parallaxEffect; // A Vector2 to specify parallax effects for both x and y axes

    // Start is called before the first frame update
    void Start()
    {
        startPos = new Vector2(transform.position.x, transform.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate distance background moves based on cam movement
        var distanceX = cam.transform.position.x * parallaxEffect.x;
        var distanceY = cam.transform.position.y * parallaxEffect.y;

        transform.position = new Vector3(startPos.x + distanceX, startPos.y + distanceY, transform.position.z);
    }
}