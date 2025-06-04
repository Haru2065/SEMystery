using UnityEngine;

public class PlayerGrabber : MonoBehaviour
{
    public Transform chair;           //���������֎q(Inspector�Őݒ�)
    public KeyCode grabKey = KeyCode.E;    //E�L�[�Œ͂�

    private bool isHolding  = false;

    public bool IsHolding
    { 
        get => isHolding; 
        set => isHolding = value; 
    }


    private bool isNearChair = false;

    private void Update()
    {
        if((isNearChair || isHolding) && Input.GetKeyDown(grabKey))
        {
            if(!isHolding)
            {
                //�֎q������
                chair.SetParent(transform);
                //���ʒu�𒲐�
                chair.localPosition = new Vector3(1f, 0f, 0f);
                isHolding = true;
            }
            else
            {
                //�֎q�𗣂�
                chair.SetParent(null);
                isHolding = false;

                //�����ʒu�𒲐�
                chair.position = transform.position + new Vector3(1f, -1f, 0f);
            }
        }
    }

    //�v���C���[���֎q�ɋ߂Â����Ƃ�
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform == chair)
        {
            isNearChair = true;
        }
    }

    //�v���C���[���֎q���痣��Ă���Ƃ�
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.transform == chair)
        {
            isNearChair = false;
        }
    }
}
