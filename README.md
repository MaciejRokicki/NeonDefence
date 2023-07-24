# NeonDefence

#### Założenia gry
Gra typu tower defence, polegająca na rozmieszczaniu wież broniących bazę przed przeciwnikami. Dostepne są 4 różne wieże - podstawowa, laser, strzelająca rakietami oraz pole spowalniajace przeciwników. Po postawieniu danej wieży jej koszt wzrasta. Dodatkowo po zapełnieniu paska doświadczenia poprzez niszczenie przeciwników jest możliwość wyboru 1 z 3 ulepszeń. Te ulepszenia mają 3 różne stopnie rzadkości - podstawowe (szare), rzadkie (niebieskie) i legendarne (złote). Mogą one odnawiać zdrowie bazy, dodawać walutę, ale przede wszystkim ulepszają wieże - zarówno podstawowe atrybuty, np. obrażenia, zasieg, szybkość obrotu, ilość pocisków na sekundę, prędkość pocisku, ale też mogą nadawać wieżom nowe możliwości, np. wybuchowe, penetrujące, samonaprowadzające, spowalniające, czy zatruwające pociski. Przeciwnicy pojawiają się falami w nieskończoność, aż do utraty całego zdrowia bazy. W grze występuje kilka rodzajów przeciwników - podstawowy, szybki I, szybki II, wytrzymały I, wytrzymały II, szybki i wytrzymały I, szybki i wytrzymały II. Zaimplementowano również system zbierania informacji o rozgrywce informujący o zdobytych punktach, czasie gry, zniszcoznych przeciwnikach, ilości wybranych ulepszeń, zebranej waluty oraz ilośc postawionych poszczególnych wież z informacją o ilości pokonanych przez nich przeciwników.

#### Aspekty techniczne
Jest to gra przeznaczona na komputery stacjonarne z windows'em oraz telefony komórkowe z systemem android.
W implementacji zastosowano takie wzorce jak: singleton (jeszcze nie jako klasa abstrakcyjna, wykorzystywany przy manager'ach, np. GameManager, TurretManager), obserwator (w postaci c# events, np. do informowania o zmianie stanu zdrowia gracza/bazy), strategia (np. do implementacji rodzaju ataku wieży - pocisk albo laser), dekorator (do nakładania efektów na przeciwników, np. spowolnienie, zatrucie. Te efekty nadawane są na określone wieże podczas wyboru ulepszeń).
Z elementów wchodzących w skład silnika Unity, wykorzystano: animacje, 2D Tilemaps, fizykę 2D, URP + efekty postprocessingu + oświetlenie 2D, ScriptableObjects (do prostego tworzenia nowych przeciwników, ulepszeń, wież), własne narzędzie w formi skryptu edytora do łatwego tworzenia nowych ulepszeń (po prostu wyklikuje się poszczególne ustawienia nowego ulepszenia z poziomu unity).

## Builds

### Windows build
https://github.com/MaciejRokicki/NeonDefence/releases/download/v1.0.1/NeonDefencePC.zip

### Android build
https://github.com/MaciejRokicki/NeonDefence/releases/download/v1.0.1/NeonDefenceAndroid.apk

## Screenshots

### Android
![](/../master/Media/android/1.png)
![](/../master/Media/android/2.png)
![](/../master/Media/android/3.png)
![](/../master/Media/android/4.png)
![](/../master/Media/android/5.png)
![](/../master/Media/android/6.png)

### Windows
![](/../master/Media/pc/1.png)
![](/../master/Media/pc/2.png)
![](/../master/Media/pc/3.png)
![](/../master/Media/pc/4.png)
![](/../master/Media/pc/5.png)
![](/../master/Media/pc/6.png)
