using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneCreation : MonoBehaviour {
    #region variables
    public Texture2D backgroundContainerPaneTex, topHalfPaneTex, bottomLeftPaneTex, bottomRightPaneTex;
    public MeshRenderer bgMesh, topHalfPaneMesh, bottomLeftPaneMesh, bottomRightPaneMesh;

    Vector3 topHalfPos, bottomLeftPos, bottomRightPos;
    bool isSwapped = false; // bool to check the position of the planes are swapped or not
    bool isMoving = false; //this bool will block users tap on Swap and reset button, when moving is on going
    public float movingSpeed = 1; // moving speed of planes when mswapping and resetting
    #endregion

    void Awake()
    {
        //assigning intial positions
        topHalfPos = topHalfPaneMesh.transform.position;
        bottomLeftPos = bottomLeftPaneMesh.transform.position;
        bottomRightPos = bottomRightPaneMesh.transform.position;
    }
    void Start()
    {
        render(); //calling render
    }

    //render function, assign texture to the 4 plane's mats
    public void render()
    {
        bgMesh.material.mainTexture = backgroundContainerPaneTex;
        topHalfPaneMesh.material.mainTexture = topHalfPaneTex;
        bottomLeftPaneMesh.material.mainTexture = bottomLeftPaneTex;
        bottomRightPaneMesh.material.mainTexture = bottomRightPaneTex;
    }

    //considering three different animations to all of the planes, and calling them
    public void PlayAnim()
    {
        Debug.Log("anim");
        topHalfPaneMesh.GetComponent<Animator>().Play("TopHalf");
        bottomLeftPaneMesh.GetComponent<Animator>().Play("BottomLeft");
        bottomRightPaneMesh.GetComponent<Animator>().Play("BottomRight");
    }

    // swapping the position of the planes and reset to the initial pos, if user clicks again
    public void SwapAndReset()
    {
        if (!isMoving)
        {
            if (isSwapped)
            {
                ResetPlace();
            }
            else
            {
                SwapPlace();
            }
            isSwapped = !isSwapped;
        }

    }
    //swapping the planes
    void SwapPlace()
    {
        Vector3 topHalfPaneMeshTarget = new Vector3(topHalfPos.x, topHalfPaneMesh.transform.position.y,bottomLeftPos.z) ;
        Vector3 bottomLeftPaneMeshTarget = new Vector3(bottomLeftPos.x, topHalfPaneMesh.transform.position.y, topHalfPos.z);
        Vector3 bottomRightPaneMeshTarget = new Vector3(bottomRightPos.x, topHalfPaneMesh.transform.position.y, topHalfPos.z);
        StartCoroutine(MovePlane(topHalfPaneMeshTarget, bottomLeftPaneMeshTarget, bottomRightPaneMeshTarget));

    }
    //resetting the planes to the initial pos
    void ResetPlace()
    {
        StartCoroutine(MovePlane(topHalfPos, bottomLeftPos, bottomRightPos));
    }

    //moving planes to the target pos
    IEnumerator MovePlane(Vector3 topHalfTargetPos, Vector3 bottomLeftTargetPos, Vector3 bottomRightTargetPos)
    {
        isMoving = true;
        while (Vector3.Distance(topHalfPaneMesh.transform.position, topHalfTargetPos) > 0.01f)
        {
            topHalfPaneMesh.transform.position = Vector3.MoveTowards(topHalfPaneMesh.transform.position, topHalfTargetPos,movingSpeed);
            bottomLeftPaneMesh.transform.position = Vector3.MoveTowards(bottomLeftPaneMesh.transform.position, bottomLeftTargetPos, movingSpeed);
            bottomRightPaneMesh.transform.position = Vector3.MoveTowards(bottomRightPaneMesh.transform.position, bottomRightTargetPos, movingSpeed);
            yield return null;
        }

        topHalfPaneMesh.transform.position = topHalfTargetPos;
        bottomLeftPaneMesh.transform.position = bottomLeftTargetPos;
        bottomRightPaneMesh.transform.position = bottomRightTargetPos;
        isMoving = false;
    }
}
