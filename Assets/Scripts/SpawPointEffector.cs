using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Events;
using System.Collections.Generic;
using System.Linq;

public class SpawPointEffector : MonoBehaviour
{
    private List<float> distances = new();
    [SerializeField] GameObject bufferPrefab;
    [SerializeField] float bufferDuration;
    [SerializeField] private Canvas canvas;

    [SerializeField] private int healthPoints;
    private Camera mainCamera;
    public List<GameObject> spawPointy = new();

    [Header("OnClick Event")]
    public UnityEvent OnClick;

    private void Start()
    {
        mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("nimo kamery");
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left mouse button
        {
            OnClick.Invoke();
        }
            
    }

    public void wyjebka()
    {

        float average = distances.Sum() / distances.Count;
    }

    public void wygranko()
    {
        float average = distances.Sum() / distances.Count;
        Debug.Log("kuniec");
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

        if(distanceFromClick<0.7)// to bedzie zmieniaæ potems
        {

            spawPointy.Remove(closestSpawPoint);
        }
        else
        {
            healthPoints -= 1;
        }

        if (healthPoints <= 0 || spawPointy.Count <= 0)
        {
            wygranko();
        }
        
    }

    private Vector2 ClickPosition()
    {
        

        Vector3 mousePosition = Input.mousePosition;
        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(mousePosition);

        //Ray ray = mainCamera.ScreenPointToRay(worldPosition);
        //RaycastHit2D hit = Physics2D.GetRayIntersection(ray, 6);

        return new Vector2(worldPosition.x, worldPosition.y);
    }

    //tu sobie spawnujemy ten buffor i dalej callujemy reszte rzeczy które siê dziej¹ kiedy skoñczy siê ³adowaæ
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
    