using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallelWorldManager : MonoBehaviour
{
    [Header("Player to manage")]
    [Tooltip("Script of player that will be moved between worlds.")]
    [SerializeField] PlayerMovement player;

    [Header("Lists of objects in parallel worlds.")]

    [Tooltip("List of objects in real world.")]
    [SerializeField] Transform upperObjectGroup;

    [Tooltip("List of objects in parallel world.")]
    [SerializeField] Transform lowerObjectGroup;


    private Animator[] upperObjectsList;
    private Animator[] lowerObjectsList;

    private bool isInParallelWorld;

    void Start()
    {
        isInParallelWorld = false;

        upperObjectsList = upperObjectGroup.GetComponentsInChildren<Animator>();
        lowerObjectsList = lowerObjectGroup.GetComponentsInChildren<Animator>();
        ManageAnimations(isInParallelWorld);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            isInParallelWorld = !isInParallelWorld;
            player.JumpToParallelWorld();
            ManageAnimations(isInParallelWorld);

        }
    }

    private void ManageAnimations(bool parallel)
    {
        if (!parallel)
        {
            foreach (Animator animator in upperObjectsList)
            {
                animator.speed = 1f;
            }
            foreach (Animator animator in lowerObjectsList)
            {
                animator.speed = 0f;
            }
        }
        else
        {
            foreach (Animator animator in upperObjectsList)
            {
                animator.speed = 0f;
            }
            foreach (Animator animator in lowerObjectsList)
            {
                animator.speed = 1f;
            }
        }
    }


}
