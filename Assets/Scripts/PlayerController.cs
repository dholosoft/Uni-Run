using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public AudioClip deadClip;
    public float jumpForce = 350f;
    private int jumpCount = 0;
    private bool isGrounded = false;
    private bool isDead = false;

    private Rigidbody2D rb;
    private Animator animator;
    private AudioSource audioSource;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update() {
        if (isDead) {
            return;
        }

        if (Input.GetMouseButtonDown(0) && jumpCount < 2) {
            jumpCount++;
            rb.velocity = Vector2.zero;
            rb.AddForce(new Vector2(0, jumpForce));
            audioSource.Play();
        }
        else if (Input.GetMouseButtonUp(0) && rb.velocity.y > 0) {
            rb.velocity = rb.velocity * 0.5f;
        }

        animator.SetBool("Grounded", isGrounded);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Dead" && !isDead) {
            Die();
        }

        GetItem(collision.gameObject, collision.gameObject.tag);
    }

    private void GetItem(GameObject gObj, string tag) {
        if (tag == "Gold") {
            //사운드 플레이
            GameManager.instance.AddScore(5);
            gObj.SetActive(false);
        }
        else if (tag == "Silver") {
            //사운드 플레이
            GameManager.instance.AddScore(3);
            gObj.SetActive(false);
        }
        else if (tag == "Bronze") {
            //사운드 플레이
            GameManager.instance.AddScore(2);
            gObj.SetActive(false);
        }
    }
 
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.contacts[0].normal.y > 0.8f) {
            //Debug.Log(collision.gameObject.transform.position.y);
            isGrounded = true;
            jumpCount = 0;
        }
    }

    private void OnCollisionExit2D(Collision2D collision) {
        isGrounded = false;
    }
    private void Die() {
        animator.SetTrigger("Die");
        audioSource.clip = deadClip;
        audioSource.Play();

        rb.velocity = Vector2.zero;
        isDead = true;

        GameManager.instance.OnPlayerDeath();
    }
}
