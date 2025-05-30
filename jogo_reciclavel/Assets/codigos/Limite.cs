using UnityEngine;

public class BasketMovement : MonoBehaviour
{
    private float minX, maxX;
    private float objectWidth;
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
        CalculateScreenBounds();

        // Garante o modo portrait (opcional)
        Screen.orientation = ScreenOrientation.Portrait;
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPosition = mainCamera.ScreenToWorldPoint(touch.position);
            touchPosition.z = 0;

            // Limite mais preciso com margem de segurança
            touchPosition.x = Mathf.Clamp(touchPosition.x, minX + 0.1f, maxX - 0.1f);
            transform.position = new Vector3(touchPosition.x, transform.position.y, 0);
        }
    }

    void CalculateScreenBounds()
    {   
        // Calcula a largura do objeto (metade da largura total)
        objectWidth = GetComponent<SpriteRenderer>().bounds.extents.x;

        // Cálculo para portrait
        float screenAspect = (float)Screen.height / (float)Screen.width; // Invertido para portrait
        float camWidth = mainCamera.orthographicSize;
        float camHeight = camWidth * screenAspect;

        // Limites horizontais (em portrait, a largura da tela é menor)
        minX = -camWidth + objectWidth;
        maxX = camWidth - objectWidth;

        Debug.Log($"Limites calculados: MinX={minX}, MaxX={maxX}");
        // No método CalculateScreenBounds()
        float margin = 0.2f; // Ajuste conforme necessário
        minX = -camWidth + objectWidth + margin;
        maxX = camWidth - objectWidth - margin;
    }

    // Método para visualizar os limites na cena (apenas no Editor)
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(new Vector3(minX, -10, 0), new Vector3(minX, 10, 0));
        Gizmos.DrawLine(new Vector3(maxX, -10, 0), new Vector3(maxX, 10, 0));
    }
}