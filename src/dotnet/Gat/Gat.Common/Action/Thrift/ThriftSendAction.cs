using System;
using System.Collections.Generic;
using System.Text;
using Gat.Common.Event;
using Thrift.Transport;
using Gat.Event.Thrift;
using Thrift.Protocol;
using Gat.Common.Util;
using System.Threading;

namespace Gat.Common.Action.Thrift
{
    public class ThriftSendAction : IAction
    {
        private readonly string _host;
        private readonly int _port;

        public ThriftSendAction(string host, int port)
        {
            _host = host;
            _port = port;
        }

        public RunnerDelegate PrepareAction(IGatEvent evt, bool forceClone)
        {
            //we clone anyways, regardless of whether forced since we go over the wire
            GatEvent clonedEvt = BuildThriftEvent(evt);
            TTransport transport = new TSocket(_host, _port);
            GatConsumer.Client client = new GatConsumer.Client(new TBinaryProtocol(transport));
            transport.Open();
            return new RunnerDelegate(delegate()
            {
                try
                {
                    return new ThriftEventResponse(client.onEvent(clonedEvt));
                }
                finally
                {
                    try { transport.Close(); }
                    catch (Exception) { }
                }
            });
        }

        public void Dispose()
        {
            //do nothing
        }

        private GatEvent BuildThriftEvent(IGatEvent evt)
        {
            GatEvent ret = new GatEvent();
            //required
            ret.Id = evt.Id;
            ret.Type = (Gat.Event.Thrift.GatEventType)(int)evt.Type;
            //optional
            if (evt.Catalog != null)
            {
                ret.Catalog = evt.Catalog;
            }
            if (evt.RowSet != null)
            {
                ret.RowSet = BuildThriftRowSet(evt.RowSet);
            }
            if (evt.Schema != null)
            {
                ret.Schema = evt.Schema;
            }
            if (evt.Sql != null)
            {
                ret.Sql = evt.Sql;
            }
            if (evt.Table != null)
            {
                ret.Table = evt.Table;
            }
            return ret;
        }

        private GatRowSet BuildThriftRowSet(IGatRowSet rowSet)
        {
            GatRowSet ret = new GatRowSet();
            ret.Columns = new List<GatColumn>();
            foreach (IGatColumn column in rowSet.Columns)
            {
                GatColumn col = new GatColumn();
                col.Name = column.Name;
                col.Ordinal = column.Ordinal;
                col.Type = new GatColumnType();
                col.Type.Type = (Gat.Event.Thrift.GatDataType)(int)column.Type.Type;
                col.Type.TypeName = column.Type.TypeName;
                if (column.Type.Precision.HasValue)
                {
                    col.Type.Precision = column.Type.Precision.Value;
                }
                if (column.Type.Scale.HasValue)
                {
                    col.Type.Scale = column.Type.Scale.Value;
                }
                ret.Columns.Add(col);
            }
            ret.Rows = new List<GatRow>();
            foreach (IGatRow row in rowSet.Rows)
            {
                GatRow r = new GatRow();
                r.Values = new List<GatValue>();
                foreach (IGatValue value in row.Values)
                {
                    GatValue val = new GatValue();
                    val.Ordinal = value.Ordinal;
                    object valObj = value.GetValue<object>();
                    if (valObj != null)
                    {
                        val.Value = ByteUtils.ToByteArray(valObj);
                    }
                    r.Values.Add(val);
                }
            }
            return ret;
        }
    }
}
