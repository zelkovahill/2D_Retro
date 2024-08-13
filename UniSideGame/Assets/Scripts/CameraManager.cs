using UnityEngine;

public class CameraManager : MonoBehaviour
{

    // 카메라 같은 경우는 Update보다 LateUpdate에서 처리하는 것이 좋다.
    public float leftLimit = 0.0f;  // 왼쪽 스크롤 제한
    public float rightLimit = 0.0f;  // 오른쪽 스크롤 제한
    public float topLimit = 0.0f;  // 위쪽 스크롤 제한
    public float bottomLimit = 0.0f;  // 아래쪽 스크롤 제한

    private GameObject player;  // 플레이어
    public GameObject subScreen;  // 서브 스크린


    public bool isForceScrollX = false; // X축 강제 스크롤 여부
    public float forceScrollSpeedX = 0.5f; // 1초간 움직일 X의 거리
    public bool isForceScrollY = false; // Y축 강제 스크롤 여부
    public float forceScrollSpeedY = 0.5f; // 1초간 움직일 Y의 거리



    // ====================================================================================================
    private void Start()
    {
        // 플레이어 찾기
        player = GameObject.FindGameObjectWithTag("Player");
        subScreen = GameObject.Find("SubScreen");
    }

    private void Update()
    {

    }

    private void LateUpdate()
    {
        MoveCamare();
    }

    private void MoveCamare()
    {
        // Find는 비용이 많이 들기 때문에 Start에서 찾아두고 Update에서 사용하는 것이 좋다. 
        // GameObject player = GameObject.FindGameObjectWithTag("Player"); // 플레이어 찾기
        if (player != null)
        {
            // 카메라의 좌표 갱신
            float x = player.transform.position.x;
            float y = player.transform.position.y;
            float z = transform.position.z;

            // 가로 방향 동기화
            if (isForceScrollX)
            {
                x = transform.position.x + (forceScrollSpeedX * Time.deltaTime);
            }

            // 양 끝에 이동 제한 적용
            if (x < leftLimit)
            {
                x = leftLimit;
            }
            else if (x > rightLimit)
            {
                x = rightLimit;
            }

            // 세로 방향 동기화
            if (isForceScrollY)
            {
                y = transform.position.y + (forceScrollSpeedY * Time.deltaTime);
            }


            // 위 아래에 이동 제한 적용
            if (y < bottomLimit)
            {
                y = bottomLimit;
            }
            else if (y > topLimit)
            {
                y = topLimit;
            }


            // 카메라 위치의 Vector3 만들기
            Vector3 v3 = new Vector3(x, y, z);
            transform.position = v3;

            // 서브 스크린 스크롤
            y = subScreen.transform.position.y;
            z = subScreen.transform.position.z;
            Vector3 v = new Vector3(x / 2.0f, y, z);
            subScreen.transform.position = v;
        }
    }




}
