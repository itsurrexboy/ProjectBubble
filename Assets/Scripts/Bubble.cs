using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{

    public enum BubbleColor { Red, Green, Blue, Yellow };
    public BubbleColor color;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bubble")) 
        {
            Bubble otherBubble = collision.gameObject.GetComponent<Bubble>();
            if (otherBubble != null && otherBubble.color == color) 
            {
                StartCoroutine(CheckAdjacentBubbles());
            }
        }
    }

    private IEnumerator CheckAdjacentBubbles()
    {
        // Find all bubbles within a certain radius
        Collider2D[] overlappingBubbles = Physics2D.OverlapCircleAll(transform.position, 0.5f); 

        // Create a list to store bubbles of the same color
        List<Bubble> matchingBubbles = new List<Bubble>();
        matchingBubbles.Add(this);

        // Recursively check for adjacent bubbles of the same color
        CheckAdjacent(overlappingBubbles, matchingBubbles);

        // If three or more bubbles are found, destroy them
        if (matchingBubbles.Count >= 3)
        {
            foreach (Bubble bubble in matchingBubbles)
            {
                Destroy(bubble.gameObject);
            }
        }

        yield return null; // Wait for one frame before continuing
    }

    private void CheckAdjacent(Collider2D[] overlappingBubbles, List<Bubble> matchingBubbles)
    {
        foreach (Collider2D collider in overlappingBubbles)
        {
            Bubble otherBubble = collider.GetComponent<Bubble>();
            if (otherBubble != null && !matchingBubbles.Contains(otherBubble) && otherBubble.color == color)
            {
                matchingBubbles.Add(otherBubble);
                CheckAdjacent(Physics2D.OverlapCircleAll(otherBubble.transform.position, 0.5f), matchingBubbles); 
            }
        }
    }
}
