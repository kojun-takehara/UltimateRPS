using UnityEngine;

public class FollowFingerTip : MonoBehaviour
{
    public Transform fingerTipMarker; // 指の先端に配置した空のGameObjectを指定
    private Vector3 initialOffset; // 初期のオフセット位置

    void Start()
    {
        if (fingerTipMarker != null)
        {
            // 初期のオフセット位置を設定
            initialOffset = transform.position - fingerTipMarker.position;
        }
    }

    void LateUpdate()
    {
        if (fingerTipMarker != null)
        {
            // 指先の位置に基づいて追尾
            transform.position = fingerTipMarker.position + initialOffset;
        }
    }
}
