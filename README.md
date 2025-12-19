#  Projet CHESS DB

---

##  Membres du binôme
- Manar : 23346
- Ayoub : 23218

---

##  Introduction 
Notre projet CHESS est une application de gestion liée au monde des échecs.
L’objectif principal est de permettre une structure de gestion organisée des joueurs, des compétitions et des matchs.



---
## Description 
L’application a été développée en C#, en utilisant le framework Avalonia UI pour l’interface graphique et sur l l’architecture MVVM (Model–View–ViewModel).
Nous avons également ajouter une base de données  SQLite, gérée via Entity Framework Core permettant la conservation des données.
Cette base de données permet d’enregistrer de manière persistante les informations telles que  :
	- les joueurs
	- les compétions
	- les matchs

Un contexte de base de données (le fichier : ChessDbContext) permet cela, il centralise l’accès aux données et définir quelles entités sera conservée dans les tables. 
Les données ne sont donc plus stockées juste en mémoire (perdues à la fermeture de l’application), mais conservées entre les différentes exécutions de l’application.

Notre fonctionnalité supplémentaire est la recherche de joueurs. Elle a été ajoutée dans les pages Joueurs, Classement et Compétitions.
Elle permet de filtrer rapidement les joueurs à partir de leur nom, améliorant ainsi l’ergonomie de l’application.
La logique de recherche est gérée dans les ViewModels, conformément à l’architecture MVV.

## Adaptabilité du projet à une autre fédération
Notre projet a été conçu de manière à être facilement adaptable à une autre fédération d’échecs (ou à une organisation du même type). Principalement grace à nos interfaces.
Nos viewsmodels ne mentionnent aucune discipline en particulier car les classes de ce dossier sont reliés aux interfaces qui elles sont générales. Il faudra donc rajouter les classes qu'il manque sous chaque interface et changer les termes echecs dans le MainWindow.axaml.cs 
Plusieurs autres points justifient cela :
- Modèle générique :
Les entités principales (joueurs, compétitions, matchs) sont indépendantes des règles spécifiques à une fédération donnée.
- Champs personnalisables :
Les différences entre fédérations peuvent être gérées via des fichiers additionnels sans modifier le cœur du modèle.
- Séparation des couches :
La logique métier est séparée de l’interface graphique grâce à l’architecture MVVM, ce qui permet de modifier les règles ou les traitements sans impacter l’UI.
- Base de données relationnelle :
L’utilisation de SQLite et d’Entity Framework Core permet une migration simple vers une autre base de données ou une autre structure si nécessaire.

Ainsi, notre projet peut être réutilisé ou étendu pour gérer d’autres fédérations avec un coût de modification tres petit.


## Principes SOLID appliqués dans le projet

S : Principe de Responsabilité Unique 
Ce principe dit qu’une classe ne doit avoir qu’une seule raison de changer donc une seule responsabilité.

Dans notre projet :
-  Les Models représentent uniquement les données 
-  Les ViewModels gèrent la logique applicative et l’état de l’interface.
-  Les Views s’occupent de l’affichage et à l’interaction utilisateur.

O : Principe Ouvert/Fermé (OCP)
Ce principe indique qu’une entité logicielle doit être ouverte à l’extension, mais fermée à la modification.
Dans notre projet :
- L’ajout de nouvelles informations sur les joueurs se fait via des champs personnalisés, sans modifier la classe principale.


## Conclusion 

Pour conclure le projet a permis de créer une application structurée répondant à des besoins concrets de gestion dans le domaine des échecs.
Grâce à l’utilisation de C#, Avalonia, SQLite, Entity Framework Core et au respect de principes de conception tels que MVVM et SOLID. Ce projet présente d'excellentes qualités de maintenabilité et d’adaptabilité.

## Bibliothèques utilisées

- C# 
- Avalonia
-SQLite
