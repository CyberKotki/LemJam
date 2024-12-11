using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResultScreen : MonoBehaviour
{
    public static ResultScreen instance;

    [SerializeField] private float screw1 = 30f;
    [SerializeField] private float screw2 = 60f;
    [SerializeField] private float screw3 = 90f;
    [SerializeField] private GameObject screwObject1;
    [SerializeField] private GameObject screwObject2;
    [SerializeField] private GameObject screwObject3;
    [SerializeField] private TextMeshProUGUI _accuracyText;


    void Awake() {
        if(instance == null) {
            instance = this;
        }
    }

    public void Show(LevelData data, float accuracy) {
        _accuracyText.text = accuracy + "%";
        
        if (accuracy > screw3)
        {
            screwObject3.SetActive(true);
        }
        else if (accuracy >= screw2)
        {
            screwObject2.SetActive(true);
        }
        else if (accuracy >= screw1)
        {
            screwObject1.SetActive(true);
        }
        
    }
}
