using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayLoop : MonoBehaviour
{
    [SerializeField] private float treshold = 50;
    [SerializeField] private LevelData nextLevelData;

    private SpawPointEffector robot;
    Mask mask;

    private HorizontalLayoutGroup h;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        h = FindAnyObjectByType<HorizontalLayoutGroup>();
        h?.gameObject.SetActive(false);
        robot = FindAnyObjectByType<SpawPointEffector>();
        mask = FindAnyObjectByType<Mask>();
        if(robot == null) {
            Debug.LogError("No robot on scene");
        }
        if(mask == null) {
            Debug.LogError("No mask on scene");
        }
        robot.enabled = false;
        if(ResultScreen.instance == null) {
            Debug.LogError("No Result screen on scene");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F)) {
            Debug.Log("Clicked");
            StartCoroutine(StartGame());
        }
    }

    public void Finish(float result) {
        StartCoroutine(finishGame(result));
    }

    private IEnumerator StartGame() {
        mask.Close();
        h?.gameObject.SetActive(true);
        yield return new WaitForSeconds(4);
        FindAnyObjectByType<SpawPointEffector>()?.reloadCanvas();
        robot.enabled = true;
    }

    private IEnumerator finishGame(float result) {
        mask.Open();
        yield return new WaitForSeconds(1);
        if(result < treshold) {
            robot.Failure();
        }
        else {
            robot.Success();
        }
        yield return new WaitForSeconds(4);
        ResultScreen.instance.Show(nextLevelData, result);
    }
}
