
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Menu : MonoBehaviour
{
    public GameObject pauseMenu;

    public void PlayGame()
    {
        /*        PlayerPrefs.SetInt("Lives", 6);
                PlayerPrefs.SetInt("Cherry", 0);
                PlayerPrefs.SetInt("ShootTime", 0);
                PlayerPrefs.SetInt("ShootMax", 10);*/
        My_Data(6,0,0,10);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void PlayEasy()
    {
        /*        PlayerPrefs.SetInt("Lives", 6);
                PlayerPrefs.SetInt("Cherry", 0);
                PlayerPrefs.SetInt("ShootTime", 0);
                PlayerPrefs.SetInt("ShootMax", 10);*/
        My_Data(999, 0, 0, 10);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Return_Menu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void Restart()
    {
        Time.timeScale = 1;
        My_Data(10,0,0,10);
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        SetSlider();
        pauseMenu.SetActive(true);
        Data.Instance.playState = false;
    }

    public void Resume()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        Data.Instance.playState = true;
    }

    public void My_Data(int lives,int Cherry,int ShootTime,int ShootMax)
    {
        PlayerPrefs.SetInt("Lives", lives);
        PlayerPrefs.SetInt("Cherry", Cherry);
        PlayerPrefs.SetInt("ShootTime", ShootTime);
        PlayerPrefs.SetInt("ShootMax", ShootMax);
    }

    public void Continue()
    {
        Time.timeScale = 1;
        Data.Instance.UpLoad();
        SceneManager.LoadScene(1);
    }


    public AudioMixer audioMixer;    // 进行控制的Mixer变量
    public Slider Main_CTL;
    public Slider BGM_CTL;
    public Slider Music_CTL;

    public void SetMainVolume()
    {
        audioMixer.SetFloat("MasterVolume", Main_CTL.value);
        // MasterVolume为我们暴露出来的Master的参数
    }

    public void SetMusicVolume()    // 控制背景音乐音量的函数
    {
        audioMixer.SetFloat("Music_Volume", Music_CTL.value);
        // MusicVolume为我们暴露出来的Music的参数
    }

    public void SetBGMVolume()    // 控制音效音量的函数
    {
        audioMixer.SetFloat("BGM_Volume", BGM_CTL.value);
        // SoundEffectVolume为我们暴露出来的SoundEffect的参数
    }

    void SetSlider()
    {
        float v;
        audioMixer.GetFloat("MasterVolume", out v);
        Main_CTL.value = v;
        audioMixer.GetFloat("Music_Volume", out v);
        Music_CTL.value = v;
        audioMixer.GetFloat("BGM_Volume", out v);
        BGM_CTL.value = v;
    }
/*    public void SetMainVolume()
    {
        audioMixer.SetFloat("MasterVolume", Main_CTL.value);// MasterVolume为我们暴露出来的Master的参数
    }

    public void SetMusicVolume()
    {
        audioMixer.SetFloat("Music1Param", Music_CTL.value);// Music1Param为我们暴露出来的Music的参数
    }

    public void SetBGMVolume()
    {
        audioMixer.SetFloat("Music2Param", BGM_CTL.value);// Music2Param为我们暴露出来的SoundEffect的参数
    }*/

}
