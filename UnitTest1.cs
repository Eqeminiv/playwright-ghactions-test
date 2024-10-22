using Microsoft.Playwright.NUnit;
using Microsoft.Playwright;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Playwright;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class Tests : PageTest
{
    [SetUp]
    public async Task Setup()
    {
        await Context.Tracing.StartAsync(new()
        {
            Title = $"{TestContext.CurrentContext.Test.ClassName}.{TestContext.CurrentContext.Test.Name}",
            Screenshots = true,
            Snapshots = true,
            Sources = true
        });
    }
    [TearDown]
    public async Task Teardown()
    {
        await Context.Tracing.StopAsync(new()
        {
            Path = Path.Combine(
                TestContext.CurrentContext.WorkDirectory,
                "playwright-traces-test",
                $"{TestContext.CurrentContext.Test.ClassName}.{TestContext.CurrentContext.Test.Name}.zip")
        });
    }


    [Test]
    public async Task HasTitle()
    {
        await Page.GotoAsync("https://playwfailright.dev");

        await Expect(Page).ToHaveTitleAsync(new Regex("Playwright"));

    }

    [Test]
    public async Task GetStartedLink()
    {
        await Page.GotoAsync("https://playwright.dev");

        await Page.GetByRole(AriaRole.Link, new() { Name = "Get started" }).ClickAsync();

        await Expect(Page.GetByRole(AriaRole.Heading, new() { Name = "Installation" })).ToBeVisibleAsync();
    }

    [Test] 
    public async Task GetNumberOfNews()
    {
        await Page.GotoAsync("https://fcbarca.com");

        var locator = await Page.GetByRole(AriaRole.Article).AllTextContentsAsync();

        

        foreach (var item in locator)
        {
            Console.WriteLine("========");
            Console.WriteLine(item);
        }

    }
}