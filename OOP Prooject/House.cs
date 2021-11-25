using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{

    [SerializeField] List<GameObject> owners = new List<GameObject>();
    public int foodCapacity = 10;
    public int currentFood = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void addOwner(GameObject gObj)
    {
        owners.Add(gObj);
    }
    public void deleteOwner(GameObject ogj)
    {
        owners.Remove(ogj);
    }
    public void addFood()
    {
        if (currentFood < foodCapacity)
            currentFood++;
    }
    public bool foodIsNeeded()
    {
        if (foodCapacity > currentFood)
            return true;
        else return false;
    }



}
