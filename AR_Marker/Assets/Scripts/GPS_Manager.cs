using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;

public class GPS_Manager : MonoBehaviour
{
    //텍스트 UI 변수
    public Text latitude_text;
    public Text longtitude_text;

    //위도 경도 변수
    public float latitude = 0;
    public float longtitude = 0;

    void Start()
    {
        StartCoroutine(GPS_On());
    }

    //GPS 처리 함수
    public IEnumerator GPS_On()
    {
        //만일 GPS 사용허가를 받지 못했다면 권한 허가 팝업을 띄운다
        if(!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            Permission.RequestUserPermission(Permission.FineLocation);

            while(!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
            {
                yield return null;
            }
        }

        //만일, GPS 장치가 켜져 있지 않으면 위치 정보를 수신할 수 없다고 표시한다
        if(!Input.location.isEnabledByUser)
        {
            latitude_text.text = "GPS Off";
            longtitude_text.text = "GPS Off";
            yield break;
        }
    }
}
