using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaselTrigger : MonoBehaviour
{
    public GameObject[] weasels;
    public GameObject bridge;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        bridge.SetActive(false);
        int i = 0;
        foreach(var w in weasels)
        {
            weasels[i].gameObject.SetActive(true);
            weasels[i].gameObject.GetComponent<WeaselScript>().canFunction = true;
            i++;
        }
    }

}
