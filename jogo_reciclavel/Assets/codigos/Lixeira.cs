// Adicione este script ao cesto
using UnityEngine;

public class BasketController : MonoBehaviour
{
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            touchPosition.z = 0;
            transform.position = new Vector3(touchPosition.x, transform.position.y, 0);
        }
    }
}