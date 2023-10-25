// Для массива текстовых файлов выполнить задание в парадигме параллельного программирования и сформировать единый выходной файл.
// Для текстового файла изменить порядок слов на противоположный
open System.IO

let getFile file = file
                  |> Seq.toList
                  |> List.rev
             

let spl(str:char list) = str
                      |> Seq.toList
                      |> List.rev
                      |> Seq.iter (printf "%A ")

                  
let data = 
    Directory.GetFiles(@"/Users/a-shdv/RiderProjects/FP/Lab5/files", "*.txt")
    |> Array.Parallel.map File.ReadAllText
    |> Array.Parallel.map Seq.toList
    |> Array.Parallel.map List.rev
    |> Array.Parallel.iter spl

[<EntryPoint>]
let main argv =
    let lst = data
    lst
    0 