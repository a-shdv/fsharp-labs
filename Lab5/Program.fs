// Для текстового файла изменить порядок слов на противоположный
open System.IO

let reverseWordOrder (line: string) =
    let words = line.Split([|' '|])
    let reversedWords = Array.rev words
    String.concat " " reversedWords

let data = 
    Directory.GetFiles("/Users/a-shdv/RiderProjects/FP/Lab5/files", "*.txt")
    |> Array.Parallel.map (fun filePath -> File.ReadAllText(filePath))
    |> Array.Parallel.map reverseWordOrder

[<EntryPoint>]
let main argv =
    let outputPath = "/Users/a-shdv/RiderProjects/FP/Lab5/files/output.txt"
    
    let lines = data |> Array.toList

    // Очистка файла
    File.WriteAllText(outputPath, "")
    
    File.WriteAllLines(outputPath, lines)

    0