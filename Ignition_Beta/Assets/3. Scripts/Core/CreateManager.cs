using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CreateManager : MonoBehaviour
{


    [MenuItem("GameObject/CreateManager/Managers/All", priority = int.MinValue)]
    static void CreateManagers(MenuCommand menuCommand)
    {
        // 1. ~Manager 이름으로 새 Object를 만든다.
        // LoadAssetAtPath의 string 중 일부라도 틀리면 작동 안함.
        GameObject managers = Instantiate(AssetDatabase.LoadAssetAtPath("Assets/5. Prefabs/Core/Managers.prefab", typeof(GameObject)), Vector3.zero, Quaternion.identity) as GameObject;
        managers.name = managers.name.Replace("(Clone)", "").Trim();

        //GameObject managers = new GameObject("Managers");
        //GameObject gm = new GameObject("GameManager");
        //GameObject cm = new GameObject("ComponentManager");
        //GameObject tm = new GameObject("ToastManager");
        //gm.AddComponent<GameManager>();
        //cm.AddComponent<ComponentManager>();
        //tm.AddComponent<Toast>();

        //gm.transform.SetParent(managers.transform);
        //cm.transform.SetParent(managers.transform);
        //tm.transform.SetParent(managers.transform);

        // 2. Hierachy 윈도우에서 어떤 오브젝트를 선택하여 생성시에는 그 오브젝트의 하위 계층으로 생성된다.
        // 그밖의 경우에는 아무일도 일어나지 않는다.
        // GameObjectUtility.SetParentAndAlign(gm, menuCommand.context as GameObject);

        // 3. 생성된 오브젝트를 Undo 시스템에 등록한다.
        Undo.RegisterCreatedObjectUndo(managers, "Create" + managers.name);

        // 4. 생성한 오브젝트를 선택한다.
        Selection.activeObject = managers;
    }

    [MenuItem("GameObject/CreateManager/Managers/GameManager", priority = int.MinValue)]
    static void CreateComponentManager(MenuCommand menuCommand)
    {
        // 1. ~Manager 이름으로 새 Object를 만든다.
        // LoadAssetAtPath의 string 중 일부라도 틀리면 작동 안함.
        GameObject gm = new GameObject("GameManager");
        gm.AddComponent<GameManager>();

        // 2. Hierachy 윈도우에서 어떤 오브젝트를 선택하여 생성시에는 그 오브젝트의 하위 계층으로 생성된다.
        // 그밖의 경우에는 아무일도 일어나지 않는다.
        // GameObjectUtility.SetParentAndAlign(gm, menuCommand.context as GameObject);

        // 3. 생성된 오브젝트를 Undo 시스템에 등록한다.
        Undo.RegisterCreatedObjectUndo(gm, "Create" + gm.name);

        // 4. 생성한 오브젝트를 선택한다.
        Selection.activeObject = gm;
    }

    [MenuItem("GameObject/CreateManager/Managers/ComponentManager", priority = int.MinValue)]
    static void CreateGameManager(MenuCommand menuCommand)
    {
        // 1. ~Manager 이름으로 새 Object를 만든다.
        // LoadAssetAtPath의 string 중 일부라도 틀리면 작동 안함.
        GameObject cm = new GameObject("ComponentManager");
        cm.AddComponent<ComponentManager>();

        // 2. Hierachy 윈도우에서 어떤 오브젝트를 선택하여 생성시에는 그 오브젝트의 하위 계층으로 생성된다.
        // 그밖의 경우에는 아무일도 일어나지 않는다.
        // GameObjectUtility.SetParentAndAlign(gm, menuCommand.context as GameObject);

        // 3. 생성된 오브젝트를 Undo 시스템에 등록한다.
        Undo.RegisterCreatedObjectUndo(cm, "Create" + cm.name);

        // 4. 생성한 오브젝트를 선택한다.
        Selection.activeObject = cm;
    }

    [MenuItem("GameObject/CreateManager/Managers/ToastManager", priority = int.MinValue)]
    static void CreateToastManager(MenuCommand menuCommand)
    {
        // 1. ~Manager 이름으로 새 Object를 만든다.
        // LoadAssetAtPath의 string 중 일부라도 틀리면 작동 안함.
        GameObject tm = Instantiate(AssetDatabase.LoadAssetAtPath("Assets/5. Prefabs/UI/ToastManager.prefab", typeof(GameObject)), Vector3.zero, Quaternion.identity) as GameObject;
        tm.name = tm.name.Replace("(Clone)", "").Trim();

        // 2. Hierachy 윈도우에서 어떤 오브젝트를 선택하여 생성시에는 그 오브젝트의 하위 계층으로 생성된다.
        // 그밖의 경우에는 아무일도 일어나지 않는다.
        // GameObjectUtility.SetParentAndAlign(gm, menuCommand.context as GameObject);

        // 3. 생성된 오브젝트를 Undo 시스템에 등록한다.
        Undo.RegisterCreatedObjectUndo(tm, "Create" + tm.name);

        // 4. 생성한 오브젝트를 선택한다.
        Selection.activeObject = tm;
    }

    [MenuItem("GameObject/CreateManager/Players/Fade")]
    static void CreateFadePlayer(MenuCommand menuCommand)
    {
        GameObject fade = Instantiate(AssetDatabase.LoadAssetAtPath("Assets/5. Prefabs/Player/Player(Fade).prefab", typeof(GameObject)), Vector3.zero, Quaternion.identity) as GameObject;
        fade.name = fade.name.Replace("(Clone)", "").Trim();

        // 3. 생성된 오브젝트를 Undo 시스템에 등록한다.
        Undo.RegisterCreatedObjectUndo(fade, "Create" + fade.name);

        // 4. 생성한 오브젝트를 선택한다.
        Selection.activeObject = fade;
    }

    [MenuItem("GameObject/CreateManager/Players/InGame")]
    static void CreateInGamePlayer(MenuCommand menuCommand)
    {
        GameObject inGame = Instantiate(AssetDatabase.LoadAssetAtPath("Assets/5. Prefabs/Player/Player(InGame).prefab", typeof(GameObject)), Vector3.zero, Quaternion.identity) as GameObject;
        inGame.name = inGame.name.Replace("(Clone)", "").Trim();

        // 3. 생성된 오브젝트를 Undo 시스템에 등록한다.
        Undo.RegisterCreatedObjectUndo(inGame, "Create" + inGame.name);

        // 4. 생성한 오브젝트를 선택한다.
        Selection.activeObject = inGame;
    }

    [MenuItem("GameObject/CreateManager/Players/InGame+")]
    static void CreateInGamePlusPlayer(MenuCommand menuCommand)
    {
        GameObject inGame = Instantiate(AssetDatabase.LoadAssetAtPath("Assets/5. Prefabs/Player/Player(InGame)+.prefab", typeof(GameObject)), Vector3.zero, Quaternion.identity) as GameObject;
        inGame.name = inGame.name.Replace("(Clone)", "").Trim();

        // 3. 생성된 오브젝트를 Undo 시스템에 등록한다.
        Undo.RegisterCreatedObjectUndo(inGame, "Create" + inGame.name);

        // 4. 생성한 오브젝트를 선택한다.
        Selection.activeObject = inGame;
    }

    [MenuItem("GameObject/CreateManager/Players/Default")]
    static void CreateDefaultPlayer(MenuCommand menuCommand)
    {
        GameObject def = Instantiate(AssetDatabase.LoadAssetAtPath("Assets/5. Prefabs/Player/Player.prefab", typeof(GameObject)), Vector3.zero, Quaternion.identity) as GameObject;
        def.name = def.name.Replace("(Clone)", "").Trim();

        // 3. 생성된 오브젝트를 Undo 시스템에 등록한다.
        Undo.RegisterCreatedObjectUndo(def, "Create" + def.name);

        // 4. 생성한 오브젝트를 선택한다.
        Selection.activeObject = def;
    }

    [MenuItem("GameObject/CreateManager/Weapons/All")]
    static void CreateAllWeapons(MenuCommand menuCommand)
    {
        GameObject pistol = Instantiate(AssetDatabase.LoadAssetAtPath("Assets/5. Prefabs/Weapons/Pistol/Pistol.prefab", typeof(GameObject)), Vector3.zero, Quaternion.identity) as GameObject;
        GameObject rifle = Instantiate(AssetDatabase.LoadAssetAtPath("Assets/5. Prefabs/Weapons/Rifle/Rifle.prefab", typeof(GameObject)), Vector3.zero, Quaternion.identity) as GameObject;
        GameObject shotgun = Instantiate(AssetDatabase.LoadAssetAtPath("Assets/5. Prefabs/Weapons/Shotgun/Shotgun.prefab", typeof(GameObject)), Vector3.zero, Quaternion.identity) as GameObject;
        GameObject grenade = Instantiate(AssetDatabase.LoadAssetAtPath("Assets/5. Prefabs/Weapons/Grenade/Grenade.prefab", typeof(GameObject)), Vector3.zero, Quaternion.identity) as GameObject;
        pistol.name = pistol.name.Replace("(Clone)", "").Trim();
        rifle.name = rifle.name.Replace("(Clone)", "").Trim();
        shotgun.name = shotgun.name.Replace("(Clone)", "").Trim();
        grenade.name = grenade.name.Replace("(Clone)", "").Trim();

        // 3. 생성된 오브젝트를 Undo 시스템에 등록한다.
        Undo.RegisterCreatedObjectUndo(pistol, "Create" + pistol.name);
        Undo.RegisterCreatedObjectUndo(rifle, "Create" + rifle.name);
        Undo.RegisterCreatedObjectUndo(shotgun, "Create" + shotgun.name);
        Undo.RegisterCreatedObjectUndo(grenade, "Create" + grenade.name);

        // 4. 생성한 오브젝트를 선택한다.
        Selection.activeObject = grenade;
    }

    [MenuItem("GameObject/CreateManager/Weapons/Pistol")]
    static void CreatePistolWeapons(MenuCommand menuCommand)
    {
        GameObject pistol = Instantiate(AssetDatabase.LoadAssetAtPath("Assets/5. Prefabs/Weapons/Pistol/Pistol.prefab", typeof(GameObject)), Vector3.zero, Quaternion.identity) as GameObject;
        pistol.name = pistol.name.Replace("(Clone)", "").Trim();

        // 3. 생성된 오브젝트를 Undo 시스템에 등록한다.
        Undo.RegisterCreatedObjectUndo(pistol, "Create" + pistol.name);

        // 4. 생성한 오브젝트를 선택한다.
        Selection.activeObject = pistol;
    }

    [MenuItem("GameObject/CreateManager/Weapons/Rifle")]
    static void CreateRifleWeapons(MenuCommand menuCommand)
    {
        GameObject rifle = Instantiate(AssetDatabase.LoadAssetAtPath("Assets/5. Prefabs/Weapons/Rifle/Rifle.prefab", typeof(GameObject)), Vector3.zero, Quaternion.identity) as GameObject;
        rifle.name = rifle.name.Replace("(Clone)", "").Trim();

        // 3. 생성된 오브젝트를 Undo 시스템에 등록한다.
        Undo.RegisterCreatedObjectUndo(rifle, "Create" + rifle.name);

        // 4. 생성한 오브젝트를 선택한다.
        Selection.activeObject = rifle;
    }

    [MenuItem("GameObject/CreateManager/Weapons/Shotgun")]
    static void CreateShotgunWeapons(MenuCommand menuCommand)
    {
        GameObject shotgun = Instantiate(AssetDatabase.LoadAssetAtPath("Assets/5. Prefabs/Weapons/Shotgun/Shotgun.prefab", typeof(GameObject)), Vector3.zero, Quaternion.identity) as GameObject;
        shotgun.name = shotgun.name.Replace("(Clone)", "").Trim();

        // 3. 생성된 오브젝트를 Undo 시스템에 등록한다.
        Undo.RegisterCreatedObjectUndo(shotgun, "Create" + shotgun.name);

        // 4. 생성한 오브젝트를 선택한다.
        Selection.activeObject = shotgun;
    }

    [MenuItem("GameObject/CreateManager/Weapons/Grenade")]
    static void CreateGrenadeWeapons(MenuCommand menuCommand)
    {
        GameObject grenade = Instantiate(AssetDatabase.LoadAssetAtPath("Assets/5. Prefabs/Weapons/Grenade/Grenade.prefab", typeof(GameObject)), Vector3.zero, Quaternion.identity) as GameObject;
        grenade.name = grenade.name.Replace("(Clone)", "").Trim();

        // 3. 생성된 오브젝트를 Undo 시스템에 등록한다.
        Undo.RegisterCreatedObjectUndo(grenade, "Create" + grenade.name);

        // 4. 생성한 오브젝트를 선택한다.
        Selection.activeObject = grenade;
    }
}
