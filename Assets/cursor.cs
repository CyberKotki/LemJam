using UnityEngine;

public class Cursor : MonoBehaviour
{
    public Texture2D cursorTexture;
    public Vector2 hotSpot = Vector2.zero;

    void Start()
    {
        //Cursor.SetCursor(cursorTexture, hotSpot, CursorMode.ForceSoftware);
    }
}
