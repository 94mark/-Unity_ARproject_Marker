using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    Rigidbody rb;
    bool isReady = true;
    Vector2 startPos;
    // Start is called before the first frame update
    void Start()
    {
        //������ٵ��� ���� �ɷ��� ��Ȱ��ȭ
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isReady)
        {
            return;
        }

        //���� ī�޶� ���� �ϴܿ� ��ġ�Ѵ�
        SetBallPosition(Camera.main.transform);

        if(Input.touchCount > 0 && isReady)
        {
            Touch touch = Input.GetTouch(0);

            //���� ��ġ�� �����ߴٸ�
            if(touch.phase == TouchPhase.Began)
            {
                //��ġ�� ������ �ȼ��� �����Ѵ�
                startPos = touch.position;
            }
            //��ġ�� �����ٸ�
            else if(touch.phase == TouchPhase.Ended)
            {
                //�հ����� �巡���� �ȼ��� y�� �Ÿ��� ���Ѵ�
                float dragDistance = touch.position.y - startPos.y;

                //AR ī�޶� �������� ���� ����(���� 45�� ����)�� �����Ѵ�
                Vector3 throwAngle = (Camera.main.transform.forward + Camera.main.transform.up).normalized;

                //���� �ɷ��� Ȱ��ȭ�ϰ� �غ� ���¸� false�� �ٲ� ���´�
                rb.isKinematic = false;
                isReady = false;

                //���� ���� * �հ��� �巡�� �Ÿ���ŭ ���� ������ ���� ���Ѵ�
                rb.AddForce(throwAngle * dragDistance * 0.005f, ForceMode.VelocityChange);
            }
        }
    }

    void SetBallPosition(Transform anchor)
    {
        //ī�޶��� ��ġ���� ���� �Ÿ���ŭ ������ Ư�� ��ġ�� ����
        Vector3 offset = anchor.forward * 0.5f + anchor.up * -0.2f;
        //���� ��ġ�� ī�޶� ��ġ���� Ư�� ��ġ��ŭ �̵��� �Ÿ��� ����
        transform.position = anchor.position + offset;
    }
}
