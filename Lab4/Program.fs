type IAccountManagement =
    abstract member OpenAccount : string -> decimal -> unit
    abstract member CloseAccount : string -> unit
    abstract member Deposit : string -> decimal -> unit
    abstract member Withdraw : string -> decimal -> unit

type IBankCustomer =
    abstract member GetName : string
    abstract member GetId : string

type BankCustomer(name: string, id: string) =
    interface IBankCustomer with
        member this.GetName = name
        member this.GetId = id

type BankEmployee(name: string, id: string) =
    inherit BankCustomer(name, id)

type BankAccount(accountId: string, initialBalance: decimal ref) =
    member this.GetAccountId = accountId
    member this.GetBalance = initialBalance
    
    new() = BankAccount(null, ref 0.0M)
    
    interface IAccountManagement with
        member this.OpenAccount (accountId: string) (initialBalance: decimal) =
            printfn "Открыт новый счет %s" accountId
        member this.CloseAccount (accountId: string) =
            printfn "Счет %s закрыт" accountId
        member this.Deposit (accountId: string) (amount: decimal) =
            this.GetBalance := !this.GetBalance + amount
            printfn "На счет %s зачислено %.2f" accountId amount
        member this.Withdraw (accountId: string) (amount: decimal) =
            if amount <= !this.GetBalance then
                this.GetBalance := !this.GetBalance - amount
                printfn "Со счета %s снято %.2f" accountId amount
            else
                printfn "Недостаточно средств на счете %s" accountId

// Класс, представляющий клиентский отдел банка
type BankCustomerDepartment() =
    let customers = System.Collections.Generic.Dictionary<string, IBankCustomer>()

    member this.AddCustomer (customer: IBankCustomer) =
        customers.Add(customer.GetId, customer)
        printfn "Добавлен новый клиент: %s" customer.GetName

    member this.GetCustomer (customerId: string) =
        if customers.ContainsKey(customerId) then
            Some(customers.[customerId])
        else
            None

    member this.OpenAccountForCustomer (customer: IBankCustomer) (accountId: string) (initialBalance: decimal ref) =
        match this.GetCustomer(customer.GetId) with
        | Some(customer) ->
            printfn "Открывается счет для клиента: %s" customer.GetName
            let account = BankAccount(accountId, initialBalance)
            account :> IAccountManagement
        | None ->
            printfn "Клиент с ID %s не найден" customer.GetId
            let account = BankAccount()
            account :> IAccountManagement

// Пример использования
let customer1 = BankCustomer("Иванов Иван", "12345")
let customer2 = BankEmployee("Петров Петр", "67890")

let department = BankCustomerDepartment()
department.AddCustomer customer1
department.AddCustomer customer2
printf "\n"

let initialBalance1 = ref 1000.0M
let customer1Account = department.OpenAccountForCustomer customer1 "A123" initialBalance1
customer1Account.Deposit "A123" 500.0M
customer1Account.Withdraw "A123" 200.0M
printf "\n"

let initialBalance2 = ref 2000.0M
let customer2Account = department.OpenAccountForCustomer customer2 "B456" initialBalance2
customer2Account.CloseAccount "B456"
printf "\n"
