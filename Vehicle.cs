using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Ability
{
    public Abilitytype abilitytype;
    public int points =0;
    public Ability()
    {  
        reset();
    }

    public void reset()
    {
        points = 0;
        abilitytype = Abilitytype.Gas; // defeact
    }
}

    public enum Abilitytype
    {
       Gas, 
       Combo,
       criticledmg,
       lucky,
       rocket

   }

[System.Serializable]
public class Vehicle
{
    // Start is called before the first frame update

    public VehicleP[] vparts;
    public List<Ability> abilities; // keeps point from parts build

    // i dont thing HP neccessary
    public float MAXHP = 3500, HP = 3500;

    public float MaxFuel = 350  , fuel = 350;
    public float drilldamage = 12f;
    public float speed = 12;

    public float rockettime = 0;

    public int[] abilitycount;
    public Vehicle()
    {
        abilities = new List<Ability>();
        abilitycount = new int[5];

    }

    // use this in script use after recreate class every signle time;
    public void loadparts(GameObject e)
    {
        //get parts
        vparts = e.GetComponentsInChildren<VehicleP>();
        setvalue();
    }


    public void setvalue()
    {  //set up value from level
        for (int i = 0; i < vparts.Length; i++)
        {
            int level = 1;
            int upg = vparts[i].upgradelevel -1;
            if (upg >= 4)
            { upg = 3; }

            level = vparts[i].baselevel + upg;
            switch (vparts[i].vparts)
            {
                case VehicleP.Vparts.Drill:
                    drilldamage = 2f + (1.4f * level);
                    //max 10 in 10
                    // 1 in 2
                    break;
                case VehicleP.Vparts.Body:
                    MAXHP = HP = 1000 + (250 * level);
                    // max in 2400 in 10
                    // 1 in 400
                    break;
                case VehicleP.Vparts.GasTank:
                    MaxFuel = fuel = 100 + (25 * level);
                    // max 10 in 500
                    //1 in 150
                    break;
                case VehicleP.Vparts.Wheel:
                    speed = 6f + (1 * level);
                    // max in 16
                    // 1 in 
                    break;

            }

        }


    }


    public int[] returnabilty()
    {

         abilitycount = new int[5];

        for (int i = 0; i < 4; i++)
        {

            if (vparts[i].upgradelevel == 5)

            {

           
            switch (vparts[i].abt)
            {
                case Abilitytype.Gas:
                    abilitycount[0]++;
                    break;
                case Abilitytype.Combo:
                    abilitycount[1]++;
                    break;
                case Abilitytype.criticledmg:
                    abilitycount[2]++;
                    break;
                case Abilitytype.lucky:
                    abilitycount[3]++;
                    break;
                case Abilitytype.rocket:
                    abilitycount[4]++;
                    break;
            }
            }

        }
      


   

        return abilitycount;
    }

    public void fuelbouns()
    {
     
        float gas = 1;
        if (abilitycount[0] > 1)
        {
            gas += 0.1f;
            if (abilitycount[0] == 4)
            {
                gas += 0.1f;
            }
        }
        MaxFuel = MaxFuel * gas;
        fuel = fuel * gas;

    }

    public int returncrit()
    {
        int crt = 0;

        int c = abilitycount[2];

        if (c > 1)
        {
            crt = 1;
            if (c == 4)
            {
                crt = 3;
            }
        }

        return crt;
    }

    public int returnluck()
    {
        int crt = 0;

        int c = abilitycount[3];

        if (c > 1)
        {
            crt = 1;
            if (c == 4)
            {
                crt = 3;
            }
        }

    

        return crt;
    }

    public float combotime()
    {

        float t = 0;

        int c = abilitycount[1];

        if (c > 1)
        {
            t = 0.5f;
            if (c == 4)
            {
                t = 1f;
            }
        }



        return t;
    }

    public float supertime()
    {

        float t = 0;

        int c = abilitycount[4];

        if (c > 1)
        {
            t = 250;
            if (c == 4)
            {
                t = 500;
            }
        }



        return t;
    }

    public void setrocket()
    {

        float lv = PlayerPrefs.GetInt("Rocket");
        if (lv == 0)
        {

            rockettime = 0 ;
        }
        else
        {


            rockettime = 3 + (1 * PlayerPrefs.GetInt("Rocket"));
        }

    }





    public void fuelchange(float f)
    {
        fuel += f;
        if (fuel < 0)
        {
            fuel = 0;
        }
    }
    public void fuelrecover()
    {
        float w = (MaxFuel * 0.2f) + 10;
        if (w > 50)
            w = 50;

        fuel += w;
        if (fuel > MaxFuel)
        {
            fuel = MaxFuel;
        }

    }

    public void hpchange(float f)
    {
        HP += f;

    }


    public void reset()
    {
        fuel = MaxFuel;
        HP = MAXHP;
    }


}
