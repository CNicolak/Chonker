using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PoopCollision : MonoBehaviour
{
    public Button box;

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.tag == "poop") {
            box.interactable = true;
        }
    }
}
