using System.Collections.Generic;

public static class Const
{
    public enum CustomObjects
    {
        Entity,
        Well,
        Spawn,
        Woodcutter,
        Warehouse,
        House,
        Tree,
        Stone,
        None
    }

    public enum Gender
    {
        Male,
        Female
    }

    public enum Item
    {
        None,
        Wood,
        Stone
    }

    public enum Parent
    {
        Entities,
        Buildings,
        Canvas
    }

    public enum Job
    {
        Woodcutter,
        Stonecutter
    }
        
    public const int WaterDecreaseChance = 400;
    public const int SleepDecreaseChance = 600;

    public static readonly List<string> MaleNames = new(){"Adam", "Ailwin", "Alan", "Alard", "Aldred", "Alexander", "Alured", "Amaury", "Amalric", "Anselm", "Arnald", "Asa", "Aubrey", "Baldric", "Baldwin", "Bartholomew", "Bennet", "Bertram", "Blacwin", "Colin", "Constantine", "David", "Edwin", "Elias", "Helyas", "Engeram", "Ernald", "Eustace", "Fabian", "Fordwin", "Forwin", "Fulk", "Gamel", "Geoffrey", "Gerard", "Gervase", "Gilbert", "Giles", "Gladwin", "Godwin", "Guy", "Hamo", "Hamond", "Harding", "Henry", "Herlewin", "Hervey", "Hugh", "James", "Jocelin", "John", "Jordan", "Lawrence", "Leofwin", "Luke", "Martin", "Masci", "Matthew", "Maurice", "Michael", "Nigel", "Odo", "Oliva", "Osbert", "Norman", "Nicholas", "Peter", "Philip", "Ralf", "Ranulf", "Richard", "Robert", "Roger", "Saer", "Samer", "Savaric", "Silvester", "Simon", "Stephan", "Terric", "Terry", "Theobald", "Thomas", "Thurstan", "Umfrey", "Waleran", "Walter", "Warin", "William", "Wimarc", "Ymbert"};
    public static readonly List<string> FemaleNames = new(){"Ada", "Adelina", "Agnes", "Albreda", "Aldith", "Aldusa", "Alice", "Alina", "Amanda", "Amice", "Amiria", "Anabel", "Annora", "Ascilia", "Avelina", "Avoca", "Avice", "Beatrice", "Basilea", "Bela", "Berta", "Celestria", "Christian", "Cecilia", "Clarice", "Constance", "Dionisia", "Edith", "Eleanor", "Elizabeth", "Emma", "Estrilda", "Isabel", "Eva", "Felicia", "Fina", "Goda", "Golda", "Grecia", "Gundrea", "Gundred", "Gunnora", "Haunild", "Hawisa", "Helen", "Helewise", "Hilda", "Ida", "Idonea", "Isolda", "Joan", "Julian", "Katherine", "Leticia", "Liecia", "Linota", "Lora", "Lucia", "Mabel", "Malota", "Margaret", "Margery", "Marsilia", "Mary", "Matilda", "Mazelina", "Millicent", "Muriel", "Nesta", "Nicola", "Philippa", "Petronilla", "Primeveire", "Richenda", "Richolda", "Roesia", "Sabina", "Sabelina", "Sarah", "Susanna", "Sybil", "Wymarc"};
    public static int GameSpeed = 1;
}