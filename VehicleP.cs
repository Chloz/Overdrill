using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleP : MonoBehaviour
{

    public enum Vparts {
        Drill,
        Body,
        Wheel,
        GasTank

    }

    public Vparts vparts;
    public int baselevel;
    public int upgradelevel;

    [Header("Ability")]
    public Abilitytype abt;

    [Header("Cost")]

    [Header("Coin")]
    public int[] coincost; // start with unlock
    [Header("Gem")]
    public int[] gemcost;


    public void setlevel(int lv)
    {
        upgradelevel = lv;
    }

    public void upgrade()
    {
        upgradelevel++;
    }

    public bool purchause()
    {
        // 0 is unlock // 1 2 3  is upgrade 
        int money = 0;
        if (gemcost[upgradelevel] != 0)
        {

            //pay gem
            money = PlayerPrefs.GetInt("Gems");
            if (money >= gemcost[upgradelevel])
            {
                money -= gemcost[upgradelevel];
                PlayerPrefs.SetInt("Gems", money);
                return true;
            }
            return false;
          
        }
        else
        {
            //pay coin
             money = PlayerPrefs.GetInt("Gold");
            if ( money >= coincost[upgradelevel])
            {
                money -= coincost[upgradelevel];
                PlayerPrefs.SetInt("Gold", money);
                return true;
            }
            return false;
        }

    }


    public bool IsMax()
    {
        if (upgradelevel >=5)
        {

            return true;

        }


        return false;
    }


    public bool Isgem()
    {  
        if (gemcost[upgradelevel] != 0)
        {
          
            return true;

        }


        return false;
    }

    public int returncoincost()
    {
      

        return coincost[upgradelevel];
    }
    public int returngemcost()
    {


        return gemcost[upgradelevel];
    }
}
