﻿// Copyright (c) AlphaSierraPapa for the SharpDevelop Team (for details please see \doc\copyright.txt)
// This code is distributed under the GNU LGPL (for details please see \doc\license.txt)

using System;
using ICSharpCode.PackageManagement.Design;
using ICSharpCode.PackageManagement.Scripting;
using ICSharpCode.Scripting.Tests.Utils;
using ICSharpCode.SharpDevelop.Project;
using NUnit.Framework;
using PackageManagement.Tests.Helpers;

namespace PackageManagement.Tests.Scripting
{
	[TestFixture]
	public class PackageInitializationScriptsRunnerForOpenedSolutionTests
	{
		FakePackageInitializationScriptsFactory fakeScriptsFactory;
		FakePackageManagementProjectService fakeProjectService;
		PackageInitializationScriptsRunnerForOpenedSolution runner;
		FakePackageManagementConsoleHost fakeConsoleHost;
		FakeScriptingConsole fakeScriptingConsole;
		PackageInitializationScriptsConsole scriptsConsole;
		
		void CreateRunner()
		{
			fakeProjectService = new FakePackageManagementProjectService();
			fakeConsoleHost = new FakePackageManagementConsoleHost();
			fakeScriptingConsole = new FakeScriptingConsole();
			fakeConsoleHost.ScriptingConsole = fakeScriptingConsole;
			scriptsConsole = new PackageInitializationScriptsConsole(fakeConsoleHost);
			fakeScriptsFactory = new FakePackageInitializationScriptsFactory();
			runner = new PackageInitializationScriptsRunnerForOpenedSolution(fakeProjectService, scriptsConsole, fakeScriptsFactory);
		}
		
		Solution OpenSolution()
		{
			var solution = new Solution();
			solution.FileName = @"d:\projects\myprojects\test.csproj";
			fakeProjectService.FireSolutionLoadedEvent(solution);
			return solution;
		}
		
		[Test]
		public void Instance_SolutionIsOpened_PackageInitializationScriptsCreatedUsingSolution()
		{
			CreateRunner();
			Solution expectedSolution = OpenSolution();
			
			Solution actualSolution = fakeScriptsFactory.SolutionPassedToCreatePackageInitializationScripts;
			
			Assert.AreEqual(expectedSolution, actualSolution);
		}
		
		[Test]
		public void Instance_SolutionOpenedAndHasPackageInitializationScripts_InvokeInitializePackagesCmdletIsRun()
		{
			CreateRunner();
			fakeScriptsFactory.FakePackageInitializationScripts.AnyReturnValue = true;
			fakeConsoleHost.IsRunning = true;
			OpenSolution();
			
			string command = fakeScriptingConsole.TextPassedToSendLine;
			string expectedCommand = "Invoke-InitializePackages";
			
			Assert.AreEqual(expectedCommand, command);
		}
		
		[Test]
		public void Instance_SolutionOpenedAndHasNoPackageInitializationScripts_InvokeInitializePackagesCmdletIsNotRun()
		{
			CreateRunner();
			fakeScriptsFactory.FakePackageInitializationScripts.AnyReturnValue = false;
			fakeConsoleHost.IsRunning = true;
			OpenSolution();
			
			string command = fakeScriptingConsole.TextPassedToSendLine;
			
			Assert.IsNull(command);
		}
	}
}
