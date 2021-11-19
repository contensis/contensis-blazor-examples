using Zengenti.Contensis.Delivery;

namespace BlazorWebAssemblyApp.Server;

internal class Settings
{
    /// e.g. https://cms-yourcmsdomain.cloud.contensis.com/
    internal static readonly string RootUri = "<SET ROOT URI>";
    internal static readonly string DeliveryProjectApiId = "<SET DELIVERY PROJECT API ID>";
    internal static readonly VersionStatus DeliveryVersionStatus = VersionStatus.Latest;
    internal static readonly string ClientId = "<SET CLIENT ID>";
    internal static readonly string SharedSecret = "<SET SHARED SECRET>";
    internal static readonly string Scopes = "Security_Administrator ContentType_Delete ContentType_Read ContentType_Write Entry_Delete Entry_Read Entry_Write Project_Read Project_Write Project_Delete DiagnosticsAllUsers DiagnosticsAdministrator Workflow_Administrator";
}
