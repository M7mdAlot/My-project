using UnityEngine;
using TMPro;

/// <summary>
/// Tracks how many coins the player has collected. Counts every Coin in the scene at
/// start, updates a top-left counter, and shows a "win" panel once they are all gathered.
///
/// Setup (empty GameObject named "GameManager" with this script):
///  - coinText : the TextMeshPro text in the top-left corner.
///  - winPanel : the UI panel to reveal on victory (leave it disabled in the scene).
/// </summary>
public class GameManager : MonoBehaviour
{
    // A single shared instance so coins can reach it with GameManager.Instance.
    public static GameManager Instance { get; private set; }

    [Header("UI References")]
    [Tooltip("Top-left counter text (TextMeshPro).")]
    public TMP_Text coinText;
    [Tooltip("Panel shown when every coin is collected. Keep it disabled at start.")]
    public GameObject winPanel;

    [Header("Optional")]
    [Tooltip("Freeze the game (time) when the player wins.")]
    public bool pauseOnWin = false;

    private int _collected;
    private int _total;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        // Count every coin currently in the scene.
        _total = FindObjectsByType<Coin>(FindObjectsSortMode.None).Length;

        if (winPanel != null) winPanel.SetActive(false);
        UpdateUI();
    }

    /// <summary>Called by a Coin when the player picks it up.</summary>
    public void CollectCoin()
    {
        _collected++;
        UpdateUI();

        if (_collected >= _total && _total > 0)
            Win();
    }

    void UpdateUI()
    {
        if (coinText != null)
            coinText.text = "Coins: " + _collected + " / " + _total;
    }

    void Win()
    {
        if (winPanel != null) winPanel.SetActive(true);
        if (pauseOnWin) Time.timeScale = 0f;
    }
}
