module Extensions
open System
open Elmish

let disposer (onDispose: unit -> unit) = { new IDisposable with member this.Dispose() = onDispose() }

module Cmd =
  let fromAsync (operation: Async<'msg>) : Cmd<'msg> =
    let delayedCmd (dispatch: 'msg -> unit): unit =
      let delayedDispatch = async {
        let! msg = operation
        dispatch msg
      }

      Async.StartImmediate delayedDispatch

    Cmd.ofSub delayedCmd

type Deferred<'t> = 
  | NotStartedYet
  | InProgress
  | Complete of 't

type AsyncOperationEvent<'t> =
  | Started
  | Finished of 't