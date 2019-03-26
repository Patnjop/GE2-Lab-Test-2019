using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Base : MonoBehaviour
{
    public float tiberium = 0;

    public TextMeshPro text;

    public GameObject fighterPrefab;

    bool tiberiumLoaded;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Renderer r in GetComponentsInChildren<Renderer>())
        {
            r.material.color = Color.HSVToRGB(Random.Range(0.0f, 1.0f), 1, 1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (tiberiumLoaded == false)
        {
            StartCoroutine("tiberiumTimer");
            tiberiumLoaded = true;
        }
        text.text = "" + tiberium;
    }

    IEnumerator tiberiumTimer()
    {
        yield return new WaitForSeconds(1f);
        if (tiberium >= 10)
        {
            GameObject fighter = GameObject.Instantiate(fighterPrefab, this.transform.position, Quaternion.identity);
            fighter.transform.SetParent(this.transform);
            foreach (Renderer q in GetComponentsInChildren<Renderer>())
            {
                q.material.color = this.GetComponent<Renderer>().material.color;
            }
            tiberium -= 7;
        }
        else
        {
            tiberium++;
        }
        tiberiumLoaded = false;
    }
}