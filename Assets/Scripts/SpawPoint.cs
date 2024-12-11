using UnityEngine;

public class SpawPoint : MonoBehaviour
{
    public Sprite missedSpaw;
    public Sprite perfectSpaw;

    private SpriteRenderer spriteRendererKonczyny;
    private SpriteRenderer spriteRendererSpawPointera;


    private void Start()
    {
        spriteRendererKonczyny = GetComponent<SpriteRenderer>();
        spriteRendererSpawPointera = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }
    public void Spaw(float distance)
    {
        if (distance < 0.6)
        {
            spriteRendererKonczyny.sprite = perfectSpaw;
            spriteRendererSpawPointera.enabled = false;
        }
        else if(distance< 1)
        {
            spriteRendererKonczyny.sprite = missedSpaw;
            spriteRendererSpawPointera.enabled = false;
        }
        else
        { 
            //miss
        }   

        Debug.Log(distance);
        Debug.Log(gameObject.name);
    }
}
