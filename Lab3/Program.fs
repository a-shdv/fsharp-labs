// В массиве все нечетные элементы, стоящие после максимального, увеличить в 10 раз.
// Пример: из массива A[5]: 3 7 1 5 4 должен получиться массив 3 7 10 50 4.

open Microsoft.FSharp.Collections

[<EntryPoint>]
let main argv =
    let arr = [| 3; 1; 1; 7; 1; 5; 4 |]

    let maxEl = arr |> Array.max
    let indexMaxEl = arr |> Array.findIndex (fun i -> i = maxEl)
    
    let printArray arr =
        arr |> Array.map string |> String.concat " " |> printfn "%s"

    printArray arr

    arr
    |> Array.iteri (fun i el ->
        if i > indexMaxEl && el % 2 <> 0 then
            arr.[i] <- el * 10)

    printArray arr
    0
