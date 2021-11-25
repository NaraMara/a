using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fox : Minion<IImFoodForFox, Fox> ,IImFoodForLion,IImFoodForBear
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
