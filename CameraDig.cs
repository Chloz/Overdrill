using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDig : MonoBehaviour
{
    // Start is called before the first frame update

    public Vector3 point;
    public Vector3 normal;
    float t = 1f;
   public List<GameObject> glist;
    float time;
    void Start()
    {
        glist = new List<GameObject>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       // t-= Time.deltaTime;
        if (t < 0)
        {
        //        Destroy(gameObject);
        }
        else
        {
            time -= 0.02f;

            if (time < 0)
            {
                time = 0.03f;
                RaycastHit hit;
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 500f, LayerMask.GetMask("Ground")))
                {
                    //  cc.transform.position = hit.point;    
                    //   car.transform.LookAt(hit.point);
                    //  transform.position = new Vector3(car.transform.position.x, 10, car.transform.position.z);
                    List<GameObject> ngl = new List<GameObject>();

                    for (int i = 0; i < glist.Count; i++)
                    {

                        if (glist[i] != null)
                        {
                            glist[i].GetComponent<MeshDeformer>().Deform(transform.position, 1.3f, 0.13f, -2.0f, -0.2f, hit.normal);
                            ngl.Add(glist[i]);
                        }

                    }
                    glist = ngl;
                }
            }
        

          
        }
      
    }

    
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "DeformableMesh")
        {
            if (!glist.Contains(collision.gameObject))
            {
                glist.Add(collision.gameObject);
              //  Debug.Log(collision.gameObject.name);
            //    Debug.Log(normal);
            }
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "DeformableMesh")
        {
            if (glist.Contains(collision.gameObject))
            {
                glist.Remove(collision.gameObject);
            //    Debug.Log(collision.gameObject.name);
           //     Debug.Log(normal);
            }
        }
    }
}
