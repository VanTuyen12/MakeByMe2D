using UnityEngine;

public class Enemy : MonoBehaviour
{
    
    [SerializeField] private float speed = 2f;
    [SerializeField] private float distance = 5f;
    private Vector3 startPos;
    private bool movingRight = true;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPos = transform.position;//  vị trí cảu enemy đầu
    }

    // Update is called once per frame
    void Update()
    {
        float leftBound = startPos.x - distance;// Điểm giới hạn bên Left
        float rightBound = startPos.x + distance;// Điểm giới hạn bên Right 
        if (movingRight)//Ktra enemy có đang di chuyển Right k
        {
            //transform.Translate thay đổi vị trí của đối tượng bằng cách cộng thêm một vector dịch chuyển vào tọa độ hiện tại
            transform.Translate(Vector2.right * (speed * Time.deltaTime));// di chuyển sang Right
            if (transform.position.x >= rightBound) {//Di chuyển Chạm of qua điểm Giới hạn
                movingRight = false; //Đạt giới hạn thì Dừng F
                Flip();
            }
        }
        else
        {
            transform.Translate(Vector2.left * (speed * Time.deltaTime));
            if (transform.position.x <= leftBound){//Di chuyển Chạm of qua điểm Giới hạn
                movingRight = true;//Đặt lại hương di chuyển bên phải
                Flip();
            }
        }
    }

    void Flip()//Quay mặt Enemy
    {
        Vector3 scaler = transform.localScale;// lấy vị trí cảu Scale trong Tramform
        scaler.x *= -1;//quay đổi Scale
        transform.localScale = scaler;// ép vị trí scale vào vị trí tranform hiện tại
    }
}
