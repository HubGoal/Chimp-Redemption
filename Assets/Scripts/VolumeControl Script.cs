using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VolumeControl : MonoBehaviour
{
    [SerializeField] Slider VolumeSlider;

    private void Start()
    {
        // Suscribirse al evento de carga de escena
        SceneManager.sceneLoaded += OnSceneLoaded;

        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
            Load();
        }
        else
        {
            Load();
        }
    }

    // Método llamado cuando se carga una nueva escena
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Detener la música al cambiar de escena
        StopMusic();
    }

    public void ChangeVolume()
    {
        AudioListener.volume = VolumeSlider.value;
        Save();
    }

    public void Load()
    {
        VolumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
    }

    public void Save()
    {
        PlayerPrefs.SetFloat("musicVolume", VolumeSlider.value);
    }

    // Método para detener la música
    private void StopMusic()
    {
        // Detener la reproducción de la música
        // Asume que tienes un objeto AudioSource para la música en la escena
        AudioSource musicSource = FindObjectOfType<AudioSource>();
        if (musicSource != null)
        {
            musicSource.Stop();
        }
    }

    private void OnDestroy()
    {
        // Desuscribirse del evento al destruir el objeto
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
