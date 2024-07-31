using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareRotateAround : MonoBehaviour
{
    public RectTransform rectTransform; // The RectTransform component of the rectangle
    public float speed = 5f; // The speed at which the object moves

    private Vector2 boundsMin; // The minimum bounds of the rectangle
    private Vector2 boundsMax; // The maximum bounds of the rectangle

    private void Start()
    {
        // Get the bounds of the rectangle
        boundsMin = rectTransform.rect.min;
        boundsMax = rectTransform.rect.max;
    }

    private void Update()
    {
        // Get the current position of the object
        Vector2 position = rectTransform.anchoredPosition;

        // Get the movement input
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate the new position based on the input and speed
        position += new Vector2(horizontalInput, verticalInput) * speed * Time.deltaTime;

        // Limit the position to the bounds of the rectangle
        position.x = Mathf.Clamp(position.x, boundsMin.x, boundsMax.x);
        position.y = Mathf.Clamp(position.y, boundsMin.y, boundsMax.y);

        // Set the new position of the object
        rectTransform.anchoredPosition = position;
    }
}
