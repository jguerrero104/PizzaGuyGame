using UnityEngine;
using UnityEngine.SceneManagement;

public class BuildingEntrance : MonoBehaviour
{
    public string interiorSceneName; // Name of the scene for the building interior
    private bool playerInTrigger = false; // Flag to check if the player is in the trigger collider

    private void Update()
    {
        if (playerInTrigger && Input.GetKeyDown(KeyCode.E))
        {
            // Load the interior scene
            SceneManager.LoadScene(interiorSceneName);
        }
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
