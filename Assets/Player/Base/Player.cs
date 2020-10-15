using System.Diagnostics.Tracing;
using UnityEngine;

public class Player : MonoBehaviour
{

    static protected bool BuildMode = false;
    static protected Rigidbody2D player;

    void Start()
    {
        if (this.GetComponent<Rigidbody2D>() != null)
        {
            player = this.GetComponent<Rigidbody2D>();
        }
    }
}
