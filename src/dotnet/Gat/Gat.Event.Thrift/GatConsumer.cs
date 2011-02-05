/**
 * Autogenerated by Thrift
 *
 * DO NOT EDIT UNLESS YOU ARE SURE THAT YOU KNOW WHAT YOU ARE DOING
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Thrift;
using Thrift.Collections;
using Thrift.Protocol;
using Thrift.Transport;
namespace Gat.Event.Thrift
{
  public class GatConsumer {
    public interface Iface {
      GatEventResponse onEvent(GatEvent evt);
    }

    public class Client : Iface {
      public Client(TProtocol prot) : this(prot, prot)
      {
      }

      public Client(TProtocol iprot, TProtocol oprot)
      {
        iprot_ = iprot;
        oprot_ = oprot;
      }

      protected TProtocol iprot_;
      protected TProtocol oprot_;
      protected int seqid_;

      public TProtocol InputProtocol
      {
        get { return iprot_; }
      }
      public TProtocol OutputProtocol
      {
        get { return oprot_; }
      }


      public GatEventResponse onEvent(GatEvent evt)
      {
        send_onEvent(evt);
        return recv_onEvent();
      }

      public void send_onEvent(GatEvent evt)
      {
        oprot_.WriteMessageBegin(new TMessage("onEvent", TMessageType.Call, seqid_));
        onEvent_args args = new onEvent_args();
        args.Evt = evt;
        args.Write(oprot_);
        oprot_.WriteMessageEnd();
        oprot_.Transport.Flush();
      }

      public GatEventResponse recv_onEvent()
      {
        TMessage msg = iprot_.ReadMessageBegin();
        if (msg.Type == TMessageType.Exception) {
          TApplicationException x = TApplicationException.Read(iprot_);
          iprot_.ReadMessageEnd();
          throw x;
        }
        onEvent_result result = new onEvent_result();
        result.Read(iprot_);
        iprot_.ReadMessageEnd();
        if (result.__isset.success) {
          return result.Success;
        }
        if (result.__isset.err1) {
          throw result.Err1;
        }
        throw new TApplicationException(TApplicationException.ExceptionType.MissingResult, "onEvent failed: unknown result");
      }

    }
    public class Processor : TProcessor {
      public Processor(Iface iface)
      {
        iface_ = iface;
        processMap_["onEvent"] = onEvent_Process;
      }

      protected delegate void ProcessFunction(int seqid, TProtocol iprot, TProtocol oprot);
      private Iface iface_;
      protected Dictionary<string, ProcessFunction> processMap_ = new Dictionary<string, ProcessFunction>();

      public bool Process(TProtocol iprot, TProtocol oprot)
      {
        try
        {
          TMessage msg = iprot.ReadMessageBegin();
          ProcessFunction fn;
          processMap_.TryGetValue(msg.Name, out fn);
          if (fn == null) {
            TProtocolUtil.Skip(iprot, TType.Struct);
            iprot.ReadMessageEnd();
            TApplicationException x = new TApplicationException (TApplicationException.ExceptionType.UnknownMethod, "Invalid method name: '" + msg.Name + "'");
            oprot.WriteMessageBegin(new TMessage(msg.Name, TMessageType.Exception, msg.SeqID));
            x.Write(oprot);
            oprot.WriteMessageEnd();
            oprot.Transport.Flush();
            return true;
          }
          fn(msg.SeqID, iprot, oprot);
        }
        catch (IOException)
        {
          return false;
        }
        return true;
      }

      public void onEvent_Process(int seqid, TProtocol iprot, TProtocol oprot)
      {
        onEvent_args args = new onEvent_args();
        args.Read(iprot);
        iprot.ReadMessageEnd();
        onEvent_result result = new onEvent_result();
        try {
          result.Success = iface_.onEvent(args.Evt);
        } catch (GatRollbackException err1) {
          result.Err1 = err1;
        }
        oprot.WriteMessageBegin(new TMessage("onEvent", TMessageType.Reply, seqid)); 
        result.Write(oprot);
        oprot.WriteMessageEnd();
        oprot.Transport.Flush();
      }

    }


    [Serializable]
    public partial class onEvent_args : TBase
    {
      private GatEvent _evt;

      public GatEvent Evt
      {
        get
        {
          return _evt;
        }
        set
        {
          __isset.evt = true;
          this._evt = value;
        }
      }


      public Isset __isset;
      [Serializable]
      public struct Isset {
        public bool evt;
      }

      public onEvent_args() {
      }

      public void Read (TProtocol iprot)
      {
        TField field;
        iprot.ReadStructBegin();
        while (true)
        {
          field = iprot.ReadFieldBegin();
          if (field.Type == TType.Stop) { 
            break;
          }
          switch (field.ID)
          {
            case 1:
              if (field.Type == TType.Struct) {
                Evt = new GatEvent();
                Evt.Read(iprot);
              } else { 
                TProtocolUtil.Skip(iprot, field.Type);
              }
              break;
            default: 
              TProtocolUtil.Skip(iprot, field.Type);
              break;
          }
          iprot.ReadFieldEnd();
        }
        iprot.ReadStructEnd();
      }

      public void Write(TProtocol oprot) {
        TStruct struc = new TStruct("onEvent_args");
        oprot.WriteStructBegin(struc);
        TField field = new TField();
        if (Evt != null && __isset.evt) {
          field.Name = "evt";
          field.Type = TType.Struct;
          field.ID = 1;
          oprot.WriteFieldBegin(field);
          Evt.Write(oprot);
          oprot.WriteFieldEnd();
        }
        oprot.WriteFieldStop();
        oprot.WriteStructEnd();
      }

      public override string ToString() {
        StringBuilder sb = new StringBuilder("onEvent_args(");
        sb.Append("Evt: ");
        sb.Append(Evt== null ? "<null>" : Evt.ToString());
        sb.Append(")");
        return sb.ToString();
      }

    }


    [Serializable]
    public partial class onEvent_result : TBase
    {
      private GatEventResponse _success;
      private GatRollbackException _err1;

      public GatEventResponse Success
      {
        get
        {
          return _success;
        }
        set
        {
          __isset.success = true;
          this._success = value;
        }
      }

      public GatRollbackException Err1
      {
        get
        {
          return _err1;
        }
        set
        {
          __isset.err1 = true;
          this._err1 = value;
        }
      }


      public Isset __isset;
      [Serializable]
      public struct Isset {
        public bool success;
        public bool err1;
      }

      public onEvent_result() {
      }

      public void Read (TProtocol iprot)
      {
        TField field;
        iprot.ReadStructBegin();
        while (true)
        {
          field = iprot.ReadFieldBegin();
          if (field.Type == TType.Stop) { 
            break;
          }
          switch (field.ID)
          {
            case 0:
              if (field.Type == TType.Struct) {
                Success = new GatEventResponse();
                Success.Read(iprot);
              } else { 
                TProtocolUtil.Skip(iprot, field.Type);
              }
              break;
            case 1:
              if (field.Type == TType.Struct) {
                Err1 = new GatRollbackException();
                Err1.Read(iprot);
              } else { 
                TProtocolUtil.Skip(iprot, field.Type);
              }
              break;
            default: 
              TProtocolUtil.Skip(iprot, field.Type);
              break;
          }
          iprot.ReadFieldEnd();
        }
        iprot.ReadStructEnd();
      }

      public void Write(TProtocol oprot) {
        TStruct struc = new TStruct("onEvent_result");
        oprot.WriteStructBegin(struc);
        TField field = new TField();

        if (this.__isset.success) {
          if (Success != null) {
            field.Name = "Success";
            field.Type = TType.Struct;
            field.ID = 0;
            oprot.WriteFieldBegin(field);
            Success.Write(oprot);
            oprot.WriteFieldEnd();
          }
        } else if (this.__isset.err1) {
          if (Err1 != null) {
            field.Name = "Err1";
            field.Type = TType.Struct;
            field.ID = 1;
            oprot.WriteFieldBegin(field);
            Err1.Write(oprot);
            oprot.WriteFieldEnd();
          }
        }
        oprot.WriteFieldStop();
        oprot.WriteStructEnd();
      }

      public override string ToString() {
        StringBuilder sb = new StringBuilder("onEvent_result(");
        sb.Append("Success: ");
        sb.Append(Success== null ? "<null>" : Success.ToString());
        sb.Append(",Err1: ");
        sb.Append(Err1== null ? "<null>" : Err1.ToString());
        sb.Append(")");
        return sb.ToString();
      }

    }

  }
}
