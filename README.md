# LearnTickSpec

A demo project showing TickSpec with Expecto

## The Gist:

 - Write a feature file:
 
```gherkin
 Feature: Addition
  In order to avoid silly mistakes
  As a math idiot
  I want to be told the sum of two numbers

  Background:
    Given A calculator

  Scenario: Add two numbers
    Given I have entered 50 into the calculator
    And I have entered 70 into the calculator
    When I press add
    Then the result should be 120 on the screen
```

 - Compile the feature files into the test assembly as embedded resources:
 
 ```xml
 <EmbeddedResource Include="Addition.feature" />
 ```
 
 - Write step definitions are F# code with Expecto assertions and TickSpec attributes:
 
 ```fsharp
 let [<Given>] ``A calculator`` () =
    Calculator.init
    
 let [<Given>] ``I have entered (.*) into the calculator`` (n:int) (calc: Calculator) =
    Calculator.push n calc

let [<When>] ``I press add`` (calc: Calculator) =
    Calculator.add calc

let [<Then>] ``the result should be (.*) on the screen`` (n:int) (calc: Calculator)=
    Expect.equal (Calculator.result calc) n "Calculator Result"
```
 
 - Discover features and step definitions using reflection over the current assembly:

```fsharp
let assembly = Assembly.GetExecutingAssembly()

let featureFromEmbeddedResource resourceName : Feature =
    let stream = assembly.GetManifestResourceStream(resourceName)
    StepDefinitions(assembly).GenerateFeature(resourceName, stream)

let testListFromFeature feature =
    feature.Scenarios
    |> Seq.map (fun scenario -> testCase scenario.Name scenario.Action.Invoke)
    |> Seq.toList
    |> testList feature.Name

[<Tests>]
let featureTests =
    assembly.GetManifestResourceNames()
    |> Array.filter (fun resource -> resource.EndsWith(".feature"))
    |> Seq.map (featureFromEmbeddedResource >> testListFromFeature)
    |> Seq.toList
    |> testList (assembly.GetName().Name)

[<EntryPoint>]
let main argv =
    Tests.runTestsInAssembly defaultConfig argv
```

