using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneCreation : MonoBehaviour {

    public Texture2D backgroundContainerPaneTex, topHalfPaneTex, bottomLeftPaneTex, bottomRightPaneTex;
    public MeshRenderer bgMesh, topHalfPaneMesh, bottomLeftPaneMesh, bottomRightPaneMesh;

    Vector3 topHalfPos, bottomLeftPos, bottomRightPos;
    bool isSwapped = false;
    bool isMoving = false;
    public float movingSpeed = 1;
    void Awake()
    {
        topHalfPos = topHalfPaneMesh.transform.position;
        bottomLeftPos = bottomLeftPaneMesh.transform.position;
        bottomRightPos = bottomRightPaneMesh.transform.position;
    }
    void Start()
    {
        render();
    }


    public void render()
    {
        bgMesh.material.mainTexture = backgroundContainerPaneTex;
        topHalfPaneMesh.material.mainTexture = topHalfPaneTex;
        bottomLeftPaneMesh.material.mainTexture = bottomLeftPaneTex;
        bottomRightPaneMesh.material.mainTexture = bottomRightPaneTex;
    }

    public void PlayAnim()
    {
        Debug.Log("anim");
        topHalfPaneMesh.GetComponent<Animator>().Play("TopHalf");
        bottomLeftPaneMesh.GetComponent<Animator>().Play("BottomLeft");
        bottomRightPaneMesh.GetComponent<Animator>().Play("BottomRight");
    }

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

    void SwapPlace()
    {
        Vector3 topHalfPaneMeshTarget = new Vector3(topHalfPos.x, topHalfPaneMesh.transform.position.y,bottomLeftPos.z) ;
        Vector3 bottomLeftPaneMeshTarget = new Vector3(bottomLeftPos.x, topHalfPaneMesh.transform.position.y, topHalfPos.z);
        Vector3 bottomRightPaneMeshTarget = new Vector3(bottomRightPos.x, topHalfPaneMesh.transform.position.y, topHalfPos.z);
        StartCoroutine(MovePlane(topHalfPaneMeshTarget, bottomLeftPaneMeshTarget, bottomRightPaneMeshTarget));

    }

    void ResetPlace()
    {
        StartCoroutine(MovePlane(topHalfPos, bottomLeftPos, bottomRightPos));
         //topHalfPaneMesh.transform.position = topHalfPos;
         //bottomLeftPaneMesh.transform.position = bottomLeftPos;
         //bottomRightPaneMesh.transform.position = bottomRightPos;
    }
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
