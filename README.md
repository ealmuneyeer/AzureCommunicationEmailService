# AzureCommunicationEmailService
 Test application for ACS email services
 

You can use this appliction to test Azure Communication Services Email Service [Prepare Email Communication resource for Azure Communication Service](https://learn.microsoft.com/en-us/azure/communication-services/concepts/email/prepare-email-communication-resource)

![image](https://github.com/ealmuneyeer/AzureCommunicationEmailService/assets/36260446/90a1967e-1a56-49ef-9aff-c498254e9f3f)


# Description: -
You can test different Email Communication Services features by using this tool. For example:
<ul>
 <li>Authenticate by using ACS connection string, AAD application, or interactive authentication</li>
 <li>Sending email with auto-rety enabled or disabled</li>
 <li>Sending email through SMTP</li>
 <li>Setting email importance</li>
 <li>Sending plain or HTML email body</li>
 <li>Sending custom headers</li>
 <li>Control email recipeents list (To, CC, BCC, Reply To)</li>
 <li>Include attachments into the email</li>
 <li>Wait until the sent email is started or completed</li>
 <li>Send multiple emails</li>
 <li>Query about email delivery status</li>
</ul>

# Note: -
You can put required setting in <i>**appsettings.json**</i> to fill the form on startup


# Tip: -
You can remove rows from <i>Receipents</i>, <i>Custom Headers</i>, and <i>Attachments</i> by selecting the first column (Clicking on the first column with your mouse), then press on the <i>Delete</i> in your keyboard
![image](https://user-images.githubusercontent.com/36260446/230054474-9f774804-cbe6-4b6f-a59d-97a964ee3267.png)

# Braking changes: -
<ul>
 <li><b>Version 2.0 - Dec 2023:<b></li>
 <ul>
  <li><i>ConnectionString</i> has been replaced in <i><b>appsettings.json</b></i> with <i>ACSEndpoint</i> and <i>ACSAccessKey</i></li>
 </ul>
</ul>
