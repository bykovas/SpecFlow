﻿using System;
using TechTalk.SpecFlow.Specs.Drivers.CucumberMessages;
using TechTalk.SpecFlow.TestProjectGenerator;
using TechTalk.SpecFlow.TestProjectGenerator.Driver;

namespace TechTalk.SpecFlow.Specs.StepDefinitions.CucumberMessages
{
    [Binding]
    public class TestCaseFinishedSteps
    {
        private readonly TestSuiteInitializationDriver _testSuiteInitializationDriver;
        private readonly TestSuiteSetupDriver _testSuiteSetupDriver;
        private readonly SolutionDriver _solutionDriver;
        private readonly VSTestExecutionDriver _vsTestExecutionDriver;
        private readonly TestCaseFinishedDriver _testCaseFinishedDriver;

        public TestCaseFinishedSteps(TestSuiteInitializationDriver testSuiteInitializationDriver, TestSuiteSetupDriver testSuiteSetupDriver, SolutionDriver solutionDriver, VSTestExecutionDriver vsTestExecutionDriver, TestCaseFinishedDriver testCaseFinishedDriver)
        {
            _testSuiteInitializationDriver = testSuiteInitializationDriver;
            _testSuiteSetupDriver = testSuiteSetupDriver;
            _solutionDriver = solutionDriver;
            _vsTestExecutionDriver = vsTestExecutionDriver;
            _testCaseFinishedDriver = testCaseFinishedDriver;
        }

        [When(@"the scenario is finished at '(.*)'")]
        public void WhenTheScenarioIsFinishedAt(DateTime finishTime)
        {
            _testSuiteInitializationDriver.OverrideTestCaseFinishedTime = finishTime;
            _testSuiteSetupDriver.EnsureAProjectIsCreated();
            _solutionDriver.CompileSolution(BuildTool.MSBuild);
            _solutionDriver.CheckSolutionShouldHaveCompiled();
            _vsTestExecutionDriver.ExecuteTests();
        }

        [Then(@"(.*) TestCaseFinished messages have been sent")]
        public void ThenTestCaseFinishedMessagesHaveBeenSent(int numberOfMessages)
        {
            _testCaseFinishedDriver.TestCaseFinishedMessagesShouldHaveBeenSent(numberOfMessages);
        }


        [Then(@"a TestCaseFinished message has been sent with the following attributes")]
        public void ThenATestCaseFinishedMessageHasBeenSentWithTheFollowingAttributes(Table table)
        {
            _testCaseFinishedDriver.TestCaseFinishedMessageShouldHaveBeenSent(table);
        }
    }
}
