using UnityEngine;
using UnityEngine.UI;

public class MenuMusicController : MonoBehaviour
{
    public AudioSource menuMusic; // Asigna aqu� la fuente de audio de la m�sica del men�
    public Slider musicSlider; // Asigna aqu� el slider para controlar el volumen de la m�sica

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
        // Puedes agregar l�gica adicional seg�n sea necesario
    }

    // M�todo llamado por el slider para cambiar el volumen de la m�sica del men�
    public void SetMenuMusicVolume(float volume)
    {
        menuMusic.volume = volume;
        PlayerPrefs.SetFloat("MenuMusicVolume", volume);
        PlayerPrefs.Save();
    }
}
