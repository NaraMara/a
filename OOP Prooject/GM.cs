     
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GM : MonoBehaviour
{
    [Header("Start parameters")]
    public int numberOfStartMinions = 2000;
    public int numberOfStartGrass = 2000;
    public int numberOfAddictionalGrass=1000;
    public int newGrassCooldown = 20 ;

    [Header("Prefabs")]

    [SerializeField] GameObject CarrotPrefab;
    [SerializeField] GameObject WheetPrefab;
    [SerializeField] GameObject ApplePrefab;

    [SerializeField] GameObject BunnyPrefab;
    [SerializeField] GameObject HumsterPrefab;
    [SerializeField] GameObject DeerPrefab;
    [SerializeField] GameObject FoxPrefab;
    [SerializeField] GameObject WolfPrefab;
    [SerializeField] GameObject LionPrefab;
    [SerializeField] GameObject BearPrefab;
    [SerializeField] GameObject PigPrefab;
    [SerializeField] GameObject RaccoonPrefab;

    private List<GameObject> minionsList= new List<GameObject>();

    public bool[,] grassMatrix = new bool[1000, 1000];
    float elapsed = 0f;
    

    // Start is called before the first frame update
    public  void GrowSomeGrass(int quantity)
    {

        
        for (int i = 0; i < quantity; i++)
        {
            Vector3 grassPos = CalculateNewPosition();
            GameObject newborn = null;
            grassPos = CalculateNewPosition();
            int type = Random.Range(0, 3);
            switch (type)
            {
                case 0:
                    {
                         newborn = Instantiate(CarrotPrefab);
                        newborn.transform.position = grassPos;
                        break;
                    }
                case 1:
                    {
                         newborn = Instantiate(WheetPrefab);
                        newborn.transform.position = grassPos;
                        break;
                    }
                case 2:
                    {
                         newborn = Instantiate(ApplePrefab);
                        newborn.transform.position = grassPos;
                        break;
                    }
             

                default:
                    break;
            }
        }
        
    }
    public void MakeSomeBabies(int quantity)
    {
        for (int i = 0; i < numberOfStartMinions; i++) //summon minions 
        {
            GameObject newborn=null;
            Vector3 minionPos = CalculateNewPosition();
            int type = Random.Range(0, 9);
            switch (type)
            {
                case 0:
                    {
                        newborn = Instantiate(BunnyPrefab);
                        newborn.transform.position = minionPos;
                        newborn.GetComponent<Bunny>().sex = DecideSex();
                        break;
                    }
                case 1:
                    {
                        newborn = Instantiate(HumsterPrefab);
                        newborn.transform.position = minionPos;
                        newborn.GetComponent<Humster>().sex = DecideSex();
                        break;
                    }
                case 2:
                    {
                        newborn = Instantiate(DeerPrefab);
                        newborn.transform.position = minionPos;
                        newborn.GetComponent<Deer>().sex = DecideSex();
                        break;
                    }
                case 3:
                    {
                        newborn = Instantiate(FoxPrefab);
                        newborn.transform.position = minionPos;
                        newborn.GetComponent<Fox>().sex = DecideSex();
                        break;
                    }
                case 4:
                    {
                        newborn = Instantiate(WolfPrefab);
                        newborn.transform.position = minionPos;
                        newborn.GetComponent<Wolf>().sex = DecideSex();
                        break;
                    }
                case 5:
                    {
                        newborn = Instantiate(LionPrefab);
                        newborn.transform.position = minionPos;
                        newborn.GetComponent<Lion>().sex = DecideSex();
                        break;
                    }
                case 6:
                    {
                        newborn = Instantiate(BearPrefab);
                        newborn.transform.position = minionPos;
                        newborn.GetComponent<Bear>().sex = DecideSex();
                        break;
                    }
                case 7:
                    {
                        newborn = Instantiate(PigPrefab);
                        newborn.transform.position = minionPos;
                        newborn.GetComponent<Pig>().sex = DecideSex();
                        break;
                    }
                case 8:
                    {
                        newborn = Instantiate(RaccoonPrefab);
                        newborn.transform.position = minionPos;
                        newborn.GetComponent<Raccoon>().sex = DecideSex();
                        break;
                    }
                
                default:
                    break;
            }

                      
            minionsList.Add(newborn);


        }
    }
    void Start()
    {
        
        GrowSomeGrass(numberOfStartGrass);
        MakeSomeBabies(numberOfStartMinions);

    }

    
    private Vector3 CalculateNewPosition()
    {
        return new Vector3(Random.Range(0, 1000), Random.Range(0, 1000), 0); 
    }
    public bool GetMatrixValue(int x, int y)
    {
        return grassMatrix[x, y];
    }
    
    public void SetMatrixValue(int x , int y )
    {
        grassMatrix[x, y] = true;
    }

    public void ClearMatrixValue(int x, int y )
    {
        grassMatrix[x, y]=false;
    }

    public bool DecideSex()
    {
        int i =Random.Range(0, 2);
        if (i == 1)
            return true;        
        else return false ;

    }


    // Update is called once per frame
    void Update()
    {
         
        //elapsed += Time.deltaTime;
        //if (elapsed >= newGrassCooldown)
        //{
            //elapsed %= newGrassCooldown;
            //GrowSomeGrass(numberOfAddictionalGrass);
        //}
    }
}
