using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;


namespace Questionaire {
    public class QBuilder
    {
        private QModel _qmodel;
        private QDataParser _qDataParser;
        private QProcessor _qProcessor;

        private System.Action OnBuildReady;

        public QBuilder() {
            _qmodel = new QModel();
            _qDataParser = new QDataParser(new string[] { ParameterFlag.DatabasePath.StampingEventStats },
                                                            ParameterFlag.DatabasePath.ChoiceStats,
                                                            ParameterFlag.DatabasePath.ExaminationStats);
            Init();
        }

        #region Public API
        public QBuilder OnComplete(System.Action ReadyCallback) {
            OnBuildReady = ReadyCallback;
            return this;
        }

        public Ticket ProcessChoice(ChoiceStats pick_choice)
        {
            if (_qProcessor == null) return default(Ticket);
            return _qProcessor.ProcessChoice(pick_choice);
        }

        public Ticket ProcessTicket(Ticket ticket)
        {
            if (_qProcessor == null) return default(Ticket);

            return _qProcessor.ProcessTicket(ticket);
        }

        public void Reset() {
            
        }
        #endregion

        #region Private API
        private async void Init()
        {
            QDataParser.ParseResult parseResult = await _qDataParser.Parse();

            _qProcessor = new QProcessor(_qmodel, parseResult);

            if (OnBuildReady != null)
                OnBuildReady();
        }
        #endregion


    }
}