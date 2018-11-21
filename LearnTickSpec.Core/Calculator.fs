namespace LearnTickSpec.Core

type Calculator = {
    Values : int list
}

module Calculator =
    let init = {Values = []}
    let push n calc = {calc with Values = n :: calc.Values}
    let add calc = {calc with Values = [List.sum calc.Values]}
    let result calc = calc.Values.Head
