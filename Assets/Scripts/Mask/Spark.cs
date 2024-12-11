using System.Collections;
using UnityEngine;

public class Spark : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(SparkCor());
    }

    private IEnumerator SparkCor() {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
