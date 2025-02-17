//----------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by https://github.com/StefH/ProxyInterfaceSourceGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//----------------------------------------------------------------------------------------

#nullable enable
using System;

namespace ProxyInterfaceSourceGeneratorTests.Source.PnP
{
    public partial class ClientObjectProxy : IClientObject
    {
        public Microsoft.SharePoint.Client.ClientObject _Instance { get; }
        

        public ProxyInterfaceSourceGeneratorTests.Source.PnP.IClientRuntimeContext Context { get => Mapster.TypeAdapter.Adapt<ProxyInterfaceSourceGeneratorTests.Source.PnP.IClientRuntimeContext>(_Instance.Context); }

        public object Tag { get => _Instance.Tag; set => _Instance.Tag = value; }

        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        public Microsoft.SharePoint.Client.ObjectPath Path { get => _Instance.Path; }

        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        public string ObjectVersion { get => _Instance.ObjectVersion; set => _Instance.ObjectVersion = value; }

        [Microsoft.SharePoint.Client.PseudoRemoteAttribute]
        public bool? ServerObjectIsNull { get => _Instance.ServerObjectIsNull; }

        public ProxyInterfaceSourceGeneratorTests.Source.PnP.IClientObject TypedObject { get => Mapster.TypeAdapter.Adapt<ProxyInterfaceSourceGeneratorTests.Source.PnP.IClientObject>(_Instance.TypedObject); }



        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        public virtual void FromJson(Microsoft.SharePoint.Client.JsonReader reader)
        {
            Microsoft.SharePoint.Client.JsonReader reader_ = reader;
            _Instance.FromJson(reader_);
        }

        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        public virtual bool CustomFromJson(Microsoft.SharePoint.Client.JsonReader reader)
        {
            Microsoft.SharePoint.Client.JsonReader reader_ = reader;
            var result__636829107 = _Instance.CustomFromJson(reader_);
            return result__636829107;
        }

        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        public void Retrieve()
        {
            _Instance.Retrieve();
        }

        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        public void Retrieve(params string[] propertyNames)
        {
            string[] propertyNames_ = propertyNames;
            _Instance.Retrieve(propertyNames_);
        }

        public virtual void RefreshLoad()
        {
            _Instance.RefreshLoad();
        }

        public bool IsPropertyAvailable(string propertyName)
        {
            string propertyName_ = propertyName;
            var result_1607091274 = _Instance.IsPropertyAvailable(propertyName_);
            return result_1607091274;
        }

        public bool IsObjectPropertyInstantiated(string propertyName)
        {
            string propertyName_ = propertyName;
            var result__181021484 = _Instance.IsObjectPropertyInstantiated(propertyName_);
            return result__181021484;
        }







        public ClientObjectProxy(Microsoft.SharePoint.Client.ClientObject instance)
        {
            _Instance = instance;
            

            Mapster.TypeAdapterConfig<Microsoft.SharePoint.Client.ClientRuntimeContext, ProxyInterfaceSourceGeneratorTests.Source.PnP.IClientRuntimeContext>.NewConfig().ConstructUsing(instance_205293328 => new ProxyInterfaceSourceGeneratorTests.Source.PnP.ClientRuntimeContextProxy(instance_205293328));
            Mapster.TypeAdapterConfig<ProxyInterfaceSourceGeneratorTests.Source.PnP.IClientRuntimeContext, Microsoft.SharePoint.Client.ClientRuntimeContext>.NewConfig().MapWith(proxy1345472640 => ((ProxyInterfaceSourceGeneratorTests.Source.PnP.ClientRuntimeContextProxy) proxy1345472640)._Instance);

            Mapster.TypeAdapterConfig<Microsoft.SharePoint.Client.ClientObject, ProxyInterfaceSourceGeneratorTests.Source.PnP.IClientObject>.NewConfig().ConstructUsing(instance_895746668 => new ProxyInterfaceSourceGeneratorTests.Source.PnP.ClientObjectProxy(instance_895746668));
            Mapster.TypeAdapterConfig<ProxyInterfaceSourceGeneratorTests.Source.PnP.IClientObject, Microsoft.SharePoint.Client.ClientObject>.NewConfig().MapWith(proxy1674261376 => ((ProxyInterfaceSourceGeneratorTests.Source.PnP.ClientObjectProxy) proxy1674261376)._Instance);


        }
    }
}
#nullable disable