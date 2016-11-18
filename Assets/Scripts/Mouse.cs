using UnityEngine;
using System.Collections;

public class Mouse : MonoBehaviour {

    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;

	void Start() {
		Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
		Cursor.visible = true;
	}

    void OnMouseExit() {
        Cursor.SetCursor(null, Vector2.zero, cursorMode);
    }
}
