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
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUsersManager/FindUserByUserNameAndPassword", ReplyAction="http://tempuri.org/IUsersManager/FindUserByUserNameAndPasswordResponse")]
        WPFLayer.ServicesImplementation.Users FindUserByUserNameAndPassword(WPFLayer.ServicesImplementation.Users user);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUsersManager/FindUserByUserNameAndPassword", ReplyAction="http://tempuri.org/IUsersManager/FindUserByUserNameAndPasswordResponse")]
        System.Threading.Tasks.Task<WPFLayer.ServicesImplementation.Users> FindUserByUserNameAndPasswordAsync(WPFLayer.ServicesImplementation.Users user);
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
        
        public WPFLayer.ServicesImplementation.Users FindUserByUserNameAndPassword(WPFLayer.ServicesImplementation.Users user) {
            return base.Channel.FindUserByUserNameAndPassword(user);
        }
        
        public System.Threading.Tasks.Task<WPFLayer.ServicesImplementation.Users> FindUserByUserNameAndPasswordAsync(WPFLayer.ServicesImplementation.Users user) {
            return base.Channel.FindUserByUserNameAndPasswordAsync(user);
        }
    }
}
