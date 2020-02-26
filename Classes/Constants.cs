using System.Collections.Generic;
using System.Security.Cryptography;
using System;
using System.Threading;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ITQuiz.Classes {
    static class AllConstants {
        public static int size;
        private static bool isPrepared;
        public static Dictionary<string, List<AQuestion>> sets = new Dictionary<string, List<AQuestion>>();
        public static List<AQuestion> set = new List<AQuestion>();
        public static Dictionary<string, int> Scores = new Dictionary<string, int>();
        public static MongoClient connect = new MongoClient();
        public static IMongoDatabase db = connect.GetDatabase("ITQuiz");
        public static IMongoCollection<BsonDocument> Coll = db.GetCollection<BsonDocument>("ITQuiz");

        public static string[] ActualQuestions = {
            "Question 1: Who?",
            "Question 2: Where?",
            "Question 3: Why?",
            "Question 4: Who?"
        };

        public static string[] ActualRightAnswers = {
            "One",
            "A",
            "I",
            "1"
        };

        public static string[] ActualOption2 = {
            "Two",
            "B",
            "II",
            "2"
        };

        public static string[] ActualOption3 = {
            "Three",
            "C",
            "III",
            "3"
        };

        public static string[] ActualOption4 = {
            "Four",
            "D",
            "IV",
            "4"
        };

        public static void Prepare() {
            for (int i = 0; i<ActualQuestions.Length; i++) {
                set.Add(new AQuestion(ActualQuestions[i], ActualRightAnswers[i], ActualOption2[i],
                        ActualOption3[i], ActualOption4[i]));
            }
            size = ActualQuestions.Length;
            isPrepared = true;
        }

        public static void AddSession(string CollegeID) {
            if (!isPrepared) Prepare();
            var newList = copyGenerator();
            copyGenerator().Shuffle();
            sets.Add(CollegeID, newList);
            Scores.Add(CollegeID, 0);
        }

        public static List<AQuestion> copyGenerator() {
            List<AQuestion> list = new List<AQuestion>();
            foreach (var item in set){
                list.Add(new AQuestion(item.Ques, item.RightOption, item.Option2, 
                    item.Option3, item.Option4));
            }
            return list;
        }

        public static void flushToDB(string TeamName) {
            BsonDocument document = new BsonDocument();
            document.Add(new BsonElement("CollegeID", TeamName));
            document.Add(new BsonElement("Score", Scores[TeamName]));
            Coll.InsertOne(document);
            
        }
    }
    public static class ThreadSafeRandom {
        [ThreadStatic] private static Random Local;

        public static Random ThisThreadsRandom {
            get { return Local ?? (Local = new Random(unchecked(Environment.TickCount * 31 + Thread.CurrentThread.ManagedThreadId))); }
        }
    }

    static class MyExtensions {
        public static void Shuffle<T>(this IList<T> list) {
            int n = list.Count;
            while (n > 1) {
                n--;
                int k = ThreadSafeRandom.ThisThreadsRandom.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    } 
}