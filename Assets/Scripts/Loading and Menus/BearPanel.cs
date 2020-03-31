using UnityEngine;
using UnityEngine.UI;

public class BearPanel : MonoBehaviour
{
    // Start is called before the first frame update
   [SerializeField] Button[] originalSkills;
   [SerializeField] Button[] newSkills;
    [SerializeField ]GameObject subPanel;
    [SerializeField] public static CurrentAbilities currentBear;
   [SerializeField] public int color;

    public void SetCurrentPanel(int colour)
    {
        color = colour;
    }
    private void OnEnable()
    {
        switch (color)
        {
            case 0:
                originalSkills[0].GetComponentInChildren<Text>().text = RedBearAbilities.basicAbility.Name;
                originalSkills[1].GetComponentInChildren<Text>().text = RedBearAbilities.specialAbility.Name;
                break;
            case 1:
                originalSkills[0].GetComponentInChildren<Text>().text = BlueBearAbilities.basicAbility.Name;
                originalSkills[1].GetComponentInChildren<Text>().text = BlueBearAbilities.specialAbility.Name;
                break;
            case 2:
                originalSkills[0].GetComponentInChildren<Text>().text = BlackBearAbilities.basicAbility.Name;
                originalSkills[1].GetComponentInChildren<Text>().text = BlackBearAbilities.specialAbility.Name;
                break;
            case 3:
                originalSkills[0].GetComponentInChildren<Text>().text = PinkBearAbilities.basicAbility.Name;
                originalSkills[1].GetComponentInChildren<Text>().text = PinkBearAbilities.specialAbility.Name;
                break;
            case 4:
                originalSkills[0].GetComponentInChildren<Text>().text = GreenBearAbilities.basicAbility.Name;
                originalSkills[1].GetComponentInChildren<Text>().text = GreenBearAbilities.specialAbility.Name;
                break;



        }

    }
   
}
