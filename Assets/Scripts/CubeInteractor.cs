using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Interactive object: when the player is standing next to it and presses the interact
/// key, it starts the cube's animation.
///
/// Setup on THIS object (the thing you walk up to):
///  - A Collider with "Is Trigger" ticked (the area the player must be inside).
///  - This script.
///  - Drag the cube's Animator into "Cube Animator".
///
/// The cube's Animator should have a Trigger parameter (default name "Play") and a
/// transition from an idle/empty state into the animation state, using that trigger.
/// (Uses the new Input System — no project settings change or restart needed.)
/// </summary>
public class CubeInteractor : MonoBehaviour
{
    [Header("What to animate")]
    [Tooltip("The Animator on the cube you made the animation for.")]
    public Animator cubeAnimator;
    [Tooltip("Trigger parameter name inside the cube's Animator Controller.")]
    public string triggerName = "Play";

    [Header("Interaction")]
    [Tooltip("Which key starts the animation. Pick from the dropdown (default E).")]
    public Key interactKey = Key.E;
    [Tooltip("Only allow starting it once.")]
    public bool oneShot = false;

    [Header("Optional")]
    [Tooltip("A 'Press E' hint object shown while the player is in range (can be left empty).")]
    public GameObject prompt;

    private bool _playerInRange;
    private bool _used;

    void Start()
    {
        if (prompt != null) prompt.SetActive(false);
    }

    void Update()
    {
        if (!_playerInRange) return;
        if (oneShot && _used) return;

        var keyboard = Keyboard.current;
        if (keyboard != null && keyboard[interactKey].wasPressedThisFrame)
        {
            if (cubeAnimator != null)
                cubeAnimator.SetTrigger(triggerName);

            _used = true;
            if (oneShot && prompt != null) prompt.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        _playerInRange = true;
        if (prompt != null && !(oneShot && _used)) prompt.SetActive(true);
    }

    void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        _playerInRange = false;
        if (prompt != null) prompt.SetActive(false);
    }
}
