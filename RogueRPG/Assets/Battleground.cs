using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battleground : MonoBehaviour
{

    //[SerializeField] List<Character> heroSide = new List<Character>();
    //[SerializeField] List<Character> enemySide = new List<Character>();
    //[SerializeField] Tile[] heroTiles = new Tile[4];
    //[SerializeField] Tile[] enemyTiles = new Tile[4];
    List<Tile> tiles = new List<Tile>();
    CombHUDManager cHUDManager;

    void Awake()
    {
        cHUDManager = FindObjectOfType<CombHUDManager>();
        tiles.Capacity = 8;
        //heroSide.Capacity = 4;
        //enemySide.Capacity = 4;
        CreateTiles();
    }

    public void MoveCharacterTo(Character character, int position)
    {
        //if (character.IsPlayable())
        //{
        //    if (heroSide[position] != null)
        //    {
        //        heroSide.Remove(character);
        //        heroSide.Insert(position, character);
        //    }
        //    else
        //    {
        //        int oldPosition = heroSide.IndexOf(character);
        //        heroSide[position] = character;
        //        heroSide[oldPosition] = null;
        //    }
        //}
        //else
        //{
        //    if (enemySide[position] != null)
        //    {
        //        enemySide.Remove(character);
        //        enemySide.Insert(position, character);
        //    }
        //    else
        //    {
        //        int oldPosition = enemySide.IndexOf(character);
        //        enemySide[position] = character;
        //        enemySide[oldPosition] = null;
        //    }
        //}
        tiles[character.getPosition()].setOccupant(tiles[position].getOccupant());
        tiles[position].setOccupant(character);
        ShowCharactersToThePlayer();
    }

    public void ShowCharactersToThePlayer()
    {
        //setOccupantsOfAllTiles(heroSide, enemySide);
        cHUDManager.ShowCombatants(tiles);
    }

    public int getPositionOf(Character character)
    {
        return tiles.Find(t => t.getOccupant() == character).getIndex();
        //if (character.IsPlayable())
        //{
        //    return heroSide.IndexOf(character);
        //}
        //else
        //{
        //    return enemySide.IndexOf(character);
        //}
    }

    public int HowManyHeroes()
    {
        return tiles.FindAll(t => t.getOccupant() != null ? t.getOccupant().IsPlayable() : false).Count;
        //int heroesCountdown = 0;
        //for (int i = 0; i < heroSide.Count; i++)
        //{
        //    if (heroSide[i] != null)
        //    {
        //        heroesCountdown++;
        //    }
        //}
        //return heroesCountdown;
    }

    public int HowManyEnemies()
    {
        return tiles.FindAll(t => t.getOccupant() != null ? !t.getOccupant().IsPlayable() : false).Count;
        //int enemiesCountdown = 0;
        //for (int i = 0; i < enemySide.Count; i++)
        //{
        //    if (enemySide[i] != null)
        //    {
        //        enemiesCountdown++;
        //    }
        //}
        //return enemiesCountdown;
    }

    public List<Character> getHeroSide()
    {
        List<Character> characters = new List<Character>();
        for (int i = 0; i < tiles.Count / 2; i++)
        {
            characters.Add(tiles[i].getOccupant());
        }
        return characters;
    }
    public List<Character> getEnemySide()
    {
        List<Character> characters = new List<Character>();
        for (int i = tiles.Count / 2; i < tiles.Count; i++)
        {
            characters.Add(tiles[i].getOccupant());
        }
        return characters;
    }

    public Tile[] getHeroesTiles()
    {
        return tiles.FindAll(t => t.isFromHero()).ToArray();
        //return heroTiles;
    }

    public Tile[] getEnemiesTiles()
    {
        return tiles.FindAll(t => !t.isFromHero()).ToArray();
        //return enemyTiles;
    }

    public Tile[] getMySideTiles(bool side)
    {
        return side ? getHeroesTiles() : getEnemiesTiles();
        //if (side)
        //    return heroTiles;
        //else
        //    return enemyTiles;
    }

    public Tile[] getMyEnemiesTiles(bool side)
    {
        return getMySideTiles(!side);
        //if (side)
        //    return enemyTiles;
        //else
        //    return heroTiles;
    }

    public void ClearAndSetASide(List<Character> side)
    {
        bool sideIsPlayers = false;
        int sideSize = side.Count;
        for (int i = 0; i < side.Count; i++)
        {
            if (side[i] != null)
            {
                sideIsPlayers = side[i].IsPlayable();
            }
        }
        if (sideIsPlayers)
        {
            for (int i = 0; i < tiles.Count / 2; i++)
            {
                tiles[i].setOccupant(null);
            }
            //heroSide.Clear();
            //this.heroSide = side;
            //for (int i = 0; i < 4 - sideSize; i++)
            //{
            //    this.heroSide.Add(null);
            //}
        }
        else
        {
            for (int i = tiles.Count / 2; i < tiles.Count; i++)
            {
                tiles[i].setOccupant(null);
            }
            //enemySide.Clear();
            //this.enemySide = side;
            //for (int i = 0; i < 4 - sideSize; i++)
            //{
            //    this.enemySide.Add(null);
            //}
        }
    }

    void CreateTiles()
    {
        for (int i = 0; i < tiles.Capacity; i++)
        {
            if (i < tiles.Count / 2)
            {
                tiles.Add(new Tile(i, true, this));
                //tiles[i] = new Tile(i, true, this);
            }
            else
            {
                tiles.Add(new Tile(i, false, this));
            }
        }
        //for (int i = 0; i < heroTiles.Length; i++)
        //{
        //    heroTiles[i] = new Tile(i, true, this);
        //}
        //for (int i = 0; i < enemyTiles.Length; i++)
        //{
        //    enemyTiles[i] = new Tile(i, false, this);
        //}
    }

    //void setOccupantsOfAllTiles(List<Character> heroSide, List<Character> enemySide)
    //{
    //    for (int i = 0; i < heroSide.Count; i++)
    //    {
    //        heroTiles[i].setOccupant(heroSide[i]);
    //    }
    //    for (int i = 0; i < enemySide.Count; i++)
    //    {
    //        enemyTiles[i].setOccupant(enemySide[i]);
    //    }
    //}

    public Tile GetTile(Character character)
    {
        return tiles.Find(t => t.getOccupant() == character);
        //if (character.IsPlayable())
        //{
        //    foreach (Tile tile in heroTiles)
        //    {
        //        if (tile.IsYourCharacter(character))
        //            return tile;
        //    }
        //}
        //else
        //{
        //    foreach (Tile tile in enemyTiles)
        //    {
        //        if (tile.IsYourCharacter(character))
        //            return tile;
        //    }
        //}
        //return null;
    }

    public Tile[] GetAlivePCTiles()
    {
        return tiles.FindAll(t => t.getOccupant() != null ? t.getOccupant().IsPlayable() && t.getOccupant().isAlive() : false).ToArray();

        //List<int> alivePCIndex = new List<int>();
        //foreach (Tile tile in heroTiles)
        //{
        //    if (tile.IsOccupied())
        //    {
        //        alivePCIndex.Add(tile.getIndex());
        //    }
        //}
        //Tile[] alivePCTiles = new Tile[alivePCIndex.Count];
        //for (int i = 0; i < alivePCIndex.Count; i++)
        //{
        //    alivePCTiles[i] = heroTiles[alivePCIndex[i]];
        //}
        //return alivePCTiles;
    }

    public Tile[] GetAliveNPCTiles()
    {
        return tiles.FindAll(t => t.getOccupant() != null ? !t.getOccupant().IsPlayable() && t.getOccupant().isAlive() : false).ToArray();
        //List<int> aliveNPCIndex = new List<int>();
        //foreach (Tile tile in enemyTiles)
        //{
        //    if (tile.IsOccupied())
        //    {
        //        aliveNPCIndex.Add(tile.getIndex());
        //    }
        //}
        //Tile[] aliveNPCTiles = new Tile[aliveNPCIndex.Count];
        //for (int i = 0; i < aliveNPCIndex.Count; i++)
        //{
        //    aliveNPCTiles[i] = enemyTiles[aliveNPCIndex[i]];
        //}
        //return aliveNPCTiles;
    }

    public Tile[] GetAliveOpponents(Character character)
    {
        return character.IsPlayable() ? GetAliveNPCTiles() : GetAlivePCTiles();
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
        //public Tile[] GetAlliesTiles() { return fromHero ? battleground.heroTiles : battleground.enemyTiles; }
        //public Tile[] GetEnemiesTiles() { return fromHero ? battleground.enemyTiles : battleground.heroTiles; }
        public Tile[] GetAlliesTiles() { return fromHero ? battleground.getHeroesTiles() : battleground.getEnemiesTiles(); }
        public Tile[] GetEnemiesTiles() { return fromHero ? battleground.getEnemiesTiles() : battleground.getHeroesTiles(); }
    }
}