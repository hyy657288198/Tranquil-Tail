using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private LayerMask groundLayer;

    private Camera cam;

    private float normalFOV = 18f;
    private float zoomedOutFOV = 30f;

    private float smoothness = 7f;

    private float yOffSet = 1f;

    // Start is called before the first frame update
    private void Start()
    {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    private void Update()
    {
        // Set the camera's position to the player's position
        transform.position = new Vector3(player.position.x, player.position.y+yOffSet, transform.position.z);

        // Define a target FOV based on whether the player is on the ground or in the air
        float targetFOV = Physics2D.Raycast(player.position, Vector2.down, 7f, groundLayer) ? normalFOV : zoomedOutFOV;

        // Smoothly interpolate between the current FOV and the target FOV
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, targetFOV, Time.deltaTime * smoothness);
    }
}
