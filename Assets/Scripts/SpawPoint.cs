using UnityEngine;

public class SpawPoint : MonoBehaviour
{
    public Sprite missedSpaw;
    public Sprite perfectSpaw;

    public SpriteRenderer spriteRendererKonczyny;
    public SpriteRenderer spriteRendererSpawPointera;


    private void Start()
    {
        spriteRendererKonczyny = GetComponent<SpriteRenderer>();
        spriteRendererSpawPointera = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }
    public void Spaw(float distance)
    {
        if (distance < 0.4)
        {
            spriteRendererKonczyny.sprite = perfectSpaw;
            spriteRendererSpawPointera.enabled = false;
        }
        else if(distance< 0.75)
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
