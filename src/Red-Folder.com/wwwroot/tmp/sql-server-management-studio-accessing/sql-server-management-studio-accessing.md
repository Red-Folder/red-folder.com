## Summary
Sometime you will need to access SQL Server with different windows credentials.  I've recently needed to access a SQL Server in a different domain with no trust.  The below shows you how to do this.

## Credit
All credit for this goes to this posting [here.](http://stackoverflow.com/questions/849149/connect-different-windows-user-in-sql-server-management-studio-2005-or-later)
## Problem
I'm using my Windows 7 machine and logged onto Domain A.  I fire up SQL Server Management Studio and try to access SQL1 on Domain B.  With no trust between Domain A &amp; B I get the following message:
"Login failed.  The login is from an untrusted domain and cannot be used with Windows Authentication.  (Microsoft SQL Server, Error: 18452)"
## Solution
Use the following (set up as a short cut):
C:\Windows\System32\runas.exe /netonly /user:DOMAINB\USERNAME "C:\Program Files (x86)\Microsoft SQL Server\100\Tools\Binn\VSShell\Common7\IDE\Ssms.exe"

Substitute the Domain and Username for credentails that are valid for accessing the SQL Server (and potentially amend your SSMS.exe path) - and away you go.
You'll be asked for your password (on the SQL Server Domain) every time you run the command (from the DOS box), but then you'll be good to go*.
(*Ignore the windows credentials it shows on the connection dialog and just enter the destination server and away you go)