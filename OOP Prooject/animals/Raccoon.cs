using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raccoon : Minion<IImFoodForRaccoon, Raccoon>,IImFoodForFox
{
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(NewIteration), 1f, movingCoolDown);
    }

    // Update is called once per frame
    void Update()
    {

    }
   
}
