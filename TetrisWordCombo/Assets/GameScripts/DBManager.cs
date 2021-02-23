using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MongoDB.Driver;
using MongoDB.Bson;

public class Stats
{
    public ObjectId id { set; get; }
    public string username { set; get; }
    public string score { set; get; }
    public string level { set; get; }
    public string words { set; get; }
    public List<string> wordlist { set; get; }
    public string playtime { set; get; }
    public string date { set; get; }
}

public class DBManager : MonoBehaviour
{
    private const string MONGO_URI = "";
    private const string DB_NAME = "";


    private static MongoClient client = new MongoClient("mongodb+srv://TetrisWordsUser:0XLZe4FLFwAtOGje@tetriswords.9v03p.mongodb.net/TetrisWords?retryWrites=true&w=majority");
    private IMongoDatabase database = client.GetDatabase("test");
    private IMongoCollection<Stats> collection;


    // Start is called before the first frame update
    void Start()
    {
        database = client.GetDatabase("TetrisWordsDB");
        collection = database.GetCollection<Stats>("UserScores");

        /*      BD Insert Tester
        Stats tester = new Stats();
        tester.username = "TestR";
        tester.score = "10000";
        tester.level = "3";
        tester.words = "10";
        tester.wordlist = new List<string>() {"ALL","BASE","US","HOPE","ARGO","TEST","PINK" };
        tester.playtime = "00:05:39";
        tester.date = "2/9/2021";

        var document = new BsonDocument();
        collection.InsertOne(tester);
        */

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region Insert Score
    public bool InsertPlayerScore(string name, string score, string level, string words, List<string> list, string time, string date)
    {
        Stats GameStats = new Stats();
        GameStats.username = name;
        GameStats.score = score;
        GameStats.level = level;
        GameStats.words = words;
        GameStats.wordlist = list;
        GameStats.playtime = time;
        GameStats.date = date;

        try
        {
            collection.InsertOne(GameStats);
        }
        catch (MongoConnectionException)
        {
            return false;
        }
        return true;
    }
    #endregion

    #region Get Scores
    public List<Stats> GetHighscores()
    {
        database = client.GetDatabase("TetrisWordsDB");
        collection = database.GetCollection<Stats>("UserScores");

        var highscores = collection.Find(new BsonDocument()).ToList();

        return highscores;
    }
    #endregion
}
