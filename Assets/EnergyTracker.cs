using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyTracker : MonoBehaviour
{
    public GameObject energyBar;
    public GameObject energyBarContainer;
    public List<GameObject> energyBars = new List<GameObject>();
    public List<GameObject> TurnThisOnWhenOff = new List<GameObject>();

    public int energyLevel = 1;

    void Start()
    {
        for(int i = 0; i < energyLevel; i++)
        {
            GameObject intance = Instantiate(energyBar, energyBarContainer.transform);
            intance.transform.localScale = new Vector3((1f/energyLevel),1,1);
            intance.transform.localPosition = new Vector3(i * (1f/(energyLevel-1)), 0, 0);
            energyBars.Add(intance);
        }
    }

     void Update()
    {
        //Debug.Log("Energy level: " + energyLevel);
        if(energyLevel<=0)
        {
            Debug.Log("Energy level is 0");
            energyLevel = 0;
            TurnOff();
        }
       
    }

    void TurnOff()
    {
        gameObject.GetComponent<Movement>().enabled = false;
        gameObject.GetComponent<SpriteRenderer>().color = Color.black;
        foreach(GameObject map in TurnThisOnWhenOff)
        {
            map.SetActive(true);
        }
        
        //animation to turn off
    }
    public void consumeEnergy()
    {
        if(energyLevel<=0)
        {
            return;
        }  
        energyLevel--;
        energyBars[energyLevel].SetActive(false);
        
        //animation to turn off
    }
}
