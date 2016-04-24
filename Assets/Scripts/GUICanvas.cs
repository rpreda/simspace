using UnityEngine;
using System.Collections;

public class GUICanvas : MonoBehaviour {

    [SerializeField]
    Transform resourceHUD;

    Transform resourceHUDInstance;

    // Use this for initialization
    void Start () {
        //resourceHUDInstance = (Transform)Instantiate(resourceHUD, transform.position, transform.rotation);
        //resourceHUDInstance.transform.SetParent(transform, false);
        //resourceHUDInstance.transform.Translate(new Vector3(0, yTutorialTextOffset, 0));
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
