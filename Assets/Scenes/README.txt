Main.unity buraya gelecek.
Unity Hub → Open → islandlasthope klasörü → Assets/Scenes/Main.unity oluştur:

1. Create Terrain 150x150, heightmap düz + dağ/fener tepesi sculpt
2. Directional Light (sun) → DayNightCycle.cs ekle
3. Player (Capsule + CharacterController + PlayerController + SurvivalManager + Inventory) + CameraRoot + Camera + Light(flashlight, Spot 40°, 25m)
4. NavMesh Bake (Window → AI → Navigation)
5. Enemy Prefab (Capsule + NavMeshAgent + EnemyAI)
6. Campfire Prefab (Point Light 2.5 intensity, 18m range)
