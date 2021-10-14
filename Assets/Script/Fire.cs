using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Fire : MonoBehaviour
{
    public Transform firePos;
    public GameObject bulletPrefab;
    private ParticleSystem muzzleFlash;

    private PhotonView pv;  // transform 값을 가져오는 역할
    private bool isMouseClick => Input.GetMouseButtonDown(1);

    // Start is called before the first frame update
    void Start()
    {
        pv = GetComponent<PhotonView>();    // 포톤 뷰 컴포넌트 연결
        muzzleFlash = firePos.Find("MuzzleFlash").GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if(pv.IsMine && isMouseClick)
        {
            FireBullet(pv.Owner.ActorNumber);
            pv.RPC("FireBullet", RpcTarget.Others, pv.Owner.ActorNumber);
        }
    }

    [PunRPC]
    void FireBullet(int actorNo)
    {
        if (!muzzleFlash.isPlaying) muzzleFlash.Play(true);

        GameObject bullet = Instantiate(bulletPrefab, firePos.position, firePos.rotation);
    }
}
