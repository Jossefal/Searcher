﻿using UnityEngine;

#pragma warning disable 649

public class ScoreController : MonoBehaviour
{
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private Animator scoreIconAnimator;

    private void OnTriggerEnter2D(Collider2D coll)
    {
        scoreIconAnimator.SetTrigger("scoreIncrease");
        scoreManager.AddPoint();
        Destroy(coll.gameObject);
    }
}
