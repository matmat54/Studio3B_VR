using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private const string PlayerPosXKey = "PlayerPosX";
    private const string PlayerPosYKey = "PlayerPosY";
    private const string PlayerPosZKey = "PlayerPosZ";

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void LoadVideo1()
    {
        SceneManager.LoadScene("Video1");
    }

    public void LoadVideo2()
    {
        SceneManager.LoadScene("Video2");
    }

    public void LoadVideo3()
    {
        SceneManager.LoadScene("Video3");
    }

    public void LoadLaifRoom()
    {
        // When loading LaifRoom, the OnSceneLoaded callback will restore the player position
        SceneManager.LoadScene("LaifRoom");
    }

    public void SavePlayerPosition(Transform player)
    {
        PlayerPrefs.SetFloat(PlayerPosXKey, player.position.x);
        PlayerPrefs.SetFloat(PlayerPosYKey, player.position.y);
        PlayerPrefs.SetFloat(PlayerPosZKey, player.position.z);
        PlayerPrefs.Save();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "LaifRoom")
        {
            // When LaifRoom is loaded, find the player and anchor to restore position
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            GameObject anchor = GameObject.Find("PlayerSpawnAnchor");
            if (player != null && anchor != null)
            {
                RestorePlayerPosition(player.transform, anchor.transform);
            }
        }
    }

    private void RestorePlayerPosition(Transform player, Transform anchor)
    {
        float x = PlayerPrefs.GetFloat(PlayerPosXKey, anchor.position.x);
        float y = PlayerPrefs.GetFloat(PlayerPosYKey, anchor.position.y);
        float z = PlayerPrefs.GetFloat(PlayerPosZKey, anchor.position.z);
        player.position = new Vector3(x, y, z);
    }

    public void QuitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
