# Winter Hunter Development Documentation
***
# Contents
## [Unity Editor](#unity-editor)
### [Camera](#camera)
### [Level](#level)
***

## Unity Editor

> ### Camera
> When creating a new scene, it is necessary to delete the default camera in the scene, and then use the already created camera prefab.  
> The camera prefab will automatically bind and follow the player upon loading.  
> **Camera prefab path: <u>Assets/Resources/Prefabs/Cameras/Main Camera</u>**

> ### Level
> + #### Terrain
>   + In the new scene, create a **Terrain** game object as the basic ground for the level. 
>   + In the Inspector window, set the **Terrain's Layer** to ***Base*** to allow interaction between the mouse input and the terrain object.
> + #### Wall (Layer)
>   + Wall is a **Layer** type that can block rays emitted from enemy camps. 
>   + When a game object marked as a Wall layer is positioned between the enemy camp and the player, the enemy camp will not be able to detect the player, causing all enemies within the camp to enter a state of disengagement from combat.
> + #### NavMesh
>   + NavMesh is an AI component in Unity used for NPC pathfinding. 
>   + After each level adjustment, the NavMesh navigation area needs to be manually rebaked:
>     1. Select <u>**Window -> AI -> Navigation**</u> from the Tool Bar at the top of the Unity Editor to open the NavMesh panel. 
>     2. In the Navigation panel, Click <u>**Object -> Scene Filter: All**</u>. 
>     3. Select **Terrain** in the Hierarchy panel, then check **Navigation Static** in the Navigation panel and set the **Navigation Area** to ***Walkable***. 
>     4. Select all **Mesh Renderers** in the Hierarchy panel whose game object prevents NPC passage, check **Navigation Static** in the Navigation panel, and then set the **Navigation Area** to ***Not Walkable***. 
>     5. Select **Terrain** in the Hierarchy, and click Bake in the Navigation panel. By adjusting the **Agent Radius**, can change the size of the non-walkable areas. If there is a previously baked NavMesh, click **Clear** first, then finally click **Bake**.
> + #### Enemy Camp
>   + 
***

## C# Scripts