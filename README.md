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


## Diagrammes 

1) Diagramme de classe : https://eur01.safelinks.protection.outlook.com/?url=https%3A%2F%2Flucid.app%2Flucidchart%2F337ae287-e9ca-4e75-9202-cfff94a394c7%2Fedit%3Fviewport_loc%3D2620%252C-3766%252C9511%252C4202%252C0_0%26invitationId%3Dinv_e5c6971d-d494-4715-895a-ae19f7f092b0&data=05%7C02%7C23346%40ecam.be%7C8e4db4fff28b458fe7ef08de3f40c1c1%7Ce3d6f09e9ba94a36ad75d9039be4fe29%7C0%7C0%7C639017744510503711%7CUnknown%7CTWFpbGZsb3d8eyJFbXB0eU1hcGkiOnRydWUsIlYiOiIwLjAuMDAwMCIsIlAiOiJXaW4zMiIsIkFOIjoiTWFpbCIsIldUIjoyfQ%3D%3D%7C0%7C%7C%7C&sdata=4mVoeZJjGEeLJJGLr%2Bw7t05HGJSWn6ZTHmfmTLftYuE%3D&reserved=0
2) Diagramme d'activité : https://lucid.app/lucidchart/314e2741-4b90-4a1e-a1fe-aa56c77ee8ef/edit?viewport_loc=190%2C-689%2C1970%2C2702%2C0_0&invitationId=inv_4a4ed2df-1a5d-4ebe-a8bb-961a2f66afe7
3) Diagramme de séquence : https://lucid.app/lucidchart/64743c52-2061-4bd2-86a7-f40089f3fb36/edit?invitationId=inv_6b7f402c-254b-4787-88a8-34ff56e382fb


## Bibliothèques utilisées

- C# 
- Avalonia
-SQLite
