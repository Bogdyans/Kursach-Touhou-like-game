
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Kursach_demo1
{
    public class LeaderBoard : Leaderboardfolder.ILoader,  Leaderboardfolder.ISaver
    {
        string path;
        List<string> lines;
        public LeaderBoard(string path)
        {
            this.path = path;
            lines = File.ReadAllLines(this.path).ToList();
        }

        public void Save(string score)
        {
            int index = FindPlace(Convert.ToInt32(score), ReturnScoreValues());
            DateTime date = DateTime.Now;
            if (index != -1)
            {
                string item = $"{index+1}. {score} - {date}";
                lines.Insert(index, item);
                lines.RemoveAt(10);
                ChangeIndexes(index);
                File.WriteAllLines(path, lines);
            }
        } 
        public List<String> Load() {
            return File.ReadAllLines(path).ToList();
        }

        private int[] ReturnScoreValues()
        {
            int[] result = new int[lines.Count];
            for (int i = 0; i < lines.Count; i++)
            {
                string[] seps = lines[i].Split(' ');
                result[i] = Convert.ToInt32(seps[1]);
            }
            return result;
        }
        private int FindPlace(int score, int[] list)
        {
            for (int i = 0; i < list.Length; i++)
                if (score > list[i]) return i;
            return -1;
        }
        private string ToScore(int score, int index, string date)
        {
            return $"{index + 1}. {score} - {date}";
        }
        public void ChangeIndexes(int index)
        {
            for (int i = index+1; i < lines.Count; i++)
            {
                string[] seps = lines[i].Split(' ');
                if (seps.Length == 5) lines[i] = ToScore(Convert.ToInt32(seps[1]),
                    i, seps[3]+" " + seps[4]);
                else lines[i] = ToScore(Convert.ToInt32(seps[1]), i, seps[3]);
            }
        }
    }
}
