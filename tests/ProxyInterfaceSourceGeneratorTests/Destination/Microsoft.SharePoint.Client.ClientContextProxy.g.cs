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
    public partial class ClientContextProxy : ProxyInterfaceSourceGeneratorTests.Source.PnP.ClientRuntimeContextProxy, IClientContext
    {
        public new Microsoft.SharePoint.Client.ClientContext _Instance { get; }
        public Microsoft.SharePoint.Client.ClientRuntimeContext _InstanceClientRuntimeContext { get; }

        public ProxyInterfaceSourceGeneratorTests.Source.PnP.IWeb Web { get => Mapster.TypeAdapter.Adapt<ProxyInterfaceSourceGeneratorTests.Source.PnP.IWeb>(_Instance.Web); }

        public Microsoft.SharePoint.Client.Site Site { get => _Instance.Site; }

        public Microsoft.SharePoint.Client.RequestResources RequestResources { get => _Instance.RequestResources; }

        public System.Version ServerVersion { get => _Instance.ServerVersion; }



        public Microsoft.SharePoint.Client.FormDigestInfo GetFormDigestDirect()
        {
            var result_333437737 = _Instance.GetFormDigestDirect();
            return result_333437737;
        }

        public override void ExecuteQuery()
        {
            _Instance.ExecuteQuery();
        }

        public override System.Threading.Tasks.Task ExecuteQueryAsync()
        {
            var result_737681611 = _Instance.ExecuteQueryAsync();
            return result_737681611;
        }







        public ClientContextProxy(Microsoft.SharePoint.Client.ClientContext instance) : base(instance)
        {
            _Instance = instance;
            _InstanceClientRuntimeContext = instance;

            Mapster.TypeAdapterConfig<Microsoft.SharePoint.Client.ClientRuntimeContext, ProxyInterfaceSourceGeneratorTests.Source.PnP.IClientRuntimeContext>.NewConfig().ConstructUsing(instance_205293328 => new ProxyInterfaceSourceGeneratorTests.Source.PnP.ClientRuntimeContextProxy(instance_205293328));
            Mapster.TypeAdapterConfig<ProxyInterfaceSourceGeneratorTests.Source.PnP.IClientRuntimeContext, Microsoft.SharePoint.Client.ClientRuntimeContext>.NewConfig().MapWith(proxy1345472640 => ((ProxyInterfaceSourceGeneratorTests.Source.PnP.ClientRuntimeContextProxy) proxy1345472640)._Instance);

            Mapster.TypeAdapterConfig<Microsoft.SharePoint.Client.ClientObject, ProxyInterfaceSourceGeneratorTests.Source.PnP.IClientObject>.NewConfig().ConstructUsing(instance_895746668 => new ProxyInterfaceSourceGeneratorTests.Source.PnP.ClientObjectProxy(instance_895746668));
            Mapster.TypeAdapterConfig<ProxyInterfaceSourceGeneratorTests.Source.PnP.IClientObject, Microsoft.SharePoint.Client.ClientObject>.NewConfig().MapWith(proxy1674261376 => ((ProxyInterfaceSourceGeneratorTests.Source.PnP.ClientObjectProxy) proxy1674261376)._Instance);

            Mapster.TypeAdapterConfig<Microsoft.SharePoint.Client.SecurableObject, ProxyInterfaceSourceGeneratorTests.Source.PnP.ISecurableObject>.NewConfig().ConstructUsing(instance592284880 => new ProxyInterfaceSourceGeneratorTests.Source.PnP.SecurableObjectProxy(instance592284880));
            Mapster.TypeAdapterConfig<ProxyInterfaceSourceGeneratorTests.Source.PnP.ISecurableObject, Microsoft.SharePoint.Client.SecurableObject>.NewConfig().MapWith(proxy_300636294 => ((ProxyInterfaceSourceGeneratorTests.Source.PnP.SecurableObjectProxy) proxy_300636294)._Instance);

            Mapster.TypeAdapterConfig<Microsoft.SharePoint.Client.ClientContext, ProxyInterfaceSourceGeneratorTests.Source.PnP.IClientContext>.NewConfig().ConstructUsing(instance_1283184912 => new ProxyInterfaceSourceGeneratorTests.Source.PnP.ClientContextProxy(instance_1283184912));
            Mapster.TypeAdapterConfig<ProxyInterfaceSourceGeneratorTests.Source.PnP.IClientContext, Microsoft.SharePoint.Client.ClientContext>.NewConfig().MapWith(proxy1267236400 => ((ProxyInterfaceSourceGeneratorTests.Source.PnP.ClientContextProxy) proxy1267236400)._Instance);

            Mapster.TypeAdapterConfig<Microsoft.SharePoint.Client.Web, ProxyInterfaceSourceGeneratorTests.Source.PnP.IWeb>.NewConfig().ConstructUsing(instance_1865313808 => new ProxyInterfaceSourceGeneratorTests.Source.PnP.WebProxy(instance_1865313808));
            Mapster.TypeAdapterConfig<ProxyInterfaceSourceGeneratorTests.Source.PnP.IWeb, Microsoft.SharePoint.Client.Web>.NewConfig().MapWith(proxy2115366516 => ((ProxyInterfaceSourceGeneratorTests.Source.PnP.WebProxy) proxy2115366516)._Instance);


        }
    }
}
#nullable disable