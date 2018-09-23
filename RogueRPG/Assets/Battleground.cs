using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battleground : MonoBehaviour
{

    [SerializeField] List<Character> heroSide = new List<Character>();
    [SerializeField] List<Character> enemySide = new List<Character>();
    [SerializeField] Tile[] heroTiles = new Tile[4];
    [SerializeField] Tile[] enemyTiles = new Tile[4];
    CombHUDManager cHUDManager;

    void Awake()
    {
        cHUDManager = FindObjectOfType<CombHUDManager>();
        heroSide.Capacity = 4;
        enemySide.Capacity = 4;
        CreateTiles();
    }

    public void MoveCharacterTo(Character character, int position)
    {
        if (character.isPlayable())
        {
            if (heroSide[position] != null)
            {
                heroSide.Remove(character);
                heroSide.Insert(position, character);
            }
            else
            {
                int oldPosition = heroSide.IndexOf(character);
                heroSide[position] = character;
                heroSide[oldPosition] = null;
            }
        }
        else
        {
            if (enemySide[position] != null)
            {
                enemySide.Remove(character);
                enemySide.Insert(position, character);
            }
            else
            {
                int oldPosition = enemySide.IndexOf(character);
                enemySide[position] = character;
                enemySide[oldPosition] = null;
            }
        }
        ShowCharactersToThePlayer();
    }

    public void ShowCharactersToThePlayer()
    {
        setOccupantsOfAllTiles(heroSide, enemySide);
        cHUDManager.ShowCombatants(heroTiles, enemyTiles);
    }

    public int getPositionOf(Character character)
    {
        if (character.isPlayable())
        {
            return heroSide.IndexOf(character);
        }
        else
        {
            return enemySide.IndexOf(character);
        }
    }

    public int HowManyHeroes()
    {
        int heroesCountdown = 0;
        for (int i = 0; i < heroSide.Count; i++)
        {
            if (heroSide[i] != null)
            {
                heroesCountdown++;
            }
        }
        return heroesCountdown;
    }

    public int HowManyEnemies()
    {
        int enemiesCountdown = 0;
        for (int i = 0; i < enemySide.Count; i++)
        {
            if (enemySide[i] != null)
            {
                enemiesCountdown++;
            }
        }
        return enemiesCountdown;
    }

    public List<Character> getHeroSide()
    {
        return heroSide;
    }
    public List<Character> getEnemySide()
    {
        return enemySide;
    }

    public Tile[] getHeroesTiles() { return heroTiles; }
    public Tile[] getEnemiesTiles() { return enemyTiles; }

    public Tile[] getMySideTiles(bool side)
    {
        if (side)
            return heroTiles;
        else
            return enemyTiles;
    }

    public Tile[] getMyEnemiesTiles(bool side)
    {
        if (side)
            return enemyTiles;
        else
            return heroTiles;
    }

    public void ClearAndSetASide(List<Character> side)
    {
        bool sideIsPlayers = false;
        int sideSize = side.Count;
        for (int i = 0; i < side.Count; i++)
        {
            if (side[i] != null)
            {
                sideIsPlayers = side[i].isPlayable();
            }
        }
        if (sideIsPlayers)
        {
            heroSide.Clear();
            this.heroSide = side;
            for (int i = 0; i < 4 - sideSize; i++)
            {
                this.heroSide.Add(null);
            }
        }
        else
        {
            enemySide.Clear();
            this.enemySide = side;
            for (int i = 0; i < 4 - sideSize; i++)
            {
                this.enemySide.Add(null);
            }
        }
    }

    void CreateTiles()
    {
        for (int i = 0; i < heroTiles.Length; i++)
        {
            heroTiles[i] = new Tile(i, true, this);
        }
        for (int i = 0; i < enemyTiles.Length; i++)
        {
            enemyTiles[i] = new Tile(i, false, this);
        }
    }

    void setOccupantsOfAllTiles(List<Character> heroSide, List<Character> enemySide)
    {
        for (int i = 0; i < heroSide.Count; i++)
        {
            heroTiles[i].setOccupant(heroSide[i]);
        }
        for (int i = 0; i < enemySide.Count; i++)
        {
            enemyTiles[i].setOccupant(enemySide[i]);
        }
    }

    public Tile GetTile(Character character)
    {
        if (character.isPlayable())
        {
            foreach (Tile tile in heroTiles)
            {
                if (tile.IsYourCharacter(character))
                    return tile;
            }
        }
        else
        {
            foreach (Tile tile in enemyTiles)
            {
                if (tile.IsYourCharacter(character))
                    return tile;
            }
        }
        return null;
    }

    public class Tile
    {
        [SerializeField] Character occupant;
        int index;
        bool fromHero;
        Battleground battleground;
        Tile[] side;
        Tile[] otherSide;

        public bool IsOccupied()
        {
            if (occupant != null)
            {
                return occupant.isAlive() ? true : false;
            }
            else
            {
                return false;
            }
        }
        public bool IsYourCharacter(Character character) { return character == occupant ? true : false; }
        public Tile(int index, bool fromHero, Battleground battleground)
        {
            this.index = index;
            this.fromHero = fromHero;
            this.battleground = battleground;
        }
        public void setOccupant(Character occupant) { this.occupant = occupant; }
        public Character getOccupant() { return occupant; }
        public int getIndex() { return index; }
        public Vector2 getLocalPosition()
        {
            if (fromHero)
            {
                return FindObjectOfType<CombHUDManager>().getHeroesCombatantHUD()[index].getRectTransform().localPosition;
            }
            else
            {
                return FindObjectOfType<CombHUDManager>().getEnemiesCombatantHUD()[index].getRectTransform().localPosition;
            }
        }
        public bool isFromHero() { return fromHero; }
        public Tile[] GetAlliesTiles() { return fromHero ? battleground.heroTiles : battleground.enemyTiles; }
        public Tile[] GetEnemiesTiles() { return fromHero ? battleground.enemyTiles : battleground.heroTiles; }
    }
}