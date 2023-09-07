using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EnterHouse : MonoBehaviour
{


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            /*            PlayerPrefs.SetInt("Lives", Data.Instance.live);
                        PlayerPrefs.SetInt("Cherry", Data.Instance.Cherry);
                        PlayerPrefs.SetInt("ShootTime", Data.Instance.shoot_time);*/
            Data.Instance.UpLoad();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        }
    }
}
