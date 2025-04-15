using System;
using UnityEngine;

public class Room : MonoBehaviour
{
    public int Width;
    public int Height;

    public int X;
    public int Y;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (RoomController.instance == null)
        {
            Debug.LogError("RoomController.instance is null");
            return;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(Width, Height, 0));
    }

    public Vector3 GetRoomCenter()
    {
        return new Vector3(X * Width, Y * Height);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
