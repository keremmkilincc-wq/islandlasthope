# ISLAND: LAST HOPE 🏝️ — Hikayeli Survival FPS (70 Gün)

> **70 günde kaç. Umut bitmeden, Mara ile birlikte. Bataklık ada seni yutmeden.**

[![Engine](https://img.shields.io/badge/Engine-Unity_2022_LTS-black?style=for-the-badge&logo=unity)](https://unity.com)
[![Days](https://img.shields.io/badge/70_GÜN-KAÇIŞ-0ea5e9?style=for-the-badge)](#-hikaye)
[![NPC](https://img.shields.io/badge/NPC-Dr.Mara-ec4899?style=for-the-badge)](#-npc)

![Banner](https://via.placeholder.com/1200x400/0a1a12/0ea5e9?text=ISLAND:+LAST+HOPE+%E2%80%94+70+GUN)

## ✨ Hikaye — Umut & Kurtuluş
Uçak düştü, Swamp Island'dasın. 70 gün sonra son helikopter geçecek. **Dr. Mara** 2 yıldır burada, kulübesinde telsizi tamir ediyor. Her gün bir görev, her görev +umut. 70. günde fener kulesini çalıştırıp kaç.

## 🎮 70 Gün Döngüsü
- **1 gün = 8dk** (5 gündüz + 3 gece) → Toplam ~9 saat
- Gündüz: Görev yap (Axe ile ağaç kes, Pickaxe ile maden, Dagger ile koru), Mara ile konuş → umut +8
- Gece: Kamp ateşi başında kal, tek kalırsan umut -0.5/s
- **Kaybetme:** Sağlık 0 veya Umut 0 → ekran gri
- **Kazanma:** Gün 70 → kule → helikopter

## 🧰 Aletler (7 asset)
| Asset | Animasyon |
|---|---|
| **Adventurer** `Adventurer.fbx` | **HAZIR - dokunma** |
| **Animated Pistol** `Pistol.fbx` (Fire/Reload/Slide) | **HAZIR - dokunma** |
| Axe `Axe.fbx` | `BobEffect` Swing |
| Pickaxe `Pickaxe.fbx` | `BobEffect` Swing |
| Dagger `Dagger.fbx` | `BobEffect` BobRotate |
| Tools `Tools.fbx` | `BobEffect` Float |
| Swamp Island `model.obj` | `BobEffect` WindSway |

> Kural: Sadece 2 animasyonlu dosya (Adventurer, Pistol) hazır, diğer 5'ine `BobEffect.cs` eklendi.

## 🗺️ Kurulum
```bash
git clone https://github.com/keremmkilincc-wq/islandlasthope.git
cd islandlasthope
# Unity Hub -> Open -> bu klasör -> Assets/Scenes/README.txt adımları
```

## 📂 Yapı
```
Assets/
 ├─ Models/
 │   ├─ Environment/SwampIsland/model.obj + mtl
 │   ├─ Characters/Adventurer/Adventurer.fbx (animasyonlu)
 │   ├─ Weapons/Axe, Pickaxe, Dagger, Pistol (Pistol animasyonlu)
 │   └─ Tools/Tools.fbx
 ├─ Scripts/
 │   ├─ PlayerController70.cs
 │   ├─ DayNightCycle70.cs (70 gün)
 │   ├─ HopeSystem.cs
 │   ├─ NPCMara.cs
 │   ├─ WeaponController.cs
 │   └─ BobEffect.cs (5 statik için animasyon)
 └─ Scenes/README.txt
Docs/GDD.md -> detaylı tasarım
```

## 📖 GDD
→ `Docs/GDD.md` tam tasarım, 70 gün görev listesi, NPC diyalogları.
