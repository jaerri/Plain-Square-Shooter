using System.Collections;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemTooltip : Tooltip
{
    public Item item;
    public GameObject tooltipValuePrefab;

    public static string FormatString(string name)
    {
        // from stacky overflowy https://stackoverflow.com/questions/4488969/split-a-string-by-capital-letters
        Regex regex = new Regex(@" 
                (?<=[A-Z])(?=[A-Z][a-z]) |
                 (?<=[^A-Z])(?=[A-Z]) |
                 (?<=[A-Za-z])(?=[^A-Za-z])", RegexOptions.IgnorePatternWhitespace); 
        return regex.Replace(name, " ");
    }

    void UpdateTooltipInfo()
    {
        foreach (Transform child in tooltipObject.transform.GetComponentsInChildren<Transform>())
        {
            switch (child.name)
            {
                case "CatagoryColor":
                    child.GetComponent<Image>().color = item.itemTypeColor;
                    break;
                case "Icon":
                    child.GetComponent<Image>().sprite = item.icon;
                    break;
                case "ItemName":
                    child.GetComponent<Text>().text = FormatString(item.name);
                    break;
                case "CatagoryName":
                    child.GetComponent<Text>().text = FormatString(item.itemTypeName);
                    child.GetComponent<Text>().color = item.itemTypeColor;
                    break;
                case "Description":
                    child.GetComponent<Text>().text = item.description;
                    break;
                case "InfoPanel":
                    foreach (Item.ItemStat value in item.itemStats)
                    {
                        GameObject statsValueObject = Instantiate(tooltipValuePrefab, child);
                        Text[] textComponents = statsValueObject.GetComponentsInChildren<Text>();
                        Color32 valueColor = value.color;         

                        foreach (Text textComponent in textComponents)
                        {                       
                            switch (textComponent.gameObject.name)
                            {
                                case "ValueName":
                                    textComponent.text = value.name;
                                    textComponent.color = valueColor;
                                    break;
                                case "Unit":
                                    textComponent.text = value.unit;
                                    textComponent.color = valueColor;
                                    break;
                                case "Value":
                                    textComponent.text = value.value.ToString();
                                    break;
                            }
                        }
                        
                        Slider valueSlider = statsValueObject.GetComponentInChildren<Slider>();
                        valueSlider.value = value.value;
                        valueSlider.GetComponentInChildren<Image>().color = valueColor;
                    }
                    break;
            }
        }
    }

    public override void CreateTooltip()
    {
        base.CreateTooltip();

        UpdateTooltipInfo();
    }
}
