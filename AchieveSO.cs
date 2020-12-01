using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Achive",menuName ="Achieve")]
public class AchieveSO : ScriptableObject
{
    [TextArea(3,10)]
    public string Description;
    public bool gems;
    public int reward;
    public int min;
    public int max;


    // Start is called before the first frame update

    // Update is called once per frame
 
}
