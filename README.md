# PROJET C# - M1 EIF - Ialgen ALLAL & Bertrand GOULOUMES

## Comment jouer ?

Notre jeu est inspiré du jeu Space Invaders. Nous avons gardé la mécanique de base où le joueur représenté par un vaisseau doit faire face à des vagues ennemies et peut leur tirer dessus en tapant sur la barre d’espace et se déplacer latéralement/horizontalement (flèches de gauche et de droite) dans les limites de la Box (cadre blanc).

Nous avons ajouté des mécaniques différentes et un thème. En effet le jeu s’appelle Space Rebellion, faisant allusion à la saga Star Wars (par le design de la page d’accueil, l’allure des vaisseaux ennemis : chasseurs de l’Empire).

Les variantes sont que les chasseurs apparaissent tous par le coin nord-ouest (ou haut gauche) de la box a rythme dépendant du niveau de jeu choisit par le joueur. Et se déplacent horizontalement jusqu’à rencontrer un mur et descendre d’une ligne et de partir dans le sens inverse.

Le jeu se finit lorsqu’un chasseur de l’Empire (`class Invader`) est sur le point de se retrouver sur la même ligne que le vaisseau de la Résistance (`class DefenderShip`), le rendant vulnérable et annonçant la fin de la Résistance.

Vous pouvez augmenter la difficulté selon le pilote sélectionné ce qui accélèrera la vitesse des chasseurs ennemis.

Vous ne pouvez tirer qu'un missile à la fois.

Il est possible de rejouer, votre score (nombre de vaisseaux abattus et temps de survie) sera affiché.


## Structure du dossier

Le jeu est composé de 3 fichiers de code principaux : 
  - Program.cs 
  - HomePage.cs : contient la `class HomePage` qui gère l'interface entre les manches.
  - Game.cs : contient la `class Game` qui gère une manche.

Puis de 4 fichiers contenant les codes de 4 classes d’objets du jeu: 
  - DefenderShip.cs : vaisseau contrôlé par le joueur, 
  - Invader.cs : vaisseau ennemi, 
  - Box.cs : cadre de jeu et aussi de la page d’accueil, 
  - Bullet.cs : missiles envoyés par les vaisseaux.

## Structure des classes principales

##### `class Program`

Contient 2 boucles `while` imbriquées. La boucle intérieure gère chaque manche individuellement en utilisant l'instance de la `class Game` nouvellement crée. La boucle extérieure permet d'effectuer lla transition entre les manches ainsi que la fin de partie.

##### `class HomePage`
Les 2 méthodes principales `Launch` et `Draw` fonctionnent de concert pour afficher à l'utilisateur les options de jeu qui lui sont possibles comme le niveau de difficulté.

##### `class Game`
Cette classe contient tous les attributs et méthodes relatif à une manche. Les méthodes principales sont les méthodes `Update` qui actualisent l'état du jeu à chaque rafraichissement par la boucle de la boucle de la `class Program`.
