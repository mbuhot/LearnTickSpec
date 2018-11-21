module LearnTickSpec.Test
open System.Reflection
open Expecto
open TickSpec

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
