// Для текстового файла изменить порядок слов на противоположный
open System.IO

let reverseWordOrder (line: string) =
    let words = line.Split([|' '|])
    let reversedWords = Array.rev words
    String.concat " " reversedWords

let data = 
    Directory.GetFiles("/Users/a-shdv/RiderProjects/FP/Lab5/files", "*.txt")
    |> Array.Parallel.map File.ReadAllText
    |> Array.Parallel.map reverseWordOrder

[<EntryPoint>]
let main argv =
    let outputPath = "/Users/a-shdv/RiderProjects/FP/Lab5/files/output1.txt"
    
    let lines = data |> Array.toSeq

    // Очистка файла (перед следующим запуском программы)
    if File.Exists(outputPath) then
        File.WriteAllText(outputPath, "")
    
    File.WriteAllLines(outputPath, lines)

    0