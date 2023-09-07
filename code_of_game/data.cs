/*using UnityEditorInternal;*/
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
/*using UnityEngine.UIElements;*/

public class Data : MonoBehaviour
{
    public static Data Instance;
    public int Cherry = 0;
    public Text CherryNum;
    public bool shoot_able = false;
    public int shoot_time = 0;
    public int shoot_max = 100;
    public Text shoottime;
    public float shoot_speed = 3;
    public int live = 4;
    public Text LiveNum;
    public bool on = true;
    public AudioSource cherryAudio,hurtedAudio,itemAudio;
    public Text dialog_text;
    public GameObject Lose_Dialog;
    public bool playState = true;
    public GameObject Win_Dialog;
/*    public AudioMixer audioMixer;
    public Slider Main_CTL;
    public Slider BGM_CTL;
    public Slider Music_CTL;*/
    //private GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        /*        Cherry = PlayerPrefs.GetInt("Cherry");
                live = PlayerPrefs.GetInt("Lives");
                shoot_time = PlayerPrefs.GetInt("ShootTime");
                LiveNum.text = live.ToString();
                CherryNum.text = Cherry.ToString();
                shoottime.text = shoot_time.ToString();*/
/*        SetVolume();*/
        Get_Data();
        if(shoot_time > 0)
        {
            shoot_able = true;
        }
        //Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    //��ʼ��
    private void Awake()
    {
        if (Instance == null)
        {
            Instance= this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    //���ӣ�ң�����ӳ��ui��
    public void Cherry_add()
    {
        Cherry += 1;
        cherryAudio.Play();
        CherryNum.text = Cherry.ToString();
    }
    //ʧȥӣ��
    public void Cherry_del(int num)
    {
        Cherry -= num;
        CherryNum.text = Cherry.ToString();
    }
    //��ÿ�ǹ����������ui����ʾ��ǰʣ���ӵ���Ŀ
    public void Get_Gun()
    {
        shoot_able = true;
        shoot_time = shoot_max;
        shoottime.text = shoot_time.ToString();
        itemAudio.Play();
    }

    public void Gun_shooting()
    {
        shoot_time -= 1;
        shoottime.text = shoot_time.ToString();
        if (shoot_time == 0)
        {
            shoot_able = false;
        }
    }

    public void Add_ShootTime()
    {
        shoot_max += 5;
        itemAudio.Play();
    }

    public void shoot_speed_change(int dir)
    {
        if (dir > 0)
        {
            shoot_speed = Mathf.Abs(shoot_speed);
        }
        else
        {
            shoot_speed = -Mathf.Abs(shoot_speed);
        }
    }

    public void live_del()
    {
        live -= 1;
        LiveNum.text = live.ToString();
        hurtedAudio.Play();
        if (live == 0)
        {
            //ExitGame();
            //������������ʹ��,�ӳ�2s��������
            //SceneManager.LoadScene(0);
            //Destroy(Player); 
            //Player = null;
            Time.timeScale = 0;
            Lose_Dialog.SetActive(true);
            //GetComponent<AudioSource>().enabled = false;
            //Invoke("Restart", 2f);
        }
    }

    public void Live_add()
    {
        live += 1;
        LiveNum.text = live.ToString();
        itemAudio.Play();
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
         UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void switch_case()
    {
        on = !on;
    }

    public void dialog_text_change(string res)
    {
        dialog_text.text = res;
    }

    public void Restart()
    {
        Time.timeScale = 1;
        PlayerPrefs.SetInt("Lives", 6);
        PlayerPrefs.SetInt("Cherry", 0);
        PlayerPrefs.SetInt("ShootTime", 0);
        PlayerPrefs.SetInt("ShootMax", 10);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void UpLoad()
    {
        PlayerPrefs.SetInt("Lives", live);
        PlayerPrefs.SetInt("Cherry", Cherry);
        PlayerPrefs.SetInt("ShootTime",shoot_time);
        PlayerPrefs.SetInt("ShootMax", shoot_max);
    }

    public void Get_Data()
    {
        Cherry = PlayerPrefs.GetInt("Cherry");
        live = PlayerPrefs.GetInt("Lives");
        shoot_time = PlayerPrefs.GetInt("ShootTime");
        shoot_max = PlayerPrefs.GetInt("ShootMax");
        LiveNum.text = live.ToString();
        CherryNum.text = Cherry.ToString();
        shoottime.text = shoot_time.ToString();
        
    }

    public void Win()
    {
        Time.timeScale = 0;
        Win_Dialog.SetActive(true);
    }

/*    public void SetVolume()
    {
        float valueDB = 0;
        Main_CTL = GameObject.Find("Main_CTL").GetComponent<Slider>();
        audioMixer.GetFloat("MasterVolume", out valueDB);
        Main_CTL.value = valueDB;
        BGM_CTL = GameObject.Find("BGM_CTL").GetComponent<Slider>();
        audioMixer.GetFloat("BGM_Volume", out valueDB);
        BGM_CTL.value = valueDB;
        Music_CTL = GameObject.Find("Music_CTL").GetComponent<Slider>();
        audioMixer.GetFloat("Music_Volume", out valueDB);
        Music_CTL.value = valueDB;

    }*/

/*    // �����������ĺ���
    public void SetMainVolume()
    {
        audioMixer.SetFloat("MasterVolume", Main_CTL.value);// MasterVolumeΪ���Ǳ�¶������Master�Ĳ���
    }

    public void SetMusicVolume()
    {
        audioMixer.SetFloat("Music1Param", Music_CTL.value);// Music1ParamΪ���Ǳ�¶������Music�Ĳ���
    }

    public void SetBGMVolume()
    {
        audioMixer.SetFloat("Music2Param", BGM_CTL.value);// Music2ParamΪ���Ǳ�¶������SoundEffect�Ĳ���
    }*/

}
