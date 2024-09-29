# 240924
1. Remy Movement 스크립트 작성
2. Animation 적용
3. Platform ObjectPooling

# 240926
1. ObjectPooling된 PlatformList들을 담을 List를 만들어 List에 추가

# 240927
1. 플랫폼마다 ObjectPooling에 않고 Grid Snapping을 사용해 플랫폼을 미리 만들어둔 후 사용

# 240929
1. ObjectPooling에 맞게 GameManager 스크립트에서 플랫폼들을 Instantiate하지 않고  ObjectPooling 스크립트의 함수를 불러오도록 수정
2. 플랫폼마다 MovePlatform 스크립트를 추가해 Player는 가만히 있고 플랫폼이 움직이도록 설정
3. ObjectPooling 스크립트에서 코드를 중복되게 사용하지 않고 함수 하나로 모든 종류의 플랫폼 Pool을 생성할 수 있도록 통합
4. 전체 플랫폼을 반환하는 로직에서 비활성화된 플랫폼만 랜덤으로 반환하는 로직으로 수정