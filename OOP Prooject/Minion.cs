using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public interface IMinion
{
     void setPoint(Vector3 v);
    void setPartnerFlag(bool f);
    void setPartnerTarget(GameObject go);
    void setMateCoolDown();
    void SetSex(bool b);
}
public interface IImFoodForHuman { }
public interface IImFoodForBunny : IImFoodForHuman { }
public interface IImFoodForHumster : IImFoodForHuman { }
public interface IImFoodForDeer : IImFoodForHuman { }
public interface IImFoodForFox : IImFoodForHuman { }
public interface IImFoodForWolf : IImFoodForHuman { }
public interface IImFoodForLion : IImFoodForHuman { }
public interface IImFoodForBear : IImFoodForHuman { }
public interface IImFoodForPig : IImFoodForHuman { }
public interface IImFoodForRaccoon : IImFoodForHuman { }

abstract public class Minion<TFood,TThis>  : MonoBehaviour 
  
    where TThis:class
{

    [SerializeField] protected GameObject camRef;
    [SerializeField] protected GameObject GMReference;
    [SerializeField] protected float movingCoolDown = 1f;
    [SerializeField] protected float hungerTreshhold = 80f;
    [SerializeField] protected float hungerTick = 1f;
    [SerializeField] protected float deathTreshold = 20f;
    [SerializeField] protected float foodSearchRadius = 5f;
    [SerializeField] protected float coupleSearchRadius = 20f;
    [SerializeField] protected float hunger = 100f;
    [SerializeField] protected float Maxhunger = 100f;
    [SerializeField] protected Transform[] sensors;
    [SerializeField] protected float MatingCooldown=0f;
    [SerializeField] protected float MatingCooldownMax = 100f;

    [Space()]
  
    [Space()]

    [Header("Layer Masks")]
    [SerializeField] protected LayerMask minion;
    [SerializeField] protected LayerMask grass;
    [SerializeField] protected LayerMask border;
    [Space()]

    protected bool foundedFood=false;
    protected bool foundedCouple = false;
    protected Collider2D[] colliders;

    [SerializeField]protected Vector3 targetPoint;
    
    protected GameObject target;
   
     

    public bool sex =true; 

    // Start is called before the first frame update
    void Start()
    {
        
         InvokeRepeating(nameof(NewIteration), 1f, movingCoolDown);
    }

    // Update is called once per frame
    void Update()
    {
       // NewIteration();
    }
    protected virtual List<GameObject> LookForTarget<TTarget>()
    {

        List<GameObject> targets = new List<GameObject>();         
        colliders = Physics2D.OverlapAreaAll(
            new Vector2(transform.position.x-foodSearchRadius,transform.position.y-foodSearchRadius),
            new Vector2(transform.position.x + foodSearchRadius, transform.position.y + foodSearchRadius)
            );
        
        for(int i=0; i<colliders.Length; i++)
        {
            if (colliders[i].gameObject.GetComponent<TTarget>() != null && colliders[i].gameObject!=gameObject)
            {
                targets.Add(colliders[i].gameObject);
            }

        }
        return targets;
       
        
    }
    protected virtual GameObject ChooseTareget(List<GameObject> targets)
    {
        if (targets == null) return null;
        else
        {
            GameObject t=null;
            float min = float.MaxValue;
            foreach (GameObject target in targets)
            {
                float delta = Mathf.Abs(target.gameObject.transform.position.x - this.transform.position.x) +
                              Mathf.Abs(target.gameObject.transform.position.y - this.transform.position.y);
                if (delta < min)
                {
                    t = target;
                    min = delta;
                }
            }
            target = t;
            return t;
        }
    }
    protected bool GoForTarget()
    {
        float DeltaX = Mathf.Abs(transform.position.x - target.transform.position.x);
        float DeltaY = Mathf.Abs(transform.position.y - target.transform.position.y);

        if (DeltaX == 0 && DeltaY == 0)
        {
            return true;
        }
        else if (DeltaX == 0)
        {
            if (Mathf.Abs(transform.position.y - target.transform.position.y + 1) > Mathf.Abs(transform.position.y - target.transform.position.y - 1))
            {
                MoveDOWN();
            }
            else MoveUP();
        }
        else
        {
            if (Mathf.Abs(transform.position.x - target.transform.position.x + 1) > Mathf.Abs(transform.position.x - target.transform.position.x - 1))
            {
                MoveLEFT();
            }
            else MoveRIGHT();
        }
        return false;
    }
    protected bool GoForPoint()
    {
        if (targetPoint == Vector3.zero) return false;
        float DeltaX = Mathf.Abs(transform.position.x - targetPoint.x);
        float DeltaY = Mathf.Abs(transform.position.y - targetPoint.y);

        if (DeltaX == 0 && DeltaY == 0)
        {
            return true;
        }
        else if (DeltaX == 0)
        {
            if (Mathf.Abs(transform.position.y - targetPoint.y + 1) > Mathf.Abs(transform.position.y - targetPoint.y - 1))
            {
                MoveDOWN();
            }
            else MoveUP();
        }
        else
        {
            if (Mathf.Abs(transform.position.x - targetPoint.x + 1) > Mathf.Abs(transform.position.x - targetPoint.x - 1))
            {
                MoveLEFT();
            }
            else MoveRIGHT();
        }
        return false;
    }

     
    protected virtual void NewIteration()
    {
        if (hunger < deathTreshold)
        {
            Destroy(gameObject);
            
        }

        hunger -= hungerTick;
        MatingCooldown--;
        if (hunger <= hungerTreshhold)
        {     
            
            if (ChooseTareget(LookForTarget<TFood>())!=null)
            {
                if (GoForTarget())
                    EAT();

            }
            else
            {

                Move(DecideTheWay());
               
            }
        }
        else
        {
            if (MatingCooldown <= 0)
            {
                if (sex)
                {
                    if (foundedCouple )
                    {
                        if (target != null)
                        {
                            if (GoForPoint() && target.transform.position == transform.position)
                            {
                                Mate();

                            }
                        }else Move(DecideTheWay());


                    }
                    else
                    {
                        if (ChooseTareget(LookForTarget<TThis>()) != null)
                        {

                            float midX = Mathf.Round((target.transform.position.x + transform.position.x)/2);
                            float midY = Mathf.Round((target.transform.position.y + transform.position.y)/2);
                            Vector3 point = new Vector3(midX, midY, 0);
                            targetPoint = point;
                            foundedCouple = true;
                            //TThis t = target.GetComponent<TThis>();

                            //t.setPoint(point);
                            target.SendMessage("setPoint", point);

                            //t.setPartnerFlag(true);
                            target.SendMessage("setPartnerFlag",true);

                            //t.setPartnerTarget(gameObject);
                            target.SendMessage("setPartnerTarget", gameObject);

                        }else Move(DecideTheWay());
                    }

                }
                else
                {
                    if (foundedCouple)
                    {
                        GoForPoint();

                    }
                    else Move(DecideTheWay());

                }
            }else Move(DecideTheWay());

        }
    }

    protected void EAT()
    {
        if(target.transform.position!=transform.position)
        {
            Debug.Log("target pos != my pos WTF");
        }
        Destroy(target);
        hunger = Maxhunger;
        foundedFood = false;
        target = null;
        targetPoint = Vector3.zero;

    }

    protected int  DecideTheWay()
    {
        List<int> Neighbours = new List<int>();
        int i=1;
        
        foreach (var sensor in sensors)
        {           
            colliders = Physics2D.OverlapPointAll(sensor.position);
            
            if (colliders.Length == 0) //empty space
            {
                Neighbours.Add(i);                             
            }                     
            
            i++;
        }
        
        int theWay=Random.Range(0, Neighbours.Count+1);
        if (Neighbours.Count > theWay)
            return Neighbours[theWay];
        else return 0;
    }

    public void MoveUP()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + 1, 0);
    }
    public void MoveDOWN()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y - 1, 0);
    }
    public void MoveRIGHT()
    {
        transform.position = new Vector3(transform.position.x + 1, transform.position.y, 0);
    }
    public void MoveLEFT()
    {
        transform.position = new Vector3(transform.position.x - 1, transform.position.y, 0);
    }
    public void Move(int dir)
    {
        
        switch (dir)
        {
            //1-up
            //2-right
            //3-down
            //4-left 
            case 1:
                {     
                    transform.position = new Vector3(transform.position.x, transform.position.y + 1, 0);             
                    break;
                }
            case 2:
                {                   
                    transform.position = new Vector3(transform.position.x+1, transform.position.y , 0);                 
                    break;
                }
            case 3:
                {                  
                    transform.position = new Vector3(transform.position.x, transform.position.y - 1, 0);                  
                    break;
                }
            case 4:
                {                   
                    transform.position = new Vector3(transform.position.x-1, transform.position.y , 0);                   
                    break;
                }
            case 0:
                {
                     
                    break;
                }

            default:
                break;
        }
        
    } 

    
    public void Mate()
    {
       
        //TThis t=target.GetComponent<TThis>() ;

        target.SendMessage("setMateCoolDown");
        //t.setMateCoolDown();

        //t.setPartnerFlag(false);
        target.SendMessage("setPartnerFlag",false);

        //t.setPoint(Vector3.zero);
        target.SendMessage("setPoint", Vector3.zero);

        //t.setPartnerTarget(null);
        target.SendMessage("setPartnerTarget", target);

        MatingCooldown = MatingCooldownMax;
        foundedCouple = false;
        target = null;
        targetPoint = Vector3.zero;
        GameObject newborn = Instantiate(gameObject);
        //newborn.GetComponent<TThis>().SetSex(GMReference.GetComponent<GM>().DecideSex());
        newborn.SendMessage("SetSex", GMReference.GetComponent<GM>().DecideSex());


    }
     
    protected bool GetSex()
    {
        return sex;

    }

    protected void DeliverHunger()
    {
        camRef.GetComponent<CameraController>().value = hunger.ToString();

    }

    protected void setMateCoolDown()
    {
        MatingCooldown = MatingCooldownMax;
    }

    protected void setPartnerFlag(bool f)
    {
        foundedCouple = f;
    }

    protected void setPartnerTarget(GameObject go)
    {
        if (go == gameObject)
        {
            target = null;
        }
        else 
        { target = go; }
    }

    protected void setPoint(Vector3 v)
    {
        
        targetPoint = v;
        
    }

    protected void SetSex(bool b)
    {
        sex = b;
    }


}
