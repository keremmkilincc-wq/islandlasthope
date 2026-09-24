Main.unity buraya gelecek - Swamp Island adası

Kurulum:
1. SwampIsland/model.obj'i sahneye sürükle, Scale 1, MeshCollider ekle, Static yap
2. Window -> AI -> Navigation -> Bake (NavMesh)
3. Player: Capsule + CharacterController + PlayerController70 + HopeSystem + WeaponController
   - CameraRoot + Main Camera + Spot Light (fener)
   - Adventurer.fbx'i FPS kollar olarak CameraRoot altına ekle (animasyonlu, Animator kullan)
4. Mara NPC: Capsule + NavMeshAgent + NPCMara + Adventurer ikinci kopyası veya basit model + HopeSystem referansı
5. Silahlar: Axe/Pickaxe/Dagger/Tools -> Her birine BobEffect.cs ekle
   - Axe: Mode Swing, speed 1.2
   - Pickaxe: Mode Swing, speed 1.0
   - Dagger: Mode BobRotate, amplitude 0.1
   - Tools: Mode Float, amplitude 0.12
   - SwampIsland: Mode WindSway, speed 0.4 (çok hafif)
   - Pistol: BobEffect EKLEME! Zaten Animator (Fire/Reload/Slide)
6. DayNightCycle70'i Directional Light'a ekle, HopeSystem'i Player'a ekle
7. Play -> 70 gün sayacı başlar, E ile Mara konuşması umut +8

Animasyon kuralı: Adventurer + Pistol = hazır, dokunma. Diğer 5 = BobEffect ile canlandır.
