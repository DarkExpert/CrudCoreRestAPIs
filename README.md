# SDWrox.API Deployment

Install .NET Core Windows Server Hosting Bundle
Before you deploy your application, you need to install the .NET Core hosting bundle for IIS – .NET Core runtime, libraries and the ASP.NET Core module for IIS.

After installation, you may need to do a “net stop was /y” and “net start w3svc” to ensure all the changes are picked up for IIS.

Download: .NET Core Windows Server Hosting <- Make sure you pick “Windows Server Hosting”

Steps to Deploy ASP.NET Core to IIS
Before you deploy, you need to make sure that WebHostBuilder is configured properly for Kestrel and IIS. Your web.config file should also exist and look similar to our example above.

Step 1: Publish to a File Folder (Added To Publish Folder)

Step 2: Copy Files to Preferred IIS Location
Now you need to copy your publish output to where you want the files to live. If you are deploying to a remote server, you may want to zip up the files and move to the server. If you are deploying to a local dev box, you can copy them locally.

For our example, I am copying the files to C:\inetpub\wwwroot\AspNetCore46

You will notice that with ASP.NET Core, there is no bin folder and it potentially copies over a ton of different .NET DLLs. Your application may also be an EXE file if you are targeting the full .NET Framework. This little sample project had over 100 DLLs in the output.


Step 3: Create Application in IIS
While creating your application in IIS is listed as a single “Step,” you will take multiple actions. First, create a new IIS Application Pool under the .NET CLR version of “No Managed Code”. Since IIS only works as a reverse proxy, it isn’t actually executing any .NET code.

Second, you can create your application under an existing or a new IIS Site. Either way, you will want to pick your new IIS Application Pool and point it to the folder you copied your ASP.NET publish output files to.


Step 4: Load Your App!
At this point, your application should load just fine. If it does not, check the output logging from it. Within your web.config file you define how IIS starts up your ASP.NET Core process. Enable output logging by setting stdoutLogEnabled=true. You may also want to change the log output location as configured in stdoutLogFile. Check out the example web.config before to see where they are set.
