using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemControl : MonoBehaviour
{
    public float mutiply = 1.5f;
    public float powUpTime = 5f;
    public float invincibleTime = 3f;


    public GameObject defend;
    public static ItemControl instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    public void StartPowerUp(GameObject player)
    {
        StartCoroutine(PowerUp(player));
    }

    public void addHealth(GameObject player)
    {
        if (player.GetComponent<CharacterController2D>().playerHealth < 3)
        {
            player.GetComponent<CharacterController2D>().playerHealth += 1;
            GM.instance.addHealth(player.GetComponent<CharacterController2D>().playerHealth, player.transform.tag == "Player" ? 1 : 2);
        }
    }

    public void startInvincible(GameObject player)
    {
        StartCoroutine(invincible(player));
        GameObject obj = Instantiate(defend, player.transform);
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localScale = new Vector3(5, 5, 1);
        obj.GetComponent<Death>().dieTime = invincibleTime;
    }



    IEnumerator invincible(GameObject player)
    {
        player.GetComponent<CharacterController2D>().setInvincible(true);
        yield return new WaitForSeconds(invincibleTime);
        player.GetComponent<CharacterController2D>().setInvincible(false);
    }

    IEnumerator PowerUp(GameObject player)
    {
        player.GetComponent<CharacterController2D>().jumpHeight *= mutiply;
        player.GetComponent<CharacterController2D>().maxSpeed *= mutiply;
        player.GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(powUpTime);
        player.GetComponent<CharacterController2D>().jumpHeight /= mutiply;
        player.GetComponent<CharacterController2D>().maxSpeed /= mutiply;
        player.GetComponent<SpriteRenderer>().color = Color.white;
    }
}
