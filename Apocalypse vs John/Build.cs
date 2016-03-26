using UnityEngine;
using System.Collections;

public class Build : MonoBehaviour {

    public GameObject Block; //the block that the building will be made of
    public GameObject Window; //windows so they look like skyscrapers
    GameObject create;
    GameObject destruct;
    bool window; //whether we build a window or a wall
    public int stories; //determines how tall the building will be
	
	void Start () {

        stories = Random.Range(stories/2, stories); //set how many stories the building will be
        for (float floor = 0; floor < stories; floor++)//create the building floor by floor
        {
            if (floor > 0)
            { //this places a block on top of the center block if we're doing more than 1 floor     
                destruct = (GameObject)Instantiate(Block, transform.position + new Vector3(0.0f, floor * GetComponent<Renderer>().bounds.size.y, 0.0f), transform.rotation);
                destruct.transform.parent = this.transform; //sets the parent of the new block to the main block

            }
            Building(new Vector3(GetComponent<Renderer>().bounds.size.x, floor * GetComponent<Renderer>().bounds.size.y, 0.0f)); //two new blocks on either side on the x axis
            Building(new Vector3(0.0f, floor * GetComponent<Renderer>().bounds.size.y, GetComponent<Renderer>().bounds.size.z)); //two new blocks on either side on the z axis
            Building(new Vector3(GetComponent<Renderer>().bounds.size.x, floor * GetComponent<Renderer>().bounds.size.y, GetComponent<Renderer>().bounds.size.z)); //two new blocks on the x and z axis
            Building(new Vector3(-GetComponent<Renderer>().bounds.size.x, floor * GetComponent<Renderer>().bounds.size.y, GetComponent<Renderer>().bounds.size.z)); //two new blocks on the -x and z axis
        }
        
	}

    void Building(Vector3 s) //places down the blocks
    {      
        create = Window; //line it up with windows
        destruct = (GameObject)Instantiate(create, transform.position + s, transform.rotation); //create new block next to center block
        destruct.transform.parent = this.transform; //set parent
        s = new Vector3(s.x * -1.0f, s.y, s.z *-1.0f); //adjust vector to negative values (not y)
        destruct = (GameObject)Instantiate(create, transform.position + s, transform.rotation); //create block on other side of center block
        destruct.transform.parent = this.transform;

    }


}
