# 240924
1. Remy Movement 스크립트 작성
2. Animation 적용
3. Platform ObjectPooling

# 240926
1. ObjectPooling된 PlatformList들을 담을 List를 만들어 List에 추가

# 240927
1. 플랫폼마다 ObjectPooling하지 않고 Grid Snapping을 사용해 플랫폼을 미리 만들어둔 후 사용

# 240929
1. ObjectPooling에 맞게 GameManager 스크립트에서 플랫폼들을 Instantiate하지 않고  ObjectPooling 스크립트의 함수를 불러오도록 수정
2. 플랫폼마다 MovePlatform 스크립트를 추가해 Player는 가만히 있고 플랫폼이 움직이도록 설정
3. ObjectPooling 스크립트에서 코드를 중복되게 사용하지 않고 함수 하나로 모든 종류의 플랫폼 Pool을 생성할 수 있도록 통합
4. 전체 플랫폼을 반환하는 로직에서 비활성화된 플랫폼만 랜덤으로 반환하는 로직으로 수정

# 240930
1. ObjectPooling script중 GetPlatform() 함수에서 inactivePlatforms 리스트에 비활성화된 플랫폼을 추가할 때 매번 해당 리스트를 초기화하지 않으면 이전 호출 때 추가된 플랫폼이 계속 누적되어 이미 활성화된 플랫폼이 다시 선택되는 문제가 발생. 이것을 막기 위해 GetPlatform() 함수를 호출할 때마다 inactivePlatforms 리스트를 초기화하는 로직 추가
2. random obstacle

# 241001
1. ObstacleDetect 스크립트를 만들고 Player에게 추가해 Player가 장애물을 감지
2. 장애물을 더 정확하게 감지할 수 있도록 Jump, Slide 할 때 Player의 Collider 위치와 높이를 조절
3. Random.insideUnitSphere를 사용해 Camera Shake


+속도 빨라짐
몇미터 달렸는지 보이도록
점프하면 카메라 위로 올라가고 내려가면 카메라 아래로 내려감