using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Events;

public class SpawPointEffector : MonoBehaviour
{
    [SerializeField] GameObject bufferPrefab;
    [SerializeField] float bufferDuration;
    [SerializeField] private Canvas canvas;

    private Camera mainCamera;    

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
            Debug.Log("siema");
        }
            
    }



    private Vector2 ClickPosition()
    {
        

        Vector3 mousePosition = Input.mousePosition;
        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(mousePosition);

        Ray ray = mainCamera.ScreenPointToRay(worldPosition);
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray);

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
        Destroy(SpawnedSpawBuffer, 2f);
    }
}
    