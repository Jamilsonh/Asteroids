using UnityEngine;

public class ScreenWrap : MonoBehaviour
{
    private float screenWidth;
    private float screenHeight;
    private bool canWrap = false;

    void Start()
    {
        Camera cam = Camera.main;
        screenHeight = cam.orthographicSize;
        screenWidth = screenHeight * cam.aspect;
    }

    void Update()
    {
        if (!canWrap)
        {
            if (IsVisible())
                canWrap = true;
            else
                return;
        }

        Vector3 pos = transform.position;

        // Horizontal
        if (pos.x > screenWidth)
            pos.x = -screenWidth;
        else if (pos.x < -screenWidth)
            pos.x = screenWidth;

        // Vertical
        if (pos.y > screenHeight)
            pos.y = -screenHeight;
        else if (pos.y < -screenHeight)
            pos.y = screenHeight;

        transform.position = pos;
    }

    bool IsVisible()
    {
        Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position);
        return viewPos.x > 0 && viewPos.x < 1 && viewPos.y > 0 && viewPos.y < 1;
    }
}
