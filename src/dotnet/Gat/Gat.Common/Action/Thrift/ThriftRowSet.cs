using System;
using System.Collections.Generic;
using System.Text;
using Gat.Common.Event;
using Gat.Event.Thrift;

namespace Gat.Common.Action.Thrift
{
    public class ThriftRowSet : IGatRowSet
    {
        private readonly GatRowSet _rowSet;

        public ThriftRowSet(GatRowSet rowSet)
        {
            _rowSet = rowSet;
        }

        public IEnumerable<IGatColumn> Columns
        {
            get { throw new NotImplementedException(); }
        }

        public IGatColumn GetColumn(int ordinal)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IGatRow> Rows
        {
            get { throw new NotImplementedException(); }
        }

        public IGatRow GetRow(int ordinal)
        {
            throw new NotImplementedException();
        }
    }
}
