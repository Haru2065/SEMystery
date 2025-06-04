using UnityEngine;

public class PlayerGrabber : MonoBehaviour
{
    public Transform chair;           //持ちたい椅子(Inspectorで設定)
    public KeyCode grabKey = KeyCode.E;    //Eキーで掴む

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
                //椅子をつかむ
                chair.SetParent(transform);
                //持つ位置を調整
                chair.localPosition = new Vector3(1f, 0f, 0f);
                isHolding = true;
            }
            else
            {
                //椅子を離す
                chair.SetParent(null);
                isHolding = false;

                //離す位置を調整
                chair.position = transform.position + new Vector3(1f, -1f, 0f);
            }
        }
    }

    //プレイヤーが椅子に近づいたとき
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform == chair)
        {
            isNearChair = true;
        }
    }

    //プレイヤーが椅子から離れているとき
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.transform == chair)
        {
            isNearChair = false;
        }
    }
}
