 Переменные
 Все значения переменных по умолчанию неизменяемы
 let name = "Test" // определение переменной (неизменяема, можно сказать, консанта)
 т.е. let name = "Bob" не изменит значение переменной

 Изменяемые переменные
 let mutable name = "Test" // определение изменяемой переменной
 name <- "New Test" // присваивание нового значения

 Операции сравнения
 = используется для проверки на равенство
 <> то же самое, что и !=

 Функции
 let имя_функции параметры = действия_функции

 let printMessage () = printfn "Hello world" // () - значение типа unit
 printMessage () // вызов функции с передачей unit параметра

 let add x y = x + y // Определение функции add
 let result = add 3 4 // Использование функции

 Функция, выполняющая несколько действия
 let printMessage() =                      // начало определения функции   
     printfn "Welcome to F#"
     let message = "Hello METANIT.COM"
     printfn "%s" message                 // конец определения функции
  
 printMessage()    // вызов функции

 _ ()
 let printMessage = printfn "Welcome to F#"
 let printMessage() = printfn "Welcome to F#"