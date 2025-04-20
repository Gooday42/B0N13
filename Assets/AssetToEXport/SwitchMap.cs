using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.TileMaps;
public class SwitchMap : MonoBehaviour
{
    public GameObject[] maps;

    void Start()
    {
       // StartCoroutine(Interpolator());
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {   
            if( GetComponent<EnergyTracker>().energyLevel > 0)
            {
                foreach(GameObject map in maps)
                {
                    map.SetActive(!map.activeSelf);
                }
                GetComponent<EnergyTracker>().consumeEnergy();

            }
           
        }
    }
   
}
