# ISLAND: LAST HOPE 🏝️ — 3D Survival FPS

> **Uçağın düştü. Ada ıssız değil. Gündüz topla, gece saklan. Son umut sensin.**

[![Version](https://img.shields.io/badge/version-v0.1.0-0ea5e9?style=for-the-badge&labelColor=0a0f19)](https://github.com/keremmkilincc-wq/islandlasthope)
[![Engine](https://img.shields.io/badge/Engine-Unity_2022_LTS-000000?style=for-the-badge&logo=unity&labelColor=0a0f19)](https://unity.com)
[![License: MIT](https://img.shields.io/badge/License-MIT-10b981?style=for-the-badge&labelColor=0a0f19)](LICENSE)
[![Platform](https://img.shields.io/badge/platform-PC-7c3aed?style=for-the-badge&labelColor=0a0f19)](#-kurulum)

![Island Last Hope Banner](https://via.placeholder.com/1200x400/0a1a12/0ea5e9?text=ISLAND:+LAST+HOPE+%E2%80%94+SURVIVAL+FPS)

---

## ✨ Konsept

SON IŞIK sonrası yeni oyun: **Açık ada survival FPS.**

- **150×150 ada:** Sahil, orman, batık uçak, mağara, deniz feneri, terk edilmiş köy. SON IŞIK'ın 90×90 evinden 2.5× büyük.
- **Gündüz / Gece döngüsü:** 12dk gündüz (topla/üret) + 8dk gece (avlanıyorsun). Fener + kamp ateşi = hayatta kalma.
- **Survival stats:** Açlık, susuzluk, sağlık, stamina, sıcaklık. Hepsi HUD'da. Yemek pişir, su arıt.
- **2 düşman:** Yamyamlar (gündüz mesafeli, gece sürü) + Ada Gardiyanı (gece boss, feneri görünce hızlanır).
- **Craft & Build:** Balta, mızrak, kamp ateşi, barınak, sinyal ateşi (final kaçış).

---

## 🎬 Oynanış Döngüsü

```
GÜNDÜZ (12dk, sis 60/180, güneş sıcak)
├─ Keşfet: 5 biome (sahil/oran/bataklık/mağara/dağ)
├─ Topla: odun, taş, lif, yiyecek, su, hurda (uçak enkazı)
├─ Craft: balta → ateş → barınak → telsiz parçası (3 parça)
└─ Hazırlan: Gece öncesi ateşi yak, yemeğini pişir

GECE (8dk, sis 15/50, el feneri şart)
├─ Ateş sönerse sıcaklık düşer, can gider
├─ Yamyamlar ateşe 18m'den yaklaşamaz, fener 25m'den görünürsün
├─ Gardiyan spawn (gecenin 3.dk'sı, 1.90m, duygu mesafesi 30m)
└─ Sabaha kadar hayatta kalırsan +1 gün, sinyal ilerler
```

**Kazanma:** 5 gece dayan + 3 telsiz parçası → deniz fenerinde sinyal yak → helikopter gelir → KAÇTIN!
**Kaybetme:** Sağlık 0 veya açlık/susuzluk 0 → ☠️ YAKALANDIN

---

## 🚀 Kurulum (Unity)

```bash
# 1) Klonla
git clone https://github.com/keremmkilincc-wq/islandlasthope.git
cd islandlasthope

# 2) Unity Hub ile aç
# - Unity 2022.3 LTS kur (https://unity.com/download)
# - Hub → Open → bu klasörü seç
# - Library/ otomatik oluşur, Assets/Scenes/Main.unity aç

# 3) Oyna
# Unity Editör → Play ▶
```

> **Unity kurulu değilse:** `Docs/web-prototype/` içinde Three.js ile küçük bir demo var → `npx serve .` ile bakılabilir. (SON IŞIK mantığı)

---

## 🎮 Kontroller

| PC | Açıklama |
|---|---|
| `WASD` | Hareket |
| `Mouse` | Bakış (Pointer Lock) |
| `SHIFT` | Koş (stamina 30/s gider, 20/s dolar) |
| `F` | Fener aç/kapa (pil 0.4/s) |
| `E` | Topla / Etkileşim / Craft |
| `TAB` | Envanter |
| `ESC` | Duraklat |
| `SPACE` | Zıpla |

---

## 🗺️ Harita (planlanan)

```
150×150 ada
├─ Dış okyanus + kumsal (0-15m, palmiyeler, enkaz parçaları)
├─ Orman (15-60m, 40 ağaç, çalılar, yiyecek spawn)
├─ Bataklık (kuzey, görüş düşük, Gardiyan spawn)
├─ Mağara (doğu dağ, telsiz parçası #2, karanlık + fener şart)
├─ Terk edilmiş köy (batı, 6 kulübe, loot, yamyam kampı)
├─ Deniz feneri (merkez tepe, final sinyal yeri, 12m yüksek)
└─ Uçak enkazı (güney sahil, başlangıç, parça #1)
```

---

## 🧠 Teknoloji

| Katman | Teknoloji | Not |
|---|---|---|
| **Engine** | `Unity 2022.3 LTS` + URP | SON IŞIK'tan Unity'ye geçiş |
| **Fizik** | `CharacterController` + AABB | `isBlocked` benzeri |
| **AI** | `NavMeshAgent` | Yamyam sürü + Gardiyan patrol/chase |
| **Survival** | `SurvivalManager.cs` | Açlık/susuzluk/sıcaklık tick 0.5s |
| **Craft** | `Inventory + CraftingSystem.cs` | 3×3 tarifler, ağırlık limiti |
| **Gündüz/Gece** | `DayNightCycle.cs` | Directional Light + fog lerp |
| **Ses** | `FMOD / AudioSource` | Gündüz kuş, gece rüzgar + kalp atışı |

---

## 📂 Proje Yapısı

```
islandlasthope/
├─ Assets/
│  ├─ Scenes/Main.unity          # ana sahne (150×150 terrain, 5 biome placeholder)
│  ├─ Scripts/
│  │  ├─ PlayerController.cs     # FPS hareket + stamina + fener
│  │  ├─ SurvivalManager.cs      # stats tick, ateş/sıcaklık
│  │  ├─ Inventory.cs            # toplama, craft, ağırlık
│  │  ├─ DayNightCycle.cs        # 20dk döngü, ışık/sis
│  │  └─ EnemyAI.cs              # NavMesh chase, ateşten korkma
│  ├─ Prefabs/  (ağaç, taş, ateş, yamyam)
│  ├─ Materials/
│  ├─ Models/   (palm, rock, uçak parçası)
│  └─ Audio/    (gece rüzgar, yamyam sesi)
├─ Packages/manifest.json
├─ ProjectSettings/
├─ Docs/GDD.md                   # detaylı tasarım dokümanı
└─ README.md
```

---

## 📖 GDD

Detaylı tasarım için → [`Docs/GDD.md`](Docs/GDD.md)

---

## 🤝 Katkı

```bash
git checkout -b feat/ada-mekanigi
# geliştir
git commit -m "feat: ada mekaniği"
git push origin feat/ada-mekanigi
# PR aç
```

---

## 📄 Lisans

MIT © 2026 Kerem Kilinc — [LICENSE](LICENSE)

> SON IŞIK evreninin devamı. Unity Learn / Poly Haven assetleri kullanılacak (CC0).
