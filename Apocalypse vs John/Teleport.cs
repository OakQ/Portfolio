using UnityEngine;
using System.Collections;
//used to Teleport Famine across the field
public class Teleport : MonoBehaviour {
    GameObject[] teleports; //holds the possible spots where she can teleport
    public ParticleSystem boom; //play this particle system when she teleports
	// Use this for initialization
	void Start () {
        teleports = GameObject.FindGameObjectsWithTag("Teleport"); //get all the possible teleports
	}

    void OnTriggerEnter(Collider col){
        if (col.tag == "Player") //if the player colides with her area
        {
            boom.Play(); //play the particle
            StartCoroutine(teleport()); //teleport away
        }
    }

    IEnumerator teleport()
    {
        yield return new WaitForSeconds(2.1f); //lets the particle system play first
        int rand = Random.Range(0, teleports.Length); //pick a random teleport

        transform.position = teleports[rand].transform.position; //go there
        boom.Stop(); //stop particle
    }
}
