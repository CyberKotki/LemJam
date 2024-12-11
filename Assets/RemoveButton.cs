using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RemoveButton : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetComponent<Button>()?.onClick.AddListener(() => Destroy(gameObject));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
