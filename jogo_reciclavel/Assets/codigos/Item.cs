using UnityEngine;

public class Item : MonoBehaviour
{
    public bool isRecyclable = true;
    public int recyclablePoints = 10;
    public int nonRecyclablePenalty = -5;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Basket"))
        {
            // Atualiza a pontuação
            if (GameManager.instance != null)
            {
                if (isRecyclable)
                {
                    GameManager.instance.AddScore(recyclablePoints);
                }
                else
                {
                    GameManager.instance.AddScore(nonRecyclablePenalty);
                }
            }

            // Faz o item desaparecer instantaneamente
            Destroy(gameObject);
        }
    }
}