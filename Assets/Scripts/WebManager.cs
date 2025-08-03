using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Andrea Ferruelo
public class WebManager : MonoBehaviour
{
 
    public static WebManager instance;
    [SerializeField] private GameObject _webbedImage;
    [SerializeField] private RectTransform _webbedBar;
    public float webbedTimer;

    private void Start()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    void Update()
    {
        if (webbedTimer > 0)
        {
            webbedTimer -= Time.deltaTime;
            _webbedBar.sizeDelta = new Vector2(840f * webbedTimer / 30f, 68);
            return;
        }
        else if (webbedTimer < 0)
        {
            webbedTimer = 0;
            PlayerActions.instance.webbed = false;
            PlayerActions.instance.player.EndWebbed();
            _webbedImage.SetActive(false);
        }
    }

    public void GetWebbed(float t)
    {
        webbedTimer = t;
        _webbedImage.SetActive(true);
        PlayerActions.instance.webbed = true;
        PlayerActions.instance.spacebarAction = EscapeWeb;
    }

    private void EscapeWeb()
    {
        if (webbedTimer > 0)
        {
            float f = Random.Range(0f, 3f);
            webbedTimer -= f;
        }
    }
}
