///**
// * Autogenerated by Thrift Compiler (0.9.1)
// *
// * DO NOT EDIT UNLESS YOU ARE SURE THAT YOU KNOW WHAT YOU ARE DOING
// *  @generated
// */
//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Text;
//using System.IO;
//using Thrift;
//using Thrift.Collections;
//using System.Runtime.Serialization;
//using Thrift.Protocol;
//using Thrift.Transport;

//public partial class HelloWorldService {
//  public interface Iface {
//    string sayHello(string username);
//    #if SILVERLIGHT
//    IAsyncResult Begin_sayHello(AsyncCallback callback, object state, string username);
//    string End_sayHello(IAsyncResult asyncResult);
//    #endif
//  }

//  public class Client : IDisposable, Iface {
//    public Client(TProtocol prot) : this(prot, prot)
//    {
//    }

//    public Client(TProtocol iprot, TProtocol oprot)
//    {
//      iprot_ = iprot;
//      oprot_ = oprot;
//    }

//    protected TProtocol iprot_;
//    protected TProtocol oprot_;
//    protected int seqid_;

//    public TProtocol InputProtocol
//    {
//      get { return iprot_; }
//    }
//    public TProtocol OutputProtocol
//    {
//      get { return oprot_; }
//    }


//    #region " IDisposable Support "
//    private bool _IsDisposed;

//    // IDisposable
//    public void Dispose()
//    {
//      Dispose(true);
//    }
    

//    protected virtual void Dispose(bool disposing)
//    {
//      if (!_IsDisposed)
//      {
//        if (disposing)
//        {
//          if (iprot_ != null)
//          {
//            ((IDisposable)iprot_).Dispose();
//          }
//          if (oprot_ != null)
//          {
//            ((IDisposable)oprot_).Dispose();
//          }
//        }
//      }
//      _IsDisposed = true;
//    }
//    #endregion


    
//    #if SILVERLIGHT
//    public IAsyncResult Begin_sayHello(AsyncCallback callback, object state, string username)
//    {
//      return send_sayHello(callback, state, username);
//    }

//    public string End_sayHello(IAsyncResult asyncResult)
//    {
//      oprot_.Transport.EndFlush(asyncResult);
//      return recv_sayHello();
//    }

//    #endif

//    public string sayHello(string username)
//    {
//      #if !SILVERLIGHT
//      send_sayHello(username);
//      return recv_sayHello();

//      #else
//      var asyncResult = Begin_sayHello(null, null, username);
//      return End_sayHello(asyncResult);

//      #endif
//    }
//    #if SILVERLIGHT
//    public IAsyncResult send_sayHello(AsyncCallback callback, object state, string username)
//    #else
//    public void send_sayHello(string username)
//    #endif
//    {
//      oprot_.WriteMessageBegin(new TMessage("sayHello", TMessageType.Call, seqid_));
//      sayHello_args args = new sayHello_args();
//      args.Username = username;
//      args.Write(oprot_);
//      oprot_.WriteMessageEnd();
//      #if SILVERLIGHT
//      return oprot_.Transport.BeginFlush(callback, state);
//      #else
//      oprot_.Transport.Flush();
//      #endif
//    }

//    public string recv_sayHello()
//    {
//      TMessage msg = iprot_.ReadMessageBegin();
//      if (msg.Type == TMessageType.Exception) {
//        TApplicationException x = TApplicationException.Read(iprot_);
//        iprot_.ReadMessageEnd();
//        throw x;
//      }
//      sayHello_result result = new sayHello_result();
//      result.Read(iprot_);
//      iprot_.ReadMessageEnd();
//      if (result.__isset.success) {
//        return result.Success;
//      }
//      throw new TApplicationException(TApplicationException.ExceptionType.MissingResult, "sayHello failed: unknown result");
//    }

//  }
//  public class Processor : TProcessor {
//    public Processor(Iface iface)
//    {
//      iface_ = iface;
//      processMap_["sayHello"] = sayHello_Process;
//    }

//    protected delegate void ProcessFunction(int seqid, TProtocol iprot, TProtocol oprot);
//    private Iface iface_;
//    protected Dictionary<string, ProcessFunction> processMap_ = new Dictionary<string, ProcessFunction>();

//    public bool Process(TProtocol iprot, TProtocol oprot)
//    {
//      try
//      {
//        TMessage msg = iprot.ReadMessageBegin();
//        ProcessFunction fn;
//        processMap_.TryGetValue(msg.Name, out fn);
//        if (fn == null) {
//          TProtocolUtil.Skip(iprot, TType.Struct);
//          iprot.ReadMessageEnd();
//          TApplicationException x = new TApplicationException (TApplicationException.ExceptionType.UnknownMethod, "Invalid method name: '" + msg.Name + "'");
//          oprot.WriteMessageBegin(new TMessage(msg.Name, TMessageType.Exception, msg.SeqID));
//          x.Write(oprot);
//          oprot.WriteMessageEnd();
//          oprot.Transport.Flush();
//          return true;
//        }
//        fn(msg.SeqID, iprot, oprot);
//      }
//      catch (IOException)
//      {
//        return false;
//      }
//      return true;
//    }

