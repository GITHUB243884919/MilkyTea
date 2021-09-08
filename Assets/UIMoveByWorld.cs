using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIMoveByWorld : MonoBehaviour
{
	public Camera mainCam;
	public Camera uiCam;
	public RectTransform canvasRect;
	public Image img;

	public GameObject org;
	public GameObject target;

    // Start is called before the first frame update
    void Start()
    {
		Vector3 pSC = mainCam.WorldToScreenPoint(org.transform.position);
		Vector2 pUI;
		RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, pSC, uiCam, out pUI);
		img.transform.localPosition = pUI;


	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
