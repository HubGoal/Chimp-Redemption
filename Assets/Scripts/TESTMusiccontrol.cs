using UnityEngine;
using UnityEngine.UI;

public class MenuMusicController : MonoBehaviour
{
    public AudioSource menuMusic; // Asigna aquí la fuente de audio de la música del menú
    public Slider musicSlider; // Asigna aquí el slider para controlar el volumen de la música

    void Start()
    {
        if (menuMusic == null || musicSlider == null)
        {
            Debug.LogError("Asigna la fuente de audio y el slider en el inspector.");
            return;
        }

        // Establece el volumen inicial
        menuMusic.volume = PlayerPrefs.GetFloat("MenuMusicVolume", 1.0f);
        musicSlider.value = menuMusic.volume;
    }

    void Update()
    {
        // Puedes agregar lógica adicional según sea necesario
    }

    // Método llamado por el slider para cambiar el volumen de la música del menú
    public void SetMenuMusicVolume(float volume)
    {
        menuMusic.volume = volume;
        PlayerPrefs.SetFloat("MenuMusicVolume", volume);
        PlayerPrefs.Save();
    }
}
