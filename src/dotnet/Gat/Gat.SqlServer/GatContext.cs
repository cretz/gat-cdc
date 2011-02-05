using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.SqlServer.Server;
using System.Text.RegularExpressions;

namespace Gat.SqlServer
{
    class GatContext
    {
        private string _schema;
        private string _table;
        private List<string> _columns;
        private TriggerAction _triggerAction;

        //properties

        public string Schema
        {
            get { return _schema; }
            set { _schema = value; }
        }

        public string Table
        {
            get { return _table; }
            set { _table = value; }
        }

        public List<string> Columns
        {
            get { return _columns; }
            set { _columns = value; }
        }

        public TriggerAction TriggerAction
        {
            get { return _triggerAction; }
            set { _triggerAction = value; }
        }

        //helper methods

        public bool IsRegexColumnPresent(Regex regex)
        {
            foreach (string column in Columns)
            {
                if (regex.IsMatch(column))
                {
                    return true;
                }
            }
            return false;
        }

    }
}
