public static class GameTags {
    public static class Team {
        public const string RED = "RED";
        public const string BLUE = "BLUE";
    }

    public static class Type {
        public const string UNIT = "UNIT";
        public const string BUILDING = "BUILDING";
        public const string PLAYER = "PLAYER";
        public const string WEAPON = "WEAPON";
        public const string WAYPOINT = "WAYPOINT";
        public const string PLACEHOLDER = "PLACEHOLDER";
    }

    public static class UnitClass {
        public const string ATTACK = "ATTACK";
        public const string DEFENSE = "DEFENSE";
        public const string SUPPORT = "SUPPORT";
        public const string SCOUT = "SCOUT";
    }

    public static class BuildingClass {
        public const string SPAWN = "SPAWN";
        public const string RESOURCE = "RESOURCE";
        public const string HQ = "HQ";
    }

    public static class WeaponClass {
        public const string MELEE = "MELEE";
        public const string RANGED = "RANGED";
        public const string EXPLOSIVE = "EXPLOSIVE";
    }

    public static string create(params string[] args) {
        return string.Join("_", args);
    }

    public static string[] dissect(string tag) {
        return tag.Split('_');
    }

    public static bool isOnTeam(string objectTag) {
        string team = dissect(objectTag)[0];
        return team == GameTags.Team.BLUE || team == GameTags.Team.RED;
    }
}
