using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kids : MonoBehaviour {


	public Transform startMarker;
	public Transform endMarker;
	public Transform endMarker1;
	public Transform endMarker2;
	public Transform posVuelta;

	public float speed = 1.0F;
	private float startTime;
	private float journeyLength;
	public bool mover = false;
	public bool volver = false;
	int x;
	// Use this for initialization
	void Start () {
		startTime = Time.time;

		x = Random.Range(0, 2);
		//Debug.Log("x "+x);
		if (x == 0)
		{
			endMarker = endMarker1;
		}
		else
			endMarker = endMarker2;

		journeyLength = Vector3.Distance(startMarker.position, endMarker.position);
	}
	
	// Update is called once per frame
	void Update () {

		if (mover)
		{
            volver = false;
            Move();
		}

		if (volver)
		{
            //Debug.Log("Entra");
			Volver();
		}
	}

	public void Move()
    {
        //GetComponent<Animator>().SetTrigger("Run");

        float distCovered = (Time.time - startTime) * speed;
		float fracJourney = distCovered / journeyLength;
		transform.position = Vector3.Lerp(startMarker.position, endMarker.position, fracJourney);
        GetComponent<SpriteRenderer>().flipX = false;

        if (x == 0 && transform.position.x > (endMarker.position.x - 0.01))
		{
			GetComponent<SpriteRenderer>().flipX = true;
			GetComponent<Animator>().SetTrigger("Castillo");
			mover = false;
		}
		else if(transform.position.x > (endMarker.position.x - 0.01))
		{
			mover = false;
			GetComponent<Animator>().SetTrigger("Castillo");
		}

	}

	public void Volver()
	{
        GetComponent<Animator>().SetTrigger("Llorar");

        float distCovered = (Time.time - startTime) * speed * 30;
		float fracJourney = distCovered / journeyLength;
		transform.position = Vector3.Lerp(endMarker.position, posVuelta.position, fracJourney);
		GetComponent<SpriteRenderer>().flipX = true;


		if (x == 0 && transform.position.x < (posVuelta.position.x + 0.01))
		{
            volver = false;
            GetComponent<Animator>().SetTrigger("Run");

            GetComponent<SpriteRenderer>().enabled = false;
		}
		else if (transform.position.x < (posVuelta.position.x + 0.01))
		{
			volver = false;
            
			GetComponent<Animator>().SetTrigger("Run");
			GetComponent<SpriteRenderer>().enabled = false;
			
		}

	}

	public void ReiniciaTiempo() {
		startTime = Time.time;

	}

}
