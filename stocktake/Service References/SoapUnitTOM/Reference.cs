﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace stocktake.SoapUnitTOM {
    using System.Data;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://www.hcc656.com", ConfigurationName="SoapUnitTOM.SoapUnit")]
    public interface SoapUnit {
        
        // CODEGEN: 参数“GetDataResult”需要其他方案信息，使用参数模式无法捕获这些信息。特定特性为“System.Xml.Serialization.XmlElementAttribute”。
        [System.ServiceModel.OperationContractAttribute(Action="http://www.hcc656.com/SoapUnit/GetData", ReplyAction="http://www.hcc656.com/SoapUnit/GetDataResponse")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        stocktake.SoapUnitTOM.GetDataResponse GetData(stocktake.SoapUnitTOM.GetDataRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.hcc656.com/SoapUnit/GetData", ReplyAction="http://www.hcc656.com/SoapUnit/GetDataResponse")]
        System.Threading.Tasks.Task<stocktake.SoapUnitTOM.GetDataResponse> GetDataAsync(stocktake.SoapUnitTOM.GetDataRequest request);
        
        // CODEGEN: 参数“GetDataMultiResult”需要其他方案信息，使用参数模式无法捕获这些信息。特定特性为“System.Xml.Serialization.XmlElementAttribute”。
        [System.ServiceModel.OperationContractAttribute(Action="http://www.hcc656.com/SoapUnit/GetDataMulti", ReplyAction="http://www.hcc656.com/SoapUnit/GetDataMultiResponse")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        stocktake.SoapUnitTOM.GetDataMultiResponse GetDataMulti(stocktake.SoapUnitTOM.GetDataMultiRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.hcc656.com/SoapUnit/GetDataMulti", ReplyAction="http://www.hcc656.com/SoapUnit/GetDataMultiResponse")]
        System.Threading.Tasks.Task<stocktake.SoapUnitTOM.GetDataMultiResponse> GetDataMultiAsync(stocktake.SoapUnitTOM.GetDataMultiRequest request);
        
        // CODEGEN: 参数“UpdateDataResult”需要其他方案信息，使用参数模式无法捕获这些信息。特定特性为“System.Xml.Serialization.XmlElementAttribute”。
        [System.ServiceModel.OperationContractAttribute(Action="http://www.hcc656.com/SoapUnit/UpdateData", ReplyAction="http://www.hcc656.com/SoapUnit/UpdateDataResponse")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        stocktake.SoapUnitTOM.UpdateDataResponse UpdateData(stocktake.SoapUnitTOM.UpdateDataRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.hcc656.com/SoapUnit/UpdateData", ReplyAction="http://www.hcc656.com/SoapUnit/UpdateDataResponse")]
        System.Threading.Tasks.Task<stocktake.SoapUnitTOM.UpdateDataResponse> UpdateDataAsync(stocktake.SoapUnitTOM.UpdateDataRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="GetData", WrapperNamespace="http://www.hcc656.com", IsWrapped=true)]
    public partial class GetDataRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://www.hcc656.com", Order=0)]
        public int UserSign;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://www.hcc656.com", Order=1)]
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string QKey;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://www.hcc656.com", Order=2)]
        [System.Xml.Serialization.XmlArrayAttribute(IsNullable=true)]
        [System.Xml.Serialization.XmlArrayItemAttribute(Namespace="http://schemas.microsoft.com/2003/10/Serialization/Arrays")]
        public string[] Params;
        
        public GetDataRequest() {
        }
        
        public GetDataRequest(int UserSign, string QKey, string[] Params) {
            this.UserSign = UserSign;
            this.QKey = QKey;
            this.Params = Params;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="GetDataResponse", WrapperNamespace="http://www.hcc656.com", IsWrapped=true)]
    public partial class GetDataResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://www.hcc656.com", Order=0)]
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public System.Data.DataTable GetDataResult;
        
        public GetDataResponse() {
        }
        
        public GetDataResponse(System.Data.DataTable GetDataResult) {
            this.GetDataResult = GetDataResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="GetDataMulti", WrapperNamespace="http://www.hcc656.com", IsWrapped=true)]
    public partial class GetDataMultiRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://www.hcc656.com", Order=0)]
        public int UserSign;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://www.hcc656.com", Order=1)]
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string QKey;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://www.hcc656.com", Order=2)]
        [System.Xml.Serialization.XmlArrayAttribute(IsNullable=true)]
        [System.Xml.Serialization.XmlArrayItemAttribute(Namespace="http://schemas.microsoft.com/2003/10/Serialization/Arrays")]
        public string[] Params;
        
        public GetDataMultiRequest() {
        }
        
        public GetDataMultiRequest(int UserSign, string QKey, string[] Params) {
            this.UserSign = UserSign;
            this.QKey = QKey;
            this.Params = Params;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="GetDataMultiResponse", WrapperNamespace="http://www.hcc656.com", IsWrapped=true)]
    public partial class GetDataMultiResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://www.hcc656.com", Order=0)]
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public System.Data.DataSet GetDataMultiResult;
        
        public GetDataMultiResponse() {
        }
        
        public GetDataMultiResponse(System.Data.DataSet GetDataMultiResult) {
            this.GetDataMultiResult = GetDataMultiResult;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1532.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.hcc656.com")]
    public partial class DocInfo : object, System.ComponentModel.INotifyPropertyChanged {
        
        private bool resultField;
        
        private bool resultFieldSpecified;
        
        private string resultDescField;
        
        private int orderTypeField;
        
        private bool orderTypeFieldSpecified;
        
        private string docEntryField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public bool Result {
            get {
                return this.resultField;
            }
            set {
                this.resultField = value;
                this.RaisePropertyChanged("Result");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ResultSpecified {
            get {
                return this.resultFieldSpecified;
            }
            set {
                this.resultFieldSpecified = value;
                this.RaisePropertyChanged("ResultSpecified");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=1)]
        public string ResultDesc {
            get {
                return this.resultDescField;
            }
            set {
                this.resultDescField = value;
                this.RaisePropertyChanged("ResultDesc");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public int OrderType {
            get {
                return this.orderTypeField;
            }
            set {
                this.orderTypeField = value;
                this.RaisePropertyChanged("OrderType");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool OrderTypeSpecified {
            get {
                return this.orderTypeFieldSpecified;
            }
            set {
                this.orderTypeFieldSpecified = value;
                this.RaisePropertyChanged("OrderTypeSpecified");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=3)]
        public string DocEntry {
            get {
                return this.docEntryField;
            }
            set {
                this.docEntryField = value;
                this.RaisePropertyChanged("DocEntry");
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="UpdateData", WrapperNamespace="http://www.hcc656.com", IsWrapped=true)]
    public partial class UpdateDataRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://www.hcc656.com", Order=0)]
        public int UserSign;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://www.hcc656.com", Order=1)]
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string QKey;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://www.hcc656.com", Order=2)]
        [System.Xml.Serialization.XmlArrayAttribute(IsNullable=true)]
        [System.Xml.Serialization.XmlArrayItemAttribute(Namespace="http://schemas.microsoft.com/2003/10/Serialization/Arrays")]
        public string[] Params;
        
        public UpdateDataRequest() {
        }
        
        public UpdateDataRequest(int UserSign, string QKey, string[] Params) {
            this.UserSign = UserSign;
            this.QKey = QKey;
            this.Params = Params;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="UpdateDataResponse", WrapperNamespace="http://www.hcc656.com", IsWrapped=true)]
    public partial class UpdateDataResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://www.hcc656.com", Order=0)]
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public stocktake.SoapUnitTOM.DocInfo UpdateDataResult;
        
        public UpdateDataResponse() {
        }
        
        public UpdateDataResponse(stocktake.SoapUnitTOM.DocInfo UpdateDataResult) {
            this.UpdateDataResult = UpdateDataResult;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface SoapUnitChannel : stocktake.SoapUnitTOM.SoapUnit, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class SoapUnitClient : System.ServiceModel.ClientBase<stocktake.SoapUnitTOM.SoapUnit>, stocktake.SoapUnitTOM.SoapUnit {
        
        public SoapUnitClient() {
        }
        
        public SoapUnitClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public SoapUnitClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SoapUnitClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SoapUnitClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        stocktake.SoapUnitTOM.GetDataResponse stocktake.SoapUnitTOM.SoapUnit.GetData(stocktake.SoapUnitTOM.GetDataRequest request) {
            return base.Channel.GetData(request);
        }
        
        public System.Data.DataTable GetData(int UserSign, string QKey, string[] Params) {
            stocktake.SoapUnitTOM.GetDataRequest inValue = new stocktake.SoapUnitTOM.GetDataRequest();
            inValue.UserSign = UserSign;
            inValue.QKey = QKey;
            inValue.Params = Params;
            stocktake.SoapUnitTOM.GetDataResponse retVal = ((stocktake.SoapUnitTOM.SoapUnit)(this)).GetData(inValue);
            return retVal.GetDataResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<stocktake.SoapUnitTOM.GetDataResponse> stocktake.SoapUnitTOM.SoapUnit.GetDataAsync(stocktake.SoapUnitTOM.GetDataRequest request) {
            return base.Channel.GetDataAsync(request);
        }
        
        public System.Threading.Tasks.Task<stocktake.SoapUnitTOM.GetDataResponse> GetDataAsync(int UserSign, string QKey, string[] Params) {
            stocktake.SoapUnitTOM.GetDataRequest inValue = new stocktake.SoapUnitTOM.GetDataRequest();
            inValue.UserSign = UserSign;
            inValue.QKey = QKey;
            inValue.Params = Params;
            return ((stocktake.SoapUnitTOM.SoapUnit)(this)).GetDataAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        stocktake.SoapUnitTOM.GetDataMultiResponse stocktake.SoapUnitTOM.SoapUnit.GetDataMulti(stocktake.SoapUnitTOM.GetDataMultiRequest request) {
            return base.Channel.GetDataMulti(request);
        }
        
        public System.Data.DataSet GetDataMulti(int UserSign, string QKey, string[] Params) {
            stocktake.SoapUnitTOM.GetDataMultiRequest inValue = new stocktake.SoapUnitTOM.GetDataMultiRequest();
            inValue.UserSign = UserSign;
            inValue.QKey = QKey;
            inValue.Params = Params;
            stocktake.SoapUnitTOM.GetDataMultiResponse retVal = ((stocktake.SoapUnitTOM.SoapUnit)(this)).GetDataMulti(inValue);
            return retVal.GetDataMultiResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<stocktake.SoapUnitTOM.GetDataMultiResponse> stocktake.SoapUnitTOM.SoapUnit.GetDataMultiAsync(stocktake.SoapUnitTOM.GetDataMultiRequest request) {
            return base.Channel.GetDataMultiAsync(request);
        }
        
        public System.Threading.Tasks.Task<stocktake.SoapUnitTOM.GetDataMultiResponse> GetDataMultiAsync(int UserSign, string QKey, string[] Params) {
            stocktake.SoapUnitTOM.GetDataMultiRequest inValue = new stocktake.SoapUnitTOM.GetDataMultiRequest();
            inValue.UserSign = UserSign;
            inValue.QKey = QKey;
            inValue.Params = Params;
            return ((stocktake.SoapUnitTOM.SoapUnit)(this)).GetDataMultiAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        stocktake.SoapUnitTOM.UpdateDataResponse stocktake.SoapUnitTOM.SoapUnit.UpdateData(stocktake.SoapUnitTOM.UpdateDataRequest request) {
            return base.Channel.UpdateData(request);
        }
        
        public stocktake.SoapUnitTOM.DocInfo UpdateData(int UserSign, string QKey, string[] Params) {
            stocktake.SoapUnitTOM.UpdateDataRequest inValue = new stocktake.SoapUnitTOM.UpdateDataRequest();
            inValue.UserSign = UserSign;
            inValue.QKey = QKey;
            inValue.Params = Params;
            stocktake.SoapUnitTOM.UpdateDataResponse retVal = ((stocktake.SoapUnitTOM.SoapUnit)(this)).UpdateData(inValue);
            return retVal.UpdateDataResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<stocktake.SoapUnitTOM.UpdateDataResponse> stocktake.SoapUnitTOM.SoapUnit.UpdateDataAsync(stocktake.SoapUnitTOM.UpdateDataRequest request) {
            return base.Channel.UpdateDataAsync(request);
        }
        
        public System.Threading.Tasks.Task<stocktake.SoapUnitTOM.UpdateDataResponse> UpdateDataAsync(int UserSign, string QKey, string[] Params) {
            stocktake.SoapUnitTOM.UpdateDataRequest inValue = new stocktake.SoapUnitTOM.UpdateDataRequest();
            inValue.UserSign = UserSign;
            inValue.QKey = QKey;
            inValue.Params = Params;
            return ((stocktake.SoapUnitTOM.SoapUnit)(this)).UpdateDataAsync(inValue);
        }
    }
}
