using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.InputSystem; // Ajouté pour le nouveau système

public class AFKHandler : MonoBehaviour
{
    [Header("Configuration")]
    public float timeBeforeFailure = 3f;
    public GameObject gameOverCanvas;
    public TextMeshProUGUI timerText;

    private float idleTimer = 0f;
    private Vector3 lastPosition;
    private bool isGameOver = false;

    void Start()
    {
        lastPosition = transform.position;
        if (timerText != null) timerText.text = "";
    }

    void Update()
    {
        if (isGameOver) return;

        bool hasMovedPhysically = Vector3.Distance(transform.position, lastPosition) > 0.05f;

        // Nouvelle méthode compatible avec le Input System Package
        bool isPressingKeys = false;
        if (Keyboard.current != null)
        {
            isPressingKeys = Keyboard.current.anyKey.isPressed;
        }

        if (hasMovedPhysically || isPressingKeys)
        {
            idleTimer = 0f;
        }
        else
        {
            idleTimer += Time.deltaTime;
        }

        lastPosition = transform.position;

        if (timerText != null)
        {
            if (idleTimer > 0.5f)
            {
                timerText.text = "AFK DETECTED: " + (timeBeforeFailure - idleTimer).ToString("F1") + "s";
            }
            else
            {
                timerText.text = "";
            }
        }

        if (idleTimer >= timeBeforeFailure)
        {
            TriggerFailure();
        }
    }

    void TriggerFailure()
    {
        isGameOver = true;

        if (gameOverCanvas != null)
            gameOverCanvas.SetActive(true);

        if (timerText != null)
            timerText.text = "";

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0f;
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}