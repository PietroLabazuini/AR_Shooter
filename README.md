# 🎮 Projet Unity : Jeu de tir en réalité augmentée
Ce projet vise à développer une application en réalité augmentée basée sur le framework ARFoundation d'Unity.
J'ai fixé plusieurs objectifs à atteindre pour construire une roadmap pour mon développement :

- 🕹️ Systèmes relatifs au joueur :
  - Implémentation des armes ✅
  - Animations de tir, de rechargement et de changement d'arme ⌛
  - Implémentation d'une interface utilisateur pour téléphone ⌛

- 🔫 Implémentation d'entités ennemies :
  - Système de points de vie et de barre de vie ✅
  - Implémentation des déplacements via NavMesh ⌛
  - Implémentation des comportements de tir ❌

- 🤳🏼 Intégration de la réalité augmentée à l'application :
  - Détection des surfaces dans l'environnement réel (sol, murs, meubles) ✅
  - Classification des surfaces détectées pour dissocier les obstacles des surfaces navigables ✅
  - Intégration de NavMesh aux surfaces pour permettre aux ennemis de se déplacer en évitant les obstacles ❌

## 🛠️ Technologies et outils utilisés

- **Unity** : Moteur de jeu pour le développement des fonctionnalités interactives.
- **AR Foundation** : Framework Unity pour les applications de réalité augmentée.
- **C#** : Langage de programmation pour les scripts.
- **Animator** : Gestion des transitions et animations des armes.
- **UI Toolkit** : Conception de l'interface utilisateur (barre de vie, menus, etc.).
