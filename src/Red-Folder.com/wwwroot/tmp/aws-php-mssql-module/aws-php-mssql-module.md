One of my recent projects involved moving a PHP website to AWS.  The website was heavily reliant on the MSSQL module which isn't installed by default by AWS.

After a bit of trial and error, it was fairly simple.

In your deployment (in our case via a zip file) add a directory called .ebextensions (note that if you are using Windows, you will need to name it .ebextensions. - this is a restriction of Windows).  Within that directory add a text file (I called mine 001-install_mssql.config) with the following contents:
%[https://gist.github.com/Red-Folder/a0a379b37654be83ca42.js]

When the zip is deployed, AWS will process the config and (if all worked well) then you should have MSSQL ready to go.

## Some explanation
AWS will process the config file in alphanumeric order.  Thus I try to keep to a convention that numbers the config files.  In this instance I only had one file, but should you have multiple it helps to number them to ensure any dependencies are honored.

Within the file, the "01-install_mssql" is just a label.  Again AWS will process the labelled commands in alphanumeric order - thus again I number them to ensure they are processed in the correct order (even though there is only one command in this instance).

The actual command evokes the <b>yum</b> to install the mssql module.  I specify which repositories to get the module from - this was because the environment I was working on seemed to have corruption in one of the repositories, thus I wanted to force which one was used.  For a clean environment you should be able to remove the "--enablerepo=amzn-updates" option.

The test is used to allow AWS to validate if the command needs to be run.  In this case, if the module is already installed, lets not waste time on yum.

## Further notes
I tested this with a 32bit Amazon Linux running PHP 5.3

I seem to remember somewhere that the MSSQL module needed 32bit to run - but I can't for the life of me remember where I found this.  If you have problems with 64bit then I'd suggest switching to 32bit to see if that helps.
