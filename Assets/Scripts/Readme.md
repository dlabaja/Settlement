# Struktura Scripts složky
- Attributes - atributy
- Components - všechny Monobehaviour komponenty
- Convertors - převodníky (zatím JSON->Model)
- Data - čisté datové třídy
- Defaults - defaultní instance datových tříd, fallback při nepovedené konverzi
- Enums - enumy
- Factories - factorky tvořící třídy z Models, musí se injectovat pokud mají deps
- Initializers - třídy initující DI, statický kontejner pro přesun dat
- Interfaces - interfacy
- Managers - DI třídy
- Models - třídy s "bussiness" logikou
  - Controllers - obsahují tranform nebo mají logiku pro ovládání objektů
  - Objects - třídy pro vlastní objekty
  - Systems - systémy používané objekty
- Utils - statické třídy se statickými metodami rozšiřující jiné třídy nebo tak nějak
- Views - třídy spravující vizuální stránku hry (render, move, ...), ideálně event-driven

Projekt používá C# preview (11), aby vše fungovalo musí se projekt v Edit -> Project Settings -> Editor -> C# Project Modifier přidat k importu