module Disposer
open System

let disposer (onDispose: unit -> unit) = { new IDisposable with member this.Dispose() = onDispose() }