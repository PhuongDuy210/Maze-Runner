using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickDetecter : MonoBehaviour
{
    public PlayerController playerController;
    public PathDrawer pathDrawer;
    private Camera mainCam;
    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        pathDrawer = GameObject.FindGameObjectWithTag("GameController").GetComponent<PathDrawer>();
        mainCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if ((Input.touchCount > 0) && (Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetTouch(0).phase == TouchPhase.Began))
            {
                Ray raycast = mainCam.ScreenPointToRay(Input.GetTouch(0).position);
                RaycastHit raycastHit;
                if (Physics.Raycast(raycast, out raycastHit))
                {
                    if(playerController != null && pathDrawer == null)
                        playerController.GetTargerList(raycastHit.transform.GetComponent<Tile>());
                    if(pathDrawer != null)
                    {
                        Tile input = raycastHit.transform.GetComponent<Tile>();
                        if (pathDrawer.cutOff)
                        {
                            if (pathDrawer.path.Peek() == input)
                                pathDrawer.PaintPath(input);
                            else
                                pathDrawer.ResetPath();
                            pathDrawer.cutOff = false;
                        }
                        else
                            pathDrawer.PaintPath(input);
                    }
                }
            }
            if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                if (pathDrawer != null)
                {
                    pathDrawer.CheckPath();
                }
            }
        }
    }

    private void OnMouseOver()
    {
        if (Application.platform == RuntimePlatform.WindowsPlayer || Application.isEditor)
        {
            if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0))
            {
                if (playerController != null && pathDrawer == null)
                    playerController.GetTargerList(transform.GetComponent<Tile>());
                if (pathDrawer != null)
                {
                    Tile input = transform.GetComponent<Tile>();
                    if (pathDrawer.cutOff)
                    {
                        if (pathDrawer.path.Peek() == input)
                            pathDrawer.PaintPath(input);
                        else
                            pathDrawer.ResetPath();
                        pathDrawer.cutOff = false;
                    }
                    else
                        pathDrawer.PaintPath(input);
                }
            }
            if(Input.GetMouseButtonUp(0))
            {
                if (pathDrawer != null)
                {
                    pathDrawer.CheckPath();
                }
            }
        }
    }
}
