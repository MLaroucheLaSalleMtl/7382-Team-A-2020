using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    private Image Health;
    [SerializeField] private Bears Hp;

    // Start is called before the first frame update
    void Start()
    {
        Health = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        Health.fillAmount = Hp.Hp / Hp.TotalHP;
    }
}
