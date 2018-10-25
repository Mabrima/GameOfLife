using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour {

    bool alive = true;
    bool nextAlive = true;
    bool wasAlive = false;
    public Material colorAlive;
    public Material colorRecentlyBorn;
    public Material colorDead;
    public Material colorRecentlyDead;


    // Use this for initialization
    void Start () {
        
	}

    public void UpdateColor()
    {
        if (alive && wasAlive)
        {
            GetComponent<MeshRenderer>().material = colorAlive;
        }
        else if (alive && !wasAlive)
        {
            GetComponent<MeshRenderer>().material = colorRecentlyBorn;
        }
        else if(!alive && wasAlive)
        {
            GetComponent<MeshRenderer>().material = colorRecentlyDead;
        }
        else
        {
            GetComponent<MeshRenderer>().material = colorDead;
        }
    }

    public void UpdateState()
    {
        wasAlive = alive;
        alive = nextAlive;
        UpdateColor();
    }

    public int CheckAlive()
    {
        if (alive)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }

    public void SetAlive(bool set)
    {
        alive = set;
    }

    public void SetNextAlive(bool set)
    {
        nextAlive = set;
    }
	
}
