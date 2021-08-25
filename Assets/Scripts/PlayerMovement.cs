using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public ParticleSystem dustTrail;

    float speed = 5f;
    float defaultSpeed = 5f;

    float dashSpeed = 12f;
    float dashPeriod = 0.7f;

    bool dash = false;
    bool isDashing = false;
    float dashNumber = 0f;
    float dashWait = 2f;
    float dashPeriodNum = 0f;

    void Start()
    {
        speed = defaultSpeed;
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);

            controller.Move(direction * speed * Time.deltaTime);
        }

        // Dashing

        if (dashNumber >= dashWait)
        {
            dash = true;
        }
        else
        {
            dash = false;
            dashNumber = dashNumber + Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (direction != Vector3.zero)
            {
                if (dash)
                {
                    speed = dashSpeed;
                    dashNumber = 0f;
                    isDashing = true;
                    dash = false;
                    dashPeriodNum = 0f;
                }
            }
        }

        // Dashing
        if (isDashing)
        {
            dashPeriodNum = dashPeriodNum + Time.deltaTime;
            dustTrail.enableEmission = true;
        }
        else
        {
            speed = defaultSpeed;
            dustTrail.enableEmission = false;
        }

        if (dashPeriodNum >= dashPeriod)
        {
            isDashing = false;
            dash = false;
        }
    }
}
