using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum Achievetype
{ //all type breakdown
    Drive,
    Totalmine,
    MeterReach,
    Unlockpast,
    farming

}
public class Achieveobj : MonoBehaviour
{
    public SwapLang swaplang;
    public  Achievetype achievetype;
    // Start is called before the first frame update
    [Header("Achievement Obj")]
    public new string name;
    public AchieveSO[] achievesteps;

    [Header("Achievement UI")]

    public int steps;
    public int value;
    public TextMeshProUGUI Achie1_Rank_Text;
    public TextMeshProUGUI Achie1_Description_Text;
    public TextMeshProUGUI Achie1_Progress_Text;
    public TextMeshProUGUI Achie1_Reward_Text;

    

    public GameObject Achie_1_Compplete; //panel


    [Header("progress")]
    public Button achbtn; // true when done
    public Slider progs;

    public GameObject[] rewardobj;

    public bool f;

    bool farming;
    public void Checkachie()
    {   // if you not seperate breakdown as individuel obj
        //get playerpref sign to counter_achie_1 using swtich(enum)
        //check steps



        switch (achievetype)
        {
            case Achievetype.Drive:
                steps = PlayerPrefs.GetInt("Achievement_1");
                value = PlayerPrefs.GetInt("DrilledDepth");


                break;
            case Achievetype.MeterReach:
                steps = PlayerPrefs.GetInt("Achievement_2");
                value = PlayerPrefs.GetInt("MeterReach");
                break;
            case Achievetype.Totalmine:
                steps = PlayerPrefs.GetInt("Achievement_3");
                value = PlayerPrefs.GetInt("Totalmine");
                break;
            case Achievetype.Unlockpast:
                steps = PlayerPrefs.GetInt("Achievement_4");
                value = PlayerPrefs.GetInt("Unlock");
                break;
            case Achievetype.farming:
                steps = 0;
                value = PlayerPrefs.GetInt("GemFarming");
                farming = true;
                break;
        }

        //steps gets, read step array
        //if step > array means steps complete
        // int ORstep = steps;
        Achie_1_Compplete.SetActive(false);
        //   Debug.Log(name + " " +value+" "+ steps + " " + achievesteps.Length);
        bool c = false;
        f = false;
        if (steps >= achievesteps.Length)
        {

            steps--;
            c = true;
            Achie_1_Compplete.SetActive(true);
            //  f = false;
        }

        //detect value

        if (value >= achievesteps[steps].max)
        {
            if (!farming)
                value = achievesteps[steps].max;

            f = true;
            achbtn.gameObject.SetActive(true);
            if (c)
            {
                //if all done dont show btn
                achbtn.gameObject.SetActive(false);
                f = false;
            }
        }


        if (swaplang.Fields == null)
        {
            Achie1_Description_Text.text = name + ": " + (steps + 1);
            Achie1_Rank_Text.text = achievesteps[steps].Description;
        }
        else
        {
           // Debug.Log(swaplang.Fields[name]);
            //   Debug.Log(swaplang.Fields[achievesteps[steps].name]);
            Achie1_Description_Text.text = swaplang.Fields[name] + " : " + (steps + 1);
            // 
            Achie1_Rank_Text.text = swaplang.Fields[achievesteps[steps].name];

        }


        float x = value - achievesteps[steps].min;
        float y = achievesteps[steps].max - achievesteps[steps].min;
        int per = (int)(x * 100/ y);

        if (per > 100)
            per = 100;

        Achie1_Progress_Text.text = per.ToString()+" %";
        progs.minValue = achievesteps[steps].min;
        progs.maxValue = achievesteps[steps].max;
        progs.value = value;

        if (achievesteps[steps].gems)
        {
            //reward gems      achievesteps[steps].reward
            rewardobj[1].SetActive(true);
            rewardobj[0].SetActive(false);
        }
        else
        {
            rewardobj[0].SetActive(true);
            rewardobj[1].SetActive(false);
        }

        Achie1_Reward_Text.text = achievesteps[steps].reward.ToString();

    }




    public void getreward()
    {

        if (achievesteps[steps].gems)
        {
            //reward gems     
            int  Gems = PlayerPrefs.GetInt("Gems") + achievesteps[steps].reward;
            if (Gems > 9999)
                Gems = 9999;
            PlayerPrefs.SetInt("Gems", Gems);

        }
        else
        {
            int coin = PlayerPrefs.GetInt("Gold") + achievesteps[steps].reward;
            if (coin > 999999)
                coin = 999999;
            PlayerPrefs.SetInt("Gold", coin);
        }



        achbtn.gameObject.SetActive(false);
        steps++;
        //rewrite steps
        switch (achievetype)
        {
            case Achievetype.Drive:
                PlayerPrefs.SetInt("Achievement_1", steps);

                break;
            case Achievetype.MeterReach:
                PlayerPrefs.SetInt("Achievement_2", steps);
                break;
            case Achievetype.Totalmine:
                PlayerPrefs.SetInt("Achievement_3", steps);
                break;
            case Achievetype.Unlockpast:
                PlayerPrefs.SetInt("Achievement_4", steps);
                break;
            case Achievetype.farming:
                value = PlayerPrefs.GetInt("GemFarming");
                value -= (int)progs.maxValue;
                PlayerPrefs.SetInt("GemFarming", value);
                break;
        }
        Checkachie();
    }


    // Update is called once per frame

}
