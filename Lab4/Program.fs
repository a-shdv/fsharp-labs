// Клиентский отдел банка
type Person(name: string, address: string, phoneNumber: string) =
    member val Name = name with get, set
    member val Address = address with get, set
    member val PhoneNumber = phoneNumber with get, set

type ICustomerService =
    abstract member Deposit: decimal -> decimal
    abstract member Withdraw: decimal -> decimal

type Customer
    (name: string, address: string, phoneNumber: string, customerId: int, accountNumber: string, balance: decimal) =
    inherit Person(name, address, phoneNumber)
    member val CustomerId = customerId with get, set
    member val AccountNumber = accountNumber with get, set
    member val Balance = balance with get, set

    interface ICustomerService with
        member this.Deposit(amount: decimal) = this.Balance + amount

        member this.Withdraw(amount: decimal) = this.Balance - amount

type IEmployeeService =
    abstract member AssignTask: string -> seq<string> -> seq<string>
    abstract member CompleteTask: string -> seq<string> -> seq<string>

type Employee(name: string, address: string, phoneNumber: string, employeeId: int, position: string, tasks: seq<string>)
    =
    inherit Person(name, address, phoneNumber)
    member val EmployeeId = employeeId with get, set
    member val Position = position with get, set
    member val Tasks = tasks with get, set

    interface IEmployeeService with
        member this.AssignTask (task: string) (tasks: seq<string>) = Seq.append tasks (seq { yield task })

        member this.CompleteTask (taskId: string) (tasks: seq<string>) =
            tasks |> Seq.filter (fun task -> task <> taskId)


[<EntryPoint>]
let main argv =
    // Клиент
    let customer =
        Customer("Тестовый Тест Тестович", "ул. Тестовая", "8 (987) 654-43-21", 1, "A123", 0.0m)

    // Клиент - депозит
    let depositBalance = (customer :> ICustomerService).Deposit 100.0m
    customer.Balance <- depositBalance
    printfn "Зачисление..."
    printfn "Счет клиента %d, номер счета %s: %M\n" customer.CustomerId customer.AccountNumber customer.Balance

    // Клиент - списание
    let withdrawBalance = (customer :> ICustomerService).Withdraw 25.0m
    customer.Balance <- withdrawBalance
    printfn "Списание..."
    printfn "Счет клиента %d, номер счета %s: %M\n" customer.CustomerId customer.AccountNumber customer.Balance

    // Работник
    let employee = Employee("Тестовый Тест Тестович", "ул. тестовая", "987-654-3210", 2, "Менеджер", [])
    
    // Работник - назначение новых задач
    let newTask = (employee :> IEmployeeService).AssignTask "Проверить баланс аккаунта" employee.Tasks
    employee.Tasks <- newTask
    
    printf "Задачи работника %d: %A\n" employee.EmployeeId employee.Tasks
    
    // Работник - завершение задач
    let completedTasks = (employee :> IEmployeeService).CompleteTask "Проверить баланс аккаунта" employee.Tasks
    employee.Tasks <- completedTasks
    printf "Задачи работника %d: %A\n" employee.EmployeeId employee.Tasks

    0
