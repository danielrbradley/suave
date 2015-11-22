﻿namespace Suave.Http

module Operators =

  let inline bind (second : 'b -> Async<'c option>) (first : 'a -> Async<'b option>) : 'a -> Async<'c option> =
    fun x -> 
      async {
        let! e = first x
        match e with
        | None ->
          return None
        | Some t ->
          return! second t }

  let inline (>>=) (first : 'a -> Async<'b option>)  (second : 'b -> Async<'c option>) : 'a -> Async<'c option> =
    fun x ->
      bind second first x

  let inline (>=>) a b = fun x ->
    match a x with
    | None   -> b x
    | r      -> r

