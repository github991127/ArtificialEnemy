using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Animation ani;
    private Rigidbody rbody;
    public float jumpHeight = 3f; // 跳跃的高度
    public float jumpDuration = 1f; // 跳跃的持续时间
    private float elapsedTime = 0f; // 记录已经过去的时间
    public float time;

    void Start()
    {
        ani = GetComponent<Animation>();
        rbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        //GG
        if (transform.position.y < -1f)
        {
            
            ani.Play("die");
            time += Time.deltaTime;
            if (time > Time.deltaTime * 240)
            {
                Destroy(gameObject);
                Debug.Log("Debug.Log(SceneManager.LoadScene(0)");
                SceneManager.LoadScene(0);
            }
            return;

        }

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 dir = new Vector3(horizontal, 0, vertical);

        if (dir != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(dir);
            ani.Play("run");
            float speedUP = 5f;
            if (Input.GetKey("j"))//冲刺
            {
                speedUP = 10f;
            }
            transform.Translate(Vector3.forward * Time.deltaTime* speedUP);
        }
        else
        {
            ani.Play("idle");
        }

        if (Input.GetKey("k") && rbody.velocity.y==0) // 跳跃
        {
            rbody.velocity = new Vector3(rbody.velocity.x, rbody.velocity.y + 6f, rbody.velocity.z);
        }


    }


}
