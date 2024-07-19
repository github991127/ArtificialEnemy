using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public enum EnemyState
{
    idle,
    run
}
public class AI : MonoBehaviour
{
    public EnemyState CurrentState=EnemyState.idle;
    private Animation ani;
    private Transform raptor;
    private NavMeshAgent agent;
    private Rigidbody rbody;
    public float time;

    void Start()
    {
        ani = GetComponent<Animation>();
        raptor = GameObject.FindWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        rbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //GG
        if (transform.position.x < -13f || transform.position.x> 13f || transform.position.z < -13f || transform.position.z > 13f)
        {
            
            ani.Play("die");
            time += Time.deltaTime;
            if (time > Time.deltaTime * 24)
            {
                Audio.Instance.AudioDie();//����GG��Ч
                //for (int i = 0; i < 9; i++)
                //{
                //    Audio.Instance.AudioDie();//����GG��Ч
                //}
                Destroy(gameObject);
            }
            return;
        }

        float distance = Vector3.Distance(raptor.position, transform.position);
        switch (CurrentState)
        {
            case EnemyState.idle:
                //վ��״̬����Ϊ�ܲ�
                if (distance > 1 && distance <= 9)
                {
                    CurrentState = EnemyState.run;
                }
                ani.Play("idle");
                agent.isStopped = true;//����ֹͣ 
                rbody.velocity = Vector3.zero;
                break;

            case EnemyState.run:
                //׷��״̬����ص�վ��
                if (distance > 9)
                {
                    CurrentState = EnemyState.idle;
                }
                ani.Play("run");
                agent.isStopped = false;//������ʼ
                agent.SetDestination(raptor.position);
                break;
        }
    }
}
