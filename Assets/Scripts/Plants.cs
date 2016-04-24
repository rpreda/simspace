using UnityEngine;
using System.Collections;

public class Plants : MonoBehaviour {

    public GameObject p0;
    public GameObject p1;
    public GameObject p2;
    public GameObject p3;
    public Light light;
    
    private  int water;
    private int level = 0;
    private Color startcolor;
  
     
	// Use this for initialization
    public Material[] materials;
    public float changeInterval = 0.33F;
    public Renderer rend;

    void Start()
    {
        p0.SetActive(false);
        p1.SetActive(false);
        p2.SetActive(false);
        p3.SetActive(false);
        light.intensity = 0;
        rend = GetComponent<Renderer>();
        rend.enabled = true;
    }

    void Update()
    {
        
    }

    void OnMouseEnter()
    {
        if (ResourceManager.GetResourceVal(Resource.ResourceType.WATER) > 20 && ResourceManager.GetResourceVal(Resource.ResourceType.WATER) > 20)
        {
            Color32 col = new Color32(51, 155, 255, 0);
            rend.material.color = col;
        }
        else
        {
            if (ResourceManager.GetResourceVal(Resource.ResourceType.WATER) < 20)
                light.intensity = 0;
            rend.material.color = Color.red;
        }
    }
    void OnMouseExit()
    {
        rend.material.color = Color.white;
    }

    void OnMouseDown()
    {
        if (ResourceManager.GetResourceVal(Resource.ResourceType.WATER) > 20 && ResourceManager.GetResourceVal(Resource.ResourceType.WATER) > 20)
        {
            if (level == 0)
            {
                light.intensity = 0.3f;
                light.color = new Color(255, 0, 255);
                p0.SetActive(true);
            }
            if (level == 1)
            {
                p1.SetActive(true);
                p0.SetActive(false);
            }
            if (level == 2)
            {
                p2.SetActive(true);
                p1.SetActive(false);
            }
            if (level == 3)
            {
                p3.SetActive(true);
                p2.SetActive(false);
            }
            if (level == 4)
            {
                p3.SetActive(false);
                level = -1;
                if (ResourceManager.GetResourceVal(Resource.ResourceType.FOOD) > 60)
                    ResourceManager.AddToResource(Resource.ResourceType.FOOD, 100 - ResourceManager.GetResourceVal(Resource.ResourceType.FOOD));
                else ResourceManager.AddToResource(Resource.ResourceType.FOOD, 40);
                light.intensity = 0;
            }
            ResourceManager.AddToResource(Resource.ResourceType.ENERGY, -10);
            level++;
            if (level != 0)
                ResourceManager.AddToResource(Resource.ResourceType.WATER, -10);
        }
        else
        {
            light.intensity = 0;
            rend.material.color = Color.red;
        }
        if (ResourceManager.GetResourceVal(Resource.ResourceType.OXYGEN) < 100)
            ResourceManager.AddToResource(Resource.ResourceType.ENERGY, 10);
    }
}
