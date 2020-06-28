# MicrosoftGraphApiDaemonApp
Description: This application retrieves  the client secret from the azure key vault and then get the access token to call Microsoft Graph APIs.

Steps:
01: Register an application which can be decribed as first applicaiton via app registration in azure active directory and get client id and client secret by adding
new client secret from Certificate & secret section.No need to use any redirect URI for this application.
Also do not get any permission to access microsoft graph API from this application.

02:This client id has been  referenced as azureKeyVaultAccessClientid at the appsettings.json .Put your client id value here.

03:This client secret has been referenced as clientSecret at App.config. Put your client secret value here and encrypt this config file.

04.The tenant id has been referenced as azureADtenantId at the appsettings.json file.Put your azure AD tenant id here.

05:Download Microsoft.Identity.Client from Nuget.It has been describe at the 15(c) step.Just download once.

06:Register another application which can be describe as second application via app registration in azure active directory and get client id and client secret.
This application identity will be used to access graph APIs.No need to use any redirect URI.

07.This client id has been  referenced as graphApiAccessClientId at the appsettings.json.

08:Create a  azure key vault client secret by clicking Generate/Import tab from secret menu in Azure key vault.Give the name and the value will
be the client secret of second application which will access graph APIs.Then finish this step by clicking create button.

09:Then go to the  Access policies menu and add access policy in Azure Key vault.

Select Key permission:get.Get option is used to read the the secret from key vault by any application.
Certificate permission field:No need to provide anything because no certificate is being used for this application.
Select principal:Click none select link and select the first application to grant permission for the first application to access azure key
vault.

10:Now  put https://yourKeyVaultName.vault.azure.net/ in appsettings.json file for the vaultBaseUrl property.

11.Now put azure key vault client secret name you filled up at step 08 in appsettings.json file for the keyVault_SecretName property.

12:Now get the required permission for second application to acces microsoft graph APIs from API permissions menu in azure AD portal.

13.Now browse this url https://login.microsoftonline.com/Enter_the_Tenant_Id_Here/adminconsent?client_id=Enter_the_Application_Id_Here after editing
with the tenant id and client id (second application client id).

14.you may see error 'AADSTS50011':No reply address is registered for the application after granting consent to the app using the preceding URL.
This happen because this application and the URL do not have a redirect URI. So please ignore the error.

15.Download the following packages from Nuget package
a.Microsoft.Azure.KeyVault
b.Microsoft.Graph
c.Microsoft.Identity.Client 
d.Also other libraries to access appsettings.json and app.config file.Check the packages section.

16.In the application 'Task <string>GetClientSecretFromKeyVault(IAuthenticationRepository iAuthRepository)' this method at IKeyVaultAccessRepository gets the access token
for the first application after authentication with azure AD by using confidential client flow then get the client secret for second application  which was kept in azure key vault.

17.In the application 'Task<string> GetGraphApiAccessToken(IAuthenticationRepository _iAuthenticationRepository, string clientSecret)' this method at ITokenProcessorRepository uses the client secret kept at the key vault returned
from  the method of the step 16 to get access token for accessing graph api.

18.In the application  Task<List<User>> GetUserDetails(IAuthenticationRepository iAuthenticationRepository, string token) this method at IUserDetailsRepository returns  the response payload of list of users from
the microsoft graph API.

19:Finally add the following code to the MicrosoftGraphApiDaemonApp.csproj file to copy the appsettings.json file to the bin folder when it  builds/rebuilds the project.

<ItemGroup>
		<None Update="appsettings.json">
			<CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
		</None>
</ItemGroup>



