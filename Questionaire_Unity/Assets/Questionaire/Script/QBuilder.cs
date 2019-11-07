using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;


namespace Questionaire {
    public class QBuilder
    {
        private QModel _qmodel;
        private QDataParser _qDataParser;

        public QBuilder() {
            _qmodel = new QModel();
            _qDataParser = new QDataParser(new string[] { ParameterFlag.DatabasePath.StampingEventStats },
                                                            ParameterFlag.DatabasePath.ChoiceStats,
                                                            ParameterFlag.DatabasePath.ExaminationStats);
            Init();
        }

        private async void Init() {

            QDataParser.ParseResult parseResult = await _qDataParser.Parse();

            Debug.Log(parseResult.EventStats.Count);
            Debug.Log(parseResult.ChoiceStats.Count);
            Debug.Log(parseResult.EventStats[0].MainValue);

        }

    }
}