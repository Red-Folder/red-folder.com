using Xunit;

namespace RedFolder.Blog.Unit.Tests;

// Temporary verification for issue #30. Close the verification PR without merging.
public class MergeGateVerificationTests
{
    [Fact]
    public void MergeGate_DeliberateFailure_BlocksMerge()
    {
        Assert.True(false, "Intentional failure to verify the master CI merge gate (issue #30).");
    }
}
