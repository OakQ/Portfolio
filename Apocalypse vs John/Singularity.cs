using UnityEngine;
using System.Collections;

public class Singularity : MonoBehaviour {
    public GameObject target; //In this case, War
    public GameObject boom; //particle system that plays on impact
    GameObject fireBoom; 
    GameObject missle;
    bool noMore; //the sing. doesn't need anymore ammo
    int size; //how big is it
	// Use this for initialization
	void Start () {
        noMore = false; //we can handle more
        size = 1; //we start small
	}
	
	// Update is called once per frame
	void Update () {      
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Missle") //if a missle runs into the singularity
        {
            if (!noMore) //and it can still fit more
            {
                missle = col.gameObject;
                missle.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0); //stop it
                missle.transform.position = transform.position; //move it to the center
                missle.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;  //freeze it    
                noMore = true; //we only need one
                
            }
            else
            {
                Destroy(col.gameObject); //destory all other missles
                missle.GetComponent<ParticleSystem>().startSize += size; //increase size of ours
                size++; // increment size
                if (missle.GetComponent<ParticleSystem>().startSize >= 50) //if the size is big enough
                {
                    GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None; //let it drop
                    missle.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None; //along with our giant fireball
                }
            }
        }
        if (col.tag == "War") //if it drops on War
        {
            missle.GetComponent<Rigidbody>().AddExplosionForce(100.0f, transform.position, 100.0f); //make it big
            fireBoom = (GameObject)Instantiate(boom, transform.position, transform.rotation); //make it loud
            Destroy(missle);  //destroy it        
            Destroy(fireBoom, 5.0f); //destroy the particles after 5 seconds
            Destroy(col.gameObject); //destory War
            StartCoroutine(load()); //load the win Screen
        }

    }

    IEnumerator load()
    {
        yield return new WaitForSeconds(6); //but only after we see War get decimated
        Application.LoadLevel(5);
    }
}
