using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : Minion<IImFoodForHuman,Human>
{
    private GameObject partner;
    private GameObject home;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    
    protected  override void NewIteration()
    {
        if (hunger < deathTreshold)
        {
            Destroy(gameObject);
            
        }
        hunger -= hungerTick;
        MatingCooldown--;
        if (home != null)
        {
            if(hunger> hungerTreshhold)
            {
                 
            }
            else
            {

            }

        }
        else
        {
            
        }
    }

    public bool GetSex()
    {
        return sex;
    }
}
