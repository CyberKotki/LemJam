using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayLoop : MonoBehaviour
{
    [SerializeField] private float treshold = 50;
    [SerializeField] private LevelData nextLevelData;

    private SpawPointEffector robot;
    Mask mask;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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
        yield return new WaitForSeconds(1);
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
