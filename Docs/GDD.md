# ISLAND: LAST HOPE — GDD v1.0 (70 Gün, Umut & NPC)

## 1. Künye
- **Tür:** Hikayeli Survival FPS (single-player, first-person)
- **Engine:** Unity 2022.3 LTS + URP + Input System + NavMesh
- **Süre:** 70 gün (oyun içi). 1 gün = 8dk (5dk gündüz + 3dk gece) → Toplam ~9 saat, hızlandırılmış modda 2-3 saat.
- **Tema:** Umut & Kurtuluş. Karanlık değil, "son ışık" tonu. NPC ile bağ kurma.
- **Kazanma:** 70. günde sinyal kulesini çalıştırıp helikopterle kaç. Kaybetme: sağlık 0 veya umut 0 (moral sistemi).

## 2. Hikaye (Umut)
- **Sen:** `Alex` (Adventurer modeli) — kargo uçağı düşer, Swamp Island bataklık adasında uyanırsın.
- **NPC:** `Dr. Mara` — adada 2 yıldır mahsur, telsizi tamir etmeye çalışıyor. Kulübesinde yaşıyor, görev verir, takas yapar, hikaye anlatır. Umut sisteminin kalbi.
- **Ada:** Swamp Island (model.obj 3M) — bataklık, enkaz, terk edilmiş köy, mağara, fener. Sisli ama güzel.
- **Arka plan:** Ada eski araştırma istasyonu, sinyal kulesi bozuk, parça toplaman gerek.

## 3. 70 Gün Döngüsü
- **Gün 1-10:** Öğretici. Mara ile tanış, barınak kur, balta/kazma öğren.
- **Gün 11-40:** Esas survival. 3 biyom açılır (bataklık → orman → mağara). Her 10 günde bir ana görev: `Su pompasını tamir et`, `Telsiz parçası #1`, `Fener yakıtı bul`.
- **Gün 41-69:** Hazırlık. Sinyal kulesi için 3 parça + 20 odun + 10 hurda topla. Mara'nın hikayesi tamamlanır (aile mektupları).
- **Gün 70:** Final. Kuleyi çalıştır → 3dk savunma (yamyam dalgası) → helikopter → KAÇTIN! Umut dolu son.

## 4. Survival Stats
| Stat | Max | Tick | Not |
|---|---|---|---|
| Sağlık | 100 |  | 0 → lose |
| Açlık | 100 | -0.07/s gündüz, -0.03/s barınakta | 0 → -1 sağlık/s |
| Susuzluk | 100 | -0.10/s | 0 → -1.5 sağlık/s |
| Stamina | 100 | koş -28/s, dolum +18/s |  |
| Umut | 100 | Mara görevi +10, gece tek başına -0.05/s, Mara yanında +0.1/s | 0 → ekran gri, hareket yavaş, lose |

## 5. NPC — Dr. Mara
- Konum: Köy kulübesi (sabit), gündüz dışarı çıkar (NavMesh wander 10m).
- Etkileşim: `E` ile konuş → görev al / takas (balık ↔ su) / hikaye dinle (umut +5).
- Görev örnekleri: `5 odun getir` (barınak), `Axe ile 3 ağaç kes`, `Pickaxe ile maden kaz`, `Dagger ile yamyamı püskürt`.

## 6. Aletler & Silahlar (7 asset)
| Asset | Dosya | Animasyon | Kullanım |
|---|---|---|---|
| **Adventurer** | `Adventurer.fbx` (8.2MB, animasyonlu) | **HAZIR - ekleme yok** | Oyuncu karakteri (FPP kollar) |
| **Animated Pistol** | `Pistol.fbx` + .blend (Fire/Reload/Slide) | **HAZIR - ekleme yok** | Tabanca, son günlerde açılır |
| **Axe** | `Axe.fbx` | **EKLENECEK** → `AxeSwing` (0.4s) | Ağaç kesme, 18 hasar |
| **Pickaxe** | `Pickaxe.fbx` | **EKLENECEK** → `PickaxeSwing` | Taş/maden kazma |
| **Dagger** | `Dagger.fbx` | **EKLENECEK** → `DaggerStab` | Yakın dövüş, hızlı 12 hasar |
| **Tools** | `Tools.fbx` | **EKLENECEK** → `ToolsIdle` float | Genel tamirat animasyonu |
| **Swamp Island** | `model.obj` + mtl | **EKLENECEK** → `SwampWind` (ağaç sallanma, su dalgası) + `FogPulse` | Ana harita |

- Animasyon eklenmeyecek 2 dosya: **Adventurer** ve **Pistol** zaten animasyonlu, direkt Animator'a bağlanacak.
- Diğer 5'ine Unity'de `AnimationClip` (Legacy/Animator) eklenecek: `BobEffect.cs` ile basit sallanma/dönme.

## 7. Harita (Swamp Island)
- Tek model `model.obj` (3M) terrain olarak kullanılacak, üzerine collider (MeshCollider) + NavMesh bake.
- Bölgeler: Sahil (başlangıç enkaz), Bataklık (ortada, sis), Köy (Mara), Mağara (giriş doğuda), Fener tepesi (final).
- Loot: odun (Axe), taş (Pickaxe), yiyecek (bataklık meyvesi), hurda (Tools ile tamir).

## 8. Düşman
- Yamyam (basit capsule, NavMesh) — gündüz 2, gece 4, ateşe 15m yaklaşamaz, Mara kulübesine giremez.
- Patron yok, vurgu umut → düşman az, survival çok.

## 9. Kontroller (Unity Input System)
WASD, Mouse, SHIFT koş, F fener (Pistol ışığı), E etkileşim, TAB envanter, 1/2/3 alet değiştir.

## 10. Roadmap
- v0.1: Swamp Island import + Adventurer FPP + 70 gün sayacı + Mara placeholder
- v0.2: Aletler (Axe/Pickaxe/Dagger) + animasyonlar (BobEffect) + envanter
- v0.3: NPC görev sistemi + umut mekaniği + Pistol (animasyonlu hazır)
- v0.4: Final kule + 70. gün cutscene + build

## 11. Asset Lisans
Quaternius, CreativeTrio, Ali12, zeoxo — Poly Pizza / CC0 olduğu varsayılıyor, Credits'e eklenecek.
