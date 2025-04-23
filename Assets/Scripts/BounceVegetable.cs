using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceVegetable : MonoBehaviour
{

    [Range(0, 10)] public float bounceForce = 5;
    [Range(0, 1)] public float bounceUpward = 0.5f;

    private Vector3 originalScale; 

    public AnimationCurve bounceCurve; 
    public float bounceDuration = 0.5f;

    private Vector3 originalPosition; 

    public void TriggerBounce() 
    {
        StartCoroutine(BounceAnimation());
    }

    void Start()
    {
        originalPosition = transform.localPosition;
        originalScale = transform.localScale;
    }


    IEnumerator BounceAnimation()
    {
        float elapsed = 0f;
        Vector3 startScale = originalScale;

        while (elapsed < bounceDuration)
        {
            elapsed += Time.deltaTime;
            float eval = bounceCurve.Evaluate(elapsed / bounceDuration);

            float squash = 1f - eval * 0.9f;
            float stretch = 1f + eval * 0.2f;

            transform.localScale = new Vector3(stretch, squash, stretch);
            yield return null;
        }

        transform.localScale = startScale;
    }


    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Vegetable"))
        {
            TriggerBounce();

            Rigidbody rb = other.rigidbody;
            if (rb != null)
            {
                Vector3 rawDirection = (other.transform.position - transform.position).normalized;

                Vector3 modifiedDirection = Vector3.Lerp(rawDirection, Vector3.up, bounceUpward).normalized;

                float impulseStrength = 5f;
                rb.AddForce(modifiedDirection * impulseStrength, ForceMode.Impulse);
            }
        }
    }
}
