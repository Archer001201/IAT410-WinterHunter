# Winter Hunter Development Documentation
***
# Contents
[Unity Editor](#unity-editor)
+ [Camera](#camera)
+ [Scene](#scene)
  + [Terrain](#terrain)
  + [Wall (Layer)](#wall-layer)
  + [NavMesh](#navmesh)
  + [Enemy Camp](#enemy-camp)
+ [Player](#player)
+ [Snowman](#snowman)
+ [Enemy](#enemy)

[Csharp Scripts](#csharp-scripts)
***

# Unity Editor

> ## Camera
> When creating a new scene, it is necessary to delete the default camera in the scene, and then use the already created camera prefab.  
> The camera prefab will automatically bind and follow the player upon loading.
> 
> **Camera prefab path: <u>Assets/Resources/Prefabs/Cameras/Main Camera</u>**

> ## Scene
> > ### Terrain
> > 1. In the new scene, create a **Terrain** game object as the basic ground for the level.  
> > 2. In the Inspector window, set the **Terrain's Layer** to ***Base*** to allow interaction between the mouse input and the terrain object.  
> > 3. Go back to **Assets** root file, and then rename **Terrain Data Asset** and move it to **Terrains** file.
>
> > ### Wall (Layer)
> > Wall is a **Layer** type that can block rays emitted from enemy camps. 
> > When a game object marked as a Wall layer is positioned between the enemy camp and the player, the enemy camp will not be able to detect the player, causing all enemies within the camp to enter a state of disengagement from combat.
> 
> > ### NavMesh
> > NavMesh is an AI component in Unity used for NPC pathfinding. 
> > After each scene adjustment, the NavMesh navigation area needs to be manually rebaked:
> > 1. Select <u>**Window -> AI -> Navigation**</u> from the Tool Bar at the top of the Unity Editor to open the NavMesh panel. 
> > 2. In the Navigation panel, Click <u>**Object -> Scene Filter: All**</u>. 
> > 3. Select **Terrain** in the Hierarchy panel, then check **Navigation Static** in the Navigation panel and set the **Navigation Area** to ***Walkable***. 
> > 4. Select all **Mesh Renderers** in the Hierarchy panel whose game object prevents NPC passage, check **Navigation Static** in the Navigation panel, and then set the **Navigation Area** to ***Not Walkable***. 
> > 5. Select **Terrain** in the Hierarchy, and click **Bake** in the Navigation panel. By adjusting the **Agent Radius**, can change the size of the non-walkable areas. If there is a previously baked NavMesh, click **Clear** first, then finally click **Bake**.
>
> > ### Enemy Camp
> > The enemy camp, an invisible game object has attributes: ***Enemy List***, ***Treasure Chest List***, ***Raycast Distance***, and ***Is Cleared***.
> > + **Enemy list:** Stores all the enemy game objects in this camp. 
> > + **Treasure Chest list:** Stores all the treasure chest game objects in this camp. 
> > + **Raycast Distance:** Sets the detection range of the camp towards the player. When the player enters this range, enemies enter the attack state; when the player leaves this range, enemies will disengage from combat. 
> > + **Is Cleared:** Checks whether all enemies in the camp have been defeated, to unlock the treasure chests. 
> > 
> > To create an enemy camp in the scene:
> > 1. First, create an **Empty Object** in the Hierarchy and rename it as the parent object of the camp.
> > 2. Then, drag the enemy camp prefab from <u>**Assets/Resources/Prefabs/Enemies/Enemy Camp**</u> to parent object of the camp as a child.
> > 3. Drag the desired enemy prefabs from <u>**Assets/Resources/Prefabs/Enemies**</u> to the parent object of the camp as children.
> > 4. (Optional) Drag the treasure chest prefabs from <u>**Assets/Resources/Prefabs/Props/TreasureChest**</u> to the parent object of the camp as a children.
> > 5. Select the enemy camp object and adjust its **position** (the starting position of the ray emission)
> > 6. Drag all enemy child objects under the parent object of the camp into the ***Enemy List***. The setup for the ***Treasure Chest List*** is the same as for the ***Enemy List***. if there are no treasure chests or enemies, set the list length to 0.
> > 7. Uncheck ***Is Cleared***, and adjust the ***Raycast Distance*** to find a suitable detection range.

> ## Player

> ## Snowman

> ## Enemy
***

# Csharp Scripts