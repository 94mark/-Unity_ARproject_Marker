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
    public float maxWaitTime = 10.0f;

    //���� �浵 ����
    public float latitude = 0;
    public float longtitude = 0;

    float waitTime = 0;

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

        //��ġ �����͸� ��û�Ѵ� -> ���� ���
        Input.location.Start();

        //GPS ���� ���°� �ʱ� ���¿��� ���� �ð� ���� ����Ѵ�
        while(Input.location.status == LocationServiceStatus.Initializing && waitTime < maxWaitTime)
        {
            yield return new WaitForSeconds(1.0f);
            waitTime++;
        }

        //���� ���� �� ������ ���еƴٴ� ���� ���
        if(Input.location.status == LocationServiceStatus.Failed)
        {
            latitude_text.text = "��ġ ���� ���� ����";
            longtitude_text.text = "��ġ ���� ���� ����";
        }

        //���� ��� �ð��� �Ѿ���� ������ �����ٸ� �ð� �ʰ������� ����Ѵ�
        if(waitTime >= maxWaitTime)
        {
            latitude_text.text = "���� ��� �ð� �ʰ�";
            longtitude_text.text = "���� ��� �ð� �ʰ�";
        }

        //���ŵ� GPS �����͸� ȭ�鿡 ����Ѵ�
        LocationInfo li = Input.location.lastData;
        latitude = li.latitude;
        longtitude = li.longitude;
        latitude_text.text = "���� : " + latitude.ToString();
        longtitude_text.text = "�浵 : " + longtitude.ToString();
    }
}
