using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject bubblePrefab;
    public float shootForce = 5f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            Vector2 direction = (mousePos - transform.position).normalized;

            GameObject bubble = Instantiate(bubblePrefab, transform.position, Quaternion.identity);
            bubble.GetComponent<Rigidbody2D>().AddForce(direction * shootForce, ForceMode2D.Impulse);
        }
    }
}