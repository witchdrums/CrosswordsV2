﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WPFLayer.ServicesImplementation {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Users", Namespace="http://schemas.datacontract.org/2004/07/Domain")]
    [System.SerializableAttribute()]
    public partial class Users : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool credentialField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string emailField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int idUserField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int idUserTypeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string passwordField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string usernameField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool credential {
            get {
                return this.credentialField;
            }
            set {
                if ((this.credentialField.Equals(value) != true)) {
                    this.credentialField = value;
                    this.RaisePropertyChanged("credential");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string email {
            get {
                return this.emailField;
            }
            set {
                if ((object.ReferenceEquals(this.emailField, value) != true)) {
                    this.emailField = value;
                    this.RaisePropertyChanged("email");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int idUser {
            get {
                return this.idUserField;
            }
            set {
                if ((this.idUserField.Equals(value) != true)) {
                    this.idUserField = value;
                    this.RaisePropertyChanged("idUser");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int idUserType {
            get {
                return this.idUserTypeField;
            }
            set {
                if ((this.idUserTypeField.Equals(value) != true)) {
                    this.idUserTypeField = value;
                    this.RaisePropertyChanged("idUserType");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string password {
            get {
                return this.passwordField;
            }
            set {
                if ((object.ReferenceEquals(this.passwordField, value) != true)) {
                    this.passwordField = value;
                    this.RaisePropertyChanged("password");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string username {
            get {
                return this.usernameField;
            }
            set {
                if ((object.ReferenceEquals(this.usernameField, value) != true)) {
                    this.usernameField = value;
                    this.RaisePropertyChanged("username");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]

    [System.Runtime.Serialization.DataContractAttribute(Name="Board", Namespace="http://schemas.datacontract.org/2004/07/Domain")]
    [System.SerializableAttribute()]
    public partial class Board : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {

    [System.Runtime.Serialization.DataContractAttribute(Name="Players", Namespace="http://schemas.datacontract.org/2004/07/Domain")]
    [System.SerializableAttribute()]
    public partial class Players : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {

        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]

        private WPFLayer.ServicesImplementation.WordsBoard[] WordsBoardsField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string boardMatrixField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int idBoardField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public WPFLayer.ServicesImplementation.WordsBoard[] WordsBoards {
            get {
                return this.WordsBoardsField;
            }
            set {
                if ((object.ReferenceEquals(this.WordsBoardsField, value) != true)) {
                    this.WordsBoardsField = value;
                    this.RaisePropertyChanged("WordsBoards");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string boardMatrix {
            get {
                return this.boardMatrixField;
            }
            set {
                if ((object.ReferenceEquals(this.boardMatrixField, value) != true)) {
                    this.boardMatrixField = value;
                    this.RaisePropertyChanged("boardMatrix");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int idBoard {
            get {
                return this.idBoardField;
            }
            set {
                if ((this.idBoardField.Equals(value) != true)) {
                    this.idBoardField = value;
                    this.RaisePropertyChanged("idBoard");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="WordsBoard", Namespace="http://schemas.datacontract.org/2004/07/Domain")]
    [System.SerializableAttribute()]
    public partial class WordsBoard : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private WPFLayer.ServicesImplementation.Board BoardField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private WPFLayer.ServicesImplementation.Word WordField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int idBoardField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int idWordField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private byte xEndField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private byte xStartField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private byte yEndField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private byte yStartField;

        private int idPlayerField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string playerDescriptionField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int playerLevelField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string playerNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string playerRankField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private WPFLayer.ServicesImplementation.Users userField;

        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]

        public WPFLayer.ServicesImplementation.Board Board {
            get {
                return this.BoardField;
            }
            set {
                if ((object.ReferenceEquals(this.BoardField, value) != true)) {
                    this.BoardField = value;
                    this.RaisePropertyChanged("Board");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public WPFLayer.ServicesImplementation.Word Word {
            get {
                return this.WordField;
            }
            set {
                if ((object.ReferenceEquals(this.WordField, value) != true)) {
                    this.WordField = value;
                    this.RaisePropertyChanged("Word");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int idBoard {
            get {
                return this.idBoardField;
            }
            set {
                if ((this.idBoardField.Equals(value) != true)) {
                    this.idBoardField = value;
                    this.RaisePropertyChanged("idBoard");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int idWord {
            get {
                return this.idWordField;
            }
            set {
                if ((this.idWordField.Equals(value) != true)) {
                    this.idWordField = value;
                    this.RaisePropertyChanged("idWord");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public byte xEnd {
            get {
                return this.xEndField;
            }
            set {
                if ((this.xEndField.Equals(value) != true)) {
                    this.xEndField = value;
                    this.RaisePropertyChanged("xEnd");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public byte xStart {
            get {
                return this.xStartField;
            }
            set {
                if ((this.xStartField.Equals(value) != true)) {
                    this.xStartField = value;
                    this.RaisePropertyChanged("xStart");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public byte yEnd {
            get {
                return this.yEndField;
            }
            set {
                if ((this.yEndField.Equals(value) != true)) {
                    this.yEndField = value;
                    this.RaisePropertyChanged("yEnd");

        public int idPlayer {
            get {
                return this.idPlayerField;
            }
            set {
                if ((this.idPlayerField.Equals(value) != true)) {
                    this.idPlayerField = value;
                    this.RaisePropertyChanged("idPlayer");

                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]

        public byte yStart {
            get {
                return this.yStartField;
            }
            set {
                if ((this.yStartField.Equals(value) != true)) {
                    this.yStartField = value;
                    this.RaisePropertyChanged("yStart");

        public string playerDescription {
            get {
                return this.playerDescriptionField;
            }
            set {
                if ((object.ReferenceEquals(this.playerDescriptionField, value) != true)) {
                    this.playerDescriptionField = value;
                    this.RaisePropertyChanged("playerDescription");

                }
            }
        }
        

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Word", Namespace="http://schemas.datacontract.org/2004/07/Domain")]
    [System.SerializableAttribute()]
    public partial class Word : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private WPFLayer.ServicesImplementation.WordsBoard[] WordsBoardsField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string clueField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool isSolvedField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string termField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public WPFLayer.ServicesImplementation.WordsBoard[] WordsBoards {
            get {
                return this.WordsBoardsField;
            }
            set {
                if ((object.ReferenceEquals(this.WordsBoardsField, value) != true)) {
                    this.WordsBoardsField = value;
                    this.RaisePropertyChanged("WordsBoards");

        [System.Runtime.Serialization.DataMemberAttribute()]
        public int playerLevel {
            get {
                return this.playerLevelField;
            }
            set {
                if ((this.playerLevelField.Equals(value) != true)) {
                    this.playerLevelField = value;
                    this.RaisePropertyChanged("playerLevel");

                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]

        public string clue {
            get {
                return this.clueField;
            }
            set {
                if ((object.ReferenceEquals(this.clueField, value) != true)) {
                    this.clueField = value;
                    this.RaisePropertyChanged("clue");

        public string playerName {
            get {
                return this.playerNameField;
            }
            set {
                if ((object.ReferenceEquals(this.playerNameField, value) != true)) {
                    this.playerNameField = value;
                    this.RaisePropertyChanged("playerName");

                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]

        public bool isSolved {
            get {
                return this.isSolvedField;
            }
            set {
                if ((this.isSolvedField.Equals(value) != true)) {
                    this.isSolvedField = value;
                    this.RaisePropertyChanged("isSolved");

        public string playerRank {
            get {
                return this.playerRankField;
            }
            set {
                if ((object.ReferenceEquals(this.playerRankField, value) != true)) {
                    this.playerRankField = value;
                    this.RaisePropertyChanged("playerRank");

                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]

        public string term {
            get {
                return this.termField;
            }
            set {
                if ((object.ReferenceEquals(this.termField, value) != true)) {
                    this.termField = value;
                    this.RaisePropertyChanged("term");

        public WPFLayer.ServicesImplementation.Users user {
            get {
                return this.userField;
            }
            set {
                if ((object.ReferenceEquals(this.userField, value) != true)) {
                    this.userField = value;
                    this.RaisePropertyChanged("user");

                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServicesImplementation.IUsersManager", CallbackContract=typeof(WPFLayer.ServicesImplementation.IUsersManagerCallback))]
    public interface IUsersManager {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUsersManager/AddUser", ReplyAction="http://tempuri.org/IUsersManager/AddUserResponse")]
        bool AddUser(WPFLayer.ServicesImplementation.Users user);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUsersManager/AddUser", ReplyAction="http://tempuri.org/IUsersManager/AddUserResponse")]
        System.Threading.Tasks.Task<bool> AddUserAsync(WPFLayer.ServicesImplementation.Users user);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUsersManager/FindUserByEmail", ReplyAction="http://tempuri.org/IUsersManager/FindUserByEmailResponse")]
        bool FindUserByEmail(string userEmail);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUsersManager/FindUserByEmail", ReplyAction="http://tempuri.org/IUsersManager/FindUserByEmailResponse")]
        System.Threading.Tasks.Task<bool> FindUserByEmailAsync(string userEmail);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUsersManager/Login", ReplyAction="http://tempuri.org/IUsersManager/LoginResponse")]
        WPFLayer.ServicesImplementation.Players Login(WPFLayer.ServicesImplementation.Users user);
        

        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUsersManager/FindUserByUserNameAndPassword", ReplyAction="http://tempuri.org/IUsersManager/FindUserByUserNameAndPasswordResponse")]
        System.Threading.Tasks.Task<WPFLayer.ServicesImplementation.Users> FindUserByUserNameAndPasswordAsync(WPFLayer.ServicesImplementation.Users user);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUsersManager/PrintConnectionMap", ReplyAction="http://tempuri.org/IUsersManager/PrintConnectionMapResponse")]
        void PrintConnectionMap();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUsersManager/PrintConnectionMap", ReplyAction="http://tempuri.org/IUsersManager/PrintConnectionMapResponse")]
        System.Threading.Tasks.Task PrintConnectionMapAsync();

        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUsersManager/Login", ReplyAction="http://tempuri.org/IUsersManager/LoginResponse")]
        System.Threading.Tasks.Task<WPFLayer.ServicesImplementation.Players> LoginAsync(WPFLayer.ServicesImplementation.Users user);

    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IUsersManagerCallback {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUsersManager/Response", ReplyAction="http://tempuri.org/IUsersManager/ResponseResponse")]
        void Response([System.ServiceModel.MessageParameterAttribute(Name="response")] bool response1);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IUsersManagerChannel : WPFLayer.ServicesImplementation.IUsersManager, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class UsersManagerClient : System.ServiceModel.DuplexClientBase<WPFLayer.ServicesImplementation.IUsersManager>, WPFLayer.ServicesImplementation.IUsersManager {
        
        public UsersManagerClient(System.ServiceModel.InstanceContext callbackInstance) : 
                base(callbackInstance) {
        }
        
        public UsersManagerClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName) : 
                base(callbackInstance, endpointConfigurationName) {
        }
        
        public UsersManagerClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public UsersManagerClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public UsersManagerClient(System.ServiceModel.InstanceContext callbackInstance, System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, binding, remoteAddress) {
        }
        
        public bool AddUser(WPFLayer.ServicesImplementation.Users user) {
            return base.Channel.AddUser(user);
        }
        
        public System.Threading.Tasks.Task<bool> AddUserAsync(WPFLayer.ServicesImplementation.Users user) {
            return base.Channel.AddUserAsync(user);
        }
        
        public bool FindUserByEmail(string userEmail) {
            return base.Channel.FindUserByEmail(userEmail);
        }
        
        public System.Threading.Tasks.Task<bool> FindUserByEmailAsync(string userEmail) {
            return base.Channel.FindUserByEmailAsync(userEmail);
        }
        
        public WPFLayer.ServicesImplementation.Players Login(WPFLayer.ServicesImplementation.Users user) {
            return base.Channel.Login(user);
        }
        
        public System.Threading.Tasks.Task<WPFLayer.ServicesImplementation.Players> LoginAsync(WPFLayer.ServicesImplementation.Users user) {
            return base.Channel.LoginAsync(user);
        }
        
        public void PrintConnectionMap() {
            base.Channel.PrintConnectionMap();
        }
        
        public System.Threading.Tasks.Task PrintConnectionMapAsync() {
            return base.Channel.PrintConnectionMapAsync();
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServicesImplementation.IGameManagement")]
    public interface IGameManagement {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGameManagement/GetBoardById", ReplyAction="http://tempuri.org/IGameManagement/GetBoardByIdResponse")]
        WPFLayer.ServicesImplementation.Board GetBoardById(int idBoard);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGameManagement/GetBoardById", ReplyAction="http://tempuri.org/IGameManagement/GetBoardByIdResponse")]
        System.Threading.Tasks.Task<WPFLayer.ServicesImplementation.Board> GetBoardByIdAsync(int idBoard);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IGameManagementChannel : WPFLayer.ServicesImplementation.IGameManagement, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class GameManagementClient : System.ServiceModel.ClientBase<WPFLayer.ServicesImplementation.IGameManagement>, WPFLayer.ServicesImplementation.IGameManagement {
        
        public GameManagementClient() {
        }
        
        public GameManagementClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public GameManagementClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public GameManagementClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public GameManagementClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public WPFLayer.ServicesImplementation.Board GetBoardById(int idBoard) {
            return base.Channel.GetBoardById(idBoard);
        }
        
        public System.Threading.Tasks.Task<WPFLayer.ServicesImplementation.Board> GetBoardByIdAsync(int idBoard) {
            return base.Channel.GetBoardByIdAsync(idBoard);
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServicesImplementation.IGameRoomManagement", CallbackContract=typeof(WPFLayer.ServicesImplementation.IGameRoomManagementCallback))]
    public interface IGameRoomManagement {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGameRoomManagement/SendInvitationToRoom", ReplyAction="http://tempuri.org/IGameRoomManagement/SendInvitationToRoomResponse")]
        void SendInvitationToRoom(int idRoom, WPFLayer.ServicesImplementation.Users userTarget);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGameRoomManagement/SendInvitationToRoom", ReplyAction="http://tempuri.org/IGameRoomManagement/SendInvitationToRoomResponse")]
        System.Threading.Tasks.Task SendInvitationToRoomAsync(int idRoom, WPFLayer.ServicesImplementation.Users userTarget);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGameRoomManagement/JoinToRoom", ReplyAction="http://tempuri.org/IGameRoomManagement/JoinToRoomResponse")]
        void JoinToRoom(int idRoom, WPFLayer.ServicesImplementation.Users newUser);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGameRoomManagement/JoinToRoom", ReplyAction="http://tempuri.org/IGameRoomManagement/JoinToRoomResponse")]
        System.Threading.Tasks.Task JoinToRoomAsync(int idRoom, WPFLayer.ServicesImplementation.Users newUser);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGameRoomManagement/CreateRoom", ReplyAction="http://tempuri.org/IGameRoomManagement/CreateRoomResponse")]
        int CreateRoom(WPFLayer.ServicesImplementation.Users user);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGameRoomManagement/CreateRoom", ReplyAction="http://tempuri.org/IGameRoomManagement/CreateRoomResponse")]
        System.Threading.Tasks.Task<int> CreateRoomAsync(WPFLayer.ServicesImplementation.Users user);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IGameRoomManagementCallback {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGameRoomManagement/ReciveInvitationToRoom", ReplyAction="http://tempuri.org/IGameRoomManagement/ReciveInvitationToRoomResponse")]
        void ReciveInvitationToRoom(int idRoom);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGameRoomManagement/UpdateRoom", ReplyAction="http://tempuri.org/IGameRoomManagement/UpdateRoomResponse")]
        void UpdateRoom(WPFLayer.ServicesImplementation.Users[] usersInRoom);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IGameRoomManagementChannel : WPFLayer.ServicesImplementation.IGameRoomManagement, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class GameRoomManagementClient : System.ServiceModel.DuplexClientBase<WPFLayer.ServicesImplementation.IGameRoomManagement>, WPFLayer.ServicesImplementation.IGameRoomManagement {
        
        public GameRoomManagementClient(System.ServiceModel.InstanceContext callbackInstance) : 
                base(callbackInstance) {
        }
        
        public GameRoomManagementClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName) : 
                base(callbackInstance, endpointConfigurationName) {
        }
        
        public GameRoomManagementClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public GameRoomManagementClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public GameRoomManagementClient(System.ServiceModel.InstanceContext callbackInstance, System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, binding, remoteAddress) {
        }
        
        public void SendInvitationToRoom(int idRoom, WPFLayer.ServicesImplementation.Users userTarget) {
            base.Channel.SendInvitationToRoom(idRoom, userTarget);
        }
        
        public System.Threading.Tasks.Task SendInvitationToRoomAsync(int idRoom, WPFLayer.ServicesImplementation.Users userTarget) {
            return base.Channel.SendInvitationToRoomAsync(idRoom, userTarget);
        }
        
        public void JoinToRoom(int idRoom, WPFLayer.ServicesImplementation.Users newUser) {
            base.Channel.JoinToRoom(idRoom, newUser);
        }
        
        public System.Threading.Tasks.Task JoinToRoomAsync(int idRoom, WPFLayer.ServicesImplementation.Users newUser) {
            return base.Channel.JoinToRoomAsync(idRoom, newUser);
        }
        
        public int CreateRoom(WPFLayer.ServicesImplementation.Users user) {
            return base.Channel.CreateRoom(user);
        }
        
        public System.Threading.Tasks.Task<int> CreateRoomAsync(WPFLayer.ServicesImplementation.Users user) {
            return base.Channel.CreateRoomAsync(user);
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServicesImplementation.IMessages", CallbackContract=typeof(WPFLayer.ServicesImplementation.IMessagesCallback))]
    public interface IMessages {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMessages/SendChatMessage", ReplyAction="http://tempuri.org/IMessages/SendChatMessageResponse")]
        void SendChatMessage(WPFLayer.ServicesImplementation.Users[] room, WPFLayer.ServicesImplementation.Users userOrigin, string message);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMessages/SendChatMessage", ReplyAction="http://tempuri.org/IMessages/SendChatMessageResponse")]
        System.Threading.Tasks.Task SendChatMessageAsync(WPFLayer.ServicesImplementation.Users[] room, WPFLayer.ServicesImplementation.Users userOrigin, string message);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMessages/SendPrivateMessage", ReplyAction="http://tempuri.org/IMessages/SendPrivateMessageResponse")]
        void SendPrivateMessage(WPFLayer.ServicesImplementation.Users userOrigin, WPFLayer.ServicesImplementation.Users userDestination, string message);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMessages/SendPrivateMessage", ReplyAction="http://tempuri.org/IMessages/SendPrivateMessageResponse")]
        System.Threading.Tasks.Task SendPrivateMessageAsync(WPFLayer.ServicesImplementation.Users userOrigin, WPFLayer.ServicesImplementation.Users userDestination, string message);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IMessagesCallback {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMessages/ReciveChatMessage", ReplyAction="http://tempuri.org/IMessages/ReciveChatMessageResponse")]
        void ReciveChatMessage(WPFLayer.ServicesImplementation.Users userOrigin, string message);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IMessagesChannel : WPFLayer.ServicesImplementation.IMessages, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class MessagesClient : System.ServiceModel.DuplexClientBase<WPFLayer.ServicesImplementation.IMessages>, WPFLayer.ServicesImplementation.IMessages {
        
        public MessagesClient(System.ServiceModel.InstanceContext callbackInstance) : 
                base(callbackInstance) {
        }
        
        public MessagesClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName) : 
                base(callbackInstance, endpointConfigurationName) {
        }
        
        public MessagesClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public MessagesClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public MessagesClient(System.ServiceModel.InstanceContext callbackInstance, System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, binding, remoteAddress) {
        }
        
        public void SendChatMessage(WPFLayer.ServicesImplementation.Users[] room, WPFLayer.ServicesImplementation.Users userOrigin, string message) {
            base.Channel.SendChatMessage(room, userOrigin, message);
        }
        
        public System.Threading.Tasks.Task SendChatMessageAsync(WPFLayer.ServicesImplementation.Users[] room, WPFLayer.ServicesImplementation.Users userOrigin, string message) {
            return base.Channel.SendChatMessageAsync(room, userOrigin, message);
        }
        
        public void SendPrivateMessage(WPFLayer.ServicesImplementation.Users userOrigin, WPFLayer.ServicesImplementation.Users userDestination, string message) {
            base.Channel.SendPrivateMessage(userOrigin, userDestination, message);
        }
        
        public System.Threading.Tasks.Task SendPrivateMessageAsync(WPFLayer.ServicesImplementation.Users userOrigin, WPFLayer.ServicesImplementation.Users userDestination, string message) {
            return base.Channel.SendPrivateMessageAsync(userOrigin, userDestination, message);
        }
    }
}
