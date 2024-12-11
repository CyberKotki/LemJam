using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Events;
using System.Collections.Generic;
using System.Linq;
using System;

public class SpawPointEffector : MonoBehaviour
{
    private List<float> distances = new();
    [SerializeField] GameObject bufferPrefab;
    [SerializeField] float bufferDuration;
    private Canvas canvas;

    [SerializeField] private int healthPoints;
    [SerializeField] private Spark sparkBadPref;
    [SerializeField] private Spark sparkGoodPref;
    private Camera mainCamera;
    public List<GameObject> spawPointy = new();
    public List<GameObject> spawed = new();

    [Header("OnClick Event")]
    public UnityEvent OnClick;

    private void Start()
    {
        mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("nimo kamery");
        }
        canvas = FindAnyObjectByType<Canvas>();
        if(canvas == null) {
            Debug.LogError("No canvas on scene. Required for SpawnPointEffector in robot");
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left mouse button
        {
            OnClick.Invoke();
        }
            
    }

    public void SpawClick()
    {
        float distanceFromClick = float.MaxValue;
        GameObject closestSpawPoint = null;
        foreach (GameObject spawPoint in spawPointy)
        {
            float nowaOdleglosc = (new Vector2(spawPoint.transform.GetChild(0).transform.position.x, spawPoint.transform.GetChild(0).transform.position.y) - ClickPosition()).magnitude;
            if(nowaOdleglosc<distanceFromClick)
            {
                distanceFromClick = nowaOdleglosc;
                closestSpawPoint = spawPoint;
            }
        }

        closestSpawPoint.GetComponent<SpawPoint>().Spaw(distanceFromClick);
        distances.Add(distanceFromClick);

        if(distanceFromClick<1.5)// to bedzie zmienia� potems
        {
            spawPointy.Remove(closestSpawPoint);
            spawed.Add(closestSpawPoint);
            Instantiate(sparkGoodPref, new Vector3(ClickPosition().x, ClickPosition().y, -0.1f), Quaternion.identity);
        }
        else
        {
            healthPoints -= 1;
            Instantiate(sparkBadPref, new Vector3(ClickPosition().x, ClickPosition().y, -0.1f), Quaternion.identity);
        }

        if (healthPoints <= 0 || spawPointy.Count <= 0)
        {
            float accuracy = distances.Select(x => 1 - Math.Clamp(x, 0, 1) * 1.3f).Sum() / distances.Count * 100;
            Debug.Log("Accuracy: " + accuracy);
            FindAnyObjectByType<GameplayLoop>()?.Finish(accuracy);
        }
        
        
    }

    public void Success() {
        Debug.Log("Success animation not implemented");
        // spawPointy to nie zespawane
        // spawed to zespawane

    }

    public void Failure() {
        Debug.Log("Failure animation not implemented");
        // spawPointy to nie zespawane
        // spawed to zespawane
    }

    private Vector2 ClickPosition()
    {
        

        Vector3 mousePosition = Input.mousePosition;
        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(mousePosition);

        //Ray ray = mainCamera.ScreenPointToRay(worldPosition);
        //RaycastHit2D hit = Physics2D.GetRayIntersection(ray, 6);

        return new Vector2(worldPosition.x, worldPosition.y);
    }

    //tu sobie spawnujemy ten buffor i dalej callujemy reszte rzeczy kt�re si� dziej� kiedy sko�czy si� �adowa�
    public void SpawnSpawBuffer()
    {
        GameObject SpawnedSpawBuffer = Instantiate(bufferPrefab, ClickPosition(), Quaternion.identity);
        SpawnedSpawBuffer.transform.SetParent(canvas.transform, false);
        SpawnedSpawBuffer.transform.position = ClickPosition();

        DOTween.To(() => SpawnedSpawBuffer.GetComponent<Image>().fillAmount, x=> SpawnedSpawBuffer.GetComponent<Image>().fillAmount = x, 1, bufferDuration).SetEase(Ease.Linear)
            .OnComplete(() => FinishedBuffer(SpawnedSpawBuffer)); 
    }

    private void FinishedBuffer(GameObject SpawnedSpawBuffer)
    {
        Destroy(SpawnedSpawBuffer, 1f);
    }
}
    