using UnityEngine;
using System.Collections;

public class RandomPosition : MonoBehaviour
{
    public int min, max, sec;
    static System.Random random = new System.Random();
    void Start()
    {
        StartCoroutine(RePositionWithDelay());
    }

    IEnumerator RePositionWithDelay()
    {
        while (true)
        {
            SetRandomPosition();
            sec = random.Next(2, 8);
            // コルーチンを遅延させてから再開させる
            yield return new WaitForSeconds(sec);
            yield return new WaitForSeconds(random.Next(10));
        }
    }

    void SetRandomPosition()
    {
        float x = Random.Range(-30.0f, 30.0f);
        float z = Random.Range(-30.0f, 30.0f);
        Debug.Log("x,z: " + x.ToString("F2") + ", " + z.ToString("F2"));
        transform.position = new Vector3(x, 0.0f, z);
    }
}