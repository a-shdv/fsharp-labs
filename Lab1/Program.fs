// Для заданного списка слов найти слова, содержащие не менее одной буквы Т
// и не более двух букв О в языке F#.
let words = ["Тот"; "Окно"; "Тест"; "Одинокий"; "Остров"; "Тропа"; "Отто"; "Трон"
             "Футбол"; "Топ"]
let filteredWords =
    words
    |>  List.filter (fun word ->
        let tCount = word |> Seq.filter (fun c -> c = 'Т') |> Seq.length
        let oCount = word |> Seq.filter (fun c -> c = 'О') |> Seq.length
        tCount >= 1 && oCount <= 2
    )

printfn "Слова, содержащие не менее одной буквы Т и не более двух букв О:"
printfn "%A" filteredWords
