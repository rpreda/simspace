using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResourceManager : MonoBehaviour {

    [SerializeField]
    int m_defaultOxyigen = 100;
    [SerializeField]
    int m_defaultEnergy = 100;
    [SerializeField]
    int m_defaultFood = 100;
    [SerializeField]
    int m_defaultWater = 100;
    
    // depletionRates
    [SerializeField]
    int m_defaultOxyigenDepletionRate = 1;
    [SerializeField]
    int m_defaultEnergyDepletionRate = 1;
    [SerializeField]
    int m_defaultFoodDepletionRate = 1;
    [SerializeField]
    int m_defaultWaterDepletionRate = 1;

    // timers
    [SerializeField]
    int m_oxyigenTimeToDeplete = 1;
    [SerializeField]
    int m_energyTimeToDeplete = 1;
    [SerializeField]
    int m_foodTimeToDeplete = 1;
    [SerializeField]
    int m_waterTimeToDeplete = 1;

    // timers
    [SerializeField]
    int m_oxygenMaxValue = 100;
    [SerializeField]
    int m_energyMaxValue = 100;
    [SerializeField]
    int m_foodMaxValue = 100;
    [SerializeField]
    int m_waterMaxValue = 100;

    // timers
    [SerializeField]
    Transform oxygenBar = null;
    [SerializeField]
    Transform foodBar = null;
    [SerializeField]
    Transform waterBar = null;
    [SerializeField]
    Transform energyBar = null;

    //[SerializeField]
    public List<Resource> m_resourceArr;

    private static bool HAS_FADED = false;
    
    static Dictionary<Resource.ResourceType, Resource> m_resources;

    void OnEnable()
    {
        // add listener
        EventManager.OnNotEnoughResources += HandleNotEnoughResources;
    }

    void OnDisable()
    {
        // remove listener
        EventManager.OnNotEnoughResources -= HandleNotEnoughResources;
    }

    void Awake()
    {
        m_resources = new Dictionary<Resource.ResourceType, Resource>();
        HAS_FADED = false;
    }

    // Use this for initialization
    void Start () {
        m_resources.Clear();
        // create default resources
        // duplicate resource type key...have it here and remove from Resource??
        m_resources[Resource.ResourceType.OXYGEN] = new Resource(oxygenBar, Resource.ResourceType.OXYGEN, m_defaultOxyigen, m_defaultOxyigenDepletionRate, m_oxyigenTimeToDeplete, m_oxygenMaxValue);
        m_resources[Resource.ResourceType.ENERGY] = new Resource(energyBar, Resource.ResourceType.ENERGY, m_defaultEnergy, m_defaultEnergyDepletionRate, m_energyTimeToDeplete, m_energyMaxValue);
        m_resources[Resource.ResourceType.FOOD] = new Resource(foodBar, Resource.ResourceType.FOOD, m_defaultFood, m_defaultFoodDepletionRate, m_foodTimeToDeplete, m_foodMaxValue);
        m_resources[Resource.ResourceType.WATER] = new Resource(waterBar, Resource.ResourceType.WATER, m_defaultWater, m_defaultWaterDepletionRate, m_waterTimeToDeplete, m_waterMaxValue);

        // oh well
        foreach (KeyValuePair<Resource.ResourceType, Resource> entry in m_resources)
        {
            Resource res = m_resources[entry.Key];
            if (res == null)
                continue;
            RectTransform rt = res.GetUIObjectTransform().GetComponent<RectTransform>();
            Vector3[] corners = new Vector3[4];
            rt.GetWorldCorners(corners);

            float panelWidth = Mathf.Abs(corners[0].x - corners[2].x);
            res.SetUIObjectWidth(panelWidth);
            UpdateBarScale(res);
        }
    }
	
	// Update is called once per frame
	void Update () {

        // first update the time values
        foreach (KeyValuePair<Resource.ResourceType, Resource> entry in m_resources)
        {
            m_resources[entry.Key].AddToAccumulatedTime(Time.deltaTime);

            if (m_resources[entry.Key].GetAccumulatedTime() > m_resources[entry.Key].GetTimeToDeplete())
            {
                // reset the accumulated time and deplete resource by rate
                if (entry.Key == Resource.ResourceType.OXYGEN && !moveScript.in_house || entry.Key == Resource.ResourceType.FOOD)
                {
                    m_resources[entry.Key].ResetAccumulatedTime();
                    m_resources[entry.Key].AddToVal(-m_resources[entry.Key].GetDepletionRate());
                }
                else if (entry.Key == Resource.ResourceType.ENERGY)
                {
                    if (moveScript.in_rover)
                    {
                        m_resources[entry.Key].ResetAccumulatedTime();
                        m_resources[entry.Key].AddToVal(-1.5f * m_resources[entry.Key].GetDepletionRate());
                    }
                    else
                    {
                        if (m_resources[entry.Key].GetVal() < m_resources[entry.Key].GetMaxValue())
                        {
                            m_resources[entry.Key].ResetAccumulatedTime();
                            m_resources[entry.Key].AddToVal(m_resources[entry.Key].GetDepletionRate());
                        }
                        else
                        {
                            m_resources[entry.Key].SetVal(m_resources[entry.Key].GetMaxValue());
                        }
                    }
                }

                // bound check with 0
                if (m_resources[entry.Key].GetVal() < 0)
                {
                    m_resources[entry.Key].SetVal(0);

                    if (!HAS_FADED && entry.Key != Resource.ResourceType.ENERGY)
                    {
                        // play death phase
                        GameObject fadeObject = GameObject.FindWithTag("Fader");
                        if (fadeObject)
                        {
                            ScreenFader screenFader = fadeObject.GetComponent<ScreenFader>();
                            if (screenFader)
                            {
                                moveScript.playerDead = true;
                                screenFader.fadeIn = !screenFader.fadeIn;
                                HAS_FADED = true;
                                StartCoroutine(WaitSomeSeconds(2)); // level restarts here
                            }
                        }
                    }
                }
                else if (HAS_FADED)
                {
                    return;
                }

                if (m_resources[entry.Key].GetVal() < 20)
                {
                    Transform obj = m_resources[entry.Key].GetUIObjectTransform();
                    Transform objText = obj.parent.parent.transform;

                    //rgb(240, 128, 128)
                    //  if (objText.GetComponent<Text>().color != Color.red)
                    //     objText.GetComponent<Text>().color = Color.red;

                    // rgb(178,34,34) firebrick

                    objText.GetComponent<Text>().color = new Color(1.0f, 0.3f, 0.3f);
                }
                else
                {
                    Transform obj = m_resources[entry.Key].GetUIObjectTransform();
                    Transform objText = obj.parent.parent.transform;
                    if (objText.GetComponent<Text>().color != Color.white)
                        objText.GetComponent<Text>().color = Color.white;
                }

                //Debug.Log("Value: " + m_resources[entry.Key].GetVal());
                UpdateBarScale(m_resources[entry.Key]);
            }
        }

//         This is a test (put something like this in your own class when you want to send an event
//         if (Input.GetKeyDown(KeyCode.J))
//         {
//             EventManager.TriggerResourceEvent(EventManager.EventType.NOT_ENOUGH_RESOURCES, Resource.ResourceType.WATER);
//         }
    }

    // can also use this for subtract
    public static void AddToResource(Resource.ResourceType resource, float val)
    {
        m_resources[resource].AddToVal(val);
        if (m_resources[resource].GetVal() > m_resources[resource].GetMaxValue())
        {
            m_resources[resource].SetVal(m_resources[resource].GetMaxValue());
        }

        UpdateBarScale(m_resources[resource]);
    }

    public static void SetResourceVal(Resource.ResourceType type, float val)
    {
        m_resources[type].SetVal(val);
    }

    public static float GetResourceVal(Resource.ResourceType type)
    {
        return m_resources[type].GetVal();
    }

    public static int GetMaxResourceVal(Resource.ResourceType type)
    {
        return m_resources[type].GetMaxValue();
    }

    private static void UpdateBarScale(Resource res)
    {
        Transform obj = res.GetUIObjectTransform();
        if (obj)
        {
            const float defaultScale = 1; // this is full length
            float newScale = defaultScale * res.GetVal() / (float)m_resources[res.GetResourceType()].GetMaxValue();

            float scaleDiff = (obj.localScale.x - newScale);

            float diff = 0;
            if (scaleDiff != 0)
                diff = scaleDiff * res.GetUIObjectWidth() / 2;

            obj.localScale = new Vector3(newScale, obj.localScale.y, obj.localScale.z);

            obj.Translate(new Vector3(-diff, 0f, 0f));
        }
    }

    private static void HandleNotEnoughResources(Resource.ResourceType resType)
    {
        Debug.Log("Not enough resources");
    }

    private static IEnumerator WaitSomeSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        //Application.LoadLevel(0);
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}
