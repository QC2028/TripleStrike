using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player1Script : MonoBehaviour
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

    public GameObject P1Slash;
    public GameObject P1SlashLeft;
    public GameObject P1SlashRight;

    public GameObject P1Slash1;
    public GameObject P1SlashLeft1;
    public GameObject P1SlashRight1;

    private bool canMove = true;
    bool canParryStraight = false;
    bool canParryLeft = false;
    bool canParryRight = false;
    bool hasParried = false;
    bool isRecovering = false;

    public GameObject P1Sprite;
    public GameObject P1SpriteTransparent;
    public GameObject P1SpriteGrey;
    public GameObject P1SpriteDarkGrey;
    public GameObject P1SpriteParry;
    public GameObject P1SpriteRed;
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
        

        if ((Input.GetKeyDown("a")|| Input.GetKey("a")) && transform.position.x > -1 && !isMoving && !isAttacking && canMove)
        {
            //Debug.Log("moved left");

            StartCoroutine(MoveLeft());
            
        }

        if ((Input.GetKeyDown("d")|| Input.GetKey("d")) && transform.position.x < 1 && !isMoving && !isAttacking && canMove)
        {
            //Debug.Log("moved right");

            StartCoroutine(MoveRight());

        }

        if ((Input.GetKeyDown("q") || Input.GetKey("q")) && transform.position.x != -1 && !isAttacking && canMove && canAttack)
        {
            //Debug.Log("attacked left");

            StartCoroutine(AttackLeft());

        }

        if ((Input.GetKeyDown("w") || Input.GetKey("w")) && !isAttacking && canMove && canAttack)
        {
            //Debug.Log("attacked straight");

            StartCoroutine(AttackStraight());

        }

        if ((Input.GetKeyDown("e") || Input.GetKey("e")) && transform.position.x != 1 && !isAttacking && canMove && canAttack)
        {
            //Debug.Log("attacked right");

            StartCoroutine(AttackRight());

        }

        if (canParryStraight && canAttack)
        {
            if(Input.GetKeyDown("q") || Input.GetKeyDown("e"))
            {
                hasParried = false;
                canParryStraight = false;
            } 
            else if (Input.GetKeyDown("w"))
            {
                hasParried = true;
                canParryStraight = false;
                StartCoroutine(Parry()); 
            }
        }

        if (canParryLeft && canAttack)
        {
            if (Input.GetKeyDown("w") || Input.GetKeyDown("e"))
            {
                hasParried = false;
                canParryLeft = false;
            }
            else if (Input.GetKeyDown("q"))
            {
                hasParried = true;
                canParryLeft = false;
                StartCoroutine(Parry());
            }
        }

        if (canParryRight && canAttack)
        {
            if (Input.GetKeyDown("q") || Input.GetKeyDown("w"))
            {
                hasParried = false;
                canParryRight = false;
            }
            else if (Input.GetKeyDown("e"))
            {
                hasParried = true;
                canParryRight = false;
                StartCoroutine(Parry());
            }
        }

        if(p1Rounds >= 3)
        {
            SceneManager.LoadScene("Player1Victory");
        }

        if (p1Lives <= 0)
        {
            parryCount = 0;
            player1Lives.text = "3";
            player2Lives.text = "3";
            p2Rounds++;
            player2Rounds.text = p2Rounds.ToString();
        }

        if((int.Parse(player1Rounds.text)!=p1Rounds)|| (int.Parse(player2Rounds.text) != p2Rounds))
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
        P1SlashLeft.SetActive(true);
        P1SlashLeft.tag = "ParryLeft";

        yield return new WaitForSeconds(attackStartup);
        P1SlashLeft.SetActive(false);
        P1SlashLeft.SetActive(true);
        P1SlashLeft.tag = "HitBox";

        yield return new WaitForSeconds(attackLag);

        P1SlashLeft.SetActive(false);
        P1SlashLeft.tag = "ParryLeft";

        yield return new WaitForSeconds(0.2f);
        isAttacking = false;
    }

    IEnumerator AttackStraight()
    {
        isAttacking = true;
        P1Slash.SetActive(true);
        P1Slash.tag = "ParryStraight";

        yield return new WaitForSeconds(attackStartup);
        P1Slash.SetActive(false);
        P1Slash.SetActive(true);
        P1Slash.tag = "HitBox";

        yield return new WaitForSeconds(attackLag);

        P1Slash.SetActive(false);
        P1Slash.tag = "ParryStraight";

        yield return new WaitForSeconds(0.2f);
        isAttacking = false;
    }

    IEnumerator AttackRight()
    {
        isAttacking = true;
        P1SlashRight.SetActive(true);
        P1SlashRight.tag = "ParryRight";

        yield return new WaitForSeconds(attackStartup);
        P1SlashRight.SetActive(false);
        P1SlashRight.SetActive(true);
        P1SlashRight.tag = "HitBox";

        yield return new WaitForSeconds(attackLag);

        P1SlashRight.SetActive(false);
        P1SlashRight.tag = "ParryRight";

        yield return new WaitForSeconds(0.2f);
        isAttacking = false;
    }

    IEnumerator Parry()
    {
        P1Sprite.SetActive(false);
        P1SpriteParry.SetActive(true);
        ParryCounter.SetActive(true);
        parryCount++;
        if(parryCount >= 3)
        {
            parryCount = 0;
            isRecovering = true;
            canMove = false;
            TripleStrikeMessage.SetActive(true);
            P1Slash1.SetActive(true);
            P1SlashLeft1.SetActive(true);
            P1SlashRight1.SetActive(true);


            yield return new WaitForSeconds(3);

            P1Slash1.SetActive(false);
            P1SlashLeft1.SetActive(false);
            P1SlashRight1.SetActive(false);

            TripleStrikeMessage.SetActive(false);
            canMove = true;

            player1Lives.text = "3";
            player2Lives.text = "3";
            p1Rounds++;
            player1Rounds.text = p1Rounds.ToString();

            P1Sprite.SetActive(true);
            P1SpriteParry.SetActive(false);
            ParryCounter.SetActive(false);
            canMove = true;
            isRecovering = false;

        }
        else
        {
            yield return new WaitForSeconds(parryTime);
            //Debug.Log("player can now move after successful parry");
            P1Sprite.SetActive(true);
            P1SpriteParry.SetActive(false);
            ParryCounter.SetActive(false);
            canMove = true;
        }

    }

    IEnumerator PlayerHit()
    {
        isRecovering = true;
        P1Sprite.SetActive(false);
        P1SpriteRed.SetActive(true);
        p1Lives--;
        player1Lives.text = p1Lives.ToString();
        parryCount = 0;
        
        yield return new WaitForSeconds(0.2f);
        canMove = true;
        P1SpriteRed.SetActive(false);
        P1SpriteTransparent.SetActive(true);
        yield return new WaitForSeconds(recoveryTime);
        isRecovering = false;
        P1SpriteTransparent.SetActive(false);
        P1Sprite.SetActive(true);
        

    }

    IEnumerator Parried()
    {
        canMove = false;
        canAttack = false;
        P1Sprite.SetActive(false);
        P1SpriteDarkGrey.SetActive(true);
        yield return new WaitForSeconds(parryStun);
        P1SpriteDarkGrey.SetActive(false);
        P1SpriteGrey.SetActive(true);
        canMove = true;
        yield return new WaitForSeconds(parryStunRecovery);
        P1SpriteGrey.SetActive(false);
        P1Sprite.SetActive(true);
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