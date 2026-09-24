# Unity Öğrenme Rehberi — Island Last Hope (Sıfırdan)

> Hiç Unity bilmeyenler için, adım adım. 10 dakikada ilk sahneni açacaksın.

## 1. Unity Hub Kur (3dk)

1. Git: **https://unity.com/download** → **Download Unity Hub** → `UnityHubSetup.exe` indir
2. Çalıştır → Kur → Aç → **Giriş yap** (Unity ID oluştur, ücretsiz)
3. Hub açılınca solda **Installs** sekmesini göreceksin

![Hub](https://via.placeholder.com/800x400/0a0a0f/0ea5e9?text=Unity+Hub+%E2%80%94+Installs)

## 2. Unity Editor Kur (5dk, 3GB)

1. Hub → **Installs → Install Editor**
2. Listeden **2022.3 LTS** seç (en stabil, projemiz bu sürüm: `ProjectSettings/ProjectVersion.txt:1`)
3. Modüller:
   - ✅ **Microsoft Visual Studio Community** (kod yazmak için)
   - ✅ **Windows Build Support (IL2CPP)**
   - Diğerlerini işaretleme, **Next → Install**
4. İndirme bitene kadar bekle (İnternete göre 10-30dk sürebilir)

## 3. Projeyi Aç

1. Hub → **Projects → Open** → `C:\Users\Kilinc\islandlasthope` klasörünü seç
2. İlk açılış **2-5dk** sürer, altta "Importing..." yazar, kapatma!
3. Açılınca Unity Editör: ortada **Scene**, altta **Project**, sağda **Inspector**

## 4. İlk Sahneyi Anla (5 temel pencere)

- **Hierarchy (sol üst):** Sahnedeki objeler (Main Camera, Directional Light)
- **Scene (orta):** 3D görünüm, sağ tık + WASD ile gez
- **Game (orta üst sekme):** Oyunu oynarken gördüğün ekran (Play ▶ ile)
- **Project (alt):** Dosyalar — `Assets/Models`, `Assets/Scripts` burada
- **Inspector (sağ):** Seçili objenin ayarları, script ekleme yeri

## 5. Swamp Island Haritayı Ekle (2dk)

1. Project → `Assets/Models/Environment/SwampIsland` → `model.obj`'i **Hierarchy'ye sürükle**
2. Hierarchy'de `model` seç → Inspector → **Add Component → Mesh Collider** → `Cooking Options` açık kalsın
3. Üst menü: **Window → AI → Navigation** → **Bake** sekmesi → **Bake** tıkla (zeminde mavi alan oluşur = yürünebilir alan)

## 6. Oyuncuyu Ekle (3dk)

1. Hierarchy → sağ tık → **Create Empty** → adını `Player` yap
2. `Player` seç → Inspector → **Add Component → Character Controller**
3. **Add Component → PlayerController70** (bizim script)
4. İçine **CameraRoot** diye boş obje oluştur → altına **Main Camera**'yı sürükle → Camera'nın **Spot Light**'ını fener olarak ayarla
5. `Player`'a **HopeSystem** ve **WeaponController** ekle (Inspector → Add Component)

## 7. Silahlara Animasyon Ekle (Kural!)

- **BobEffect EKLENECEK 5'ine:**
  - Project → `Axe/Axe.fbx` → Hierarchy'ye sürükle → Add Component → **BobEffect** → Mode: **Swing**
  - `Pickaxe` → **Swing**, `Dagger` → **BobRotate**, `Tools` → **Float**, `SwampIsland` → **WindSway**
- **EKLENMEYECEK 2'sine DOKUNMA:**
  - `Adventurer.fbx` ve `Pistol.fbx` zaten animasyonlu → sadece Hierarchy'ye koy, Animator otomatik gelir, BobEffect ekleme!

## 8. Mara NPC Ekle

1. Hierarchy → Create Empty → `Mara`
2. Add Component → **Nav Mesh Agent** + **NPCMara**
3. `Mara`'ya **Adventurer.fbx**'i sürükle (görünüm), HopeSystem'deki `maraTransform`'a `Mara`'yı bağla

## 9. Gündüz/Gece ve Oyna

1. Hierarchy → **Directional Light** seç → Add Component → **DayNightCycle70**
2. Üst ortada **▶ Play** tıkla → WASD + Mouse ile yürü, **E** ile Mara ile konuş, **1-5** ile silah değiştir
3. **ESC** ile imleci geri al, Play'i durdurmak için tekrar **▶** tıkla

## 10. Kaydet ve Push

- **Ctrl+S** → `Assets/Scenes/Main.unity` olarak kaydet (File → Save)
- Sonra: Hub'ı kapat, `C:\Users\Kilinc\islandlasthope` klasöründe `git add . && git commit -m "feat: ilk sahne" && git push` (biz yaparız)

---

## Sık Sorunlar

- **"Library uzun sürüyor"** → Normal, ilk açılış 5dk.
- **Pembe materyal** → Project → obj seç → Inspector → Materials → Extract Materials
- **Karakter düşüyor** → Player'a **Character Controller** eksik
- **Mavi NavMesh yok** → Window → AI → Navigation → Bake

Takılırsan ekran görüntüsü at, beraber çözelim!
