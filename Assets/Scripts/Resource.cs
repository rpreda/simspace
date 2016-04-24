using UnityEngine;
using System.Collections;
using System;

public class Resource {

    public enum ResourceType { OXYGEN, FOOD, WATER, ENERGY };

    ResourceType type;
    float val = 0;
    int depletionRate = 0;
    float timeToDeplete = 0; // in seconds
    float accumulatedTime = 0; // in seconds
    int maxValue = 100;
    // this shouldn't be here, but hey
    Transform uiObj;
    float uiObjWidth = 0;

    public Resource(Transform obj, ResourceType type, int val, int depletionRate, int timeToDeplete, int maxValue)
    {
        this.type = type;
        this.val = val;
        this.depletionRate = depletionRate;
        this.timeToDeplete = timeToDeplete;
        this.maxValue = maxValue;
        this.uiObj = obj;
    }

    public float GetVal()
    {
        return val;
    }

    public void AddToVal(float val)
    {
        this.val += val;
    }

    public void SetVal(float val)
    {
        this.val = val;
    }

    public void AddToAccumulatedTime(float val)
    {
        this.accumulatedTime += val;
    }

    public void ResetAccumulatedTime()
    {
        this.accumulatedTime = 0;
    }

    public float GetAccumulatedTime()
    {
        return accumulatedTime;
    }

    public float GetTimeToDeplete()
    {
        return timeToDeplete;
    }

    public int GetDepletionRate()
    {
        return depletionRate;
    }

    public int GetMaxValue()
    {
        return maxValue;
    }

    public ResourceType GetResourceType()
    {
        return type;
    }

    public Transform GetUIObjectTransform()
    {
        return uiObj;
    }

    public void SetUIObjectWidth(float width)
    {
        uiObjWidth = width;
    }

    public float GetUIObjectWidth()
    {
        return uiObjWidth;
    }
}
