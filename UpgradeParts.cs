using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UpgradeParts : MonoBehaviour
{

    // Start is called before the first frame update
    // base slider is chilren of upgrade slider
    public GameObject upgradepanel;
    public SwapLang swapLang;
    [Header("Vehicle parts Select")]
    public GameObject[] DrillP;
    public GameObject[] BodyP;
    public GameObject[] EngineP;
    public GameObject[] Fuelp;

    [Header("Vehicle parts realtime")]
    public GameObject[] DrillP2;
    public GameObject[] BodyP2;

    [Header("Part Select")]
    public int partsselct; // select 4 group 0 ~ 3


    [Header("Parts index")]
    public int iDrill;
    public int iBody;
    public int iEngine;
    public int iFuel;


    [Header("Vehicle Status")]
    public Slider Drill;
    public Slider Body;
    public Slider Engine;
    public Slider Fuel;

    [Header("Value")]
    public float[] values;
    public float[] values2;

    [Header("Parts Level")]
    public Slider Upgradeslider;
    public Slider Baseslider;

    public GameObject lockicon;
    public GameObject unlockbtn;
    public GameObject confrimbtn;
    public GameObject Upgradebtn;
    public GameObject upgradeeffect;
    public Transform pparent;
    public GameObject[] imags;
    public TextMeshProUGUI costtext;
    public GameObject MAXLV;

    public GameObject[] buttons;

    public TextMeshProUGUI partna;
    public GameObject partnat;

    [Header("Vehicle")]
    public Vehicle vehicle;

    public Lab l;
    [Header("Passive Ability")]
    public Transform parent;
    public List<GameObject> abilitylist;
    public List<GameObject> sortlist;

    public GameObject[] abicon;

    [Header("Other")]
    public float globeltime; // 

    public GameObject CloneTemp;

    public static bool IsHaveUsed = false;

    bool openupg;

    private GameObject clone;


    public AudioSource buy;
    private void Awake()
    {
#if UNITY_ANDROID || UNITY_IPHONE
   
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Application.targetFrameRate = 60;
#endif
    }

    private void Start()
    {
        
        // Screen.SetResolution(720, 1280, true);

        // int newuser = PlayerPrefs.GetInt("New");
        int newuser = PlayerPrefs.GetInt("New");




        if (newuser == 0)
        {
            PlayerPrefs.SetInt("New", 1);
            // 0 = lock, 1 = unlock, 2 3 4 add star, 5 get aby, maximum to 5 upgrade level
            PlayerPrefs.SetString("DrillLevel", "10000");
            PlayerPrefs.SetString("BodyLevel", "10000");
            PlayerPrefs.SetString("EngineLevel", "10000");
            PlayerPrefs.SetString("FuelLevel", "10000");

            // parts index where is confrim use from 0 to 4 
            PlayerPrefs.SetInt("PartsDrill", 0);
            PlayerPrefs.SetInt("PartsBody", 0);
            PlayerPrefs.SetInt("PartsEngine", 0);
            PlayerPrefs.SetInt("PartsFuel", 0);

            PlayerPrefs.SetInt("Gold", 0);
            PlayerPrefs.SetInt("Gems", 0);

            PlayerPrefs.SetInt("Achievement_1", 0);
            PlayerPrefs.SetInt("Achievement_2", 0);
            PlayerPrefs.SetInt("Achievement_3", 0);
            PlayerPrefs.SetInt("Achievement_4", 0);

            PlayerPrefs.SetInt("DrilledDepth",0);
            PlayerPrefs.SetInt("MeterReach",0);
            PlayerPrefs.SetInt("Totalmine",0);
            PlayerPrefs.SetInt("Unlock",0);

            PlayerPrefs.SetInt("CoinsMachine",0);
            PlayerPrefs.SetInt("Rocket",0);
            PlayerPrefs.SetInt("GemFarming", 0);



        
        }
        else
        {
   

            //      PlayerPrefs.SetInt("Gems", 1000);
            //        PlayerPrefs.SetInt("Rocket", 0);
        }

        iDrill = PlayerPrefs.GetInt("PartsDrill");
        iBody = PlayerPrefs.GetInt("PartsBody");
        iEngine = PlayerPrefs.GetInt("PartsEngine");
        iFuel = PlayerPrefs.GetInt("PartsFuel");


        vehicle = new Vehicle();
        values = new float[4];

        values2 = new float[4];
        if (!IsHaveUsed)
        {
            clone = Instantiate(CloneTemp, transform.position, transform.rotation) as GameObject;
            clone.tag = "Globel";
            clone.GetComponent<UpgradeParts>().vehicle = vehicle;
            IsHaveUsed = true;
            //      Debug.Log("this log obviously show create");
            DontDestroyOnLoad(clone.transform.gameObject);
        }
        else
        {
            clone = GameObject.FindGameObjectWithTag("Globel");
            globeltime = clone.GetComponent<UpgradeParts>().globeltime;
        }
        vehicledata();
        selectmodel(0);


    }



    

    void checkmax()

    {
        int value = 0;

        string   leveltext = PlayerPrefs.GetString("DrillLevel");
        string c = "";
        int currentlevel = 0;
        for (int i = 0; i < 5; i++)
        {
            c = leveltext[i].ToString();
             currentlevel = int.Parse(c);
            if (currentlevel == 5)
            {
                value++;
            }
        }


        leveltext = PlayerPrefs.GetString("BodyLevel");
         c = "";
         currentlevel = 0;
        for (int i = 0; i < 5; i++)
        {
            c = leveltext[i].ToString();
            currentlevel = int.Parse(c);
            if (currentlevel == 5)
            {
                value++;
            }
        }



         leveltext = PlayerPrefs.GetString("EngineLevel");
         c = "";
         currentlevel = 0;
        for (int i = 0; i < 5; i++)
        {
            c = leveltext[i].ToString();
            currentlevel = int.Parse(c);
            if (currentlevel == 5)
            {
                value++;
            }
        }

         leveltext = PlayerPrefs.GetString("FuelLevel");
         c = "";
         currentlevel = 0;
        for (int i = 0; i < 5; i++)
        {
            c = leveltext[i].ToString();
            currentlevel = int.Parse(c);
            if (currentlevel ==5)
            {
                value++;
            }
        }

       PlayerPrefs.SetInt("Unlock",value);

    }


    // Update is called once per frame
    void FixedUpdate()
    {
        globeltime += Time.deltaTime;
        if (!gameObject.CompareTag( "Globel"))
        {
            if (openupg == true)
            {
                updateui();
            }

        }
      
    }


 





    public void openupgrade(bool o)
    {
        openupg = o;
    }

    public void reopen()
    {
        iDrill = PlayerPrefs.GetInt("PartsDrill");
        iBody = PlayerPrefs.GetInt("PartsBody");
        iEngine = PlayerPrefs.GetInt("PartsEngine");
        iFuel = PlayerPrefs.GetInt("PartsFuel");
       // partsselct = 0;

        selectmodel(0);
        updatemodel();
        resetstatues();
        openupgrade(true);


    }


    public void  vehicledata()
    {


        VehicleP[] newvp = new VehicleP[4];

      //  iDrill = PlayerPrefs.GetInt("PartsDrill");
      //  iBody = PlayerPrefs.GetInt("PartsBody");
      //  iEngine = PlayerPrefs.GetInt("PartsEngine");
      //  iFuel = PlayerPrefs.GetInt("PartsFuel");

        newvp[0] = DrillP[PlayerPrefs.GetInt("PartsDrill")].GetComponent<VehicleP>();
        string c = PlayerPrefs.GetString("DrillLevel")[PlayerPrefs.GetInt("PartsDrill")].ToString();
        newvp[0].upgradelevel = int.Parse(c);
        newvp[1] = BodyP[PlayerPrefs.GetInt("PartsBody")].GetComponent<VehicleP>();
        c = PlayerPrefs.GetString("BodyLevel")[PlayerPrefs.GetInt("PartsBody")].ToString();
        newvp[1].upgradelevel = int.Parse(c);
        newvp[2] = EngineP[PlayerPrefs.GetInt("PartsEngine")].GetComponent<VehicleP>();
        c = PlayerPrefs.GetString("EngineLevel")[PlayerPrefs.GetInt("PartsEngine")].ToString();
        newvp[2].upgradelevel = int.Parse(c);
        newvp[3] = Fuelp[PlayerPrefs.GetInt("PartsFuel")].GetComponent<VehicleP>();
        c = PlayerPrefs.GetString("FuelLevel")[PlayerPrefs.GetInt("PartsFuel")].ToString();
        newvp[3].upgradelevel = int.Parse(c);




        vehicle.vparts = newvp;
        vehicle.setvalue();
        vehicle.returnabilty();
        vehicle.fuelbouns();
        clone.GetComponent<UpgradeParts>().vehicle=  vehicle;
        l.setrocket(vehicle.supertime());
        checkmax();


    }

    void loadlevel()
    {

    }


    public void updatemodel()
    {
        // disactive all object
        //reactive confrim obj
        for (int i = 0; i < 5; i++)
        {
       
            BodyP2[i].SetActive(false);
            if (i == PlayerPrefs.GetInt("PartsBody"))
            {
                BodyP2[i].SetActive(true);
                BodyP2[i].GetComponent<Partsholder>().showdrills();
                BodyP2[i].GetComponent<Partsholder>().showtanks();
            }
        }

        for (int i = 0; i < 5; i++)
        {
            DrillP[i].SetActive(false);
            DrillP2[i].SetActive(false);
            //PlayerPrefs.GetInt("PartsDrill")
            if (i == iDrill)
            {
                DrillP[i].SetActive(true);
                //      DrillP2[i].SetActive(true);
            }

        }

        for (int i = 0; i < 5; i++)
        {
            BodyP[i].SetActive(false);
            //PlayerPrefs.GetInt("PartsBody")
            if (i == iBody)
            {
                BodyP[i].SetActive(true);
            }
        }

        for (int i = 0; i < 5; i++)
        {
            EngineP[i].SetActive(false);
            //PlayerPrefs.GetInt("PartsEngine")
            if (i == iEngine)
            {
                EngineP[i].SetActive(true);
            }
        }

        for (int i = 0; i < 5; i++)
        {
            Fuelp[i].SetActive(false);
            //PlayerPrefs.GetInt("PartsFuel")
            if (i == iFuel)
            {
                Fuelp[i].SetActive(true);
            }
        }
        /*
        switch (partsselct)
        {
            case 0:
                //drill []
                for (int i = 0; i < 5; i++)
                {
                    DrillP[i].SetActive(false);
                    DrillP2[i].SetActive(false);

                    if (i == PlayerPrefs.GetInt("PartsDrill"))
                    {
                        DrillP[i].SetActive(true);
                        //      DrillP2[i].SetActive(true);
                    }

                }


                // currentlevel= 
                break;
            case 1:
                // body
                for (int i = 0; i < 5; i++)
                {
                    BodyP[i].SetActive(false);
                    if (i == PlayerPrefs.GetInt("PartsBody"))
                    {
                        BodyP[i].SetActive(true);
                              }
                }

                break;
            case 2:
                //engine
                for (int i = 0; i < 5; i++)
                {
                    EngineP[i].SetActive(false);
                    if (i == PlayerPrefs.GetInt("PartsEngine"))
                    {
                        EngineP[i].SetActive(true);
                    }
                }
                break;
            case 3:
           
                for (int i = 0; i < 5; i++)
                {
                    Fuelp[i].SetActive(false);
                    if (i == PlayerPrefs.GetInt("PartsFuel"))
                    {
                        Fuelp[i].SetActive(true);
                    }
                }

                //fuel
                break;

        }

    
     */
      



    }

    public void selectparts(int i )
    {
        
        
        partsselct = i;
        selectmodel(0);
    }

    public void selectmodel(int dir)
    {  // -1 = left , 1 = right btn, 0 no change
        string leveltext = "";
        string c = "";
        int currentlevel = 0;
        int v = 0;
        int rlv = 0;
        switch (partsselct)
        {
            case 0:
                //drill []
                DrillP[iDrill].SetActive(false);
                iDrill+= dir;
                if (iDrill > 4)
                {
                    iDrill = 0;
                }
                else if (iDrill < 0)
                {
                    iDrill = 4;
                }
                DrillP[iDrill].SetActive(true);
                if (swapLang.Fields != null)
                { partna.text = swapLang.Fields["Damage:"]; }
                else
                { partna.text = "Damage:"; }
                leveltext = PlayerPrefs.GetString("DrillLevel");

                c = leveltext[iDrill].ToString();
                 currentlevel =int.Parse(c);
                lockicon.SetActive(false);

                //update upgrade level

                Baseslider.value = DrillP[iDrill].GetComponent<VehicleP>().baselevel;
                DrillP[iDrill].GetComponent<VehicleP>().setlevel(currentlevel);



                //0 , 1 , 2 3 4 ,5

                 rlv = currentlevel;
                if (currentlevel > 0)
                {
                    //is unlock and upgrade lv, which is 2, back to 1
                    rlv--;

                    if (currentlevel ==5)
                    {
                        //is unlock and upgrade lv, which is 2, back to 1
                        rlv--;

                  
                    }
                }                                               // 6       3
                v = DrillP[iDrill].GetComponent<VehicleP>().baselevel + rlv;

                Upgradeslider.value = v;
              /*  if (currentlevel == 5 && Upgradeslider.value<=10)
                {
                    Upgradeslider.value -= 1;
                }*/
             

                resetdisplay();

               
                if (DrillP[iDrill].GetComponent<VehicleP>().IsMax() == false)
                {
                    if (DrillP[iDrill].GetComponent<VehicleP>().Isgem())
                    {
                        imags[1].SetActive(true);
                        imags[0].SetActive(false);
                        costtext.text = DrillP[iDrill].GetComponent<VehicleP>().returngemcost().ToString();
                    }
                    else
                    {
                        imags[0].SetActive(true);
                        imags[1].SetActive(false);
                        costtext.text = DrillP[iDrill].GetComponent<VehicleP>().returncoincost().ToString();
                    }
                }


                MAXLV.SetActive(false);
                if (currentlevel == 0)
                {
                    lockicon.SetActive(true);
                //    unlockbtn.SetActive(true);
                    confrimbtn.SetActive(false);
                    Upgradebtn.SetActive(true);
                }
                else
                {
              //      unlockbtn.SetActive(false);
                    confrimbtn.SetActive(true);
                    Upgradebtn.SetActive(true);

                    if (currentlevel == 5)
                    {
                        Upgradebtn.SetActive(false);
                        MAXLV.SetActive(true);
                        displayabilityicon(iDrill);
                    }
                }
                

                // currentlevel= 
                break;
            case 1:
                // body
                BodyP[iBody].SetActive(false);
                iBody += dir;
                if (iBody > 4)
                {
                    iBody = 0;
                }
                else if (iBody < 0)
                {
                    iBody = 4;
                }
                BodyP[iBody].SetActive(true);
                leveltext = PlayerPrefs.GetString("BodyLevel");
                if (swapLang.Fields != null)
                { partna.text = swapLang.Fields["Armor:"]; }
                else
                { partna.text = "Armor:"; }
                
                //    char c = leveltext[iDrill];
                c = leveltext[iBody].ToString();
                currentlevel = int.Parse(c);
                lockicon.SetActive(false);

                Baseslider.value = BodyP[iBody].GetComponent<VehicleP>().baselevel;
                BodyP[iBody].GetComponent<VehicleP>().setlevel (currentlevel);

                rlv = currentlevel;
                if (currentlevel > 0)
                {
                    //is unlock and upgrade lv, which is 2, back to 1
                    rlv--;

                    if (currentlevel == 5)
                    {
                        //is unlock and upgrade lv, which is 2, back to 1
                        rlv--;


                    }
                }                                               // 6       3
                v = BodyP[iBody].GetComponent<VehicleP>().baselevel + rlv;

                Upgradeslider.value = v;

              

                resetdisplay();

                if (BodyP[iBody].GetComponent<VehicleP>().IsMax() == false)
                {
                    if (BodyP[iBody].GetComponent<VehicleP>().Isgem())
                    {
                        imags[1].SetActive(true);
                        imags[0].SetActive(false);
                        costtext.text = BodyP[iBody].GetComponent<VehicleP>().returngemcost().ToString();
                    }
                    else
                    {
                        imags[0].SetActive(true);
                        imags[1].SetActive(false);
                        costtext.text = BodyP[iBody].GetComponent<VehicleP>().returncoincost().ToString();
                    }
                }


                MAXLV.SetActive(false);
                if (currentlevel == 0)
                {
                    lockicon.SetActive(true);
            //        unlockbtn.SetActive(true);
                    confrimbtn.SetActive(false);
                    Upgradebtn.SetActive(true);
                }
                else
                {
           //         unlockbtn.SetActive(false);
                    confrimbtn.SetActive(true);
                    Upgradebtn.SetActive(true);
                    if (currentlevel == 5)
                    {
                        Upgradebtn.SetActive(false);
                        MAXLV.SetActive(true);
                        displayabilityicon(iBody);

                    }
                }
                

                break;
            case 2:
                //engine
                EngineP[iEngine].SetActive(false);
                iEngine += dir;
                if (iEngine > 4)
                {
                    iEngine = 0;
                }
                else if (iEngine < 0)
                {
                    iEngine = 4;
                }
                if (swapLang.Fields != null)
                { partna.text = swapLang.Fields["Speed:"]; }
                else
                { partna.text = "Speed:"; }
               
                EngineP[iEngine].SetActive(true);
                leveltext = PlayerPrefs.GetString("EngineLevel");
                //      leveltext = PlayerPrefs.GetString("FuelLevel");

                //    char c = leveltext[iDrill];
                c = leveltext[iEngine].ToString();
                currentlevel = int.Parse(c);
                lockicon.SetActive(false);

                Baseslider.value = EngineP[iEngine].GetComponent<VehicleP>().baselevel;
                EngineP[iEngine].GetComponent<VehicleP>().setlevel(currentlevel);

                rlv = currentlevel;
                if (currentlevel > 0)
                {
                    //is unlock and upgrade lv, which is 2, back to 1
                    rlv--;

                    if (currentlevel == 5)
                    {
                        //is unlock and upgrade lv, which is 2, back to 1
                        rlv--;


                    }
                }                                               // 6       3
                v = EngineP[iEngine].GetComponent<VehicleP>().baselevel + rlv;

                Upgradeslider.value = v;
         

                resetdisplay();

                if (EngineP[iEngine].GetComponent<VehicleP>().IsMax() == false)
                {
                    if (EngineP[iEngine].GetComponent<VehicleP>().Isgem())
                    {
                        imags[1].SetActive(true);
                        imags[0].SetActive(false);
                        costtext.text = EngineP[iEngine].GetComponent<VehicleP>().returngemcost().ToString();
                    }
                    else
                    {
                        imags[0].SetActive(true);
                        imags[1].SetActive(false);
                        costtext.text = EngineP[iEngine].GetComponent<VehicleP>().returncoincost().ToString();
                    }
                }


                MAXLV.SetActive(false);
                if (currentlevel == 0)
                {
                    lockicon.SetActive(true);
            //        unlockbtn.SetActive(true);
                    confrimbtn.SetActive(false);
                    Upgradebtn.SetActive(true);
                }
                else
                {
         //           unlockbtn.SetActive(false);
                    confrimbtn.SetActive(true);
                    Upgradebtn.SetActive(true);
                    if (currentlevel == 5)
                    {
                        Upgradebtn.SetActive(false);
                        MAXLV.SetActive(true);

                        displayabilityicon(iEngine);
                    }
                }

                break;
            case 3:
                Fuelp[iFuel].SetActive(false);
                iFuel += dir;
                if (iFuel > 4)
                {
                    iFuel = 0;
                }
                else if (iFuel < 0)
                {
                    iFuel = 4;
                }
                Fuelp[iFuel].SetActive(true);
                leveltext = PlayerPrefs.GetString("FuelLevel");
                if (swapLang.Fields != null)
                { partna.text = swapLang.Fields["Fuel:"]; }
                else
                { partna.text = "Fuel:"; }
             
                //    char c = leveltext[iDrill];
                c = leveltext[iFuel].ToString();
                currentlevel = int.Parse(c);
                lockicon.SetActive(false);

                Baseslider.value = Fuelp[iFuel].GetComponent<VehicleP>().baselevel;
                Fuelp[iFuel].GetComponent<VehicleP>().setlevel(currentlevel);

                rlv = currentlevel;
                if (currentlevel > 0)
                {
                    //is unlock and upgrade lv, which is 2, back to 1
                    rlv--;

                    if (currentlevel == 5)
                    {
                        //is unlock and upgrade lv, which is 2, back to 1
                        rlv--;


                    }
                }                                               // 6       3
                v = Fuelp[iFuel].GetComponent<VehicleP>().baselevel + rlv;

                Upgradeslider.value = v;
           

                resetdisplay();

                if (Fuelp[iFuel].GetComponent<VehicleP>().IsMax() == false)
                {
                    if (Fuelp[iFuel].GetComponent<VehicleP>().Isgem())
                    {
                        imags[1].SetActive(true);
                        imags[0].SetActive(false);
                        costtext.text = Fuelp[iFuel].GetComponent<VehicleP>().returngemcost().ToString();
                    }
                    else
                    {
                        imags[0].SetActive(true);
                        imags[1].SetActive(false);
                        costtext.text = Fuelp[iFuel].GetComponent<VehicleP>().returncoincost().ToString();
                    }
                }


                MAXLV.SetActive(false);
                if (currentlevel == 0)
                {
                    lockicon.SetActive(true);
                  //  unlockbtn.SetActive(true);
                    confrimbtn.SetActive(false);
                    Upgradebtn.SetActive(true);
                }
                else
                {
                   
                            
               ///     unlockbtn.SetActive(false);
                    confrimbtn.SetActive(true);
                    Upgradebtn.SetActive(true);
                    if (currentlevel ==5)
                      
                    {
                        Upgradebtn.SetActive(false);
                        MAXLV.SetActive(true);
                        displayabilityicon(iFuel);
                    }
                }

                //fuel
                break;

        }
        btnselect();
       // updatemodel();

    }

    public void btnselect()
    {
        for (int i = 0; i < 4; i++)
        {
            buttons[i].SetActive(false);
     
        }
        buttons[partsselct].SetActive(true);
     
    }
   
    public void upgrades()
    {   // include unlock 
        // check parts panel and parts index
      

        string test = "";
        string tn = "";
        string lv = "";

        bool isunlock=false;

        switch (partsselct)
        {  
            case 0:
                //drill
                // return Cost in VehicleP???
                test = PlayerPrefs.GetString("DrillLevel");           
                tn = "";             
                for (int i = 0; i < 5; i++)
                {
                    if (i == iDrill)
                    {
                        lv = test[i].ToString();
                        int n = int.Parse(lv);
                        
                            
                        if (n < 5)
                        {
                            if (DrillP[iDrill].GetComponent<VehicleP>().purchause() == true)
                            {
                                n++;
                                PlayerPrefs.SetInt("PartsDrill", iDrill);
                                updatemodel();
                                vehicledata();
                                updatestatusslider();
                                buy.Play();
                                upgradeeff();
                            }
                          
                        }
                        tn = tn + n.ToString();
                        if (n == 5)
                            displayabilityicon(iDrill);
                    }
                    else
                    {
                        tn = tn + test[i];
                    }
                }
                PlayerPrefs.SetString("DrillLevel", tn);


               
           //  

                break;
            case 1:
                test = PlayerPrefs.GetString("BodyLevel");
                
                tn = "";

                for (int i = 0; i < 5; i++)
                {

                    if (i == iBody)
                    {
                        lv = test[i].ToString();
                        int n = int.Parse(lv);
                        if (n < 5)
                        {
                            if (BodyP[iBody].GetComponent<VehicleP>().purchause() == true)
                            {
                                n++;
                                PlayerPrefs.SetInt("PartsBody", iBody);
                                updatemodel();
                                vehicledata();
                                updatestatusslider();
                                buy.Play();
                                upgradeeff();
                            }

                        }
                        tn = tn + n.ToString();
                        if (n == 5)
                            displayabilityicon(iBody);
                    }
                    else
                    {
                        tn = tn + test[i];
                    }
                }
                PlayerPrefs.SetString("BodyLevel", tn);
          
                break;
            case 2:
                test =  PlayerPrefs.GetString("EngineLevel");
                
                tn = "";

                for (int i = 0; i < 5; i++)
                {

                    if (i == iEngine)
                    {
                        lv = test[i].ToString();
                        int n = int.Parse(lv);
                        if (n < 5)
                        {
                            if (EngineP[iEngine].GetComponent<VehicleP>().purchause() == true)
                            {
                                n++;
                                PlayerPrefs.SetInt("PartsEngine", iEngine);
                                updatemodel();
                                vehicledata();
                                updatestatusslider();
                                buy.Play();
                                upgradeeff();
                            }

                        }
                        tn = tn + n.ToString();
                        if (n == 5)
                            displayabilityicon(iEngine);
                    }
                    else
                    {
                        tn = tn + test[i];
                    }
                }
                PlayerPrefs.SetString("EngineLevel", tn);
                break;
            case 3:

                //fuel
                test =  PlayerPrefs.GetString("FuelLevel");

                
                tn = "";

                for (int i = 0; i < 5; i++)
                {

                    if (i == iFuel)
                    {
                        lv = test[i].ToString();
                        int n = int.Parse(lv);
                        if (n < 5)
                        {
                            if (Fuelp[iFuel].GetComponent<VehicleP>().purchause() == true)
                            {
                                n++;
                                PlayerPrefs.SetInt("PartsFuel", iFuel);
                                updatemodel();
                                vehicledata();
                                updatestatusslider();
                                buy.Play();
                                upgradeeff();
                            }

                        }
                        tn = tn + n.ToString();
                        if (n == 5)
                         
                            displayabilityicon(iFuel);
                    }
                    else
                    {
                        tn = tn + test[i];
                    }
                }
                PlayerPrefs.SetString("FuelLevel", tn);
                break;

        }
      

        //after unlock refresh ui 
        selectmodel(0);


    }


    public void confirm()
    {
        // confrim select parts and rewrite prefs
        // frist check which part panel
     //   upgradeeff();
        switch (partsselct)
        {    case 0:
                //drill []
                // directly rewrite 
                PlayerPrefs.SetInt("PartsDrill", iDrill);
                
                break;
            case 1:

                PlayerPrefs.SetInt("PartsBody", iBody);
                break;
            case 2:
                PlayerPrefs.SetInt("PartsEngine", iEngine);
                break;
            case 3:
                PlayerPrefs.SetInt("PartsFuel", iFuel);
                //fuel
                break;

        }
        // update real vehicle model
        updatemodel();
        vehicledata();
        updatestatusslider();

      
    }

    public void UpdateVehicle()
    {
        //using array from UImanager
        // if 
    }

    public void updateslider()
    {
        //update slider bar when player change or upgrades parts

    }

    void updateui()
    {
       
        if (values[0] < 0)
        {
            if (Drill.value < vehicle.drilldamage)
            {
                Drill.value -= values[0];
            }
            else
            {
                Drill.value = vehicle.drilldamage;
            }
        }
        else
        {
            if (Drill.value > vehicle.drilldamage)
            {
                Drill.value -= values[0];
            }
            else
            {
                Drill.value = vehicle.drilldamage;
            }
        }

        if (values[1] < 0)
        {
            if (Body.value < vehicle.HP)
            {
                Body.value -= values[1];
            }
            else
            {
                Body.value = vehicle.HP;
            }
        }
        else
        {
            if (Body.value > vehicle.HP)
            {
                Body.value -= values[1];
            }
            else
            {
                Body.value = vehicle.HP;
            }
        }

        if (values[2] < 0)
        {
            if (Engine.value < vehicle.speed)
            {
                Engine.value -= values[2];
            }
            else
            {
                Engine.value = vehicle.speed;
            }
        }
        else
        {
            if (Engine.value > vehicle.speed)
            {
                Engine.value -= values[2];
            }
            else
            {
                Engine.value = vehicle.speed;
            }
        }

        if (values[3] < 0)
        {
            if (Fuel.value < vehicle.fuel)
            {
                Fuel.value -= values[3];
            }
            else
            {
                Fuel.value = vehicle.fuel;
            }
        }
        else
        {
            if (Fuel.value > vehicle.fuel)
            {
                Fuel.value -= values[3];
            }
            else
            {
                Fuel.value = vehicle.fuel;
            }
        }



      
    

    }
    public void resetstatues()
    {
        Drill.value =0;

        Body.value = 0;


        Engine.value = 0;

        Fuel.value = 0;
        updatestatusslider();
       
    }

    public void updatestatusslider()
    {
        //update slider bar where player can more visible on vehicle status
        values = new float[4];
        values[0] = (Drill.value - vehicle.drilldamage) * 0.02f;
        values2[0] = Drill.value - vehicle.drilldamage;
       
        values[1] = (Body.value - vehicle.HP) * 0.02f;
        values2[1] = Body.value - vehicle.HP;
        values[2] = (Engine.value - vehicle.speed) * 0.02f;
        values2[2] = Engine.value - vehicle.speed;
        values[3] = (Fuel.value - vehicle.fuel) *0.02f;
        values2[3] = Fuel.value - vehicle.fuel;

        for (int i = 0; i < 4; i++)
        {
        

            if (values2[i] < 0)
                values2[i] = values2[i] * -1f;
        }

      

    }

    public void upgradeeff()
    {
        GameObject eff = Instantiate(upgradeeffect, Upgradebtn.transform.position,Quaternion.identity, pparent);
        eff.SetActive(true);
        Destroy(eff, 3.5f);

    }


    //ability

    public void GeneratePassiveUI()
    {
        resetPassiveUI();
       int[] ablitycount = vehicle.returnabilty();

        sortlist = new List<GameObject>();

        for (int i = 4; i > 0; i--)
        {
            for (int j = 0; j < 5; j++)
            {
                //Debug.Log(ablitycount[j]);
                if (ablitycount[j] == i)
                {  
                    GameObject e = Instantiate(abilitylist[j], parent);
                    e.GetComponent<abilytext>().Addtext(i, swapLang.Fields);
                    sortlist.Add(e);
                    ablitycount[j] = 0;
                }
            }
        }
        if (sortlist.Count == 0)
        {
            partnat.SetActive(true);
        }
        else
        { partnat.SetActive(false); }


    }

    public void resetPassiveUI()
    {
        if(sortlist.Count>0)

        {
            for (int j = 0; j < sortlist.Count; j++)
            {
                Destroy(sortlist[j]);
            }
            sortlist = new List<GameObject>();
        }


    }

    void displayabilityicon(int i )
    {
     


            
                abicon[i].SetActive(true);

           
    }
    void resetdisplay()
    {
        for (int j = 0; j < 5; j++)
        {
            abicon[j].SetActive(false);
        }


    }
    float t;
    bool c;
    private void OnApplicationPause(bool focus)
    {
        if (focus)
        {
            Debug.Log("进入后台");
            c = true;
            countdown();
        }
        else
        {
            Debug.Log("进入前台");
            c = false;
            if (t >= 5)
            {
                Application.LoadLevel(0);
            }
        }
    }

    IEnumerator countdown()
    {
        t = 0;
        while (c)
        {
            t ++;

            yield return new WaitForSeconds(1f);
        
        }

       
    }

     

}


