# ISLAND: LAST HOPE — Game Design Document v0.1

## 1. Künye
- **Tür:** 3D Survival FPS (single-player)
- **Engine:** Unity 2022.3 LTS + URP
- **Platform:** PC (WebGL fallback sonra)
- **Hedef süre:** 30-45dk / 5 gece
- **İlham:** SON IŞIK (90×90 ev → 150×150 ada), The Forest, Green Hell, Firewatch

## 2. Hikaye
Uçak `KH-117` Pasifik'te düşer. Sen tek sağ kalan sensin. Ada haritada yok. Telsiz parçaları uçak enkazı, mağara ve köye dağılmış. Deniz feneri hâlâ çalışıyor ama yakıtı yok. 5 gece dayan, sinyali yak, kurtul. Ada seni istemiyor.

## 3. Core Loop
1. **Gündüz:** Keşfet → Topla (odun/taşı/lif/yiyecek) → Craft → Barınak/Ateş kur → Telsiz parçası ara
2. **Gece:** Ateş başında kal veya fenerle gizlice ilerle → Yamyam/Gardiyan'dan kaç → Sabahı gör
3. **Meta:** Her sabah sağlık/açlık sıfırlanmaz, kalıcı. 5. gün final.

## 4. Stats
| Stat | Max | Tick | Ölüm |
|---|---|---|---|
| Sağlık | 100 | - | 0 → lose |
| Açlık | 100 | -0.08/s (gündüz), -0.04/s (gece ateş başında) | 0 → -1 sağlık/s |
| Susuzluk | 100 | -0.12/s | 0 → -1.5 sağlık/s |
| Stamina | 100 | koş -30/s, dolum +20/s | 0 → koşamaz |
| Sıcaklık | 100 | gece ateşten uzak -0.5/s, ateş başında +1/s | <20 → -0.5 sağlık/s |
| Pil (fener) | 100 | açık -0.4/s | 0 → karanlık |

HUD: SON IŞIK'taki gibi üst bar + pil halkası.

## 5. Harita Detay
- **Terrain 150×150, heightmap:** sahil 0m, orman 2-5m, dağ 12m, fener tepesi 18m.
- **Biome'lar:**
  - Sahil: başlangıç, 3 palmiye kümesi, enkaz, yengeç (yiyecek)
  - Orman: 40 ağaç (Land: 3 varyant), 20 çalı, mantar spawn 2dk
  - Bataklık: kuzey, sis yoğun, Gardiyan spawn noktası (0,60)
  - Mağara: doğu (60,10), içi karanlık, yarasa sesi, parça #2, soğuk
  - Köy: batı (-50,-10), 6 kulübe (kapısız), yamyam kamp ateşi, parça #3
  - Fener: merkez (0,0), 12m kule, tepe sinyal yeri, yakıt lazım (odun 20)

## 6. Düşmanlar
### Yamyam (Cannibal)
- Boy: 1.75m, speed gündüz 3.2, gece 4.0
- Davranış: NavMesh patrol (köy ↔ orman), oyuncuyu 18m görür, 12m duyar (koşarsan), ateşe 18m yaklaşamaz
- Hasar: 15 / vuruş, vuruş arası 1.2s
- Sayı: gündüz 2, gece 4 (sürü)

### Gardiyan (Guardian)
- Boy: 1.90m, speed 4.5, sadece gece (gecenin 3. dk'sı spawn)
- Davranış: doğrudan chase, fener açıksa 30m'den görür, kapalıysa 12m
- Hasar: 25 / vuruş, tek başına
- Özellik: ateşten korkmaz, barınağa giremez ama kapıda bekler

## 7. Craft Tarifleri
| Ürün | Malzeme | Kullanım |
|---|---|---|
| Balta | taş2 + lif3 + odun1 | ağaç kesme 2× hızlı |
| Mızrak | odun2 + taş1 + lif2 | 30 hasar, fırlatılabilir |
| Kamp ateşi | odun5 + taş3 | sıcaklık + ışık 18m, yemek pişirme |
| Barınak | odun10 + lif6 | gece güvenli (yamyam giremez), save |
| Sinyal ateşi | odun20 + yakıt1 | final, fener tepesinde |
| Su arıtıcı | hurda2 + lif2 | kirli su → temiz su |
| Pişmiş et | çiğ et1 + ateş | +30 açlık, +10 sağlık |

## 8. Ses & Işık
- Gündüz: kuş, dalga, rüzgar hafif (0.4)
- Gece: rüzgar 0.7, kalp atışı <30 sıcaklıkta, yamyam çığlığı 20m
- Işık: Directional sun lerp (gündüz intensity 1.0, gece 0.05), fog gündüz 60/180, gece 15/50, ateş point light 2.5

## 9. Kazanma/Kaybetme
- Kazanma: 5 gece + 3 parça + fenerde sinyal → helikopter cutscene → WIN
- Kaybetme: sağlık 0 → LOSE (Jumpscare Gardiyan)

## 10. Roadmap
- v0.1: Terrain + PlayerController + DayNight + Survival tick + 1 yamyam
- v0.2: Inventory + Craft + ateş/barınak + orman loot
- v0.3: Mağara + köy + 3 parça + fener final
- v0.4: Ses, polish, build (PC .exe), itch.io

## 11. Kontroller (Unity Input System)
WASD, Mouse, SHIFT, F, E, TAB, ESC, SPACE → `PlayerController.cs` → `InputActionAsset`

## 12. Kaynak
- Modeller: Poly Haven, Kenney.nl (CC0), SON IŞIK'taki FBX/OBJ'ler reuse edilebilir
- Ses: freesound.org (CC0)
