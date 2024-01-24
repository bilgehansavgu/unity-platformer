using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class enemyPatrol : MonoBehaviour
{
	public GameObject PointA;
	public GameObject PointB;
	private Rigidbody2D rb;
	private Transform currentPoint;
	public float speed;
    void Start()
    {
	    rb = GetComponent<Rigidbody2D>();
	    currentPoint = PointB.transform;
    }
    void Update()
    {
	    Vector2 point = currentPoint.position - transform.position;
	    if (currentPoint == PointB.transform)
	    {
		    rb.velocity = new Vector2(speed, 0);
	    } 
	    else
	    {
		    rb.velocity = new Vector2(-speed, 0);
	    }

	    if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == PointB.transform)
	    {
		    currentPoint = PointA.transform;
	    }
	    if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == PointA.transform)
	    {
		    currentPoint = PointB.transform;
	    }
    }

    private void flip()
    {
	    Vector3 localScale = transform.localScale;
	    localScale.x *= -1;
	    transform.localScale = localScale;

    }

    private void OnDrawGizmos()
    {
	    Gizmos.DrawWireSphere(PointA.transform.position, 0.5f);
	    Gizmos.DrawWireSphere(PointB.transform.position, 0.5f);
	    Gizmos.DrawLine(PointA.transform.position, PointB.transform.position);
	    
    }
}
