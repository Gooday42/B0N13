using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchMap : MonoBehaviour
{
    public GameObject[] maps;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {   
            if( GetComponent<EnergyTracker>().energyLevel > 0)
            {
                foreach (GameObject map in maps)
                {
                    map.SetActive(!map.activeSelf);
                }            
                GetComponent<EnergyTracker>().consumeEnergy();

            }
           
        }
    }
}
