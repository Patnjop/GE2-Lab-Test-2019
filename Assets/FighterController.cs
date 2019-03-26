using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterController : MonoBehaviour
{
    public List<GameObject> bases = new List<GameObject>();
    public int tiberium = 7;
    // Start is called before the first frame update
    void Awake()
    {
        int rnd = Random.Range(0, 3);
        if (bases[rnd].transform.position == this.transform.position)
        {
            if (rnd < 3)
            { rnd++; }
            else if(rnd == 3)
            { rnd--; }
        }
        this.GetComponent<Seek>().targetGameObject = bases[rnd];
    }
}
