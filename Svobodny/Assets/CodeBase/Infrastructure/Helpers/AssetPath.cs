namespace CodeBase.Infrastructure.Helpers
{
    public static class AssetPath
    {
        public const string EnemyKnifePath = "Enemies/Knife";
        public const string TestEnemyPath = "Enemies/TestEnemy";
        public const string TestNpcPath = "Npcs/TestNpc";
        public const string CharacterPath = "Character/Character";
        public const string EnemySpawnerPath = "Enemies/Enemy Spawner";
        public const string NpcSpawnerPath = "Npcs/Npc Spawner";
        public const string ObjectSpawnerPath = "UsableObjects/Object Spawner";
        public const string ItemsUIHandlerPath = "ItemsUIHandler";
        public const string GunsUIHandlerPath = "GunsUIHandler";
        public const string HealthUIHandlerPath = "HealthUIHandler";
        public const string EssentialsUIHandlerPath = "EssentialsUIHandler";
        public const string GunObjectSpawnerPath = "Usableobjects/Gun Object Spawner";
        public const string EssentialObjectSpawnerPath = "UsableObjects/Essential ObjectSpawner";
        
        public static class StaticDataPath
        {
            public const string Level = "Static Data/Levels";
            public const string Monster = "Static Data/Monsters";
            public const string Npc = "Static Data/NPCs";
            public const string Character = "Static Data/Character/CharacterData";
            public const string Item = "Static Data/Items";
            public const string Gun = "Static Data/Guns";
            public const string Sound = "Static Data/Sounds";
            public const string Essential = "Static Data/Essentials";
            public const string Cursor = "Static Data/Cursors";
        }

        public static class ObjectsPath
        {
            public const string WardrobePath = "UsableObjects/Wardrobe";
            public const string KeyOnePath = "UsableObjects/Keys/KeyOne";
            public const string KeyTwoPath = "UsableObjects/Keys/KeyTwo";
            public const string KeyThreePath = "UsableObjects/Keys/KeyThree";
            public const string KeyFourPath = "UsableObjects/Keys/KeyFour";
            public const string KeyFivePath = "UsableObjects/Keys/KeyFive";
            public const string KeySixPath = "UsableObjects/Keys/KeySix";
            public const string DoorPath = "UsableObjects/Doors/Door";
            public const string ClosedDoorPath = "UsableObjects/Doors/ClosedDoor";

            public static class Guns
            {
                public const string KnifePath = "UsableObjects/Guns/Knife";
                public const string PistolPath = "UsableObjects/Guns/Pistol";
            }

            public static class Essentials
            {
                public const string BulletPath = "UsableObjects/Essentials/Bullet";
                public const string MedicinePath = "UsableObjects/Essentials/Medicine";
            }
        }

        public static class UIPath
        {
            public const string UIRoot = "UI/UIRoot";
            public const string ItemSlot = "UI/ItemSlot";
            public const string GunSlot = "UI/GunSlot";
            public const string EssentialSlot = "UI/EssentialSlot";
            public const string Hud = "UI/HUD";
            public const string DeathMenu = "UI/Windows/DeathWindow";
            public const string DoorOneWindow = "UI/Windows/DoorOneWindow";
            public const string DoorTwoWindow = "UI/Windows/DoorTwoWindow";
            public const string DoorThreeWindow = "UI/Windows/DoorThreeWindow";
            public const string DoorFourWindow = "UI/Windows/DoorFourWindow";
            public const string DoorFiveWindow = "UI/Windows/DoorFiveWindow";
            public const string DoorSixWindow = "UI/Windows/DoorSixWindow";
            public const string PauseWindow = "UI/Windows/PauseWindow";
            public const string ButtonInteraction = "UI/Windows/InteractionButton";

            public static class Dialogs
            {
                public const string Level1InitialDialog = "UI/Windows/Dialogs/Level1InitialDialog";
                public const string Level1EndDialog = "UI/Windows/Dialogs/Level1EndDialog";
                public const string Level2InitialDialog = "UI/Windows/Dialogs/Level2InitialDialog";
                public const string Level2EndDialog = "UI/Windows/Dialogs/Level2EndDialog";
                public const string Level3InitialDialog = "UI/Windows/Dialogs/Level3InitialDialog";
                public const string Level3EndDialog = "UI/Windows/Dialogs/Level3EndDialog";
                public const string KnifeDialog = "UI/Windows/Dialogs/KnifeDialog";
                public const string FinalDialog = "UI/Windows/Dialogs/FinalDialog";
            }
        }

        public static class Gizmos
        {
            public const string KnifeImage = "Knife";
            public const string PistolImage = "Handgun";
            public const string BulletImage = "Ammo";
            public const string MedicineImage = "Heal";
        }
    }
}