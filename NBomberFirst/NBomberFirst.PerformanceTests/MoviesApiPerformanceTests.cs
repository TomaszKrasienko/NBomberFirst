
using FluentAssertions;
using NBomber;
using NBomber.Contracts;
using NBomber.CSharp;
using NBomber.Plugins.Http.CSharp;
using Xunit.Abstractions;

namespace NBomberFirst.PerformanceTests;

public class MoviesApiPerformanceTests
{
    private readonly ITestOutputHelper _testOutputHelper;
    public MoviesApiPerformanceTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }
    
    [Fact]
    public async Task GetById_ShouldHandle_AtLeast100RequestsPerSecond()
    {
        const string url = "http://localhost:5096/getById/f8924cec-b5df-4d3c-27d1-08db9022cbd7";
        HttpClient httpClient = new HttpClient();

        var getMoviesStep = Step.Create("get movie", HttpClientFactory.Create(), async context =>
        {
            try
            {
                var req =  Http.CreateRequest("GET", url);
                return await Http.Send(req, context);
            }
            catch
            {
                return Response.Fail();
            }
        });

        const int expectedRequestPerSecond = 100;
        const int durationSeconds = 5;
        
        var scenario = ScenarioBuilder.CreateScenario("Movie API Fetch", getMoviesStep)
            .WithWarmUpDuration(TimeSpan.FromSeconds(5))
            .WithLoadSimulations(LoadSimulation.NewKeepConstant(100, TimeSpan.FromSeconds(durationSeconds)));

        var stats = NBomberRunner
            .RegisterScenarios(scenario)
            .Run();

        var scenarioStats = stats.ScenarioStats[0];
        
        _testOutputHelper.WriteLine($"OK: {stats.OkCount}, FAILED: {stats.FailCount}");
        
        
        stats.OkCount.Should().BeGreaterOrEqualTo(durationSeconds * expectedRequestPerSecond);
        stats.FailCount.Should().Be(0);
        scenarioStats.LatencyCount.LessOrEq800.Should().BeGreaterOrEqualTo(durationSeconds * expectedRequestPerSecond);
    }
}