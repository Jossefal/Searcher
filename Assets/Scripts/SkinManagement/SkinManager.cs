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

    public static bool BuyEnvironemntSkin(SkinData skinData)
    {
        if (DataManager.diamondsCount.GetValue() >= skinData.price && !DataManager.environmentSkinIds.Contains(skinData.id))
        {
            DataManager.diamondsCount -= skinData.price;
            DataManager.environmentSkinIds.Add(skinData.id);
            return true;
        }
        else
            return false;
    }

    public static bool BuyShipSkin(SkinData skinData)
    {
        if (DataManager.diamondsCount.GetValue() >= skinData.price && !DataManager.shipSkinIds.Contains(skinData.id))
        {
            DataManager.diamondsCount -= skinData.price;
            DataManager.shipSkinIds.Add(skinData.id);
            return true;
        }
        else
            return false;
    }
}
