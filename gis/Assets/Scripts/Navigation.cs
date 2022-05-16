using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.Notifications.Android;
public class Navigation : MonoBehaviour
{
    public GameObject mainMenuScreen;
    public GameObject dataControlScreen;
    public GameObject liveStatusScreen;
    public GameObject energyStatusScreen;
    public GameObject windEnergyScreen;
    public GameObject solarEnergyScreen;
    public GameObject energyChoosen;
    public GameObject dataComment;
    public GameObject dataValue;
    public GameObject dataValue2;
    public GameObject solarYear;
    public GameObject graphicsContainer;
    public GameObject windYear;
    public GameObject graphicsContainerWind;
    public Color cylinderGreen;
    public Color cylinderRed;
    public GameObject cylinderStatus;
    public GameObject cylinders;
    public GameObject graphicsContainerEnergy;
    List<bool> cols=new List<bool>();
    public float xsense, ysense, coeff;
    private void Start()
    {
        var channel = new AndroidNotificationChannel()
        {
            Id = "channel_id",
            Name = "Default Channel",
            Importance = Importance.Default,
            Description = "Generic notifications",
        };
        AndroidNotificationCenter.RegisterNotificationChannel(channel);
        for (int i = 0; i < 6; i++)
        {
            cols.Add(true);
        }
        turnMain();
    }
    private void Update()
    {
        for (int i = 0; i < 6; i++)
        {
            cols[i] = (PlayerPrefs.GetInt(i.ToString())==1);
        }
        bool boolean = true;
        for (int i = 0; i < 6; i++)
        {
            if (cols[i] == false) boolean = false;
        }
        if (boolean)
        {
            cylinderStatus.GetComponent<TMP_Text>().text = "Cihaz Saðlam";
        }
        else
        {
            string str="";
            int len = 0;
            for (int i = 0; i < 6; i++)
            {
                if (cols[i] == true) continue;
                len++;
                if (str.Length != 0) str += ", ";
                str += (i+1).ToString();
            }
            str += " Numaralý Silindir"+((len==1)?"":"ler")+" Hasarlý";
            cylinderStatus.GetComponent<TMP_Text>().text = str;
            AndroidNotificationCenter.CancelAllDisplayedNotifications();
            var notification = new AndroidNotification();
            notification.Title = "Serpentes";
            notification.Text = "Cihaz Hasarlý. Lütfen kontrol ediniz.";
            notification.LargeIcon = "icon_1";
            AndroidNotificationCenter.SendNotification(notification, "channel_id");
        }
        for (int i = 0; i < 6; i++)
        {
            if (cols[i] == true) cylinders.transform.GetChild(i).gameObject.GetComponent<RawImage>().color = cylinderGreen;
            else cylinders.transform.GetChild(i).GetComponent<RawImage>().color = cylinderRed;
        }
        int chos = 0;
        switch (solarYear.GetComponent<TMP_Text>().text)
        {
            case "2022": chos = 0; break;
            case "2021": chos = 1; break;
            case "2020": chos = 2; break;
            case "2019": chos = 3; break;
            case "2018": chos = 4; break;
            case "2017": chos = 5; break;
            case "2016": chos = 6; break;
            case "Son 3 Yýl": chos = 7; break;
            case "Son 5 Yýl": chos = 8; break;
            default: chos = 9; break;
        }
        for (int i = 0; i < 10; i++)
        {
            if (i == chos) graphicsContainer.transform.GetChild(i).gameObject.SetActive(true);
            else graphicsContainer.transform.GetChild(i).gameObject.SetActive(false);
        }
        chos = 0;
        switch (windYear.GetComponent<TMP_Text>().text)
        {
            case "2022": chos = 0; break;
            case "2021": chos = 1; break;
            case "2020": chos = 2; break;
            case "2019": chos = 3; break;
            case "2018": chos = 4; break;
            case "2017": chos = 5; break;
            case "2016": chos = 6; break;
            case "Son 3 Yýl": chos = 7; break;
            case "Son 5 Yýl": chos = 8; break;
            default: chos = 9; break;
        }
        for (int i = 0; i < 10; i++)
        {
            if (i == chos) graphicsContainerWind.transform.GetChild(i).gameObject.SetActive(true);
            else graphicsContainerWind.transform.GetChild(i).gameObject.SetActive(false);
        }
        chos = 0;
        switch (energyChoosen.GetComponent<TMP_Text>().text)
        {
            case "2022": chos = 0; break;
            case "2021": chos = 1; break;
            case "2020": chos = 2; break;
            case "2019": chos = 3; break;
            case "2018": chos = 4; break;
            case "2017": chos = 5; break;
            case "2016": chos = 6; break;
            case "Son 3 Yýl": chos = 7; break;
            case "Son 5 Yýl": chos = 8; break;
            default: chos = 9; break;
        }
        for (int i = 0; i < 10; i++)
        {
            if (i == chos) graphicsContainerEnergy.transform.GetChild(i).gameObject.SetActive(true);
            else graphicsContainerEnergy.transform.GetChild(i).gameObject.SetActive(false);
        }
    }
    public void energyScreen()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        mainMenuScreen.SetActive(false);
        dataControlScreen.SetActive(true);
        liveStatusScreen.SetActive(false);
        energyStatusScreen.SetActive(false);
        windEnergyScreen.SetActive(false);
        solarEnergyScreen.SetActive(false);
    }
    public void statusScreen()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        mainMenuScreen.SetActive(false);
        dataControlScreen.SetActive(false);
        liveStatusScreen.SetActive(true);
        energyStatusScreen.SetActive(false);
        windEnergyScreen.SetActive(false);
        solarEnergyScreen.SetActive(false);
    }
    public void climateScreen()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        mainMenuScreen.SetActive(false);
        dataControlScreen.SetActive(false);
        liveStatusScreen.SetActive(false);
        energyStatusScreen.SetActive(true);
        windEnergyScreen.SetActive(false);
        solarEnergyScreen.SetActive(false);
    }
    public void turnMain()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        mainMenuScreen.SetActive(true);
        dataControlScreen.SetActive(false);
        liveStatusScreen.SetActive(false);
        energyStatusScreen.SetActive(false);
        windEnergyScreen.SetActive(false);
        solarEnergyScreen.SetActive(false);
    }
    public void solarStatistics()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        mainMenuScreen.SetActive(false);
        dataControlScreen.SetActive(false);
        liveStatusScreen.SetActive(false);
        energyStatusScreen.SetActive(false);
        windEnergyScreen.SetActive(false);
        solarEnergyScreen.SetActive(true);
    }
    public void windStatistics()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        mainMenuScreen.SetActive(false);
        dataControlScreen.SetActive(false);
        liveStatusScreen.SetActive(false);
        energyStatusScreen.SetActive(false);
        windEnergyScreen.SetActive(true);
        solarEnergyScreen.SetActive(false);
    }
    public string getValue(string value)
    {
        return "N/A";
        //Firebase Yazýlacak
    }
    public void brokeOne()
    {
        List<int> arr = new List<int>();

        for (int i = 0; i < cols.Count; i++)
        {
            if (cols[i] == true)
            {
                arr.Add(i);
            }
        }
        if (arr.Count == 0) return;
        PlayerPrefs.SetInt(arr[Random.Range(0, arr.Count - 1)].ToString(),0);
    }
    public void fixOne()
    {
        List<int> arr = new List<int>();
        for (int i = 0; i < cols.Count; i++)
        {
            if (cols[i] == false)
            {
                arr.Add(i);
            }
        }
        if (arr.Count == 0) return;
        PlayerPrefs.SetInt(arr[Random.Range(0, arr.Count - 1)].ToString(), 1);
    }
}