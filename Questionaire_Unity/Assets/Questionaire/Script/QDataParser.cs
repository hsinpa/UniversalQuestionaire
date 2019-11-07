using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Questionaire.Utility;
using System.Linq;

namespace Questionaire
{
    public class QDataParser
    {
        string[] RawEventStats;
        string RawChoicePath;
        string RawExaminationPath;
       
        public QDataParser(string[] EventStatsPath, string ChoicePath, string ExaminationPath)
        {
            int eventStatCount = EventStatsPath.Length;

            RawEventStats = new string[eventStatCount];

            for (int i = 0; i < eventStatCount; i++) {
                RawEventStats[i] = Resources.Load<TextAsset>(EventStatsPath[i]).text;
            }

            RawChoicePath = Resources.Load<TextAsset>(ChoicePath).text;
            RawExaminationPath = Resources.Load<TextAsset>(ExaminationPath).text;
        }

        public async Task<ParseResult> Parse() {
            ParseResult parseResult = new ParseResult();

            if (RawEventStats == null || this.RawChoicePath == null || this.RawExaminationPath == null) return parseResult;

            await Task.Run(() =>
            {
                parseResult.EventStats = ParseStatsArray<EventStats>(RawEventStats);
                parseResult.ChoiceStats = ParseStats<ChoiceStats>(RawChoicePath);
            });

            Reset();

            return (parseResult);
        }

        private List<T> ParseStatsArray<T>(string[] RawStatArray) where T : struct
        {
            List<T> StatArray = new List<T>();

            int eventStatCount = RawStatArray.Length;

            for (int i = 0; i < eventStatCount; i++)
            {
                StatArray.AddRange(JsonHelper.FromJson<T>(RawStatArray[i]).ToList());
            }

            return StatArray;
        }

        private List<T> ParseStats<T>(string RawStatString) where T : struct
        {
            List<T> StatArray = new List<T>();
            StatArray.AddRange(JsonHelper.FromJson<T>(RawStatString).ToList());

            return StatArray;
        }

        public struct ParseResult {
            public List<ChoiceStats> ChoiceStats;
            public List<EventStats> EventStats;
            public List<ExaminationStats> ExaminationStats;
        }

        private void Reset() {
            RawEventStats = null;
            RawChoicePath = null;
            RawExaminationPath = null;
        }

    }
}