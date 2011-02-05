using System;
using System.Collections.Generic;
using System.Text;
using Gat.Event.Thrift;
using Gat.Common.Event;

namespace Gat.Common.Action.Thrift
{
    public class ThriftEventResponse : IGatEventResponse
    {
        private static readonly IEnumerable<string> EMPTY_ENUMERABLE = new List<string>(0).AsReadOnly();

        private readonly GatEventResponse _response;
        private IGatRowSet _rowSet;

        public ThriftEventResponse(GatEventResponse response)
        {
            _response = response;
        }

        public bool? Rollback
        {
            get
            {
                if (!_response.__isset.rollback)
                {
                    return null;
                }
                else
                {
                    return _response.Rollback;
                }
            }
        }

        public string ExecuteInstead
        {
            get { return _response.ExecuteInstead; }
        }

        public IGatRowSet ReturnInstead
        {
            get
            {
                if (_response.__isset.returnInstead && _rowSet == null)
                {
                    _rowSet = new ThriftRowSet(_response.ReturnInstead);
                }
                return _rowSet;
            }
        }

        public IEnumerable<string> Messages
        {
            get
            {
                return !_response.__isset.messages ? EMPTY_ENUMERABLE :
                    _response.Messages;
            }
        }
    }
}
