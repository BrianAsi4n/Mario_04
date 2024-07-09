using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlappyController : MonoBehaviour
{
    

    [SerializeField] private Camera pCamera;
    [SerializeField] private Transform flappy;
    
    [Header("Config")]
    [SerializeField] private float movementSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float rotationSpeed;

    private Rigidbody2D rb;
    private bool isPause;

    

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        isPause = false;
        
    }

    void Update()
    {
        if (!isPause)
        {
            Move();
            RotateFlappy();
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Flap();
            }
           
        }
    }

    

    private void Move()
    {
        Vector3 moveVector = Vector3.right * Time.deltaTime * movementSpeed;
        transform.Translate(moveVector);
        pCamera.transform.Translate(moveVector);
        //cong vao khoang cach nguoi choi di chuyen vao bo dem
       LevelGenerator.Instance.ShiftDistance += Time.deltaTime * movementSpeed;
    }

    private void RotateFlappy()
    {
        //Xoay chú chim tới góc -45 độ sử dụng hàm lerp
        flappy.rotation = Quaternion.Lerp(Quaternion.Euler(flappy.rotation.eulerAngles),
            Quaternion.Euler(new Vector3(0, 0, -45)),
            Time.deltaTime * rotationSpeed);
    }

    private void Flap()
    {
        //xoay chu chim len goc 45 do
        flappy.rotation = Quaternion.Euler(new Vector3(0, 0, 45));
        rb.velocity = new Vector3(rb.velocity.x, 0);
        rb.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
    }
}
