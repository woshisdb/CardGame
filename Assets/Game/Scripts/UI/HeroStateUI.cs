using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HeroStateUI : MonoBehaviour
{
    public TextMeshProUGUI knowledge;
    public TextMeshProUGUI brave;
    public TextMeshProUGUI charm;
    public TextMeshProUGUI agility;
    public TextMeshProUGUI strength;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GameArchitect.Instance!=null && GameArchitect.Instance.heroCardModel!=null)
        {
            knowledge.text = "知识:"+GameArchitect.Instance.heroCardModel.GetProperty(AnimalProperty.knowledge) + "";
            brave.text = "勇气:" + GameArchitect.Instance.heroCardModel.GetProperty(AnimalProperty.brave) + "";
            charm.text = "吸引力:" + GameArchitect.Instance.heroCardModel.GetProperty(AnimalProperty.charm) + "";
            agility.text = "敏捷:" + GameArchitect.Instance.heroCardModel.GetProperty(AnimalProperty.agility) + "";
            strength.text = "力量:" + GameArchitect.Instance.heroCardModel.GetProperty(AnimalProperty.strength) + "";
        }
    }
}
