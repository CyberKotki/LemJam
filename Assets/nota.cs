using UnityEngine;

public class nota : MonoBehaviour
{

    public Sprite hoveredSprite;
    public Sprite defaultSprite;
    private void OnMouseEnter()
    {
        GetComponent<SpriteRenderer>().sprite = hoveredSprite;
    }

    private void OnMouseExit()
    {
        GetComponent<SpriteRenderer>().sprite = defaultSprite;
    }
}
