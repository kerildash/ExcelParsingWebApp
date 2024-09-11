# ExcelParsingWebApp

An ASP.NET MVC Web application to work with Excel documents of proper format (example of document: https://docs.google.com/spreadsheets/d/1xCJILPm6HjkCVmvfaR2-3VXNK7yOs5Y4/edit?usp=sharing&ouid=113268685917474802428&rtpof=true&sd=true).
The app allows to upload documents to an sql database and to view the documents and its content in a like in a document way.
Screecast with the example of using the app: https://drive.google.com/file/d/1zPCT9chiLqilAiez90GtMb-LrK2_LDvv/view?usp=sharing

## How to set up the application
Before using the application, you need to configure and establish the connection to a database.
1. Paste the connection string of your empty database into the `appsettings.json` file
2. In the Package Manager Console, run:
   ```
   PM> Update-Database
   ```
3. Build and run the app
5. Go to http://localhost:5192 manually (you might have to point out another port here).
