using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class War_Fire : MonoBehaviour {
    public GameObject missle; //the missle that War shoots
    static GameObject singularity; //invisible object far above War
    static bool resetTarget; //tells War if he should switch targets
    public float cd; //used to set and reset cooldown
    float cooldown; //cooldown on his missle
    int b; //random number that decides which building he'll attack
    public bool fired = false; //if he's fired or not
    static GameObject target; //War's intended target
    GameObject building; //a building 
    GameObject[] buildings;//all of the buildings
    public Text damageT; //text that displays damage done to the city
    public static int damage; //city's damage
	// Use this for initialization
	void Start () {
        cooldown = cd; //set cooldown
        buildings = GameObject.FindGameObjectsWithTag("Building"); //set buildings
        b = Random.Range(0, buildings.Length); //random number based on number of buildings
        building = buildings[b]; //sets the building
        target = building; //sets the target
        resetTarget = true; //lets war reset his target later
        singularity = GameObject.FindGameObjectWithTag("Singularity");
        damage = 0; //start with no damage done
        damageT.text = "Damage: 0%";
	}
	
	// Update is called once per frame
	void Update () {
        if (damage >= 100) //if damage reaches 100%
        {
            damageT.text = "Damage: 100%";
            Application.LoadLevel(6); //load lose screen
        }

        if (cooldown > 0) //if cooldown hasn't run down
        {
            cooldown -= Time.deltaTime; //decrement cooldown
            fired = false; //War hasn't fired yet
        }
        else
        {
            //create a missle in front of War
            GameObject firedMissle = (GameObject)Instantiate(missle, new Vector3(transform.position.x, transform.position.y + 10.0f, transform.position.z - 5.0f) + transform.forward, transform.rotation);       
            firedMissle.GetComponent<Rigidbody>().velocity = (target.transform.position - transform.position).normalized * 200; //launch missle towards building
            cooldown = cd; //reset cooldown
            fired = true; //we have fired
            b = Random.Range(0, buildings.Length); //pick a new random number
            building = buildings[b]; //pick a new building

            if (resetTarget) //if War can still reset
            {
                target = building; //set his target to be the building
                if (damage < 100)
                {
                    damage += Random.Range(3, 8); //increase damage done with each missle launch
                    damageT.text = "Damage: " + damage + "%";
                }
            }
        }

        if (!fired) //if he hasn't fired yet
        {
            //roatae War towards the building
            Vector3 lookPos = building.transform.position - transform.position;
            lookPos.y = 0;
            Quaternion rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime);
        }
	}

    public void generatorsActive() //called when all the generators have been activated by the player. Stops War from firing at buildings
    {
        target = singularity; //sets his target to be the singularity
        resetTarget = false; //he can't change targets
        cd = 1.0f; //make him fire faster (in a panic)
    }
}
