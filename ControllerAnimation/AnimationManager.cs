﻿using UnityEngine;
using System.Collections;

public class AnimationManager : MonoBehaviour
{
    private float
        directionAxisZ,
        directionAxisX;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
            Debug.LogError("Animator not find!", this);
    }

    //debug
    private void Update()
    {
        #region Read and delete this!!!
        directionAxisZ = Input.GetAxis("Vertical");
        directionAxisX = Input.GetAxis("Horizontal");
        float runButton = Input.GetAxis("Run") ;

        directionAxisX += directionAxisX > 0 ? runButton : directionAxisX < 0 ? -runButton : 0;
        directionAxisZ += directionAxisZ > 0 ? runButton : directionAxisZ < 0 ? -runButton : 0;

        ApllyMotion(directionAxisX, directionAxisZ);
        ApllyJump(Input.GetKeyDown(KeyCode.Space));

        if (Input.GetKeyDown(KeyCode.Alpha1))
            ApllyDeath(Random.Range(1, 3));
        if (Input.GetKeyDown(KeyCode.Alpha2))
            ApllyClimbLedge(Random.Range(1, 4));
        else
            ApllyClimbLedge(0);

        //reset animation
        if (Input.GetKeyDown(KeyCode.Tab))        
            ResetAnimation();
        #endregion
    }
    /// <summary>
    /// Задает или возвращает значение воспроизведения скорости анимации
    /// </summary>
    public float AnimationSpeed
    {
        get
        {
            return animator.speed;
        }
        set
        {
            animator.speed = value;
        }
    }
    /// <summary>
    /// Служит для определения направления анимации, значения принимаются от -2 до +2,
    /// при этом от -1 до +1 - хотьба, от -2 до +2 - бег
    /// </summary>
    /// <param name="directionAxisX">Анимация влево/вправо</param>
    /// <param name="directionAxisZ">Анимация вперед/назад</param>
    public void ApllyMotion(float directionAxisX, float directionAxisZ)
    {
        //so... this line makes advanced idle
        //axis return 0f when it not pressed
        animator.SetBool("Idle", (directionAxisX == 0f && directionAxisZ == 0f) ? true : false);

        animator.SetFloat("Direction_Z", directionAxisZ);
        animator.SetFloat("Direction_X", directionAxisX);
    }
    /// <summary>
    /// Служит для определения выбора одно из вариантов позы смерти.
    /// Принимает числа больше ноля. Каждое число соотсветсвует позе. 
    /// </summary>
    /// <param name="poseDeath"></param>
    public void ApllyDeath(int poseDeath)
    {
        if (poseDeath < 0)
            Debug.LogError("Posture of death can only be non-negative", this);
        animator.SetInteger("Death", poseDeath);
    }
    /// <summary>
    /// Служит для определения выбора одно из вариантов перелазанья.
    /// Может быть вызвано только при движении. Принимает числа больше ноля.
    /// Каждое число соотсветсвует позе. 
    /// </summary>
    /// <param name="climbLedgePose"></param>
    public void ApllyClimbLedge(int climbLedgePose)
    {
        if (climbLedgePose < 0)
            Debug.LogError("Posture of clim Ledge can only be non-negative", this);
        animator.SetInteger("ClimbLedge", climbLedgePose);
    }
    /// <summary>
    /// Служит для начала анимаций прыжка. Принимает значение true или false
    /// </summary>
    /// <param name="jump"></param>
    public void ApllyJump(bool jump)
    {
        animator.SetBool("Jump", jump);
    }
    /// <summary>
    /// Reset all animation to default
    /// </summary>
    public void ResetAnimation()
    {
        animator.SetInteger("Death", 0);
        animator.SetInteger("ClimbLedge", 0);
        animator.SetBool("Jump", false);
        animator.Play("Motion");
    }
}