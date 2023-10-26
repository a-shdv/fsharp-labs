// В массиве все нечетные элементы, стоящие после максимального, увеличить в 10 раз.
// Пример: из массива A[5]: 3 7 1 5 4 должен получиться массив 3 7 10 50 40.

// open Microsoft.FSharp.Collections
//
// [<EntryPoint>]
// let main argv =
//     let arr = [| 3; 1; 1; 7; 1; 5; 4 |]
//
//     let max = arr
//               |> Array.max
//
//     let index = arr
//                 |> Array.findIndex (fun x -> x = max)
//
//     arr.[index + 1 ..]// не должен создаваться срез (через цикл for)
//
//     |> Array.map (fun x -> x * 10)
//     |> Array.append arr.[0..index]
//     |> printf "%A "
//
//     0

open Microsoft.FSharp.Collections

[<EntryPoint>]
let main argv =
    let arr = [| 3; 7; 1; 5; 4 |]

    let max = arr |> Array.max
    let index = arr |> Array.findIndex (fun x -> x = max)

    arr
    |> Array.iter (fun x -> printf "%d " x)
    printf "\n"
    
    for i = index + 1 to arr.Length - 1 do
        arr.[i] <- arr.[i] * 10

    arr
    |> Array.iter (fun x -> printf "%d " x)

    0



