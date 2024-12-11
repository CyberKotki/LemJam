using UnityEngine;

public class SpawPoint : MonoBehaviour
{
    public Sprite missedSpaw;
    public Sprite perfectSpaw;

    public void Spaw(float distance)
    {
        if(distance<0.4)
        {
            //PERFECTSPAW
        }
        else if(distance< 0.7)
        {
            //poprostuspaw taki chyjowy
        }
        else
        {
            //miss
        }   

        Debug.Log(distance);
        Debug.Log(gameObject.name);
    }
}
