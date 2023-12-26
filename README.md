# AzureCommunicationEmailService
 Test application for ACS email services
 

You can use this appliction to test Azure Communication Services Email Service [Prepare Email Communication resource for Azure Communication Service](https://learn.microsoft.com/en-us/azure/communication-services/concepts/email/prepare-email-communication-resource)

![image](https://github.com/ealmuneyeer/AzureCommunicationEmailService/assets/36260446/9746031c-a77e-4977-80bd-c0eaa79ca066)

# Description: -
You can test different Email Communication Services features by using this tool. For example:
 - Authenticate by using ACS connection string, AAD application, or interactive authentication
 - Sending email with auto-rety enabled or disabled
 - Sending email through SMTP by using [MailKit](https://www.nuget.org/packages/MailKit/) or [System.Net.Mail](https://learn.microsoft.com/en-us/dotnet/api/system.net.mail?view=net-8.0)
 - Setting email importance
 - Sending plain or HTML email body
 - Sending custom headers
 - Control email recipeents list (To, CC, BCC, Reply To)
 - Include attachments into the email
 - Wait until the sent email is started or completed
 - Send multiple emails
 - Query about email delivery status

# Note: -
You can put required setting in <i>**appsettings.json**</i> to fill the form on startup


# Tip: -
You can remove rows from <i>Receipents</i>, <i>Custom Headers</i>, and <i>Attachments</i> by selecting the first column (Clicking on the first column with your mouse), then press on the <i>Delete</i> in your keyboard
![image](https://user-images.githubusercontent.com/36260446/230054474-9f774804-cbe6-4b6f-a59d-97a964ee3267.png)

# Braking changes: -
<ul>
 <li><b>Version 2.0 - Dec 2023:</b></li>
 <ul>
  <li><i>ConnectionString</i> has been replaced in <i><b>appsettings.json</b></i> with <i>ACSEndpoint</i> and <i>ACSAccessKey</i></li>
 </ul>
</ul>
