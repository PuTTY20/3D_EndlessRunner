using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    MoveObject _move;
    RemyCtrl _remy;
    GameObject selectedPlatform = null;
    GameObject selectedObstacle = null;

    public int score = 0;
    float posZ = 0f;
    float deciamlScore = 0f;
    float addScore = 0f;
    float initAddScore = 1f;
    float midAddScore = 1.5f;
    float maxAddScore = 2.25f;


    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    void Start()
    {
        addScore = initAddScore;
        StartCoroutine(ActivatePlatforms());
        StartCoroutine(ActiveObstacle());

        _remy = GameObject.Find("Remy").GetComponent<RemyCtrl>();
    }

    void Update()
    {
        if (_move != null)
            posZ = _move.zPos;

        if (!_remy.isDie)
            IncreaseScore();
    }

    IEnumerator ActivatePlatforms()
    {
        while (true)
        {
            #region ObjectPooling을 사용하지 않을 때
            // //랜덤하게 하나의 플랫폼을 선택하여 활성화
            // int randomIndex = Random.Range(0, PlatformList.Count);
            // GameObject selectedPlatform = Instantiate(PlatformList[randomIndex]);
            // // 위치 설정
            // selectedPlatform.transform.position = new Vector3(0, 0, 76f);
            // selectedPlatform.SetActive(true);
            #endregion

            selectedPlatform = ObjectPooling.poolingManager.GetPlatform();

            if (selectedPlatform != null)
            {
                selectedPlatform.SetActive(true);
                _move = selectedPlatform.GetComponent<MoveObject>();
                selectedPlatform.transform.position = new Vector3(0, 0, 84f + posZ);
            }

            yield return new WaitForSeconds(4.0f);
        }
    }

    IEnumerator ActiveObstacle()
    {
        while (true)
        {
            selectedObstacle = ObjectPooling.poolingManager.GetObstacle();
            if (selectedObstacle != null)
            {
                selectedObstacle.SetActive(true);

                #region ObstacleCtrl에서 setActive(false)된 자식 Obj를 true로 바꾸기 위한 코드
                int idx = selectedObstacle.transform.childCount;
                for (int i = 0; i < idx; i++)
                    selectedObstacle.transform.GetChild(i).gameObject.SetActive(true);
                #endregion

                selectedObstacle.transform.position = new Vector3(0f, 0, 40f);
            }

            yield return new WaitForSeconds(3f);
        }
    }

    void IncreaseScore()
    {
        if (score >= 200)
        {
            addScore = maxAddScore;
            Debug.Log(addScore);
        }
        else if (score >= 100)
        {
            addScore = midAddScore;
            Debug.Log(addScore);
        }

        deciamlScore += Time.deltaTime * addScore; // 초당 1씩 증가

        if (deciamlScore >= 1f)
        {
            score += (int)deciamlScore; // 정수 부분만큼 점수(score)에 더함
            deciamlScore -= Mathf.Floor(deciamlScore); // 정수로 더해진 값 만큼 deciamlScore에서 뺌
        }
    }
}