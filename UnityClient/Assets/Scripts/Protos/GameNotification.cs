// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: protos/game-notification.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace GameNotification {

  /// <summary>Holder for reflection information generated from protos/game-notification.proto</summary>
  public static partial class GameNotificationReflection {

    #region Descriptor
    /// <summary>File descriptor for protos/game-notification.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static GameNotificationReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "Ch5wcm90b3MvZ2FtZS1ub3RpZmljYXRpb24ucHJvdG8SEGdhbWVOb3RpZmlj",
            "YXRpb24ikgEKDkxvY2F0aW9uVXBkYXRlEjwKBXVzZXJzGAEgAygLMi0uZ2Ft",
            "ZU5vdGlmaWNhdGlvbi5Mb2NhdGlvblVwZGF0ZS5Vc2VyTG9jYXRpb24aQgoM",
            "VXNlckxvY2F0aW9uEgoKAmlkGAEgASgJEhAKCHBsYXllcklkGAIgASgNEgkK",
            "AXgYAyABKAISCQoBeRgEIAEoAmIGcHJvdG8z"));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { },
          new pbr::GeneratedClrTypeInfo(null, null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::GameNotification.LocationUpdate), global::GameNotification.LocationUpdate.Parser, new[]{ "Users" }, null, null, null, new pbr::GeneratedClrTypeInfo[] { new pbr::GeneratedClrTypeInfo(typeof(global::GameNotification.LocationUpdate.Types.UserLocation), global::GameNotification.LocationUpdate.Types.UserLocation.Parser, new[]{ "Id", "PlayerId", "X", "Y" }, null, null, null, null)})
          }));
    }
    #endregion

  }
  #region Messages
  public sealed partial class LocationUpdate : pb::IMessage<LocationUpdate> {
    private static readonly pb::MessageParser<LocationUpdate> _parser = new pb::MessageParser<LocationUpdate>(() => new LocationUpdate());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<LocationUpdate> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::GameNotification.GameNotificationReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public LocationUpdate() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public LocationUpdate(LocationUpdate other) : this() {
      users_ = other.users_.Clone();
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public LocationUpdate Clone() {
      return new LocationUpdate(this);
    }

    /// <summary>Field number for the "users" field.</summary>
    public const int UsersFieldNumber = 1;
    private static readonly pb::FieldCodec<global::GameNotification.LocationUpdate.Types.UserLocation> _repeated_users_codec
        = pb::FieldCodec.ForMessage(10, global::GameNotification.LocationUpdate.Types.UserLocation.Parser);
    private readonly pbc::RepeatedField<global::GameNotification.LocationUpdate.Types.UserLocation> users_ = new pbc::RepeatedField<global::GameNotification.LocationUpdate.Types.UserLocation>();
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pbc::RepeatedField<global::GameNotification.LocationUpdate.Types.UserLocation> Users {
      get { return users_; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as LocationUpdate);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(LocationUpdate other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if(!users_.Equals(other.users_)) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      hash ^= users_.GetHashCode();
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
      users_.WriteTo(output, _repeated_users_codec);
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      size += users_.CalculateSize(_repeated_users_codec);
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(LocationUpdate other) {
      if (other == null) {
        return;
      }
      users_.Add(other.users_);
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
            users_.AddEntriesFrom(input, _repeated_users_codec);
            break;
          }
        }
      }
    }

    #region Nested types
    /// <summary>Container for nested types declared in the LocationUpdate message type.</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static partial class Types {
      public sealed partial class UserLocation : pb::IMessage<UserLocation> {
        private static readonly pb::MessageParser<UserLocation> _parser = new pb::MessageParser<UserLocation>(() => new UserLocation());
        private pb::UnknownFieldSet _unknownFields;
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public static pb::MessageParser<UserLocation> Parser { get { return _parser; } }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public static pbr::MessageDescriptor Descriptor {
          get { return global::GameNotification.LocationUpdate.Descriptor.NestedTypes[0]; }
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        pbr::MessageDescriptor pb::IMessage.Descriptor {
          get { return Descriptor; }
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public UserLocation() {
          OnConstruction();
        }

        partial void OnConstruction();

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public UserLocation(UserLocation other) : this() {
          id_ = other.id_;
          playerId_ = other.playerId_;
          x_ = other.x_;
          y_ = other.y_;
          _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public UserLocation Clone() {
          return new UserLocation(this);
        }

        /// <summary>Field number for the "id" field.</summary>
        public const int IdFieldNumber = 1;
        private string id_ = "";
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public string Id {
          get { return id_; }
          set {
            id_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
          }
        }

        /// <summary>Field number for the "playerId" field.</summary>
        public const int PlayerIdFieldNumber = 2;
        private uint playerId_;
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public uint PlayerId {
          get { return playerId_; }
          set {
            playerId_ = value;
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
          return Equals(other as UserLocation);
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public bool Equals(UserLocation other) {
          if (ReferenceEquals(other, null)) {
            return false;
          }
          if (ReferenceEquals(other, this)) {
            return true;
          }
          if (Id != other.Id) return false;
          if (PlayerId != other.PlayerId) return false;
          if (!pbc::ProtobufEqualityComparers.BitwiseSingleEqualityComparer.Equals(X, other.X)) return false;
          if (!pbc::ProtobufEqualityComparers.BitwiseSingleEqualityComparer.Equals(Y, other.Y)) return false;
          return Equals(_unknownFields, other._unknownFields);
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public override int GetHashCode() {
          int hash = 1;
          if (Id.Length != 0) hash ^= Id.GetHashCode();
          if (PlayerId != 0) hash ^= PlayerId.GetHashCode();
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
          if (Id.Length != 0) {
            output.WriteRawTag(10);
            output.WriteString(Id);
          }
          if (PlayerId != 0) {
            output.WriteRawTag(16);
            output.WriteUInt32(PlayerId);
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
          if (Id.Length != 0) {
            size += 1 + pb::CodedOutputStream.ComputeStringSize(Id);
          }
          if (PlayerId != 0) {
            size += 1 + pb::CodedOutputStream.ComputeUInt32Size(PlayerId);
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
        public void MergeFrom(UserLocation other) {
          if (other == null) {
            return;
          }
          if (other.Id.Length != 0) {
            Id = other.Id;
          }
          if (other.PlayerId != 0) {
            PlayerId = other.PlayerId;
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
                Id = input.ReadString();
                break;
              }
              case 16: {
                PlayerId = input.ReadUInt32();
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

    }
    #endregion

  }

  #endregion

}

#endregion Designer generated code