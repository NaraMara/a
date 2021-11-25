using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrot : Grass,IImFoodForBear
{
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(Spread), startCoolDown, spreadCoolDown);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
