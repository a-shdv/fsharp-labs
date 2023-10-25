module Lab2.Program

open System.IO
open System.Text.RegularExpressions

// Функция для извлечения слов из текста
let splitTextIntoWords (text: string) =
    let pattern = @"\w+"
    let regex = new Regex(pattern)
    let matches = regex.Matches(text)
    seq { for m in matches do yield m.Value }

// Функция для подсчета слов или словосочетаний
let countWords (words: seq<string>) =
    words
    |> Seq.groupBy id
    |> Seq.map (fun (word, group) -> (word, Seq.length group))

// Функция для нахождения наиболее часто встречающегося словосочетания
let findMostFrequentPhrase (filename: string) =
    let text = File.ReadAllText(filename)
    let words = splitTextIntoWords text
    let wordCounts = countWords words
    let sortedWordCounts = Seq.sortBy (fun (_, count) -> -count) wordCounts
    match Seq.tryHead sortedWordCounts with
    | Some (word, count) -> Some (word, count)
    | None -> None

let mostFrequentPhrase = findMostFrequentPhrase "/Users/a-shdv/RiderProjects/FP/Lab2/text.txt"

match mostFrequentPhrase with
| Some (phrase, count) -> printfn "Самое часто встречающееся словосочетание: %s (%d раз)" phrase count
| None -> printfn "Текстовый файл пуст или не найден"
