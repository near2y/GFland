using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBoomSkill : MonoBehaviour
{

    SphereCollider boomCollider;
    bool inSkill = false;
    float skillTimer = 0;
    float skillAccumulateTime = 2000;
    ParticleSystem skill = null;

    public Transform player = null;
    public float boomATK = 500;
    

    // Start is called before the first frame update
    void Start()
    {
        skill = GetComponent<ParticleSystem>();
        boomCollider = GetComponent<SphereCollider>();
        boomCollider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (inSkill)
        {
            skillTimer += Time.deltaTime * 1000;
            if (skillTimer >= skillAccumulateTime)
            {
                inSkill = false;
                StartCoroutine(InBoom());
            }
            skill.transform.position = player.transform.position;
        }
    }

    public void ReleaseSkill()
    {
        skill.transform.position = player.transform.position;
        skill.Play();
        inSkill = true;
        skillTimer = 0;
        SceneManager.Instance.player.playerSkillBar.SkillProgress = 0;
    }

    IEnumerator InBoom()
    {
        boomCollider.enabled = true;
        yield return new WaitForSeconds(1);
        boomCollider.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        if(enemy != null)
        {
            enemy.HitSubHp(boomATK, 0.3f);
        }
    }
}
