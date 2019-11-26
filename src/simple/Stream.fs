module Stream

open FSharp.Control
open FSharp.Control.AsyncRx

type TestObserver<'a> (handler: 'a -> unit) =
  interface IAsyncObserver<'a> with
      member __.OnNextAsync x = async {
          do handler x
      }
      member __.OnErrorAsync err = async {
          printfn "Error"
      }
      member __.OnCompletedAsync () = async {
          printfn "Completed"
      }

let subscribe (handler: 'a -> unit) (obs: IAsyncObservable<'a>) = 
    obs.SubscribeAsync (TestObserver handler)
    |> Async.Ignore
    |> Async.StartImmediate

let create<'a> () =
  let (dispatch, obs) = subject<'a>()
  let post = fun a -> dispatch.OnNextAsync(a)
  post, obs