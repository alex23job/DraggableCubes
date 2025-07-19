using UnityEngine;
using DG.Tweening;

public class ProbaDOTween : MonoBehaviour
{
    private float timer = 5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0) timer -= Time.deltaTime;
        else
        {
            timer = 5f;
            Shake();
        }
    }

    private void Shake()
    {
        transform.DOShakePosition(duration: 1f, strength: 0.5f, vibrato: 10, randomness: 1f);
    }
}
