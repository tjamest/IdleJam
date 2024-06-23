using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject ButtonParent;
    public GameObject TextParent;

    public GameObject TextPrefab;
    public GameObject ButtonPrefab;

    public List<ResourceScriptableObject> resources = new List<ResourceScriptableObject>();

    protected List<Tuple<GameObject, ResourceType>> resourceButtons = new List<Tuple<GameObject, ResourceType>>();
    protected List<Tuple<GameObject, ResourceType>> resourceText = new List<Tuple<GameObject, ResourceType>>();

    public void Start()
    {
        AddMoneyResource(1);
    }

    public void Update()
    {

        // faz um loop entre os resources
        // verifica qual tá automatizado
        // se entiver automatizado -> vai diminuindo o cycleDuration e verifica se ele chegou a 0
        // se chegou a zero, aumenta o amount += increase e amount *= mulplier
        // zera o cycleDuration
        foreach (var resource in resources)
        {

        }

    }

    // Button Resource
    public void AddMoneyResource(float value)
    {
        AddResource(ResourceType.Money, value);
        Button button = GetResourceButton(ResourceType.Money).GetComponent<Button>();
        button.onClick.AddListener(() => AddPotatoResource(0));
        button.onClick.AddListener(() => AddResource(ResourceType.Money, 1));
    }

    public void AddPotatoResource(float value)
    {
        AddResource(ResourceType.Potato, value);
        Button button = GetResourceButton(ResourceType.Potato).GetComponent<Button>();
        button.onClick.AddListener(() => AddCornResource(0));
    }

    public void AddCornResource(float value)
    {
        AddResource(ResourceType.Corn, value);
    }

    protected void AddResource(ResourceType type, float value)
    {
        ResourceScriptableObject ResourceSO = GetResource(type);
        ResourceSO.amount += value;

        if (!HasResourceText(type))
        {
            TextMeshProUGUI[] textChildren = TextPrefab.GetComponentsInChildren<TextMeshProUGUI>();
            textChildren[0].text = ResourceSO.type.ToString();
            textChildren[1].text = ResourceSO.amount.ToString();

            TextMeshProUGUI buttonText = ButtonPrefab.GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = ResourceSO.type.ToString();

            resourceText.Add(new Tuple<GameObject, ResourceType>(Instantiate(TextPrefab, TextParent.transform), type));
            resourceButtons.Add(new Tuple<GameObject, ResourceType>(Instantiate(ButtonPrefab, ButtonParent.transform), type));
        }
        else
        {
            UpdateResourceText(type, ResourceSO.amount);
        }
    }

    // Resource Text
    protected void UpdateResourceText(ResourceType type, float amount)
    {
        foreach (var item in resourceText)
        {
            if (item.Item2 == type)
            {
                TextMeshProUGUI[] textChildren = item.Item1.GetComponentsInChildren<TextMeshProUGUI>();
                textChildren[1].text = amount.ToString();
            }
        }
    }

    protected bool HasResourceText(ResourceType type)
    {
        if (resourceText.Count > 0)
        {
            foreach (var item in resourceText)
            {
                if (item.Item2 == type)
                {
                    return true;
                }
            }
        }

        return false;
    }

    // Resource Button
    protected GameObject GetResourceButton(ResourceType type)
    {
        foreach (var item in resourceButtons)
        {
            if (item.Item2 == type)
            {
                return item.Item1;
            }
        }

        return null;
    }


    // Resource SO
    protected ResourceScriptableObject GetResource(ResourceType type)
    {
        foreach (var resource in resources)
        {
            if (resource.type == type)
            {
                return resource;
            }
        }

        return null;
    }


}
