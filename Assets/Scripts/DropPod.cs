using UnityEngine;
using System.Collections;

public class DropPod : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {
            SpawnMgr.RemoveDropPod(gameObject.transform);
            Destroy(gameObject);

            // give points
            Debug.Log("Gief points");
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            SpawnMgr.RemoveDropPod(gameObject.transform);
            Destroy(gameObject);

            // give points
            Debug.Log("Gief points");
        }
    }
}
