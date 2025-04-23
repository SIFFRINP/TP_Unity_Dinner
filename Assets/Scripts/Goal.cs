using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class Goal : MonoBehaviour
{
    ParticleSystem particles;
    GameManager gameManager;
    [Min(0)] public int pointsOnGoal = 1;
 
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        particles = GetComponentInChildren<ParticleSystem>();
    }
    private void OnTriggerEnter(Collider other)
    {
        // J'ai recherché sur internet comment on comparait deux objets
        //On a créé un tag pour les deux prefabs pour que ca marche
        if (other.CompareTag("Vegetable"))
        {
            if (particles != null)
            {
                particles.Play();
            }
            if (gameManager != null)
            {
                gameManager.AddScore(pointsOnGoal);
            }
            // on ajoute une coroutine car c'etait supprimé trop vite apres
            StartCoroutine(DestroyAfterDelay(other.gameObject, 0.5f));
        }
    }
    private IEnumerator DestroyAfterDelay(GameObject vegetable, float delai)
    {
        yield return new WaitForSeconds(delai);
        Destroy(vegetable);
    }
 
}