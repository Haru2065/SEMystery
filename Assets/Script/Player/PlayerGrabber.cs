using UnityEngine;

public class PlayerGrabber : MonoBehaviour
{
    public Transform chair;           //���������֎q(Inspector�Őݒ�)
    public KeyCode grabKey = KeyCode.E;    //E�L�[�Œ͂�

    //�֎q��͂�ł��邩
    private bool isHolding  = false;

    /// <summary>
    /// �֎q��͂�ł��邩�̃Q�b�^�[�Z�b�^�[
    /// </summary>
    public bool IsHolding
    { 
        get => isHolding; 
        set => isHolding = value; 
    }

    //�֎q���߂��ɂ��邩
    private bool isNearChair = false;

    private void Update()
    {
        //�֎q���߂��ɂ��邩�A�͂߂邩�ł���E�������ƈ֎q��͂߂āA������
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
