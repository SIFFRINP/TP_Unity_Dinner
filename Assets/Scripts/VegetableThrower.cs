using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 
public class VegetableThrower : MonoBehaviour
{
    [Tooltip("Collection of vegetables that can be thrown")]
    public Rigidbody[] vegetables;
 
    [Header("Throw")]
    [Range(0, 20), Tooltip("Minimum force to apply to a vegetable when throwing")]
    public float minThrowForce = 2;
    [Range(0, 20), Tooltip("Maximum force to apply to a vegetable when throwing")]
    public float maxThrowForce = 10;
    [Range(0.5f, 3), Tooltip("Time the player has to hold the button to throw at max strength")]
    float throwForce01;

    public float throwForceChargeTime = 1;
    [Range(0, 30), Tooltip("Maximum torque to apply to a vegetable when throwing")]
    public float maxRandomTorque = 10;
 
    [Header("UI")]
    public Image chargeBarFilling;
 
    void Update()
    {
        // Charger la force lorsque le clic gauche est maintenu
        if (Input.GetMouseButton(0))
        {
            if (throwForce01 < 1)
            {
                throwForce01 += Time.deltaTime / throwForceChargeTime;
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            LaunchVegetable();
            throwForce01 = 0;
        }
 
        if (chargeBarFilling != null)
        {
            chargeBarFilling.fillAmount = throwForce01;
        }
    }
 
    public Rigidbody InstantiateRandomVegetable()
    {
        if (vegetables == null || vegetables.Length == 0)
        {
            Debug.Log("Tableau de légumes vide.");
            return null;
        }
 
        int number = Random.Range(0, vegetables.Length);
        Rigidbody selectedVegetable = vegetables[number];
        Rigidbody instance = Instantiate(selectedVegetable, transform.position, Quaternion.identity);
 
        return instance;
    }
 
    public void ThrowVegetable(Rigidbody vegetable, float throwForce)
    {
        if (vegetable == null)
        {
            Debug.Log("Aucun légume à lancer.");
            return;
        }
 
        Vector3 launchDirection = transform.forward;
        vegetable.AddForce(launchDirection * throwForce, ForceMode.Impulse);
 
        Vector3 randomTorque = Random.insideUnitSphere * maxRandomTorque;
        vegetable.AddTorque(randomTorque, ForceMode.Impulse);
    }
 
    public void LaunchVegetable()
    {
        Rigidbody vegetable = InstantiateRandomVegetable();
 
        if (vegetable != null)
        {
            float throwForce = Mathf.Lerp(minThrowForce, maxThrowForce, throwForce01);
 
            ThrowVegetable(vegetable, throwForce);
        }
    }
}