using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float speed;
    CharacterController characterController;
    public List<Animator> anims;

    [SerializeField] private float acceleration = 4f;
    [SerializeField] private float deceleration = 2f;
    float moveX;
    int velocityX;

    bool isMove = false;
    bool rightPress;
    bool leftPress;

    public void Start()
    {
        velocityX = Animator.StringToHash("MoveX");
        characterController = GetComponent<CharacterController>();
    }

    public void Update()
    {
        Move();
        AnimStateControl();
    }

    public void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");

        Vector3 movement = new Vector3(horizontal, 0f, 0f);
        movement = Vector3.ClampMagnitude(movement, 1f);
        movement = transform.TransformDirection(movement);
        movement *= speed * Time.deltaTime;

        characterController.Move(movement);
    }

    void AnimStateControl()
    {
        leftPress = Input.GetKey(KeyCode.A);
        rightPress = Input.GetKey(KeyCode.D);

        if (leftPress)
        {
            moveX -= acceleration * Time.deltaTime;
            isMove = true;
        }
        if (rightPress)
        {
            moveX += acceleration * Time.deltaTime;
            isMove = true;
        }
        if (!rightPress && !leftPress && moveX > 0 && moveX != 0f)
        {
            moveX -= deceleration * Time.deltaTime;
            isMove = false;
        }
        if (!rightPress && !leftPress && moveX < 0 && moveX != 0f)
        {
            moveX += deceleration * Time.deltaTime;
            isMove = false;
        }
        if (!rightPress && !leftPress && (moveX < 0.05 && moveX > 0.05))
        {
            moveX = 0.0f;
        }
        if (moveX < -1) moveX = -1;

        if (moveX > 1) moveX = 1f;

        foreach (Animator anim in anims)
        {
            anim.SetFloat(velocityX, moveX);
        }
    }

    public void SoldierAddList(GameObject soldier)
    {
        anims.Add(soldier.GetComponent<Animator>());

        int[] valuesX = { -1, 1, -2, 2, 3, -3, 4, -4, 5, -5 };
        int[] valuesY = { 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, };

        int i = anims.Count - 1;

        if (anims[i] != null)
        {
            SoldierTranform(i, valuesX[i - 1], valuesY[i - 1]);
        }
    }

    public void SoldierTranform(int index, int valueX, int valueY)
    {
        float transformValue = 0.5f;

        anims[index].gameObject.transform.localPosition = new Vector3(transformValue * valueX, 0f, -transformValue * valueY);
    }

    public void RemoveSoldier(int value)
    {
        for (int i = 0; i < value; i++)
        {
            if (anims[anims.Count - 1] != null)
            {
                Destroy(anims[anims.Count - 1].gameObject);
                anims.RemoveAt(anims.Count - 1);
            }

            else return;
        }
    }
}
