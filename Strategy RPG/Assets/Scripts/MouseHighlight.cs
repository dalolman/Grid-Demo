using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseHighlight : MonoBehaviour
{
    private Material origin;
    public Material highlight;

    private Renderer objRender;
    private GameObject highlightedObj;

    // Start is called before the first frame update
    void Start()
    {
        highlightedObj = null;
    }

    // Update is called once per frame
    void Update()
    {
       Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            GameObject hitObj = hit.collider.gameObject;

            if (hitObj != highlightedObj)
            {
                if (highlightedObj != null)
                {
                    ResetHighlight();
                }

                objRender = hitObj.GetComponent<Renderer>();
                if (objRender != null)
                {
                    origin = objRender.material;
                    objRender.material = highlight;
                    highlightedObj = hitObj;
                }
            }
        }
        else if (highlight != null)
        {
            ResetHighlight();
        }
    }

    void ResetHighlight()
    {
        if (objRender != null)
        {
            objRender.material = origin;
            highlightedObj = null;
            objRender = null;
        }
    }
}
