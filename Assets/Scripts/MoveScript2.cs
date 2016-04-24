using UnityEngine;
using System.Collections;

public class MoveScript2 : MonoBehaviour {
    float thrust = 150f;
    public Rigidbody rb;
    public Vector3 cameraFollowOffset = new Vector3(0, 10, -10);
    public Camera player;
	// Use this for initialization
	void Start () {
        rb = this.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        //moveScript player_movement = player.GetComponent<moveScript>();
        if (Input.GetKeyDown("f") && Vector3.Distance(player.transform.position, transform.position) < 10)
        {
            if (moveScript.in_rover)
            {
                moveScript.in_rover = false;
                player.transform.rotation.Set(0f, 0.3f, 0f, 0.9f);
            }
            else
            {
                Debug.Log(player.transform.rotation.ToString());
                moveScript.in_rover = true;
                player.transform.rotation = this.transform.rotation;
            }
            //rb.AddForce(new Vector3(thrust, 0, thrust));
        }
        if (moveScript.in_rover)
        {
            Vector3 rotation = new Vector3(0, Input.GetAxis("Horizontal") * 5, 0);
            this.transform.Rotate(rotation);
            float vert = Input.GetAxis("Vertical");
            rb.AddForce(transform.forward * vert * thrust);
            player.transform.position = transform.position + cameraFollowOffset;
            player.transform.rotation = this.transform.rotation;
        }
	}
}
