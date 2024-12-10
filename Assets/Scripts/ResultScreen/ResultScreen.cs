using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResultScreen : MonoBehaviour
{
    public static ResultScreen instance;

    [SerializeField] private float screw1;
    [SerializeField] private float screw2;
    [SerializeField] private float screw3;
    [SerializeField] private string title0;
    [SerializeField] private string title1;
    [SerializeField] private string title2;
    [SerializeField] private string title3;


    [Space(10)]
    [SerializeField] Image panel;
    [SerializeField] private TextMeshProUGUI _titleText;
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private HorizontalLayoutGroup _screwContainer;
    [SerializeField] private TextMeshProUGUI _accuracyText;
    [SerializeField] private Button _nextButton;

    private LevelData _levelData;


    void Awake() {
        if(instance == null) {
            instance = this;
        }
    }

    void Start() {
        _nextButton.onClick.AddListener(NextLevel);
    }

    public void Show(LevelData data, float accuracy) {
        _levelText.text = data.title;
        _accuracyText.text = accuracy.ToString();
        _levelData = data;

        int screwCount = 0;
        string title = title0;
        if(accuracy >= screw1) {
            title = title1;
            screwCount++;
        }
        if(accuracy >= screw2) {
            screwCount++;
            title = title2;
        }
        if(accuracy >= screw3) {
            screwCount++;
            title = title3;
        }

        for(int i=0; i<_screwContainer.transform.childCount; i++) {
            _screwContainer.transform.GetChild(i).gameObject.SetActive(false);
        }

        for(int i=0; i<screwCount; i++) {
            _screwContainer.transform.GetChild(i).gameObject.SetActive(true);
        }
        _titleText.text = title;
        panel.gameObject.SetActive(true);
    }

    void NextLevel() {
        if(_levelData is not null) {
            Debug.Log("Next level not implemented! " + _levelData.nextScene);
            panel.gameObject.SetActive(false);
        }
    }
}
