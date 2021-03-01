using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startDATA : MonoBehaviour
{
    public GameObject croix_party2;
    public GameObject croix_party;
    public GameObject createparty;
    public GameObject joinparty;

    public void joinwin()
    {
        joinparty.SetActive(true);
    }

    public void Startwin()
    {
        createparty.SetActive(true);
    }
    public void croixfunction()
    {
        createparty.SetActive(false);
    }
    public void croixfunction2()
    {
        joinparty.SetActive(false);
    }


    /*// Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }*/

}
