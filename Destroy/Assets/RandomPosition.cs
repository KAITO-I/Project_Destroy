using UnityEngine;
using System.Collections;

public class RandomPosition : MonoBehaviour
{
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
            // コルーチンを遅延させてから再開させる
            yield return new WaitForSeconds(2);
            yield return new WaitForSeconds(random.Next(10));
        }
    }

    void SetRandomPosition()
    {
        float x = Random.Range(-10.0f, 10.0f);
        float z = Random.Range(-10.0f, 10.0f);
        Debug.Log("x,z: " + x.ToString("F2") + ", " + z.ToString("F2"));
        transform.position = new Vector3(x, 0.0f, z);
    }
}