using UnityEngine;
using System.Collections;

public class PlaneControls : MonoBehaviour {
	float vert;
	float horz;
	float currentZ;
	float currentY;

	public float speed;
	// Use this for initialization
	void Start () {
		currentY = transform.position.y;
		currentZ = transform.position.z;
	}
	// Update is called once per frame
	void Update () {
		vert = Input.GetAxis ("Vertical");
		horz = Input.GetAxis("Horizontal");
		if (horz > 0.01f) {
			Debug.Log ("Right");
			//transform.position = Vector3.MoveTowards (transform.position, new Vector3 (transform.position.x, transform.position.y, currentZ - 1000.0f), speed * Time.deltaTime);
            Vector3 dir = new Vector3 (transform.position.x, transform.position.y, -1000.0f) - transform.position;
            Vector3 movement = dir.normalized * speed * Time.deltaTime;
            if (movement.magnitude > dir.magnitude) movement = dir;
                GetComponent<CharacterController>().Move(movement);
		} 
		else if (horz < -0.01f) {
			Debug.Log ("Left");
            Vector3 dir = new Vector3(transform.position.x, transform.position.y, 1000.0f) - transform.position;
            Vector3 movement = dir.normalized * speed * Time.deltaTime;
            if (movement.magnitude > dir.magnitude) movement = dir;
            GetComponent<CharacterController>().Move(movement);
		}
		else{
			currentZ = transform.position.z;
		}

		if (vert > 0.01f) {		
			Debug.Log ("Up");
            Vector3 dir = new Vector3(transform.position.x, 1000.0f, transform.position.z) - transform.position;
            Vector3 movement = dir.normalized * speed * Time.deltaTime;
            if (movement.magnitude > dir.magnitude) movement = dir;
            GetComponent<CharacterController>().Move(movement);
		} 
		else if (vert < -0.01f) {
			Debug.Log ("Down");
            Vector3 dir = new Vector3(transform.position.x, -1000.0f, transform.position.z) - transform.position;
            Vector3 movement = dir.normalized * speed * Time.deltaTime;
            if (movement.magnitude > dir.magnitude) movement = dir;
            GetComponent<CharacterController>().Move(movement);
		}
		else{
			currentY = transform.position.z;
		}

	}
}
