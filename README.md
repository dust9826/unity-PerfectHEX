# unity-PerfectHEX

탑뷰 턴제 전략 RPG입니다(2021.12 ~ 2022.02, 1인). 게임 안의 모든 오브젝트와 상호작용할 수 있도록 하는 것을 목표로 했습니다.

> 소스는 `master` 브랜치에 있습니다.

- 기술 Unity, C#

## 시스템

- **상태 머신 전투 흐름** — 유닛 선택 → 이동 목표 → 이동 시퀀스 → 명령 선택 → 상호작용 목표 → 상호작용 시퀀스 → 적 턴으로 이어지는 State 계열 클래스와 StateMachine, Turn 관리.
- **탐험과 전투** — ExploreState와 BattleController 사이의 전환.
- **보드와 타일** — BoardController, TileData / TileLayer, 3D 타일맵 배치와 선택(TileSelectController).
- **유닛과 상호작용** — Entity 상속 구조, Movement, Interaction, 문(DoorController).
- **UI·카메라** — 능력 메뉴 패널, 정보 이벤트, 카메라 리그.

## License

Battle Of The Creek by Alexander Nakarada | https://www.serpentsoundstudios.com
Hymn To The Gods by Alexander Nakarada | https://www.serpentsoundstudios.com
Behind The Sword by Alexander Nakarada | https://www.serpentsoundstudios.com
Music promoted by https://www.chosic.com/free-music/all/
Creative Commons CC BY 4.0
https://creativecommons.org/licenses/by/4.0/
