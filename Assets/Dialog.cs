using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class Dialog : MonoBehaviour
{
    [SerializeField] private Image im;
    [SerializeField] private Button button;
    public List<Sprite> sprites = new();
    int currentIntex  = 0;
    private AspectRatioFitter arf;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        im.sprite = sprites[0];
        arf = GetComponent<AspectRatioFitter>();
        arf.aspectRatio = im.sprite.rect.width / im.sprite.rect.height;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            currentIntex++;
            if(currentIntex < sprites.Count)
            {
                im.sprite = sprites[currentIntex];
                arf.aspectRatio = im.sprite.rect.width / im.sprite.rect.height;
            }
            if(currentIntex == sprites.Count - 1)
            {
                //im.sprite.rect.Set(im.sprite.rect.x, im.sprite.rect.y, 9, 1);
                button.gameObject.SetActive(true);
            }
        }
    }
}
