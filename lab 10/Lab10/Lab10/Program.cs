using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab10
{
    // Типа roll game 
    public class Game<T> : IEnumerable<T> where T : Player 
    {
        public BlockingCollection<T> players = new BlockingCollection<T>();
        public Dictionary<int, T> dict = new Dictionary<int, T>();
        public Player winner;
        Random rnd = new Random();
        public void StartGame()
        {
            foreach (var item in players)
            {
                item.number = rnd.Next(0, 100);
            }
            winner = players.OrderByDescending(i => i.number).First();
        }
        public void Show()
        {
            foreach (var item in players)
            {
                Console.WriteLine(item);
            }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        IEnumerator<T> GetEnumerator()
        {
            foreach (T foo in this.players)
            {
                yield return foo;
                // yield return (Foo)foo;
            }
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return ((IEnumerable<T>)players).GetEnumerator();
        }
    }

    public class Player
    {
        public string name;
        public int number;
        
        public Player(string name)
        {
            this.name = name;
            this.number = 0;

        }
        public override string ToString()
        {
            return name + " зароллил число " + number;
        }

    }

    class Program
    {
        public static void Ch(object sender, NotifyCollectionChangedEventArgs e)
        {
            Console.WriteLine("Collection changed with action " + e.Action);
        }
        static void Main(string[] args)
        {
            Player firstPlayer = new Player("Artyom");
            Player secondPlayer = new Player("Kolya");
            Player thirdPlayer = new Player("Dima");
            Player foursPlayer = new Player("Vova");

            Game<Player> RollGame = new Game<Player>();
                
            RollGame.players.Add(firstPlayer);
            RollGame.players.Add(secondPlayer);
            RollGame.players.TryAdd(thirdPlayer, TimeSpan.FromSeconds(1));
            RollGame.players.TryAdd(foursPlayer);
            RollGame.players.Take(); //удаление
            RollGame.players.CompleteAdding(); // закончили добавление
            //RollGame.players.Add(thirdPlayer); //ошибка

            RollGame.StartGame();

            RollGame.Show();
            Console.WriteLine("Победитель -  " + RollGame.winner.name);
            BlockingCollection<int> test = new BlockingCollection<int>();
            test.Add(1);
            test.Add(5);
            test.TryAdd(6);
            int x;
            test.TryTake(out x);
            foreach (var item in test)
                Console.WriteLine(item);
            RollGame.dict.Add(1, firstPlayer);
            RollGame.dict.Add(2, secondPlayer);
            RollGame.dict.Add(3, thirdPlayer);
            RollGame.dict.Add(4, foursPlayer);

            foreach (KeyValuePair<int, Player> element in RollGame.dict)
                Console.WriteLine("Ключ: {0}, Значение: {1}", element.Key, element.Value);


            if (RollGame.dict.ContainsValue(secondPlayer)) Console.WriteLine("Содержит");
            else Console.WriteLine("Не содержит");

            ObservableCollection<int> obs = new ObservableCollection<int>();
            obs.CollectionChanged += Ch;
            obs.Add(5);
            obs.Remove(5);

        }
    }
}
