using System;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;
    [SerializeField] private float speed = 2f;
    private Vector3 target;
    private Transform Player; 
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        target = pointA.position;
    }

    // Update is called once per frame
    void Update()
    {
        //di chuyển một điểm từ vị trí hiện tại đến vị trí mục tiêu với tốc độ giới hạn
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);//di chuyển từ vtri max định đến target
        
        //tính khoảng cách(ko phải vị trí) Euclidean giữa hai điểm trong không gian 3D
        //nếu kc 2đ nhỏ hơn 0.1f thì
        if (Vector3.Distance(transform.position,target) < 0.1f) //=> Kết quả Vector3.Distance() là một giá trị float
        {
            if (target == pointA.position) // Ktra target = điểm A chưa
            {
                target = pointB.position; // chuyển target sang điểm B
            }
            else
            {
                target = pointA.position; // Nếu GameObject chưa đến thì chạy tiếp đến target(đA)
            }
        }
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.SetParent(transform);//Đặt Player con của Platform để player di chuyển theo Platform
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.SetParent(null);//Đặt lại vtri Player 
        }
    }
}
