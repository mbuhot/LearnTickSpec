module LearTickSpec.Test

open TickSpec
open Expecto
open LearnTickSpec.Core

let [<Given>] ``A calculator`` () =
    Calculator.init

let [<Given>] ``I have entered (.*) into the calculator`` (n:int) (calc: Calculator) =
    Calculator.push n calc

let [<When>] ``I press add`` (calc: Calculator) =
    Calculator.add calc

let [<When>] ``I enter (.*) into the calculator`` (n:int) (calc: Calculator) =
    Calculator.push n calc

let [<Then>] ``the result should be (.*) on the screen`` (n:int) (calc: Calculator)=
    Expect.equal (Calculator.result calc) n "Calculator Result"