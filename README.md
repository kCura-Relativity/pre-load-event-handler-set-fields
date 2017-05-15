# pre-load-event-handler-set-fields
This is a sample Pre-Load Event Handler that sets default values for Document fields.  This can also be used for fields located on a Relativity Dynamic Object (RDO).

While this is an open source project on the kCura GitHub account, support is only available through through the Relativity developer community. You are welcome to use the code and solution as you see fit within the confines of the license it is released under. However, if you are looking for support or modifications to the solution, we suggest reaching out to the Project Champion listed below or kCura can assist in putting you in touch with a Developer Partner.

# Project Setup
This project requires references to kCura's Relativity® SDK dlls.  These dlls are not part of the open source project and can be obtained by contacting support@kCura.com.  In the "packages" folder under "Source" you will need to create a "Relativity" folder if one does not exist.  You will need to add the following dlls:

- kCura.EventHandler.dll
- kCura.Relativity.Client.dll
- Relativity.API.dll (This is added as you can utilize the RSAPI to conduct interaction with Relativity data via this API.)

