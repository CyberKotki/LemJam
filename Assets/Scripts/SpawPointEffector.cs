using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Events;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEditor;
using System.Threading.Tasks;

public class SpawPointEffector : MonoBehaviour
{
    private List<float> distances = new();
    [SerializeField] GameObject bufferPrefab;
    [SerializeField] float bufferDuration;
    [SerializeField] private Canvas canvas;

    [SerializeField] private int healthPoints;
    [SerializeField] private Spark sparkBadPref;
    [SerializeField] private Spark sparkGoodPref;
    [SerializeField] private GameObject heartPref;
    
    [SerializeField] private AudioClip[] weldClips;

    public GameObject container;
    private Camera mainCamera;
    public List<GameObject> spawPointy = new();

    public float cooldownTime = 1f; // Czas cooldownu w sekundach

    private bool canClick = true; // Flaga kontrolująca możliwość kliknięcia


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
        if (Input.GetMouseButtonDown(0) && canClick) // Sprawdź lewy przycisk myszy i cooldown
        {
            OnClick.Invoke();
            StartCooldownAsync();
        }
    }

    private async void StartCooldownAsync()
    {
        canClick = false; // Zablokuj kliknięcia
        await Task.Delay((int)(cooldownTime * 1000)); // Odczekaj czas cooldownu w milisekundach
        canClick = true; // Odblokuj kliknięcia
    }

    public void Failure()
    {

        float average = distances.Sum() / distances.Count;
    }

    public void Success()
    {
        float average = distances.Sum() / distances.Count;
        Debug.Log("kuniec");    
    }

    public void SpawClick(Vector2 ClickPosition)
    {   
        // SFXManager.instance.PlayRandomSFXClip(weldClips, transform);
        float distanceFromClick = float.MaxValue;
        GameObject closestSpawPoint = null;
        foreach (GameObject spawPoint in spawPointy)
        {
            float nowaOdleglosc = (new Vector2(spawPoint.transform.GetChild(0).transform.position.x, spawPoint.transform.GetChild(0).transform.position.y) - ClickPosition).magnitude;
            if(nowaOdleglosc<distanceFromClick)
            {
                distanceFromClick = nowaOdleglosc;
                closestSpawPoint = spawPoint;
            }
        }

        closestSpawPoint.GetComponent<SpawPoint>().Spaw(distanceFromClick);
        distances.Add(distanceFromClick);

        if(distanceFromClick< 1)// to bedzie zmienia potems
        {

            spawPointy.Remove(closestSpawPoint);
            //spawed.Add(closestSpawPoint);
            Instantiate(sparkGoodPref, new Vector3(ClickPosition.x, ClickPosition.y, -0.1f), Quaternion.identity);
        }
        else
        {
            healthPoints -= 1;
            Destroy(container.transform.GetChild(0).gameObject);
            Instantiate(sparkBadPref, ClickPosition, Quaternion.identity);
        }

        if (healthPoints <= 0 || spawPointy.Count <= 0)
        {
            float accuracy = distances.Select(x => 1.2f - Math.Clamp(x, 0, 1) * 1f).Sum() / distances.Count * 100;
            Debug.Log("Accuracy: " + accuracy);
            FindAnyObjectByType<GameplayLoop>()?.Finish(accuracy);
        }
        
        
    }

    public void Wygranko() 
    {
        Debug.Log("Success animation not implemented");
        // spawPointy to nie zespawane
        // spawed to zespawane

    }

    public void przegranko() 
    {
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

    //tu sobie spawnujemy ten buffor i dalej callujemy reszte rzeczy które się dzieją kiedy skończy się ładować
    public void SpawnSpawBuffer()
    {
        Vector2 clickPosition = ClickPosition();

        GameObject SpawnedSpawBuffer = Instantiate(bufferPrefab, ClickPosition(), Quaternion.identity);
        SpawnedSpawBuffer.transform.SetParent(canvas.transform, false);
        SpawnedSpawBuffer.transform.position = ClickPosition();

        DOTween.To(() => SpawnedSpawBuffer.GetComponent<Image>().fillAmount, x=> SpawnedSpawBuffer.GetComponent<Image>().fillAmount = x, 1, bufferDuration).SetEase(Ease.Linear)
            .OnComplete(() => FinishedBuffer(SpawnedSpawBuffer, clickPosition)); 
    }

    private void FinishedBuffer(GameObject SpawnedSpawBuffer, Vector2 ClickPosition)
    {
        Destroy(SpawnedSpawBuffer, cooldownTime);
        SpawClick(ClickPosition);
    }

    //public void reloadCanvas() {
    //    HorizontalLayoutGroup hlg = FindAnyObjectByType<HorizontalLayoutGroup>();
    //    if(hlg == null) {
    //        Debug.LogError("No canvas or container for lives");
    //    }
    //    for(int i=0; i<hlg.transform.childCount; i++) {
    //    Destroy(hlg.transform.GetChild(i).gameObject);
    //    }
    //    for(int i=0; i<healthPoints; i++) {
    //        Instantiate(heartPref, hlg.transform);
    //    }
    //}
}
    