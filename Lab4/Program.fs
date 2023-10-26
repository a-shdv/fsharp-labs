type IAccountManagement =
    abstract member OpenAccount: string -> decimal -> unit
    abstract member CloseAccount: string -> unit
    abstract member Deposit: string -> decimal -> unit
    abstract member Withdraw: string -> decimal -> unit

type IBankCustomer =
    abstract member Name: string
    abstract member Id: string

type BankCustomer(name: string, id: string) =
    interface IBankCustomer with
        member this.Name = name
        member this.Id = id

type BankEmployee(name: string, id: string, isHappy: bool) =
    inherit BankCustomer(name, id)
    let m_isHappy = isHappy
    member this.isHappy = m_isHappy

type BankAccount(accountId: string, initialBalance: decimal ref) =
    member this.AccountId = accountId
    member this.Balance = initialBalance

    new() = BankAccount(null, ref 0.0M)

    interface IAccountManagement with
        member this.OpenAccount (accountId: string) (initialBalance: decimal) =
            printfn "Открыт новый счет %s" accountId

        member this.CloseAccount(accountId: string) = printfn "Счет %s закрыт" accountId

        member this.Deposit (accountId: string) (amount: decimal) =
            this.Balance := !this.Balance + amount
            printfn "На счет %s зачислено %.2f" accountId amount

        member this.Withdraw (accountId: string) (amount: decimal) =
            if amount <= !this.Balance then
                this.Balance := !this.Balance - amount
                printfn "Со счета %s снято %.2f" accountId amount
            else
                printfn "Недостаточно средств на счете %s" accountId

// Класс, представляющий клиентский отдел банка
type BankCustomerDepartment() =
    let customers = System.Collections.Generic.Dictionary<string, IBankCustomer>()

    member this.AddCustomer(customer: IBankCustomer) =
        customers.Add(customer.Id, customer)
        printfn "Добавлен новый клиент: %s" customer.Name

    member this.GetCustomer(customerId: string) =
        if customers.ContainsKey(customerId) then
            Some(customers.[customerId])
        else
            None

    member this.OpenAccountForCustomer (customer: IBankCustomer) (accountId: string) (initialBalance: decimal ref) =
        match this.GetCustomer(customer.Id) with
        | Some(customer) ->
            printfn "Открывается счет для клиента: %s" customer.Name
            let account = BankAccount(accountId, initialBalance)
            account :> IAccountManagement
        | None ->
            printfn "Клиент с ID %s не найден" customer.Id
            let account = BankAccount()
            account :> IAccountManagement

// Пример использования
let customer = BankCustomer("Иванов Иван", "12345")
let employee = BankEmployee("Петров Петр", "67890", true)

let department = BankCustomerDepartment()
department.AddCustomer customer
department.AddCustomer employee
printf "\n"

let initialBalanceClient = ref 1000.0M
let customerAccount =
    department.OpenAccountForCustomer customer "A123" initialBalanceClient
customerAccount.Deposit "A123" 500.0M
customerAccount.Withdraw "A123" 200.0M
printf "\n"

let initialBalanceEmployee = ref 2000.0M
let employeeAccount =
    department.OpenAccountForCustomer employee "B456" initialBalanceEmployee
employeeAccount.CloseAccount "B456"
let mood = employee.isHappy = false
printf "isHappy: %A" mood
printf "\n"
