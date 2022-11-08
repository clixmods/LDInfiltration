using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCircle : MonoBehaviour
{
    public float distance= 1;

    public int points = 2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var startPos = transform.position;

        var dir = transform.forward *distance ;
        var endPos = dir + startPos;
        Debug.DrawLine(startPos , endPos);
        var dirInvert = -transform.forward *distance ;
        var endPosInvert = dirInvert + startPos;
        Debug.DrawLine(startPos , endPosInvert);

        for (int i = 0; i < points; i++)
        {
            float x = (distance * Mathf.Cos(i) + Mathf.Tan(Mathf.Cos(i))) * transform.forward.magnitude ;
            float y = (distance * Mathf.Sin(i) + Mathf.Tan(Mathf.Sin(i))) * transform.forward.magnitude;

            var coord = (transform.position + new Vector3(x, y, transform.forward.z));
            
            var directionAdjusted = coord - startPos;

            directionAdjusted = directionAdjusted.normalized + new Vector3(0,0,transform.forward.z);
            
            directionAdjusted = directionAdjusted * distance;
            var finalEndPos = startPos + directionAdjusted;
          
            
            Debug.DrawLine(startPos , coord);
           
        }
    }
}
