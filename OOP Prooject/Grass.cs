using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : MonoBehaviour,IImFoodForPig,IImFoodForRaccoon,IImFoodForBunny,IImFoodForHumster,IImFoodForDeer
{

    [SerializeField] protected GM GMReference;
    [SerializeField] protected float spreadCoolDown ;
    [SerializeField] protected float startCoolDown;
    [SerializeField] protected int spreadRadius=3;
    
    // Start is called before the first frame update
    void Start()
    {
        

    }


    protected void Spread()
    {
        Vector3 newPos = CalcNewPosition();
        
        GMReference.SetMatrixValue((int)newPos.x, (int)newPos.y);

        GameObject newborn = Instantiate(gameObject);
        newborn.transform.position = newPos;
        
    }
 
    protected Vector3 CalcNewPosition()
    {
        float leftBorder = transform.position.x - spreadRadius;
        float rightBorder = transform.position.x + spreadRadius;
        float downBorder = transform.position.y - spreadRadius;
        float UpBorder = transform.position.y + spreadRadius;
        if(leftBorder<0)
        {
            leftBorder = 0;
        }
        if (downBorder < 0)
        {
            downBorder = 0;
        }
        if (rightBorder > 999)
        {
            rightBorder = 999;
        }
        if (UpBorder > 999)
        {
            UpBorder = 999;
        }
        return new Vector3(Random.Range((int)leftBorder, (int)rightBorder +1), Random.Range((int)downBorder, (int)UpBorder+1), 0);
    }

    // Update is called once per frame     
  
}
