using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallController : MonoBehaviour
{
    public float resetTime = 3.0f;
    public float captureRate = 0.3f;  //��ȹ Ȯ��(30%)
    public Text result;
    public GameObject effect;

    Rigidbody rb;
    bool isReady = true;
    Vector2 startPos;
    // Start is called before the first frame update
    void Start()
    {
        //��ȹ ��� �ؽ�Ʈ�� ���� ���·� �ʱ�ȭ�Ѵ�
        result.text = "";

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

                //3�� �Ŀ� ���� ��ġ �� �ӵ��� �ʱ�ȭ
                Invoke("ResetBall", resetTime);
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


    private void ResetBall()
    {
        //�����ɷ��� ��Ȱ��ȭ�ϰ� �ӵ��� �ʱ�ȭ�Ѵ�
        rb.isKinematic = true;
        rb.velocity = Vector3.zero;

        //�غ� ���·� �����Ѵ�
        isReady = true;

        gameObject.SetActive(true);
    }


    private void OnCollisionEnter(Collision collision)
    {
        //�غ� ���¶�� �浹 ó�� �̺�Ʈ �Լ��� �׳� �����Ų��
        if(isReady)
        {
            return;
        }
        //��ȹ Ȯ���� ��÷�Ѵ�(0 ~ 1.0 ������ �Ǽ�)
        float draw = Random.Range(0, 1.0f);

        if(draw <= captureRate)
        {
            result.text = "��ȹ ����!";
        }
        else
        {
            result.text = "��ȹ�� ������ �����ƽ��ϴ�...";
        }
        //����Ʈ�� �����Ѵ�
        Instantiate(effect, collision.transform.position, Camera.main.transform.rotation);

        //����� ĳ���͸� �����ϰ� ���� ��Ȱ��ȭ�Ѵ�
        Destroy(collision.gameObject);
        gameObject.SetActive(false);
    }
}
