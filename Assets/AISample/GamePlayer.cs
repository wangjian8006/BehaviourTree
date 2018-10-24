using System;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayer : MonoBehaviour
{
    public static GamePlayer Instance;

    private bool isStart = false;

    public GameObject boss;

    public GameObject player;

    public BossObject bossObject;

    public PlayerObject playerObject;

    private int m_iCollisionGround = 0;

    public void Start()
    {
        Instance = this;
        BTMapping.RegisterNodeType();
        BTMapping.RegisterAgentType();

        m_iCollisionGround = (1 << LayerMask.NameToLayer("Floor"));
    }

    public void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space) == true)
        {
            GameStart();
            return;
        }
        if (isStart == false) return;

        if (Input.GetKey(KeyCode.W) == true) playerObject.OnMove(new Vector3(playerObject.transform.position.x + playerObject.walkLength, playerObject.transform.position.y, playerObject.transform.position.z));
        else if (Input.GetKey(KeyCode.S) == true) playerObject.OnMove(new Vector3(playerObject.transform.position.x - playerObject.walkLength, playerObject.transform.position.y, playerObject.transform.position.z));
        else if (Input.GetKey(KeyCode.A) == true) playerObject.OnMove(new Vector3(playerObject.transform.position.x, playerObject.transform.position.y, playerObject.transform.position.z + playerObject.walkLength));
        else if (Input.GetKey(KeyCode.D) == true) playerObject.OnMove(new Vector3(playerObject.transform.position.x, playerObject.transform.position.y, playerObject.transform.position.z - playerObject.walkLength));
        else if (Input.GetKeyUp(KeyCode.H) == true) playerObject.OnSpell(1);
        else if (Input.GetKeyUp(KeyCode.J) == true) playerObject.OnSpell(2);
        else if (Input.GetKeyUp(KeyCode.K) == true) playerObject.OnSpell(3);

        if (Input.GetMouseButtonUp(0) == true)
        {
            RaycastHit hit;
            Ray screenRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            bool bHit = Physics.Raycast(screenRay, out hit, 1000.0f, m_iCollisionGround);
            if (bHit) playerObject.OnMove(hit.point);
        }
    }

    public void GameStart()
    {
        if (isStart == true) return;
        isStart = true;
        boss = GameObject.Find("boss");
        player = GameObject.Find("player");
        bossObject = boss.AddComponent<BossObject>();
        playerObject = player.AddComponent<PlayerObject>();
        playerObject.SetTarget(bossObject);
    }

    public void OnAttackBoss(int spellID)
    {
        SpellData s = SpellData.GetData(spellID);
        bossObject.OnDamage(s.attack, playerObject);
    }
}
