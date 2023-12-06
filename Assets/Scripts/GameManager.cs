using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public static GameManager instance;

    public bool isGameover = false;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameoverText;

    private int score = 0; 

    void Awake() {
        if (instance == null) {
            instance = this;
        }
        else {
            Debug.Log("���� 2�� �̻��� ���� �Ŵ����� �����մϴ�.");
            Destroy(gameObject);
        }
    }

    void Update() {
        if (isGameover && Input.GetMouseButtonDown(0)) {
            SceneManager.LoadScene(0);
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void AddScore(int newScore) {
        if (!isGameover) {
            score += newScore;
            scoreText.text = "SCORE : " + score.ToString();
        }
    }

    public void OnPlayerDeath() {
        isGameover = true;
        gameoverText.gameObject.SetActive(true);
    }
}
