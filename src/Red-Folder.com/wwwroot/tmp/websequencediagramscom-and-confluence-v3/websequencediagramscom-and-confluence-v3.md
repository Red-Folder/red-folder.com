Currently producing a number of UML Sequence diagrams for a project using the superb https://www.websequencediagrams.com/ site.

To save for posterity, I wanted to add the diagrams to the clients internal Confluence server.

WebSequenceDiagrams provide a plugin for Confluence V4+

However, not all is lost, simply create your own Macro.

From within Confluence (based on 3.5.1):


* Browse -> Confluence Admin
* Select User Macros on the left hand side menu
* Click on Create a User Macro
* Give it a name (I called in sequence)
* Set the Macro Body Processing to "Render HTML"
* Set Output Format to HTML
* Copy the below into the Template
* Click Save

HTML to paste in:%[https://gist.github.com/Red-Folder/37ce29b60b54173e315b.js]
Then within your Confluence page, just add:

![Image](/media/blog/websequencediagramscom-and-confluence-v3/Confluence-2BWSD-2Bcode.png)

Which should give you:

![Image](/media/blog/websequencediagramscom-and-confluence-v3/Confluence-2BWSD-2Bresult.png)

All good