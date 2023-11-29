using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    
    [SerializeField]
    private float playerSpeed;
    
    [SerializeField]
    private KeyCode leftKey;
    [SerializeField]
    private KeyCode rightKey;
    [SerializeField]
    private KeyCode upKey;
    [SerializeField]
    private KeyCode downKey;
    [SerializeField]
    private KeyCode changeKey1;
    [SerializeField]
    private KeyCode changeKey2;
    [SerializeField]
    private KeyCode pauseKey;

    public GameObject[] forms;
    private int currentForm = 0;
    private int formAmount;
    private int indexMin = 0;

    [SerializeField]
    private Animator solidFormAnimator;
    [SerializeField]
    private Animator liquidFormAnimator;
    [SerializeField]
    private Animator gasFormAnimator;
    private bool facingRight;

    [SerializeField]
    private GameObject pausePanel;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        formAmount = forms.Length;
        ChangeForm(currentForm);
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        ChangeCurrForm();

        if (Input.GetKey(pauseKey))
        {
            PauseGame();
        }
    }

    private void Flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;

        facingRight = !facingRight;
    }

    private void Movement()
    {
        if (Input.GetKey(leftKey))
        {
            if (!facingRight)
            {
                Flip();
            }
            rb.velocity = Vector2.left * playerSpeed;
        }
        else if (Input.GetKey(rightKey))
        {
            if (facingRight)
            {
                Flip();
            }
            rb.velocity = Vector2.right * playerSpeed;
        }
        if(currentForm == 2)
        {
            if (Input.GetKey(upKey))
            {
                rb.velocity = Vector2.up * playerSpeed;
            }
            else if (Input.GetKey(downKey))
            {
                rb.velocity = Vector2.down * playerSpeed;
            }
        }
        if (rb.velocity.x != 0)
        {
            switch (currentForm)
            {
                case 0:
                    solidFormAnimator.SetBool("isMoving", true);
                    break;
                case 1:
                    liquidFormAnimator.SetBool("isMoving", true);
                    break;
                case 2:
                    gasFormAnimator.SetBool("isMoving", true);
                    break;
            }
        }
        else
        {
            switch (currentForm)
            {
                case 0:
                    solidFormAnimator.SetBool("isMoving", false);
                    break;
                case 1:
                    liquidFormAnimator.SetBool("isMoving", false);
                    break;
                case 2:
                    gasFormAnimator.SetBool("isMoving", false);
                    break;
            }
        }
    }

    private void ChangeCurrForm()
    {
        if (Input.GetKeyDown(changeKey1))
        {
            if (currentForm > indexMin)
            {
                currentForm--;
            }
        }
        if (Input.GetKeyDown(changeKey2))
        {
            if (currentForm < formAmount-1)
            {
                currentForm++;
            }
        }
        ChangeForm(currentForm);
    }

    private void ChangeForm(int formIndex)
    {
        int i = 0;
        while (i < formAmount)
        {
            if (i == formIndex)
            {
                forms[i].SetActive(true);
            }
            else
            {
                forms[i].SetActive(false);
            }
            i++;
        }

        CheckForm();
    }

    private void CheckForm()
    {
        if (currentForm == 0)
        {
            ChangeFormValue(1, 3, 2);
        }
        else if (currentForm == 1)
        {
            ChangeFormValue(0.0001f, 1, 10);
        }
        else
        {
            ChangeFormValue(0.0001f, 0, 2);
        }
    }

    private void ChangeFormValue(float mass, int gravity, int speed)
    {
        rb.mass = mass;
        rb.gravityScale = gravity;
        playerSpeed = speed;
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
    }
}
