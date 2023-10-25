open System.IO
open System.Text.RegularExpressions

// Функция для извлечения слов из текста
let splitTextIntoWords text =
    let pattern = @"\w+"
    let regex = new Regex(pattern)
    let matches = regex.Matches(text)
    seq { for m in matches do yield m.Value }

// Функция для подсчета слов или словосочетаний
let countWords words =
    words
    |> Seq.groupBy id
    |> Seq.map (fun (word, group) -> (word, Seq.length group))
    |> Seq.toList

// Функция для нахождения наиболее часто встречающегося словосочетания
let findMostFrequentPhrase filename =
    let text = File.ReadAllText(filename)
    let words = splitTextIntoWords text
    let wordCounts = countWords words
    let sortedWordCounts = List.sortBy (fun (_, count) -> -count) wordCounts
    match sortedWordCounts with
    | [] -> None
    | (word, count) :: _ -> Some (word, count)

// Замените "sample.txt" на путь к вашему текстовому файлу
let mostFrequentPhrase = findMostFrequentPhrase "/Users/a-shdv/RiderProjects/FP/FP/input.txt"

match mostFrequentPhrase with
| Some (phrase, count) -> printfn "Самое часто встречающееся словосочетание: %s (%d раз)" phrase count
| None -> printfn "Текстовый файл пуст или не найден"
