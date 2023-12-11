using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 3;
    public float jumpPower = 15;
    public float gravity = -50;
    public float rotSpeed = 100;
    float yVelocity = 0;
    float camAngle;
    float bodyAngle;
    //test1
    CharacterController cc;

    public Transform camera;
    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    void Update()
    {
        // �÷��̾� �̵�
        RotateCamera();
        //�¿�� ��ü ȸ�� �ϰ� �ʹ�.
        RotateBody();
        //����� �Է¿� ���� �̵��ϰ� �ʹ�.
        Move();
    }

    private void RotateCamera()
    {
        float value = Input.GetAxis("Mouse Y");
        camAngle += value * rotSpeed * Time.deltaTime;
        camAngle = Mathf.Clamp(camAngle, -40, 25);

        camera.localEulerAngles = new Vector3(-camAngle, 0, 0);
    }
    //�¿�� ��ü ȸ�� �ϰ� �ʹ�.
    private void RotateBody()
    {
        float value = Input.GetAxis("Mouse X");
        bodyAngle += value * rotSpeed * Time.deltaTime;

        transform.eulerAngles = new Vector3(0, bodyAngle, 0);
    }
    private void Move()
    {
        //����� �Է¿� ����
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        //������ �ʿ�
        Vector3 dir = new Vector3(h, 0, v) * speed;
        //ī�޶� �ٶ󺸴� �������� ���� ��ȯ
        dir = camera.TransformDirection(dir);

        // �ٴڿ� ������ ���� �ӵ��� 0���� ����
        if (cc.isGrounded)
        {
            yVelocity = 0;
        }
        //�߷��� �����ϰ� �ʹ�.
        //v = v0 + at
        yVelocity += gravity * Time.deltaTime;
        dir.y = yVelocity;
        //�̵��ϰ� �ʹ�.
        cc.Move(dir * Time.deltaTime);
    }
}
