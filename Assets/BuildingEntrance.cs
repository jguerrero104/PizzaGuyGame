using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections; // Include the System.Collections namespace for coroutines

public class BuildingEntrance : MonoBehaviour
{
    public string interiorSceneName; // Name of the scene for the building interior
    public AudioClip interactionSound; // The sound clip to play on interaction
    private AudioSource audioSource; // Audio source component reference
    private bool playerInTrigger = false; // Flag to check if the player is in the trigger collider

    private void Start()
    {
        audioSource = GetComponent<AudioSource>(); // Get the AudioSource component
        if (audioSource == null) // Ensure there is an AudioSource component
        {
            Debug.LogError("No AudioSource component found!");
        }
    }

    private void Update()
    {
        if (playerInTrigger && Input.GetKeyDown(KeyCode.E))
        {
            // Play the interaction sound
            if (audioSource != null && interactionSound != null)
            {
                audioSource.PlayOneShot(interactionSound);
                StartCoroutine(LoadSceneAfterSound(interactionSound.length)); // Start the coroutine with the sound clip length
            }
            else
            {
                // If no sound is available, load the scene immediately
                SceneManager.LoadScene(interiorSceneName);
            }
        }
    }

    private IEnumerator LoadSceneAfterSound(float delay)
    {
        yield return new WaitForSeconds(delay); // Wait for the duration of the sound
        SceneManager.LoadScene(interiorSceneName); // Load the scene after the sound has played
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerCharacter"))
        {
            playerInTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerCharacter"))
        {
            playerInTrigger = false;
        }
    }
}
