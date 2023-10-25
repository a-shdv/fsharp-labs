// Клиентский отдел банка

open System


type Bank(name, address, phone) as self =
    do
        self.name <- name
        self.address <- address
        self.phone <- phone

    new() = Bank(null, null, null)

    [<DefaultValue>]
    val mutable name: string

    [<DefaultValue>]
    val mutable address: string

    [<DefaultValue>]
    val mutable phone: string

    member _.ToString = name + " is located on " + address + ". Contact us: " + phone


[<AbstractClass>]
type Client(first_name, last_name, birth_date) as self =
    do
        self.first_name <- first_name
        self.last_name <- last_name
        self.birth_date <- birth_date

    [<DefaultValue>]
    val mutable first_name: String

    [<DefaultValue>]
    val mutable last_name: String

    [<DefaultValue>]
    val mutable birth_date: DateTime

    member _.ToString =
        "Client of the bank: "
        + first_name
        + " "
        + last_name
        + ". Born at "
        + birth_date.ToString("dd-MM-yyyy")


type ForeignClient(first_name, last_name, birth_date, country, id_number) as self =
    inherit Client(first_name, last_name, birth_date)

    do
        self.country <- country
        self.id_number <- id_number

    [<DefaultValue>]
    val mutable country: string

    [<DefaultValue>]
    val mutable id_number: int

    member _.ToString =
        base.ToString
        + ". Foreigner with country: "
        + country
        + ". And id_number is "
        + id_number.ToString()



type IndividualClient(first_name, last_name, birth_date, registration, passport_info) as self =
    inherit Client(first_name, last_name, birth_date)

    [<DefaultValue>]
    val mutable registration: string

    [<DefaultValue>]
    val mutable passport_info: int

    do
        self.registration <- registration
        self.passport_info <- passport_info

    member _.ToString =
        base.ToString
        + ". Registrated on "
        + self.registration
        + ". Passport information "
        + self.passport_info.ToString()


[<EntryPoint>]
let main argv =
    let local_bank = Bank("Sberbank", "Ulyanovsk, Kamyshinskaya st, 4A", "+249028902")
    printfn "%s" local_bank.ToString

    let foreign_client =
        ForeignClient("Deutsche Bank", "Alexanderstraße 5", DateTime.Parse("2023-01-01"), "Germany", 9823497)

    let individual =
        IndividualClient("Ivan", "Ivanov", DateTime.Parse("1970-01-01"), "Moscow, Tverskaya st.", 9878767)

    printfn "%s" foreign_client.ToString
    printfn "%s" individual.ToString

    0
