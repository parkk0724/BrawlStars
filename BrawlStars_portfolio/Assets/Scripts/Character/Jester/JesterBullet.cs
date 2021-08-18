using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class JesterBullet : MonoBehaviour
{
    public UnityAction Fever = null;
    public GameObject HitEffect = null;
    float Speed = 20f;
    Vector3 Originpos;
    public LayerMask layerMask_t = 0;
    public LayerMask layerMask_o = 0;
    [SerializeField] AudioSource ShotSound;
    [SerializeField] AudioSource ShotSound_2;
    AudioSource ShotSound_3;
    SoundManager soundManager;
    Coroutine SoundEffect;
    Coroutine SoundEffect_2;
    void Start()
    {
        soundManager = FindObjectOfType<SoundManager>();
        ShotSound = soundManager.Jestershoteffect.GetComponent<AudioSource>();
        ShotSound_2 = soundManager.Jestershoteffect_2.GetComponent<AudioSource>();
        ShotSound_3 = soundManager.Jestershoteffect_3.GetComponent<AudioSource>();
        Destroy(this.gameObject, 2f);
        Originpos = this.transform.position;
    }
    

    
    // Update is called once per frame
    void Update()
    {

        Vector3 curPos = this.transform.position;
        Vector3 nextPos = curPos + this.transform.forward * Speed * Time.deltaTime;

        this.transform.position = nextPos;
        float dist = Vector3.Distance(Originpos, nextPos);
        float dist_2 = Vector3.Distance(curPos, nextPos);


        RaycastHit hit;
        if(Physics.Raycast(curPos ,nextPos-curPos ,out hit ,dist_2 , layerMask_t))
        {
            if(hit.transform.gameObject.GetComponent<Monster>())
            {
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                Jester jester = player.GetComponent<Jester>();

                jester.FeverUp();
                hit.transform.gameObject.GetComponent<Monster>().Hit(jester.GetATK() / 2, Color.red);
                if (SoundEffect_2 != null) StopCoroutine(SoundEffect_2);
                SoundEffect_2 = StartCoroutine(initBullet_2(ShotSound_2));


            }
        }


        if(Physics.Raycast(curPos ,nextPos-curPos ,out hit ,dist_2 , layerMask_o))
        {
            if (SoundEffect != null) StopCoroutine(SoundEffect);
            SoundEffect = StartCoroutine(initBullet(ShotSound));
            
        }


        if(dist > 10)
        {
            Destroy(gameObject);
        }
    }
    IEnumerator initBullet(AudioSource source)
    {
        GameObject obj = Instantiate(HitEffect, this.transform.position, this.transform.rotation);
        Destroy(obj.gameObject, 0.2f);
        Destroy(this.gameObject);
        source.Play();
        yield return new WaitForSeconds(0.1f);
        source.Play();

    }
    IEnumerator initBullet_2(AudioSource source)
    {
        GameObject obj = Instantiate(HitEffect, this.transform.position, this.transform.rotation);
        Destroy(obj.gameObject, 0.2f);
        Destroy(this.gameObject);
        source.Play();
        yield return new WaitForSeconds(0.1f);
        source.Play();
    }
    private void OnTriggerEnter(Collider other)
    {
        //if (other.gameObject.CompareTag("Monster"))
        //{
        //
        //    //Jester jester = GameObject.FindWithTag("Player").GetComponent<Jester>();
        //    GameObject obj = GameObject.Find("Jester");
        //    Jester jester = obj.GetComponent<Jester>();
        //    jester.FeverUp();
        //    if (other.GetComponent<Monster>())
        //    {
        //        other.GetComponent<Monster>().Hit(jester.GetATK() + Random.Range(-5, 5), Color.red);
        //
        //    }
        //}
        //if (other.gameObject.CompareTag("Monster") || other.gameObject.CompareTag("Wall") || other.gameObject.CompareTag("Obstacle"))
        //{
        //    GameObject obj = Instantiate(HitEffect, other.transform.position, this.transform.rotation);
        //    Destroy(this.gameObject);
        //    Destroy(obj.gameObject, 0.2f);
        //}
    }
}
