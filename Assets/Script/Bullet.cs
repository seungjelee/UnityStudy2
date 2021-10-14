﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // 총알을 발사할 플레이어의 고유 번호
    public GameObject effect;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * 1000.0f);
        Destroy(this.gameObject, 3.0f);
    }

    private void OnCollisionEnter(Collision coll)
    {
        // 충돌 지점 추출 =>GetContact()
        var contact = coll.GetContact(0);
        // 충돌 지점에 스파크 이펙트 생성
        var obj = Instantiate(effect, contact.point, Quaternion.LookRotation(-contact.normal));
        Destroy(obj, 2.0f);
        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
