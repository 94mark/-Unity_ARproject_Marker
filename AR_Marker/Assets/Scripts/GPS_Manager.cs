using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;

public class GPS_Manager : MonoBehaviour
{
    //�ؽ�Ʈ UI ����
    public Text latitude_text;
    public Text longtitude_text;

    //���� �浵 ����
    public float latitude = 0;
    public float longtitude = 0;

    void Start()
    {
        StartCoroutine(GPS_On());
    }

    //GPS ó�� �Լ�
    public IEnumerator GPS_On()
    {
        //���� GPS ����㰡�� ���� ���ߴٸ� ���� �㰡 �˾��� ����
        if(!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            Permission.RequestUserPermission(Permission.FineLocation);

            while(!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
            {
                yield return null;
            }
        }

        //����, GPS ��ġ�� ���� ���� ������ ��ġ ������ ������ �� ���ٰ� ǥ���Ѵ�
        if(!Input.location.isEnabledByUser)
        {
            latitude_text.text = "GPS Off";
            longtitude_text.text = "GPS Off";
            yield break;
        }
    }
}
