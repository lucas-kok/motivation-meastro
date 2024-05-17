using System;

public enum SceneType
{
    MAIN_MENU_SCENE,
    BACKSTORY_SCENE,
    TUTORIAL_SCENE,
    DECISION_ROOM_SCENE,
    IMPACT_SCENE,
    CHALLENGE_ROOM_SCENE,
    FINAL_ROOM_SCENE
}

public static class RoomTypeExtensions
{
    public static string GetSceneName(this SceneType roomType)
    {
        switch (roomType)
        {
            case SceneType.DECISION_ROOM_SCENE:
                return "DecisionRoomScene";
            case SceneType.CHALLENGE_ROOM_SCENE:
                return "ChallengeRoomScene";
            case SceneType.IMPACT_SCENE:
                return "ImpactScene";
            case SceneType.FINAL_ROOM_SCENE:
                return "FinalRoomScene";
            case SceneType.MAIN_MENU_SCENE:
                return "MainMenuScene";
            case SceneType.TUTORIAL_SCENE:
                return "TutorialScene";
            case SceneType.BACKSTORY_SCENE:
                return "BackstoryScene";
            default:
                throw new ArgumentOutOfRangeException(nameof(roomType), roomType, "Unknown RoomType");
        }
    }
}