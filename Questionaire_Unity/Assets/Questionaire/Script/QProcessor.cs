using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Questionaire
{

    public class QProcessor
    {

        QModel _qmodel;
        QDataParser.ParseResult _rawParseResult;

        public QProcessor(QModel qmodel, QDataParser.ParseResult parseResult)
        {
            this._qmodel = qmodel;
            this._rawParseResult = parseResult;
        }

        public Ticket ProcessChoice(ChoiceStats pick_choice) {
            return new Ticket();
        }

        public Ticket ProcessTicket(Ticket ticket) {
            return new Ticket();
        }

        public Ticket GetTicketFromID(string id) {
            int EventStatsIndex = this._rawParseResult.EventStats.FindIndex(x => x._ID == id);
            var ticket = new Ticket();

            if (EventStatsIndex < 0) return ticket;

            EventStats eventStat = this._rawParseResult.EventStats[EventStatsIndex];

            ticket.eventStats = eventStat;

            if (eventStat.Tag == ParameterFlag.EventTag.Choice) {

            }

            return ticket;
        }

        private List<ChoiceStats> GetChoiceSet(string choice_id) {

            List<ChoiceStats> choiceSet = _rawParseResult.ChoiceStats.FindAll(x => x.ChoiceID == choice_id);

            return FilterSelectedChoice(choiceSet);
        }

        private List<ChoiceStats> FilterSelectedChoice(List<ChoiceStats> choice_set) {
            List<ChoiceStats> filterSet = new List<ChoiceStats>();
            int choiceLength = choice_set.Count;
            for (int i = 0; i < choiceLength; i++) {
               bool isSelected = this._qmodel.IsChoiceSelected(choice_set[i].ChoiceID, choice_set[i].UniqueID);
                if (!isSelected)
                    filterSet.Add(choice_set[i]);
            }

            return filterSet;
        }

    }
}