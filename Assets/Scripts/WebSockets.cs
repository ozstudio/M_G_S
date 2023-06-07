using System.Collections;
using System.Collections.Generic;
using WebSocketSharp;
using UnityEngine;
using UnityEngine.UI;
using System.Xml.Serialization;
using System.IO;
using System.Linq;
using UnityEngine.Windows;
using Newtonsoft.Json;

[System.Serializable]
public class WebSockets : MonoBehaviour
{
    WebSocket webSocket;

    public GameObject odo;
    [SerializeField]
    VLCPlayerExample playerExample;
    [SerializeField]
    private Slider _volSlider;
    [SerializeField]
    private Slider _soundsSlider;
    [SerializeField]
    private Toggle _toggleMus;
    [SerializeField]
    private Toggle _toggleSounds;
    [SerializeField]
    private Text adressText;
    [SerializeField]
    private Text portText;
    private string _odoLc = "0";
    [SerializeField]
    private AudioSource _audioClip;

    private TextAsset _textAsset;

    // Start is called before the first frame update
    void Start()
    {
        ReadFromConfigFile();
        webSocket = new WebSocket( "ws://" + adressText.text +":" + portText.text + "/ws" );
        webSocket.OnMessage += (sender, e) => {
            _odoLc = e.Data;
            print(_odoLc);
            ResponseFromServerFunc();
            ChangeOdoText();

        };
       
        webSocket.Connect();
    }
    private void Update()
    {
        odo.GetComponent<Text>().text = _odoLc;
    }
    void ChangeOdoText()
    {
        odo.GetComponent<Text>().text = _odoLc;
    }
   

    void ResponseFromServerFunc()
    {
        print( _odoLc);
        var t = JsonUtility.FromJson<ResponseFromServer>(_odoLc);
        print(t + "from func");
    }
    private void OnDisable()
    {
        webSocket.Close();
    }
    public void Play()
    {
        _audioClip.Stop();
        playerExample.Open();
    }
    public void VolMusic()
    {
        playerExample.SetVolume(int.Parse(_volSlider.value.ToString()));
    }
    public void VolSounds()
    {
        _audioClip.volume = _soundsSlider.value;
    }
    public void ToggleSounds()
    {
        
        if (_toggleSounds.isOn)
        {
            _audioClip.Play();
        }
        else
            _audioClip.Pause();



    }

    public void ToggleMusic()
    {
        if (_toggleMus.isOn)
        {
            playerExample.SetVolume(int.Parse(_volSlider.value.ToString()));
        }
        else
          playerExample.SetVolume(0);
    }

    private void ReadFromConfigFile()
    {
        string path = Application.dataPath + "/config.txt";
        StreamReader reader = new StreamReader(path);
        var adr = System.IO.File.ReadLines(path).ElementAt(0)
            .Substring(14);
        var port = System.IO.File.ReadLines(path).ElementAt(1)
            .Substring(5);
        adressText.text = adr;
        portText.text = port;
        


        reader.Close();
    }

}
