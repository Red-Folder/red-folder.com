I encountered some problems when uploading my plugin to the Cordova repository.

I'd use "plugman publish bgs-core" as per the documentation, which returned "Plugin published".  I thought that this had meant it worked ... how wrong I was.

After some experimentation, I realized that I had the Plugin block wrong - I simply had the version in the wrong format (originally had it as "2.0").  Adding the third level, making it "2.0.0" resolved.

This gave me a confirmation message as per the below:

![Image](/media/blog/plugman-publish-problemsPublishResults.png)
