using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutterController : MonoBehaviour
{

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int layerMask = 1 << 8;
        RaycastHit hit;
        bool hasHit = Physics.Raycast(GetMouseRay(), out hit, Mathf.Infinity, layerMask);
        if (hasHit)
        {
            if (Input.GetMouseButton(0))
            {
                transform.position = new Vector3(hit.point.x, hit.point.y, 0);
            }
        }
    }
    private static Ray GetMouseRay()
    {
        return Camera.main.ScreenPointToRay(Input.mousePosition);
    }
}
