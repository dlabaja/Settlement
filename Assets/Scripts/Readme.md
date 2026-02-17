# Struktura Scripts složky
- Attributes - atributy
- Components - všechny Monobehaviour komponenty
- Controllers - ovládání modelů podle vstupu z komponent
- Constants - konstanty které nemůžou být v enumu (enum je jenom na int)
- Convertors - převodníky (zatím JSON->Model)
- Data - čisté datové třídy
- DataTypes - vlastní datové struktury
- Delegates - delegáti (funkce)
- Enums - enumy
- Factories - factorky tvořící třídy z Models, musí se injectovat (některé mají deps)
- Initializers - třídy initující DI, statický kontejner pro přesun async dat z bootu do hry
- Instances - instance objektů
- Interfaces - interfacy
- Services - DI třídy
- Models - třídy s "bussiness" logikou, čistý C# bez Unity
- Utils - statické třídy se statickými metodami rozšiřující jiné třídy nebo tak nějak
- Views - třídy spravující vizuální stránku hry (render, move, ...), ideálně event-driven

# Obecná architektura
#### Component -> Controller -> Model --(event)-> View
- Component - MonoBehaviour, Unity lifecycle, Input systém, init View, správa Controlleru
- Controller - reference na objekty, ovládání Modelů
- Model - Čistý objekt pokud možno bez Unity, testovatelné, logika
- View - vizuální stránka objektů (materiály, ...), ovládáno skrz eventy z Modelů

![](https://i.imgur.com/wb7Fc5Q.png)

# O projektu
- Projekt používá C# preview (11), aby vše fungovalo musí se projekt v Edit -> Project Settings -> Editor -> C# Project Modifier přidat k importu