//    public void sayHello_Process(int seqid, TProtocol iprot, TProtocol oprot)
//    {
//      sayHello_args args = new sayHello_args();
//      args.Read(iprot);
//      iprot.ReadMessageEnd();
//      sayHello_result result = new sayHello_result();
//      result.Success = iface_.sayHello(args.Username);
//      oprot.WriteMessageBegin(new TMessage("sayHello", TMessageType.Reply, seqid)); 
//      result.Write(oprot);
//      oprot.WriteMessageEnd();
//      oprot.Transport.Flush();
//    }

//  }


//  #if !SILVERLIGHT
//  [Serializable]
//  #endif
//  public partial class sayHello_args : TBase
//  {
//    private string _username;

//    public string Username
//    {
//      get
//      {
//        return _username;
//      }
//      set
//      {
//        __isset.username = true;
//        this._username = value;
//      }
//    }


//    public Isset __isset;
//    #if !SILVERLIGHT
//    [Serializable]
//    #endif
//    public struct Isset {
//      public bool username;
//    }

//    public sayHello_args() {
//    }

//    public void Read (TProtocol iprot)
//    {
//      TField field;
//      iprot.ReadStructBegin();
//      while (true)
//      {
//        field = iprot.ReadFieldBegin();
//        if (field.Type == TType.Stop) { 
//          break;
//        }
//        switch (field.ID)
//        {
//          case 1:
//            if (field.Type == TType.String) {
//              Username = iprot.ReadString();
//            } else { 
//              TProtocolUtil.Skip(iprot, field.Type);
//            }
//            break;
//          default: 
//            TProtocolUtil.Skip(iprot, field.Type);
//            break;
//        }
//        iprot.ReadFieldEnd();
//      }
//      iprot.ReadStructEnd();
//    }

//    public void Write(TProtocol oprot) {
//      TStruct struc = new TStruct("sayHello_args");
//      oprot.WriteStructBegin(struc);
//      TField field = new TField();
//      if (Username != null && __isset.username) {
//        field.Name = "username";
//        field.Type = TType.String;
//        field.ID = 1;
//        oprot.WriteFieldBegin(field);
//        oprot.WriteString(Username);
//        oprot.WriteFieldEnd();
//      }
//      oprot.WriteFieldStop();
//      oprot.WriteStructEnd();
//    }

//    public override string ToString() {
//      StringBuilder sb = new StringBuilder("sayHello_args(");
//      sb.Append("Username: ");
//      sb.Append(Username);
//      sb.Append(")");
//      return sb.ToString();
//    }

//  }


//  #if !SILVERLIGHT
//  [Serializable]
//  #endif
//  public partial class sayHello_result : TBase
//  {
//    private string _success;

//    public string Success
//    {
//      get
//      {
//        return _success;
//      }
//      set
//      {
//        __isset.success = true;
//        this._success = value;
//      }
//    }


//    public Isset __isset;
//    #if !SILVERLIGHT
//    [Serializable]
//    #endif
//    public struct Isset {
//      public bool success;
//    }

//    public sayHello_result() {
//    }

//    public void Read (TProtocol iprot)
//    {
//      TField field;
//      iprot.ReadStructBegin();
//      while (true)
//      {
//        field = iprot.ReadFieldBegin();
//        if (field.Type == TType.Stop) { 
//          break;
//        }
//        switch (field.ID)
//        {
//          case 0:
//            if (field.Type == TType.String) {
//              Success = iprot.ReadString();
//            } else { 
//              TProtocolUtil.Skip(iprot, field.Type);
//            }
//            break;
//          default: 
//            TProtocolUtil.Skip(iprot, field.Type);
//            break;
//        }
//        iprot.ReadFieldEnd();
//      }
//      iprot.ReadStructEnd();
//    }

//    public void Write(TProtocol oprot) {
//      TStruct struc = new TStruct("sayHello_result");
//      oprot.WriteStructBegin(struc);
//      TField field = new TField();

//      if (this.__isset.success) {
//        if (Success != null) {
//          field.Name = "Success";
//          field.Type = TType.String;
//          field.ID = 0;
//          oprot.WriteFieldBegin(field);
//          oprot.WriteString(Success);
//          oprot.WriteFieldEnd();
//        }
//      }
//      oprot.WriteFieldStop();
//      oprot.WriteStructEnd();
//    }

//    public override string ToString() {
//      StringBuilder sb = new StringBuilder("sayHello_result(");
//      sb.Append("Success: ");
//      sb.Append(Success);
//      sb.Append(")");
//      return sb.ToString();
//    }

//  }

//}
