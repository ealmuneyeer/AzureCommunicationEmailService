# Azure Communication Services Email Tesrting Tool
 This is an unofficial tool for testing the [Azure Communication Services Email Service](https://learn.microsoft.com/en-us/azure/communication-services/concepts/email/prepare-email-communication-resource). There are no guarantees it is free of bugs

## Description: -
You can test various features of the Email Communication Services using this tool. For example:
+ Authenticate using ACS connection string, Entra ID, interactive authentication, or SMTP basic authentication.
+ Send emails with auto-retry enabled or disabled.
+ Send emails through SMTP using [MailKit](https://www.nuget.org/packages/MailKit/) or [System.Net.Mail](https://learn.microsoft.com/en-us/dotnet/api/system.net.mail?view=net-8.0).
+ Set email importance.
+ Send plain text or HTML email bodies.
+ Add custom headers.
+ Manage email recipients (To, CC, BCC, Reply To).
+ Include attachments and/or inline attachments in the email.
+ Get images as Base64 strings.
+ Wait until the sent email is started or completed.
+ Send multiple emails to simulate bulk email sending.
+ Query email delivery status.
+ Connect to Azure Service Bus to receive email events
   
## Prerequisites
To run this tool, you need the following:
+ An [Azure Communication Services](https://learn.microsoft.com/en-us/azure/communication-services/overview) resource. You can create it by following the steps in the [Quickstart: Create and manage Communication Services resources](https://learn.microsoft.com/en-us/azure/communication-services/quickstarts/create-communication-resource?tabs=windows&pivots=platform-azp)
+  An Email Communication Service resource. You can create it by following the steps in the [Quickstart: Create and manage Email Communication Service resources](https://learn.microsoft.com/en-us/azure/communication-services/quickstarts/email/create-email-communication-resource?pivots=platform-azp)
+  An Azure Managed Domain or Custom Domain provisioned under your Email Communication Service resource. More information can be found in the [Quickstart: How to add Azure Managed Domains to Email Communication Service](https://learn.microsoft.com/en-us/azure/communication-services/quickstarts/email/add-azure-managed-domains?pivots=platform-azp) and [Quickstart: How to add custom verified email domains](https://learn.microsoft.com/en-us/azure/communication-services/quickstarts/email/add-custom-verified-domains?pivots=platform-azp).
+  A connected domain in your Azure Communication Services resource. For more information, follow the steps in the [Quickstart: How to connect a verified email domain](https://learn.microsoft.com/en-us/azure/communication-services/quickstarts/email/connect-email-communication-resource?pivots=programming-language-rest)
+ <i>Optional:</i> An [Azure Service Bus](https://learn.microsoft.com/en-us/azure/service-bus-messaging/service-bus-messaging-overview) resource with a namespace and queue. You can create it by following the public documentation on how to [Use Azure portal to create a Service Bus namespace and a queue](https://learn.microsoft.com/en-us/azure/service-bus-messaging/service-bus-quickstart-portal).
+ .Net 8.0. You can download it from the [.Net download page](https://dotnet.microsoft.com/en-us/download/dotnet).

## Before You Run
After downloading the project and before building it with Visual Studio, prepare your environment as follows:
+ <i>Optional:</i> Configure your Azure Communication Services resource events to send all email-related events to the Azure Service Bus queue created in the prerequisites. For more information, refer to the [Subscribe to Azure Communication Services events](https://learn.microsoft.com/en-us/azure/communication-services/quickstarts/events/subscribe-to-events?pivots=platform-azp) documentation.
+ <i>Optional:</i> Modify the <i>**appsettings.json**</i> file in your project with the required information. This will help automatically fill in the required information on startup. Alternatively, you can manually fill or edit the information on startup.


## Notes: -
+ You can remove rows from <i>Receipents</i>, <i>Custom Headers</i>, and <i>Attachments</i> by selecting the first column (Clicking on it with your mouse), then pressing <i>Delete</i> in your keyboard
![image](https://user-images.githubusercontent.com/36260446/230054474-9f774804-cbe6-4b6f-a59d-97a964ee3267.png)

## Braking changes: -
<ul>
  <li><b>Version 4.0 - Dec 2024:</b></li>
 <ul>
  <li><i>AADDefaultCredentials</i> has been renamed in <i><b>appsettings.json</b></i> to <i>EntraIdDefaultCredentials</i></li>
  <li><i>AADClientSecret</i> has been renamed in <i><b>appsettings.json</b></i> to <i>EntraIdClientSecret</i></li>
  <li><i>AAD_ClientId</i> has been renamed in <i><b>appsettings.json</b></i> to <i>EntraId_ClientId</i></li>
  <li><i>AAD_ClientSecret</i> has been renamed in <i><b>appsettings.json</b></i> to <i>EntraId_ClientSecret</i></li>
 </ul>
 <li><b>Version 2.0 - Dec 2023:</b></li>
 <ul>
  <li><i>ConnectionString</i> has been replaced in <i><b>appsettings.json</b></i> with <i>ACSEndpoint</i> and <i>ACSAccessKey</i></li>
 </ul>
</ul>
