using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lion : Minion<IImFoodForLion, Lion>
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
