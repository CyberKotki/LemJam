using UnityEngine;

public class SpawanieRobota : MonoBehaviour
{
    public Sprite g³owa;
    public Sprite lewaRenca;
    public Sprite prawaRenca;

    [System.Serializable]
    public class SpriteThree
    {
        public Sprite sprite1;
        public Sprite sprite2;
        public Sprite sprite3;

        public SpriteThree(Sprite sprite1, Sprite sprite2, Sprite sprite3)
        {
            this.sprite1 = sprite1;
            this.sprite2 = sprite2;
            this.sprite3 = sprite3;
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}

