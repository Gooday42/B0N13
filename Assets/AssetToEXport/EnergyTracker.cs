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
    private int maxLevel;

    void Start()
    {
        maxLevel = energyLevel;
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
        gameObject.GetComponent<PlayerMovement>().enabled = false;
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

<<<<<<< HEAD
    public void AddEnergy() {
        if (energyLevel == maxLevel) {
=======
    public void AddEnergy()
    {
        if(energyLevel>=energyBars.Count)
        {
>>>>>>> 701c81e207bea90a7044cad4b2f4f37773caca01
            return;
        }
        energyLevel++;
        energyBars[energyLevel].SetActive(true);
<<<<<<< HEAD
    }

=======
        
        
        //animation to turn on
    }
>>>>>>> 701c81e207bea90a7044cad4b2f4f37773caca01
}
