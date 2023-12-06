using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour {
    public GameObject[] obstacles;
    public GameObject[] items;
    private bool stepped = false;

    void OnEnable() {
        stepped = false;

        for(int i = 0; i < obstacles.Length; i++) {
            if (Random.Range(0, 6) == 0) {
                obstacles[i].SetActive(true);
            }
            else {
                obstacles[i].SetActive(false);
            }
        }

        for (int i = 0; i < items.Length; i++) {
            if (Random.Range(0, 3) == 0) {
                items[i].SetActive(true);
            }
            else {
                items[i].SetActive(false);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Player" && !stepped) {
            stepped = true;
            GameManager.instance.AddScore(1);
        }
    }
}
