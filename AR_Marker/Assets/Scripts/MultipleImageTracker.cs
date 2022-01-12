using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class MultipleImageTracker : MonoBehaviour
{
    ARTrackedImageManager imageManager;
    // Start is called before the first frame update
    void Start()
    {
        imageManager = GetComponent<ARTrackedImageManager>();

        //�̹��� �ν� ��������Ʈ�� ����� �Լ��� �����Ѵ�
        imageManager.trackedImagesChanged += OnTrackedImage;
    }

    public void OnTrackedImage(ARTrackedImagesChangedEventArgs args)
    {
        //���� �ν��� �̹������� ��� ��ȸ�Ѵ�
        foreach(ARTrackedImage trackedImage in args.added)
        {
            //�̹��� ���̺귯������ �ν��� �̹����� �̸��� �о�´�
            string imageName = trackedImage.referenceImage.name;

            //Resources �������� �ν��� �̹����� �̸��� ������ �̸��� �������� ã�´�
            GameObject imagePrefab = Resources.Load<GameObject>(imageName);

            //����, �˻��� �������� �����Ѵٸ�
            if(imagePrefab != null)
            {
                //�̹����� ��ġ�� �������� �����Ѵ�
                GameObject go = Instantiate(imagePrefab, trackedImage.transform.position, trackedImage.transform.rotation);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
