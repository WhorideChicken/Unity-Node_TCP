// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: protos/response.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace Response {

  /// <summary>Holder for reflection information generated from protos/response.proto</summary>
  public static partial class ResponseReflection {

    #region Descriptor
    /// <summary>File descriptor for protos/response.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static ResponseReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "ChVwcm90b3MvcmVzcG9uc2UucHJvdG8SCHJlc3BvbnNlIlQKCFJlc3BvbnNl",
            "EhEKCWhhbmRsZXJJZBgBIAEoDRIUCgxyZXNwb25zZUNvZGUYAiABKA0SEQoJ",
            "dGltZXN0YW1wGAMgASgEEgwKBGRhdGEYBCABKAwiSgoPSW5pdGlhbFJlc3Bv",
            "bnNlEg4KBmdhbWVJZBgBIAEoCRIRCgl0aW1lc3RhbXAYAiABKAQSCQoBeBgD",
            "IAEoAhIJCgF5GAQgASgCYgZwcm90bzM="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { },
          new pbr::GeneratedClrTypeInfo(null, null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::Response.Response), global::Response.Response.Parser, new[]{ "HandlerId", "ResponseCode", "Timestamp", "Data" }, null, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::Response.InitialResponse), global::Response.InitialResponse.Parser, new[]{ "GameId", "Timestamp", "X", "Y" }, null, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  public sealed partial class Response : pb::IMessage<Response> {
    private static readonly pb::MessageParser<Response> _parser = new pb::MessageParser<Response>(() => new Response());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<Response> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Response.ResponseReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Response() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Response(Response other) : this() {
      handlerId_ = other.handlerId_;
      responseCode_ = other.responseCode_;
      timestamp_ = other.timestamp_;
      data_ = other.data_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Response Clone() {
      return new Response(this);
    }

    /// <summary>Field number for the "handlerId" field.</summary>
    public const int HandlerIdFieldNumber = 1;
    private uint handlerId_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public uint HandlerId {
      get { return handlerId_; }
      set {
        handlerId_ = value;
      }
    }

    /// <summary>Field number for the "responseCode" field.</summary>
    public const int ResponseCodeFieldNumber = 2;
    private uint responseCode_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public uint ResponseCode {
      get { return responseCode_; }
      set {
        responseCode_ = value;
      }
    }

    /// <summary>Field number for the "timestamp" field.</summary>
    public const int TimestampFieldNumber = 3;
    private ulong timestamp_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public ulong Timestamp {
      get { return timestamp_; }
      set {
        timestamp_ = value;
      }
    }

    /// <summary>Field number for the "data" field.</summary>
    public const int DataFieldNumber = 4;
    private pb::ByteString data_ = pb::ByteString.Empty;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pb::ByteString Data {
      get { return data_; }
      set {
        data_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as Response);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(Response other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (HandlerId != other.HandlerId) return false;
      if (ResponseCode != other.ResponseCode) return false;
      if (Timestamp != other.Timestamp) return false;
      if (Data != other.Data) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (HandlerId != 0) hash ^= HandlerId.GetHashCode();
      if (ResponseCode != 0) hash ^= ResponseCode.GetHashCode();
      if (Timestamp != 0UL) hash ^= Timestamp.GetHashCode();
      if (Data.Length != 0) hash ^= Data.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (HandlerId != 0) {
        output.WriteRawTag(8);
        output.WriteUInt32(HandlerId);
      }
      if (ResponseCode != 0) {
        output.WriteRawTag(16);
        output.WriteUInt32(ResponseCode);
      }
      if (Timestamp != 0UL) {
        output.WriteRawTag(24);
        output.WriteUInt64(Timestamp);
      }
      if (Data.Length != 0) {
        output.WriteRawTag(34);
        output.WriteBytes(Data);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (HandlerId != 0) {
        size += 1 + pb::CodedOutputStream.ComputeUInt32Size(HandlerId);
      }
      if (ResponseCode != 0) {
        size += 1 + pb::CodedOutputStream.ComputeUInt32Size(ResponseCode);
      }
      if (Timestamp != 0UL) {
        size += 1 + pb::CodedOutputStream.ComputeUInt64Size(Timestamp);
      }
      if (Data.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeBytesSize(Data);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(Response other) {
      if (other == null) {
        return;
      }
      if (other.HandlerId != 0) {
        HandlerId = other.HandlerId;
      }
      if (other.ResponseCode != 0) {
        ResponseCode = other.ResponseCode;
      }
      if (other.Timestamp != 0UL) {
        Timestamp = other.Timestamp;
      }
      if (other.Data.Length != 0) {
        Data = other.Data;
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 8: {
            HandlerId = input.ReadUInt32();
            break;
          }
          case 16: {
            ResponseCode = input.ReadUInt32();
            break;
          }
          case 24: {
            Timestamp = input.ReadUInt64();
            break;
          }
          case 34: {
            Data = input.ReadBytes();
            break;
          }
        }
      }
    }

  }

  public sealed partial class InitialResponse : pb::IMessage<InitialResponse> {
    private static readonly pb::MessageParser<InitialResponse> _parser = new pb::MessageParser<InitialResponse>(() => new InitialResponse());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<InitialResponse> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Response.ResponseReflection.Descriptor.MessageTypes[1]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public InitialResponse() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public InitialResponse(InitialResponse other) : this() {
      gameId_ = other.gameId_;
      timestamp_ = other.timestamp_;
      x_ = other.x_;
      y_ = other.y_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public InitialResponse Clone() {
      return new InitialResponse(this);
    }

    /// <summary>Field number for the "gameId" field.</summary>
    public const int GameIdFieldNumber = 1;
    private string gameId_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string GameId {
      get { return gameId_; }
      set {
        gameId_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "timestamp" field.</summary>
    public const int TimestampFieldNumber = 2;
    private ulong timestamp_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public ulong Timestamp {
      get { return timestamp_; }
      set {
        timestamp_ = value;
      }
    }

    /// <summary>Field number for the "x" field.</summary>
    public const int XFieldNumber = 3;
    private float x_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public float X {
      get { return x_; }
      set {
        x_ = value;
      }
    }

    /// <summary>Field number for the "y" field.</summary>
    public const int YFieldNumber = 4;
    private float y_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public float Y {
      get { return y_; }
      set {
        y_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as InitialResponse);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(InitialResponse other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (GameId != other.GameId) return false;
      if (Timestamp != other.Timestamp) return false;
      if (!pbc::ProtobufEqualityComparers.BitwiseSingleEqualityComparer.Equals(X, other.X)) return false;
      if (!pbc::ProtobufEqualityComparers.BitwiseSingleEqualityComparer.Equals(Y, other.Y)) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (GameId.Length != 0) hash ^= GameId.GetHashCode();
      if (Timestamp != 0UL) hash ^= Timestamp.GetHashCode();
      if (X != 0F) hash ^= pbc::ProtobufEqualityComparers.BitwiseSingleEqualityComparer.GetHashCode(X);
      if (Y != 0F) hash ^= pbc::ProtobufEqualityComparers.BitwiseSingleEqualityComparer.GetHashCode(Y);
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (GameId.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(GameId);
      }
      if (Timestamp != 0UL) {
        output.WriteRawTag(16);
        output.WriteUInt64(Timestamp);
      }
      if (X != 0F) {
        output.WriteRawTag(29);
        output.WriteFloat(X);
      }
      if (Y != 0F) {
        output.WriteRawTag(37);
        output.WriteFloat(Y);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (GameId.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(GameId);
      }
      if (Timestamp != 0UL) {
        size += 1 + pb::CodedOutputStream.ComputeUInt64Size(Timestamp);
      }
      if (X != 0F) {
        size += 1 + 4;
      }
      if (Y != 0F) {
        size += 1 + 4;
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(InitialResponse other) {
      if (other == null) {
        return;
      }
      if (other.GameId.Length != 0) {
        GameId = other.GameId;
      }
      if (other.Timestamp != 0UL) {
        Timestamp = other.Timestamp;
      }
      if (other.X != 0F) {
        X = other.X;
      }
      if (other.Y != 0F) {
        Y = other.Y;
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 10: {
            GameId = input.ReadString();
            break;
          }
          case 16: {
            Timestamp = input.ReadUInt64();
            break;
          }
          case 29: {
            X = input.ReadFloat();
            break;
          }
          case 37: {
            Y = input.ReadFloat();
            break;
          }
        }
      }
    }

  }

  #endregion

}

#endregion Designer generated code