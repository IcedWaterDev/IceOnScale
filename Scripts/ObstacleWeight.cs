using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleWeight : MonoBehaviour
{
    private float weightFromObstacle;
    private ScaleManager scaleManager;
    private bool hasCollided;

    private void Start()
    {
        weightFromObstacle = this.GetComponent<Rigidbody2D>().mass;
        scaleManager = GameObject.Find("Scale Manager").GetComponent<ScaleManager>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!hasCollided)
        {
            hasCollided = true;
            scaleManager.weightOnScale += weightFromObstacle;
        }
    }
}
