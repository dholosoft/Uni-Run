using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundLoop : MonoBehaviour {
    float width;

    void Awake() {
        BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();
        width = boxCollider.size.x;
    }

    void Update() {
        if (transform.position.x <= -width) {
            Vector2 offset = new Vector2(width * 2, 0);
            transform.position = (Vector2)transform.position + offset;
        }
    }
}
