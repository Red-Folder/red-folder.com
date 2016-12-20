I recently made a mistake when copying code from a Visual Studio extension course (all entirely my fault).

The course I was following was [Customizing and Extending Visual Studio 2010 by Writing Code](http://pluralsight.com/training/Courses/TableOfContents/vs2010-vsx-code)

But when I ran the demo (chapter 2), I received the following error:

![Image](/media/blog/compositioncontractmismatchexception/Exception-message.png)

When I reviewed the log I found the following exception details:

```
System.ComponentModel.Composition.CompositionContractMismatchException: Cannot cast the underlying exported value of type 'ToDoMarker.ToDoProvider.CreateTagger ContractName="Microsoft.VisualStudio.Text.Tagging.ITaggerProvider")' to type 'Microsoft.VisualStudio.Text.Tagging.ITaggerProvider'.
```

Which meant pretty much nothing to me.

After some playing around, I realized it was a result of me putting the attributes in the wrong place within the ToDoProvider.cs.

Originally I added the attributes at the method level:
![Image](/media/blog/compositioncontractmismatchexception/Before.png)

This was incorrect, it needed to be at the class level:
![Image](/media/blog/compositioncontractmismatchexception/After.png)

Hopefully by posting this, anyone else getting the same problem will find it through web search rather than trial and error ;)
