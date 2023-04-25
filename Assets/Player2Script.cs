using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player2Script : MonoBehaviour
{
    public float movementLag;
    public float attackStartup;
    public float attackLag;
    public float parryTime;
    public float parryStun;
    public float parryStunRecovery;
    public float recoveryTime;

    private bool isMoving = false;
    private bool isAttacking;
    private bool canAttack = true;

    public GameObject P2Slash;
    public GameObject P2SlashLeft;
    public GameObject P2SlashRight;

    public GameObject P2Slash1;
    public GameObject P2SlashLeft1;
    public GameObject P2SlashRight1;

    private bool canMove = true;
    bool canParryStraight = false;
    bool canParryLeft = false;
    bool canParryRight = false;
    bool hasParried = false;
    bool isRecovering = false;

    public GameObject P2Sprite;
    public GameObject P2SpriteTransparent;
    public GameObject P2SpriteGrey;
    public GameObject P2SpriteDarkGrey;
    public GameObject P2SpriteParry;
    public GameObject P2SpriteRed;
    public GameObject ParryCounter;

    public Text player1Lives;
    public Text player1Rounds;
    public Text player2Lives;
    public Text player2Rounds;

    private int p1Lives = 3;
    private int p1Rounds = 0;
    private int p2Lives = 3;
    private int p2Rounds = 0;

    public GameObject TripleStrikeMessage;


    private int parryCount;


    //private bool hasMoved = true;

    void Start()
    {
    }

    void Update()
    {


        if ((Input.GetKeyDown("j") || Input.GetKey("j")) && transform.position.x > -1 && !isMoving && !isAttacking && canMove)
        {
            //Debug.Log("moved left");

            StartCoroutine(MoveLeft());

        }

        if ((Input.GetKeyDown("l") || Input.GetKey("l")) && transform.position.x < 1 && !isMoving && !isAttacking && canMove)
        {
            //Debug.Log("moved right");

            StartCoroutine(MoveRight());

        }

        if ((Input.GetKeyDown("u") || Input.GetKey("u")) && transform.position.x != -1 && !isAttacking && canMove && canAttack)
        {
            //Debug.Log("attacked left");

            StartCoroutine(AttackLeft());

        }

        if ((Input.GetKeyDown("i") || Input.GetKey("i")) && !isAttacking && canMove && canAttack)
        {
            //Debug.Log("attacked straight");

            StartCoroutine(AttackStraight());

        }

        if ((Input.GetKeyDown("o") || Input.GetKey("o")) && transform.position.x != 1 && !isAttacking && canMove && canAttack)
        {
            //Debug.Log("attacked right");

            StartCoroutine(AttackRight());

        }

        if (canParryStraight && canAttack)
        {
            if (Input.GetKeyDown("u") || Input.GetKeyDown("o"))
            {
                hasParried = false;
                canParryStraight = false;
            }
            else if (Input.GetKeyDown("i"))
            {
                hasParried = true;
                canParryStraight = false;
                StartCoroutine(Parry());
            }
        }

        if (canParryLeft && canAttack)
        {
            if (Input.GetKeyDown("i") || Input.GetKeyDown("o"))
            {
                hasParried = false;
                canParryLeft = false;
            }
            else if (Input.GetKeyDown("u"))
            {
                hasParried = true;
                canParryLeft = false;
                StartCoroutine(Parry());
            }
        }

        if (canParryRight && canAttack)
        {
            if (Input.GetKeyDown("u") || Input.GetKeyDown("i"))
            {
                hasParried = false;
                canParryRight = false;
            }
            else if (Input.GetKeyDown("o"))
            {
                hasParried = true;
                canParryRight = false;
                StartCoroutine(Parry());
            }
        }

        if (p2Rounds >= 3)
        {
            SceneManager.LoadScene("Player2Victory");
        }

        if (p2Lives <= 0)
        {
            parryCount = 0;
            player1Lives.text = "3";
            player2Lives.text = "3";
            p1Rounds++;
            player1Rounds.text = p1Rounds.ToString();
        }

        if ((int.Parse(player1Rounds.text) != p1Rounds) || (int.Parse(player2Rounds.text) != p2Rounds))
        {
            parryCount = 0;
        }
    }

    private void LateUpdate()
    {
        p1Lives = int.Parse(player1Lives.text);
        p1Rounds = int.Parse(player1Rounds.text);
        p2Lives = int.Parse(player2Lives.text);
        p2Rounds = int.Parse(player2Rounds.text);
    }

    IEnumerator MoveLeft()
    {
        transform.position += new Vector3(-0.5f, 0, 0);

        isMoving = true;
        yield return new WaitForSeconds(movementLag);
        isMoving = false;
    }

    IEnumerator MoveRight()
    {
        transform.position += new Vector3(0.5f, 0, 0);

        isMoving = true;
        yield return new WaitForSeconds(movementLag);
        isMoving = false;
    }

    IEnumerator AttackLeft()
    {
        isAttacking = true;
        P2SlashLeft.SetActive(true);
        P2SlashLeft.tag = "ParryLeft";

        yield return new WaitForSeconds(attackStartup);
        P2SlashLeft.SetActive(false);
        P2SlashLeft.SetActive(true);
        P2SlashLeft.tag = "HitBox";

        yield return new WaitForSeconds(attackLag);

        P2SlashLeft.SetActive(false);
        P2SlashLeft.tag = "ParryLeft";

        yield return new WaitForSeconds(0.2f);
        isAttacking = false;
    }

    IEnumerator AttackStraight()
    {
        isAttacking = true;
        P2Slash.SetActive(true);
        P2Slash.tag = "ParryStraight";

        yield return new WaitForSeconds(attackStartup);
        P2Slash.SetActive(false);
        P2Slash.SetActive(true);
        P2Slash.tag = "HitBox";

        yield return new WaitForSeconds(attackLag);

        P2Slash.SetActive(false);
        P2Slash.tag = "ParryStraight";

        yield return new WaitForSeconds(0.2f);
        isAttacking = false;
    }

    IEnumerator AttackRight()
    {
        isAttacking = true;
        P2SlashRight.SetActive(true);
        P2SlashRight.tag = "ParryRight";

        yield return new WaitForSeconds(attackStartup);
        P2SlashRight.SetActive(false);
        P2SlashRight.SetActive(true);
        P2SlashRight.tag = "HitBox";

        yield return new WaitForSeconds(attackLag);

        P2SlashRight.SetActive(false);
        P2SlashRight.tag = "ParryRight";

        yield return new WaitForSeconds(0.2f);
        isAttacking = false;
    }

    IEnumerator Parry()
    {
        P2Sprite.SetActive(false);
        P2SpriteParry.SetActive(true);
        ParryCounter.SetActive(true);
        parryCount++;
        if (parryCount >= 3)
        {
            parryCount = 0;
            isRecovering = true;
            canMove = false;
            TripleStrikeMessage.SetActive(true);
            P2Slash1.SetActive(true);
            P2SlashLeft1.SetActive(true);
            P2SlashRight1.SetActive(true);


            yield return new WaitForSeconds(3);

            P2Slash1.SetActive(false);
            P2SlashLeft1.SetActive(false);
            P2SlashRight1.SetActive(false);

            TripleStrikeMessage.SetActive(false);
            canMove = true;

            player1Lives.text = "3";
            player2Lives.text = "3";
            p2Rounds++;
            player2Rounds.text = p2Rounds.ToString();

            P2Sprite.SetActive(true);
            P2SpriteParry.SetActive(false);
            ParryCounter.SetActive(false);
            canMove = true;
            isRecovering = false;

        }
        else
        {
            yield return new WaitForSeconds(parryTime);
            //Debug.Log("player can now move after successful parry");
            P2Sprite.SetActive(true);
            P2SpriteParry.SetActive(false);
            ParryCounter.SetActive(false);
            canMove = true;
        }

    }

    IEnumerator PlayerHit()
    {
        isRecovering = true;
        P2Sprite.SetActive(false);
        P2SpriteRed.SetActive(true);
        p2Lives--;
        player2Lives.text = p2Lives.ToString();
        parryCount = 0;

        yield return new WaitForSeconds(0.2f);
        canMove = true;
        P2SpriteRed.SetActive(false);
        P2SpriteTransparent.SetActive(true);
        yield return new WaitForSeconds(recoveryTime);
        isRecovering = false;
        P2SpriteTransparent.SetActive(false);
        P2Sprite.SetActive(true);


    }

    IEnumerator Parried()
    {
        canMove = false;
        canAttack = false;
        P2Sprite.SetActive(false);
        P2SpriteDarkGrey.SetActive(true);
        yield return new WaitForSeconds(parryStun);
        P2SpriteDarkGrey.SetActive(false);
        P2SpriteGrey.SetActive(true);
        canMove = true;
        yield return new WaitForSeconds(parryStunRecovery);
        P2SpriteGrey.SetActive(false);
        P2Sprite.SetActive(true);
        canAttack = true;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "HitBox" && !isRecovering) //what happens when you get hit by the hitbox
        {

            if (hasParried)
            {
                //Debug.Log("player1 has parried!");
                hasParried = false;
            }
            else
            {
                StartCoroutine(PlayerHit());
            }
            canParryStraight = false;
            canParryLeft = false;
            canParryRight = false;
            //Debug.Log("player hit");
        }

        if (collision.gameObject.tag == "ParryStraight" && !isRecovering) //what happens when you get hit during parry window
        {
            canMove = false;
            canParryStraight = true;
            //Debug.Log("parry hitbox p1");
        }

        if (collision.gameObject.tag == "ParryRight" && !isRecovering) //what happens when you get hit during parry window
        {
            canMove = false;
            canParryLeft = true;
            //Debug.Log("parry hitbox LEFT");
        }

        if (collision.gameObject.tag == "ParryLeft" && !isRecovering) //what happens when you get hit during parry window
        {
            canMove = false;
            canParryRight = true;
            //Debug.Log("parry hitbox RIGHT");
        }

        if (collision.gameObject.tag == "ParryCounter" && !isRecovering)
        {
            StartCoroutine(Parried());
        }

    }

}