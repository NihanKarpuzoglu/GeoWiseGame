using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotificationHelper : MonoBehaviour
{
    private IEnumerator notificationCoroutine;
    private IEnumerator notificationCoroutine2;

    [SerializeField]
    private Image _notificationImage;
    [SerializeField]
    private Text _notificationText;
    private Color _color;//notification bg color
    private Color _color2;//notification text color

    private void Awake()
    {
        _color = _notificationImage.color;
        _color.a = 0;
        _notificationImage.color = _color;

        _color2 = _notificationText.color;
        _color2.a = 0;
        _notificationText.text = "";
    }
    public void showNotification(string message, int time)
    {
        if (notificationCoroutine != null)
        {
            StopCoroutine(notificationCoroutine);
        }
        if (notificationCoroutine2 != null)
        {
            StopCoroutine(notificationCoroutine2);
        }

        notificationCoroutine = FadeImage(_notificationImage, time);
        notificationCoroutine2 = FadeText(message, time);
        StartCoroutine(notificationCoroutine);
        StartCoroutine(notificationCoroutine2);
    }
    IEnumerator FadeImage(Image notificationImage, int time)
    {
        notificationImage.enabled = true;
        notificationImage.color = new Color(_color.r, _color.g, _color.b, 0);
        for (float i = 0; i <= 1; i += Time.deltaTime)
        {
            notificationImage.color = new Color(_color.r, _color.g, _color.b, i);
            yield return null;
        }

        yield return new WaitForSeconds(time);
        // fade from opaque to transparent
        for (float i = 1; i >= 0; i -= Time.deltaTime)
        {
            notificationImage.color = new Color(_color.r, _color.g, _color.b, i);
            yield return null;
        }

    }
    IEnumerator FadeText(string text, int time)
    {
        _notificationText.text = text;
        _color2 = _notificationText.color;
        _color2.a = 0;
        for (float i = 0; i <= 1; i += Time.deltaTime)
        {
            _notificationText.color = new Color(_color2.r, _color2.g, _color2.b, i);
            yield return null;
        }
        yield return new WaitForSeconds(time);
        for (float i = 1; i >= 0; i -= Time.deltaTime)
        {
            _notificationText.color = new Color(_color2.r, _color2.g, _color2.b, i);
            if (i == 0)
                _notificationText.text = "";
            yield return null;
        }

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
