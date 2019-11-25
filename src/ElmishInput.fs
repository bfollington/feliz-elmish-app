module ElmishInput

open Feliz
open Elmish
open Feliz.ElmishComponents

open Button

type State = { input: string }

type Msg =
    | Update of string
    | Clear

let init = { input = "" }, Cmd.none

let update (msg: Msg) (state: State) =
    match msg with
    | Update msg -> { state with input = msg }, Cmd.none
    | Clear -> { state with input = "" }, Cmd.none

let render (state: State) (dispatch: Msg -> unit) =
    Html.div [
        Html.input [
          prop.type' "text"
          prop.value state.input
          prop.onChange (Update >> dispatch)
        ]
        button { label = "Clear"; onClick = fun _ -> dispatch Clear}
    ]

let elmishInput = React.elmishComponent("ElmishInput", init, update, render)