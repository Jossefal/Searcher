public static class SkinManager
{
    public static void SetCurrentEnvironemntSkin(int id)
    {
        DataManager.currentEnvironmentSkinId = new SafeInt(id);
    }

    public static void SetCurrentShipSkin(int id)
    {
        DataManager.currentShipSkinId = new SafeInt(id);
    }
}
